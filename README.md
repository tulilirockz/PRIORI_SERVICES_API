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

docker run --name priori-api -p 5000:5000 --network=host \
  -e PRIORI_DATABASE_PORT="algoaqui" \
  -e PRIORI_DATABASE_NAME="algoaqui" \
  -e PRIORI_DATABASE_USER="algoaqui" \
  -e PRIORI_DATABASE_IP="algoaqui" \
  -e PRIORI_DATABASE_PASSWORD="algoaqui" \
  -e PRIORI_SECRET_JWT_KEY="algoaqui" \
  priori-api-image
```
