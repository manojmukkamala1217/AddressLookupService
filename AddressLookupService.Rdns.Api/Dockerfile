#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AddressLookupService.Rdns.Api/AddressLookupService.Rdns.Api.csproj", "AddressLookupService.Rdns.Api/"]
COPY ["Api.Library/Api.Library.csproj", "Api.Library/"]
RUN dotnet restore "AddressLookupService.Rdns.Api/AddressLookupService.Rdns.Api.csproj"
COPY . .
WORKDIR "/src/AddressLookupService.Rdns.Api"
RUN dotnet build "AddressLookupService.Rdns.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AddressLookupService.Rdns.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AddressLookupService.Rdns.Api.dll"]