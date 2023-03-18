/*Dropar a database se ela j� existir!*/
USE master
GO
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'Priori')
	DROP DATABASE Priori
GO

CREATE DATABASE Priori;
GO
USE Priori;
GO

SET LANGUAGE 'Portuguese'
GO

--EXCLUS�O tblTipoInvestidor

CREATE TABLE tblConsultores (
id_consultor INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(40),
cpf_f VARCHAR(11),
email VARCHAR(100),
telefone CHAR(15),
data_contratacao DATE,
data_demissao DATE,
status VARCHAR(8),
usuario VARCHAR(50) UNIQUE,
senha VARCHAR(100)
)
GO

--REMO��O FK id_tipoInvestidor
CREATE TABLE tblClientes (
id_cliente int identity primary key,
nome_c varchar (40),
id_tipoinvestidor int,
id_consultor int foreign key references tblConsultores(id_consultor),
cpf_c varchar(11),
email varchar (40),
Senha varchar (25),
pontuacao numeric,
endereco varchar (60),
telefone char (15),
data_adesao date,
status_c varchar (8)
)
GO

--CORRE��O: DATE-TIME // REMO��O FK E RENOMEIO id_riscoInvestidor
CREATE TABLE tblInvestimentos (
id_investimento INT IDENTITY PRIMARY KEY,
id_riscoInvestimento NUMERIC,
nome VARCHAR(40),
tipo_investimento VARCHAR(5),
rentabilidade_fixa numeric (8,4),
rentabilidade_variavel numeric (8,2),
data_atualizacao DATETIME,
vencimento DATE,
valor_minimo NUMERIC(8,2),
tempo_minimo NUMERIC(3)
)
GO

--CORRE��O: DATE-TIME
CREATE TABLE tblAtualizacao (
id_atualizacao int identity primary key,
id_consultor int foreign key references tblConsultores(id_consultor),
id_investimento int foreign key references tblInvestimentos(id_investimento),
data_atualizacao DATETIME,
rentFixaAntiga numeric (8,4),
rentVarAntiga numeric (8,2),
rentFixaAtual numeric (8,4),
rentVarAtual numeric (8,2)
)
go

--CORRE��O: DATE-TIME
CREATE TABLE tblCarteiraInvestimentos (
id_efetuacao INT IDENTITY PRIMARY KEY,
id_cliente_carteira INT FOREIGN KEY REFERENCES tblClientes(id_cliente),
id_investimento INT FOREIGN KEY REFERENCES tblInvestimentos(id_investimento),
rentabilidade_fixa NUMERIC(8,4),
rentabilidade_variavel NUMERIC(8,2),
data_efetuacao DATETIME,
valor_aplicado NUMERIC(8,2),
data_encerramento DATE,
status VARCHAR(8),
saldo NUMERIC(8,2)
)
GO

CREATE TABLE tblPostBlog (
id_post INT IDENTITY(1,1) PRIMARY KEY,
id_autor INT FOREIGN KEY REFERENCES tblConsultores(id_consultor),
data_criacao DATETIME,
categoria VARCHAR(30),
titulo VARCHAR(100),
conteudo VARCHAR(2000),
)
GO

INSERT INTO tblPostBlog (id_autor, data_criacao, categoria, titulo, conteudo)
VALUES 
(1, '2022-01-01 10:00:00', 'Tecnologia', 'Últimas tendências em tecnologia', 'Nesta postagem, vamos falar sobre as últimas tendências em tecnologia e como elas estão mudando o mundo.'),
(2, '2022-01-05 15:30:00', 'Saúde', 'Cuidados com a saúde mental', 'Nesta postagem, vamos falar sobre como cuidar da sua saúde mental e como isso pode impactar positivamente na sua vida.'),
(3, '2022-01-10 09:00:00', 'Negócios', 'Como a tecnologia pode ajudar a impulsionar seus negocios', 'Nesta postagem, vamos falar sobre como a tecnologia pode ajudar a impulsionar seus negócios e como você pode aproveitar essas oportunidades.'),
(4, '2022-01-15 14:00:00', 'Educação', 'Melhores práticas para estudar e se preparar para o vestibular', 'Nesta postagem, vamos falar sobre as melhores práticas para estudar e se preparar para o vestibular ou para a faculdade.'),
(5, '2022-01-20 11:30:00', 'Entretenimento', 'Filmes e séries mais aguardados deste ano', 'Nesta postagem, vamos falar sobre os filmes e séries mais aguardados deste ano e dar algumas dicas para quem gosta de maratonar.')

