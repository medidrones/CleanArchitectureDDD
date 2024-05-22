using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Vehiculos.SearchVehiculos;
using CleanArchitecture.Domain.Abstractions;
using Dapper;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Text;

namespace CleanArchitecture.Application.Vehiculos.ReportVehiculoPdf;

internal sealed class ReportVehiculoPdfQueryHandler : IQueryHandler<ReportVehiculoPdfQuery, Document>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public ReportVehiculoPdfQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<Document>> Handle(ReportVehiculoPdfQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var builder = new StringBuilder("""
            SELECT
                v.id as Id,
                v.modelo as Modelo,
                v.vin as Vin,
                v.precio_monto as Precio
            FROM vehiculos AS v            
        """);

        var search = string.Empty;
        var where = string.Empty;

        if (!string.IsNullOrEmpty(request.Modelo))
        {
            search = "%" + request.Modelo + "%";
            where = $"WHERE v.modelo LIKE @Search";
            builder.AppendLine(where);
        }

        builder.AppendLine(" ORDER BY v.modelo ");

        var vehiculos = await connection.QueryAsync<VehiculoResponse>(
            builder.ToString(),
            new { Search = search });

        var report = Document.Create(container => {
            container.Page(page => { 
                page.Margin(50);
                page.Size(PageSizes.A4.Landscape());
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));
                page.Header()
                    .AlignCenter()
                    .Text("Vehiculos: Modernos de Alta Gama")
                    .SemiBold()
                    .FontSize(24)
                    .FontColor(Colors.Blue.Darken2);
                page.Content()
                    .Padding(25)
                    .Table(table => {
                        table.ColumnsDefinition(columns => {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header => {
                            header.Cell().Element(CellStyle).Text("Modelo");
                            header.Cell().Element(CellStyle).Text("Vin");
                            header.Cell().Element(CellStyle).AlignRight().Text("Precio");
                            
                            static IContainer CellStyle(IContainer container) 
                            {  
                                return container.DefaultTextStyle(x => x.SemiBold())
                                    .PaddingVertical(5)
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Black); 
                            }
                        });

                        foreach (var vehiculo in vehiculos)
                        {
                            table.Cell().Element(CellStyle).Text(vehiculo.Modelo);
                            table.Cell().Element(CellStyle).Text(vehiculo.Vin);
                            table.Cell().Element(CellStyle).AlignRight().Text($"${vehiculo.Precio}");

                            static IContainer CellStyle(IContainer container)
                            {
                                return container
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .PaddingVertical(5);
                            }
                        }
                    });
            });
        });

        return report;
    }
}
