FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "BiblioTech.sln"
RUN dotnet build "BiblioTech.Api/BiblioTech.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BiblioTech.Api/BiblioTech.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BiblioTech.Api.dll"]