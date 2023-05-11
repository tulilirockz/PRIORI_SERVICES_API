FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build-env

RUN apk upgrade --update && apk add icu-libs icu-data-full tzdata

WORKDIR /App

COPY . .

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base

RUN apk upgrade --update && apk add icu-libs icu-data-full tzdata

ENV PORT 5000
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

WORKDIR /App

COPY --from=build-env /App/out .

ENV DOTNET_EnableDiagnostics=0

ENTRYPOINT ["dotnet", "PRIORI_SERVICES_API.dll"]
