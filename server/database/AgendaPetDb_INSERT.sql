CREATE DATABASE AgendaPetDb
GO

USE AgendaPetDb 
GO

CREATE TABLE RacaPet
(
	RacaID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeRaca VARCHAR(40) NOT NULL,
)
GO

INSERT INTO RacaPet(NomeRaca) VALUES
('Sem Raça Definida (SRD)'),
('Golden Retriever'),
('Shih Tzu'),
('Bulldog Francês'),
('Poodle'),
('Pincher'),
('Persa'),
('Siamês'),
('Maine Coon'),
('Ragdoll'),
('Calopsita'),
('Canário Belga'),
('Periquito Australiano'),
('Agapornis'),
('Hamster Sírio'),
('Hamster Anão Russo'),
('Porquinho-da-Índia'),
('Chinchila'),
('Mini Lionhead'),
('Netherland Dwarf'),
('Fuzzy Lop')
GO

CREATE TABLE ComportamentoPet
(
	ComportamentoID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeComportamento VARCHAR(40) NOT NULL,
)
GO

INSERT INTO ComportamentoPet(NomeComportamento) VALUES
('Dócil'),
('Hiperativo'),
('Agressivo'),
('Possessivo'),
('Medroso'),
('Antissocial')
GO

CREATE TABLE PortePet
(
	PorteID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomePorte VARCHAR(40) NOT NULL,
)
GO

INSERT INTO PortePet(NomePorte) VALUES
('Pequeno'),
('Médio'),
('Grande')
GO

CREATE TABLE StatusAgendamento
(
	StatusAgendamentoID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeStatus VARCHAR(40) NOT NULL
)
GO

INSERT INTO StatusAgendamento(NomeStatus) VALUES
('Pendente'),
('Concluído'),
('Cancelado')
GO

CREATE TABLE TipoUsuario
(
	TipoUsuarioID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeTipo VARCHAR(40) NOT NULL,
)
GO

INSERT INTO TipoUsuario(NomeTipo) VALUES
('Funcionário'),
('Tutor')
GO

CREATE TABLE Usuario 
(
	UsuarioID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	Nome VARCHAR(120) NOT NULL,
	NumeroTelefone VARCHAR(15) UNIQUE NOT NULL,
	Email VARCHAR(255) UNIQUE NOT NULL,
	Senha VARBINARY(32) NULL,
	TipoUsuarioID UNIQUEIDENTIFIER NOT NULL,
	StatusUsuarioID BIT DEFAULT (1),

	CONSTRAINT FK_Usuario_TipoUsuario_TipoUsuarioID FOREIGN KEY (TipoUsuarioID) REFERENCES TipoUsuario(TipoUsuarioID),
)
GO

INSERT INTO Usuario(Nome, NumeroTelefone, Email, Senha, TipoUsuarioID) VALUES
('Felipe Miriani', '11 99999-9999', 'penguimfelipe@email.com', HASHBYTES('SHA2_256', '123'), (select t.TipoUsuarioID from TipoUsuario t where t.NomeTipo = 'Tutor'))

CREATE TABLE TipoAnimal
(
	TipoAnimalID UNiQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeTipo VARCHAR(40) NOT NULL
)
GO

INSERT INTO TipoAnimal(NomeTipo) VALUES
('Cachorro'),
('Gato'),
('Pássaro'),
('Roedor'),
('Coelho')
GO

CREATE TABLE Pet
(
	PetID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	Nome VARCHAR(50) NOT NULL,
	TipoAnimalID UNIQUEIDENTIFIER NOT NULL,
	ComportamentoID UNIQUEIDENTIFIER NOT NULL,
	RacaID UNIQUEIDENTIFIER NOT NULL,
	PorteID UNIQUEIDENTIFIER NOT NULL,
	UsuarioID UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT FK_Pet_Usuario_UsuarioID FOREIGN KEY (UsuarioID) REFERENCES Usuario(UsuarioID),
	CONSTRAINT FK_Pet_Raca_RacaID FOREIGN KEY (RacaID) REFERENCES RacaPet(RacaID),
	CONSTRAINT FK_Pet_TipoAnimal_TipoAnimalID FOREIGN KEY (TipoAnimalID) REFERENCES TipoAnimal(TipoAnimalID),
	CONSTRAINT FK_Pet_Comportamento_ComportamentoID FOREIGN KEY (ComportamentoID) REFERENCES ComportamentoPet(ComportamentoID),
	CONSTRAINT FK_Pet_Porte_PorteID FOREIGN KEY (PorteID) REFERENCES PortePet(PorteID)
)
GO

-- ADICIONADO: Tempo por servico.
CREATE TABLE Servico
(
	ServicoID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeServico VARCHAR(40) NOT NULL,
	Preco DECIMAL(6,2) NOT NULL,
	TempoServico INT NOT NULL
)
GO

-- ADICIONADO: Tempo total.
CREATE TABLE Agendamento
(
	AgendamentoID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	DataAgendamento DATE NOT NULL,
	HoraAgendamento TIME NOT NULL,
	ValorTotal DECIMAL(6,2) NOT NULL,
	StatusAgendamentoID UNIQUEIDENTIFIER NOT NULL,
	FuncionarioID UNIQUEIDENTIFIER NOT NULL,
	PetId UNIQUEIDENTIFIER NOT NULL,
	TempoTotal INT NOT NULL

	CONSTRAINT FK_Agendamento_StatusAgendamento_StatusAgendamentoId FOREIGN KEY (StatusAgendamentoID) REFERENCES StatusAgendamento(StatusAgendamentoID),
	CONSTRAINT FK_Agendamento_Funcionario_FuncionarioID FOREIGN KEY (FuncionarioID) REFERENCES Usuario(UsuarioID),
	CONSTRAINT FK_Agendamento_Pet_PetID FOREIGN KEY (PetID) REFERENCES Pet(PetID)
)
GO

CREATE TABLE LogAgendamento
(
	LogAgendamentoID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	DataModificacao DATETIME DEFAULT GETDATE(),
	DataAnteriorAgendameto DATETIME NOT NULL,
	StatusAgendamentoAnterior NVARCHAR(30) NOT NULL,
	ServicosPorAgendamento NVARCHAR(MAX) NOT NULL,
	AgendamentoID UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT FK_LogAgendamento_Agendamento_AgendamentoID FOREIGN KEY(AgendamentoID) REFERENCES Agendamento(AgendamentoID)
)
GO

CREATE TABLE AgendamentoServico
(
	ServicoID UNIQUEIDENTIFIER NOT NULL,
	AgendamentoID UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT PK_AgendamentoServico_ServicoID_AgendamentoID PRIMARY KEY (ServicoID, AgendamentoID),
	CONSTRAINT FK_AgendamentoServico_Servico_ServicoID FOREIGN KEY (ServicoID) REFERENCES Servico(ServicoID),
	CONSTRAINT FK_AgendamentoServico_Agendamento_AgendamentoID FOREIGN KEY (AgendamentoID) REFERENCES Agendamento(AgendamentoID)
)
GO