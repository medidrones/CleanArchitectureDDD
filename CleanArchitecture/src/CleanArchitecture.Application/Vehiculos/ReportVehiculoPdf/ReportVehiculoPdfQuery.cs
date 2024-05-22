using CleanArchitecture.Application.Abstractions.Messaging;
using QuestPDF.Fluent;

namespace CleanArchitecture.Application.Vehiculos.ReportVehiculoPdf;

public sealed record ReportVehiculoPdfQuery(
    string Modelo) : IQuery<Document>;