INSERT INTO tblConsultores (nome, cpf_f, email, telefone, data_contratacao, status, usuario, senha) VALUES 
('Gabriel Almeida', '33333333333', 'gabriel.alves330@etec.sp.gov.br', '11910101111', '01/01/2018', 'ATIVO', 'Gabriel_Almeida', 'PRIORI'),
('Gustavo Sperandio', '11111111111', 'gustavo.sperandio@etec.sp.gov.br', '11920202121', '09/11/2020', 'ATIVO', 'Gustavo_Sperandio', 'PRIORI'),
('Eros Machado', '22222222222', 'eros.machado@etec.sp.gov.br', '11930303131', '25/04/2019', 'ATIVO', 'Eros_Machado', 'PRIORI'),
('Erik Santana', '44444444444', 'erik.oliveira37@etec.sp.gov.br', '(11)94040-4040', '20/02/2023', 'ATIVO', 'Erik_Santana', 'PRIORI'),
('Arthur Exalta��o', '55555555555', 'arthur.exaltacao@etec.sp.gov.br', '(11)95050-5050', '28/02/2023', 'ATIVO', 'Arthur_Exaltacao', 'PRIORI')
GO

INSERT INTO tblClientes (nome_c, id_tipoinvestidor, id_consultor, cpf_c, email, senha, telefone, endereco, data_adesao, status_c) VALUES 
('Marcos Pontes', 3, 2, 77031335078, 'pontes_marcos@gov.br', 'MARCOS01', '(61) 94002-8922', 'Esplanada dos Minist�rios, 5', '21/01/2021', 'ATIVO'),
('Fabiana Fernandes', 2, 3, 80033768072, 'fernandes0102@gmail.com', 'FabiFabi', '(21) 95421-7845', 'Rua dos Pinhais, 1', '04/06/2021', 'ATIVO'),
('Rog�rio Dias', 2, 1, 54877126031, 'coroger@gmail.com', 'OiRogerio', '(11) 98564-3296', 'Avenida Alcantara Machado, 39', '01/09/2021', 'ATIVO'),
('Luiz Henrique', 1, 2, 03518476009, 'luizyh@uol.com.br', 'GetulioVargas', '(11) 4002-8922', 'Avenida Cruzeiro do Sul, 15', '01/01/2022', 'ATIVO')
GO

