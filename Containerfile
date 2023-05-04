FROM cgr.dev/chainguard/dotnet-sdk:latest AS builder

WORKDIR /App
COPY . .

RUN dotnet publish -r linux-x64 --self-contained true -c Release -o out

FROM cgr.dev/chainguard/dotnet-runtime:latest AS final

EXPOSE 5000 

WORKDIR /App
COPY --from=builder /App/out .

ENV DOTNET_EnableDiagnostics=0
ENTRYPOINT ["dotnet", "PRIORI_SERVICES_API.dll"]
