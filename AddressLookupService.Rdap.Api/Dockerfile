#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AddressLookupService.Rdap.Api/AddressLookupService.Rdap.Api.csproj", "AddressLookupService.Rdap.Api/"]
COPY ["Api.Library/Api.Library.csproj", "Api.Library/"]
RUN dotnet restore "AddressLookupService.Rdap.Api/AddressLookupService.Rdap.Api.csproj"
COPY . .
WORKDIR "/src/AddressLookupService.Rdap.Api"
RUN dotnet build "AddressLookupService.Rdap.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AddressLookupService.Rdap.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AddressLookupService.Rdap.Api.dll"]