FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/CleanArchitecture.Api/CleanArchitecture.Api.csproj", "src/CleanArchitecture.Api/"]
COPY ["src/CleanArchitecture.Application/CleanArchitecture.Application.csproj", "src/CleanArchitecture.Application/"]
COPY ["src/CleanArchitecture.Domain/CleanArchitecture.Domain.csproj", "src/CleanArchitecture.Domain/"]
COPY ["src/CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj", "src/CleanArchitecture.Infrastructure/"]
RUN dotnet restore "./src/CleanArchitecture.Api/CleanArchitecture.Api.csproj"
COPY . .
WORKDIR "/src/src/CleanArchitecture.Api"
RUN dotnet build "./CleanArchitecture.Api.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "./CleanArchitecture.Api.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.Api.dll"]