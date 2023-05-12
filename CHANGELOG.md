# Changelog

## [1.2.0](https://github.com/Priori-Services/API/compare/v1.1.0...v1.2.0) (2023-05-12)


### Features

* adicionar handeling para todos os erros de BD ([685c60c](https://github.com/Priori-Services/API/commit/685c60c1bd78ce2511352d93a8c64999c899a3c7))
* configuração da API dedicada em classe ([2283fec](https://github.com/Priori-Services/API/commit/2283fec8b1b503aff45a9d24696c13f8d3c758c3))
* container mais leve utilizando chainguard images e fix ports no README ([7ac28e5](https://github.com/Priori-Services/API/commit/7ac28e5c4352713398b463d12da880b88e8c8b44))
* retornar ID junto de JWT ao login ([1a156a6](https://github.com/Priori-Services/API/commit/1a156a68aaa2f51e7932823b09597fc8e0827947))
* start.sh p/ setar variaveis de ambiente no linux ([271f8ab](https://github.com/Priori-Services/API/commit/271f8ab436481b7557ef84e422dd1141177dafa4))
* usar senhas em autenticação e cadastro do sistema sem utilizar parâmetros ([15d2fcb](https://github.com/Priori-Services/API/commit/15d2fcb6f8c2d771c0fc5d40d6973b631bc3ed23))
* **WIP:** melhorar tratamento de erros e robustês do código dos Controllers ([bc2180c](https://github.com/Priori-Services/API/commit/bc2180c3c618b7d93c7be1e2f3740f7935f4ddbb))


### Bug Fixes

* arquivos gerados automaticamente ([727007b](https://github.com/Priori-Services/API/commit/727007b723fb196a1ab40b525b0931794696c7f9))
* arquivos gerados automaticamente ([264f215](https://github.com/Priori-Services/API/commit/264f215c290e4bcfa3fdea1a83c9ade93cd00975))
* arquivos gerados automaticamentes ([56c7325](https://github.com/Priori-Services/API/commit/56c7325f59198d44c665d0ac289defa40f840358))
* atualizacao controller não sendo executado ([e5ad65e](https://github.com/Priori-Services/API/commit/e5ad65e4cf742c2eb71a2d7e43eb26e9d5ed9b58))
* checkar corretamente se usuário não existe antes de login ([eec6ddb](https://github.com/Priori-Services/API/commit/eec6ddbfbc979c5d0628df4613c14961af9f5312))
* especificar PRIORI_DATABASE_USER na config ([01316a0](https://github.com/Priori-Services/API/commit/01316a0540a4b9de93597290fead7147ba188787))
* implementar SID corretamente em consultor ([55dfe80](https://github.com/Priori-Services/API/commit/55dfe800302a1a6076dd7073ec8715a1d31c1f6b))
* melhorar segurança com post request no ClienteController ([b5bb507](https://github.com/Priori-Services/API/commit/b5bb507556f74425f3c8a1d35a3713b105ee786b))
* mover namespace PRIORI[...].Models para Model ([c3529e2](https://github.com/Priori-Services/API/commit/c3529e216f69c076722c0dc2035bf8e97f9c9f74))
* permissões corretas para atualizações ([2152c1f](https://github.com/Priori-Services/API/commit/2152c1fc6f3bb64589e099da0b9c74cca14a1e9d))
* permitir funcionalidade dos endpoints da Cateira Investimentos ([2dbdbc7](https://github.com/Priori-Services/API/commit/2dbdbc7e196808ea2beea17b9d07990c44e9406f))
* remove visualstudio project files ([eda1408](https://github.com/Priori-Services/API/commit/eda140818467dba9f7dd437fd51b31233bb91a7f))
* remover campos desnecessários em Model de Carteira ([4787d39](https://github.com/Priori-Services/API/commit/4787d39ff01bd06d938edd245b1cee76134fefd3))
* silenciar erros de nulificação do DbContext ([07c3c41](https://github.com/Priori-Services/API/commit/07c3c4113611946e1fe1520954949dfe0ede59d6))
* simplificação do container da API ([cf3ad6a](https://github.com/Priori-Services/API/commit/cf3ad6a46e2cd984c9e887b013191e86a41c4c03))
* typo em declaração da tabela Clientes ([5a4f165](https://github.com/Priori-Services/API/commit/5a4f1653b7a76b2d2d2dd5d0e02b224c73fb34e6))
* typo nas variáveis do programa ([b0eca86](https://github.com/Priori-Services/API/commit/b0eca86d28e52bfc152a3b72f32e57cd190819f2))
* variavel PRIORIUSER não sendo corretamente transferida p execução ([104deb3](https://github.com/Priori-Services/API/commit/104deb35edaea11d49b302bbb1685f55fabc8c7c))
* **wip,minor:** remove some requirements for registering an user ([82838d6](https://github.com/Priori-Services/API/commit/82838d6e480a89bbd745982393b310c8d4d5f40d))


### Reverts

* remover senha do DBO ([13c79ea](https://github.com/Priori-Services/API/commit/13c79ea87bd3faabf49bb85afc18be381c66807b))
* senha especificada por meio de argumento em vez de DBO ([d1f6a75](https://github.com/Priori-Services/API/commit/d1f6a75363e5f5c4b2b243c536c369ce556ff1a9))

## [1.1.0](https://github.com/Priori-Services/API/compare/v1.0.0...v1.1.0) (2023-04-15)


### Features

* Data Annotations ([#13](https://github.com/Priori-Services/API/issues/13)) ([20e9e70](https://github.com/Priori-Services/API/commit/20e9e7073d442249e3eaf28bcb4379ff472ce60e))
* README descritivo mostrando como utilizar o projeto corretamente ([c413f9b](https://github.com/Priori-Services/API/commit/c413f9bf5fa149d0dfc84932759755e7b1c345e0))


### Bug Fixes

* implementar connection string e JWT key  corretamente ([bd3d88d](https://github.com/Priori-Services/API/commit/bd3d88dc1503acbd4f30408df5bc707b99a66b0c))
* mover start p/ example para que não possam ocorrer pushes incorretos ([94940e3](https://github.com/Priori-Services/API/commit/94940e38521bc5ddee70a7263d88246b072b6d13))
* resolvendo comflito model CarteiraInvestimento ([#11](https://github.com/Priori-Services/API/issues/11)) ([a539ee5](https://github.com/Priori-Services/API/commit/a539ee519f0e47d3326571d2b37aed84dbe24e2f))

## 1.0.0 (2023-04-15)


### Features

* add actions for standardizing development ([b053ec4](https://github.com/Priori-Services/API/commit/b053ec48aadba22767652f4ddcf76ddb49f88cd9))
* appsettings exemplar ([edd2bc0](https://github.com/Priori-Services/API/commit/edd2bc00c2e0ffc53fc8cf030d5a3dcb127187f0))
* devcontainer p/ standard dev env ([364c35b](https://github.com/Priori-Services/API/commit/364c35b5e893ee3ce37642a08513106d62b6540c))
* env variables for database configuration ([8a58a07](https://github.com/Priori-Services/API/commit/8a58a07b706b8a4273ad7c62f83afda27c7ac7ed))
* More descriptive endpoints for blog posts ([65bca83](https://github.com/Priori-Services/API/commit/65bca83bb309df8b3c3217840b305e4c2fe031c8))
* remover senha não-criptografada ([7c10f75](https://github.com/Priori-Services/API/commit/7c10f754767229e04ae64d2b630c656a362bac58))


### Bug Fixes

* better readability and separation of namespace scoping ([16a6688](https://github.com/Priori-Services/API/commit/16a6688ce59912f7cfb0ee818486aaab8d5dede5))
* conventional commits on develop ([1309026](https://github.com/Priori-Services/API/commit/130902647dd9546bf711438764722654d9cd0580))
* remove visual studio cache files ([247ed5e](https://github.com/Priori-Services/API/commit/247ed5ee7a6d445e9710a3a9b09d1cc62115484e))
* remover referência desnecessária ([e4cf3b1](https://github.com/Priori-Services/API/commit/e4cf3b18b3583d354ab9db7ee5551d7f35983fff))
* run release actiono on develop ([9fecde3](https://github.com/Priori-Services/API/commit/9fecde3998a2471cc2864d384dccdbdb4fdbfd9c))
* typo em data encerramento ([7a6f244](https://github.com/Priori-Services/API/commit/7a6f244f5bdd653cc57c3e744b695043ea50351b))
* typo em metodo CreateInvestimento ([0cae178](https://github.com/Priori-Services/API/commit/0cae17803ef7e38e9c2618c26f5674c4903375c9))
* typo em tipo investimento ([578e18e](https://github.com/Priori-Services/API/commit/578e18e0bf1493a42bb0e1ded44ec4770bc6c034))
