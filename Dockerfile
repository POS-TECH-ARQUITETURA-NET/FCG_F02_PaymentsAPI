
# ------------------------ Build stage ------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ENV DOTNET_CLI_HOME=/tmp
ENV NUGET_PACKAGES=/root/.nuget/packages

COPY src/PaymentsAPI/PaymentsAPI.csproj src/PaymentsAPI/
RUN dotnet restore src/PaymentsAPI/PaymentsAPI.csproj -p:DisableImplicitNuGetFallbackFolder=true

COPY src/PaymentsAPI/ src/PaymentsAPI/
RUN dotnet publish src/PaymentsAPI/PaymentsAPI.csproj -c Release -o /app/publish --no-restore \
    -p:DisableImplicitNuGetFallbackFolder=true

# ------------------------ Runtime stage ------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "PaymentsAPI.dll"]
