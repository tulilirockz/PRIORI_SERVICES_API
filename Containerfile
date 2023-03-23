FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base

RUN apk upgrade --update && apk add icu-libs icu-data-full tzdata

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build-env

RUN apk upgrade --update && apk add icu-libs icu-data-full tzdata

WORKDIR /App

COPY . .

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM base AS final

EXPOSE 80

WORKDIR /App

COPY --from=build-env /App/out .

ENV DOTNET_EnableDiagnostics=0

ENTRYPOINT ["dotnet", "PRIORI_SERVICES_API.dll"]