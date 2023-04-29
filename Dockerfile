FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY src/WebAPI/published/ ./
ENTRYPOINT ["dotnet", "WebAPI.dll"]