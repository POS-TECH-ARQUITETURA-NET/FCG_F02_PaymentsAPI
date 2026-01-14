
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY src/PaymentsAPI/PaymentsAPI.csproj src/PaymentsAPI/
RUN dotnet restore src/PaymentsAPI/PaymentsAPI.csproj
COPY src/PaymentsAPI/ src/PaymentsAPI/
RUN dotnet publish src/PaymentsAPI/PaymentsAPI.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "PaymentsAPI.dll"]
