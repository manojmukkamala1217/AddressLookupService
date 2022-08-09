#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AddressLookupService.GeoIp.Api/AddressLookupService.GeoIp.Api.csproj", "AddressLookupService.GeoIp.Api/"]
COPY ["Api.Library/Api.Library.csproj", "Api.Library/"]
RUN dotnet restore "AddressLookupService.GeoIp.Api/AddressLookupService.GeoIp.Api.csproj"
COPY . .
WORKDIR "/src/AddressLookupService.GeoIp.Api"
RUN dotnet build "AddressLookupService.GeoIp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AddressLookupService.GeoIp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AddressLookupService.GeoIp.Api.dll"]