INSERT INTO tblInvestimentos (id_riscoInvestimento, nome, tipo_investimento, rentabilidade_fixa, rentabilidade_variavel, data_atualizacao, vencimento, valor_minimo, tempo_minimo) VALUES
(1, 'Tesouro - Selic 2026', 'SELIC', 0.0981, 13.75, '14/01/2023 14:32:05', '01/03/2026', 126.54, 30),
(1, 'Tesouro - Selic 2029', 'SELIC', 0.1742, 13.75, '14/01/2023 14:33:15', '01/03/2029', 125.59, 30),
(1, 'Tesouro - Prefixado 2026', 'PRE', 12.33, 0.0, '14/01/2023 14:33:45', '01/01/2026', 35.43, 30),
(2, 'Tesouro - Prefixado 2029', 'PRE', 12.44, 0.0, '14/01/2023 14:34:01', '01/01/2029', 34.89, 30),
(3, 'Tesouro - Prefixado 2033 J.S.', 'PRE', 12.35, 0.0, '14/01/2023 14:34:25', '01/05/2033', 35.12, 30),
(2, 'Tesouro - IPCA 2029', 'IPCA', 6.10, 5.79, '21/01/2023 17:34:45', '15/05/2029', 55.76, 30),
(2, 'Tesouro - IPCA 2035', 'IPCA', 6.26, 5.79, '21/01/2023 17:34:59', '15/05/2035', 39.02, 30),
(3, 'Tesouro - IPCA 2045', 'IPCA', 6.43, 5.79, '21/01/2023 17:35:17', '15/05/2045', 31.81, 30),
(2, 'Tesouro - IPCA 2032 J.S.', 'IPCA', 6.15, 5.79, '21/01/2023 17:35:38', '15/08/2032', 41.25, 30),
(3, 'Tesouro - IPCA 2040 J.S.', 'IPCA', 6.31, 5.79, '21/01/2023 17:37:15', '15/08/2040', 40.79, 30),
(3, 'Tesouro - IPCA 2055 J.S.', 'IPCA', 6.34, 5.79, '21/01/2023 17:37:55', '15/05/2055', 39.95, 30),
(1, 'CDB PR� - BTG SICREDI', 'PRE', 12.2, 0.0, '21/02/2023 21:48:05', '13/11/2029', 1000.47, 90),
(1, 'LCI PR� - BANCO PAN', 'PRE', 14.1, 0.0,'21/02/2023 21:48:45', '18/10/2024', 1001.00, 90),
(1, 'LCA PR� - BTG PACTUAL', 'PRE', 11.75, 0.0,'21/02/2023 21:49:00', '05/05/2025', 1000.85, 90),
(2, 'CDB CDI - BANCO PARANA', 'CDI', 13.923, 13.65, '21/02/2023 21:49:23','21/08/2023', 100.00, 120), --cdi calculado
(2, 'LCI CDI - PRIORI', 'CDI', 14.3325, 13.65,'21/02/2023 21:49:43', '20/11/2023', 1003.00, 360), --cdi calculado
(2, 'LCI CDI - BANCO INTER', 'CDI', 11.466, 13.65,'21/02/2023 21:50:06', '20/08/2023', 1003.00, 90), --cdi calculado
(2, 'LCA IPCA - PRIORI', 'IPCA', 4.00, 5.79,'21/02/2023 21:50:24', '20/07/2024', 1005.00, 30),
(2, 'LCA IPCA - BTG PACTUAL', 'IPCA', 3.60, 5.79,'21/02/2023 21:50:53', '15/05/2025', 1005.00, 30)
GO

--ATUALIZAR: 'NOME' (PR�XIMAS AULAS BD3)
INSERT INTO tblCarteiraInvestimentos (id_cliente_carteira, id_investimento, rentabilidade_fixa, rentabilidade_variavel, data_efetuacao, valor_aplicado, status, saldo)  VALUES
(1, 6, 4.0, 5.79, '24/01/2021 10:48:05', 3500.50, 'ATIVO', 1250.00),
(3, 5, 14.1, 0.0, '28/02/2021 12:18:05', 4500.00, 'ATIVO', 320.00),
(1, 17, 11.46, 13.65, '06/09/2021 21:41:45', 5000.00, 'ATIVO',0.00), -- fixa calculada
(1, 1, 0.0981, 13.75, '14/04/2022 23:41:25', 6500.00, 'ATIVO', 3500.00),
(2, 2, 0.1742, 13.75, '06/07/2021 19:34:15', 1750.00, 'ATIVO', 250.00),
(2, 4, 12.4, 0.0, '10/10/2021 07:23:41', 2500.00, 'ATIVO', 0.00),
(3, 8, 6.26, 5.79, '10/10/2021 01:16:12', 2500.00, 'ATIVO', 0.00),
(1, 5, 12.3, 0.0, '10/10/2021 13:42:32', 2500.00, 'ATIVO', 0.00),
(4, 11, 6.31, 5.79, '05/08/2022 23:12:52', 5000.00, 'ATIVO', 0.00),
(2, 4, 12.2, 0.0, '20/12/2021 10:52:43', 1000.00, 'ATIVO', 300.00),
(3, 15, 13.923, 13.65, '14/05/2022 12:42:30', 4000.00, 'ATIVO', 0.00) --fixa calculada
GO

--USE Priori;

select * from tblInvestimentos
SELECT * from tblPostBlog
