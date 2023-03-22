FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine

RUN apk upgrade --update

WORKDIR /app

COPY . .

RUN dotnet build -f net7.0 -c Release -r linux-musl-x64 --self-contained -v m

EXPOSE 5000

CMD [ "/app/bin/Release/net7.0/linux-musl-x64/PRIORI_SERVICES_API" ]