# Priori Services API 

Este repositório contém o código fonte para a API do projeto (de TCC) Priori Services

## Variáveis de ambiente

Para que o projeto possa ser executado corretamente, devem ser especificadas as seguintes variáveis de ambiente em seu local de desenvolvimento:

```
PRIORI_DATABASE_PORT, PRIORI_DATABASE_NAME, PRIORI_DATABASE_USER, PRIORI_DATABASE_IP, PRIORI_DATABASE_PASSWORD, PRIORI_SECRET_JWT_KEY
```

## Utilização

Para redundância, criamos varias formas de executar esse projeto, mas, todas dependem do [SDK do Dotnet 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) p/ executar

### Windows

Você pode simplesmente executar a task "dotnet watch" no VSCode ou executar o programa por meio do arquivo "start.cmd.example". Primeiramente deve-se copiar start.cmd.example p/ start.cmd, alterar suas variáveis de ambiente, e executar!

### Linux

A forma mais simples é só executar o programa dotnet com as variáveis de ambiente diretamente especificadas antes do comando, como o exemplo: `PRIORI_DATABASE_PORT=1433 dotnet watch`

Você também pode executar a task "dotnet watch" por meio do VSCode e configura-la para suas necessidades.

### Container

- O container deve ser preferencialmente executado com os outros especificados no [repositório META](https://github.com/Priori-Services/META)

1. Utilize sua forma preferida de executar Docker ou Podman e crie uma imagem com base nesse projeto
2. Crie seu container mapeando as portas e a rede para bridge!

```sh
docker build -t priori-api-image https://github.com/Priori-Services/API

docker run --name priori-api -p 8080:80 --network=host \
  -e PRIORI_DATABASE_PORT="1433" \
  -e PRIORI_DATABASE_NAME="Priori" \
  -e PRIORI_DATABASE_USER="sa" \
  -e PRIORI_DATABASE_IP="localhost" \
  -e PRIORI_DATABASE_PASSWORD="_ScoobyDooby23" \
  -e PRIORI_SECRET_JWT_KEY="SUPER_SECRET_JWT_KEYYYYY12312321" \
  --pull \
  ghcr.io/priori-services/priori_api:latest
```

# Banco de Dados no Docker

Para fazer o setup do banco de dados desse projeto usando Docker, deve-se seguir as seguintes instruções:

## Criar o container

```sh
# "$PWD/data" tem de ser a pasta onde os arquivos SQL serão inseridos
docker run --name "priori-db" \
            -e "ACCEPT_EULA=Y" \
            -e "MSSQL_SA_PASSWORD=_ScoobyDooby23" \
            -p 1433:1433 \
            -v $PWD/data:/data:Z \
            -d \
            mcr.microsoft.com/mssql/server:2022-latest
```

## Inserir o Banco de Dados no container

```sh
# /data deve ter os arquivos, execute eles por lá
docker exec -it priori-db /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P _ScoobyDooby23 -i /data/setup.sql
```
