USE AgendaPetDb
GO


USE AgendaPetDb
GO

-------------------------------------------------
-- RAÇAS
-------------------------------------------------

INSERT INTO RacaPet (NomeRaca)
VALUES
('Labrador'),
('Beagle'),
('Yorkshire'),
('Border Collie'),
('Angorá')

GO


-------------------------------------------------
-- COMPORTAMENTOS
-------------------------------------------------

INSERT INTO ComportamentoPet (NomeComportamento)
VALUES
('Calmo'),
('Brincalhão'),
('Ansioso'),
('Protetor')

GO

select * from ComportamentoPet

-------------------------------------------------
-- PORTES
-------------------------------------------------

INSERT INTO PortePet (NomePorte)
VALUES
('Mini'),
('Gigante')

GO


-------------------------------------------------
-- STATUS AGENDAMENTO
-------------------------------------------------

INSERT INTO StatusAgendamento (NomeStatus)
VALUES
('Confirmado'),
('Em Atendimento'),
('Não Compareceu')

GO


-------------------------------------------------
-- TIPO USUÁRIO
-------------------------------------------------

INSERT INTO TipoUsuario (NomeTipo)
VALUES
('Administrador')

GO


-------------------------------------------------
-- TIPOS DE ANIMAL
-------------------------------------------------

INSERT INTO TipoAnimal (NomeTipo)
VALUES
('Peixe'),
('Réptil'),
('Tartaruga')

GO


-------------------------------------------------
-- SERVIÇOS
-------------------------------------------------

INSERT INTO Servico
(
    NomeServico,
    Preco,
    TempoServico
)
VALUES
('Banho Premium',80.00,60),
('Tosa Higiênica',45.00,30),
('Escovação Dental',35.00,20),
('Hidratação',55.00,40),
('Limpeza de Ouvido',25.00,15)

GO

-------------------------------------------------
-- USUÁRIO FUNCIONÁRIO
-------------------------------------------------

INSERT INTO Usuario
(
    Nome,
    NumeroTelefone,
    Email,
    Senha,
    TipoUsuarioID
)
VALUES
(
    'João Banhista',
    '11 97777-1000',
    'funcionario@agendapet.com',
    HASHBYTES('SHA2_256','123'),
    (
        SELECT TipoUsuarioID
        FROM TipoUsuario
        WHERE NomeTipo='Funcionário'
    )
)

GO


-------------------------------------------------
-- USUÁRIO TUTOR
-------------------------------------------------

INSERT INTO Usuario
(
    Nome,
    NumeroTelefone,
    Email,
    Senha,
    TipoUsuarioID
)
VALUES
(
    'Maria Ferreira',
    '11 98888-2000',
    'tutor@agendapet.com',
    HASHBYTES('SHA2_256','123'),
    (
        SELECT TipoUsuarioID
        FROM TipoUsuario
        WHERE NomeTipo='Tutor'
    )
)

GO


-------------------------------------------------
-- PET
-------------------------------------------------

INSERT INTO Pet
(
    Nome,
    TipoAnimalID,
    ComportamentoID,
    RacaID,
    PorteID,
    UsuarioID
)
VALUES
(
    'Max',
    (
        SELECT TipoAnimalID
        FROM TipoAnimal
        WHERE NomeTipo='Cachorro'
    ),

    (
        SELECT ComportamentoID
        FROM ComportamentoPet
        WHERE NomeComportamento='Dócil'
    ),

    (
        SELECT RacaID
        FROM RacaPet
        WHERE NomeRaca='Golden Retriever'
    ),

    (
        SELECT PorteID
        FROM PortePet
        WHERE NomePorte='Grande'
    ),

    (
        SELECT UsuarioID
        FROM Usuario
        WHERE Email='tutor@agendapet.com'
    )
)

GO


USE AgendaPetDb
GO

USE AgendaPetDb
GO


-------------------------------------------------
-- ADMIN
-------------------------------------------------

INSERT INTO Usuario
(
    UsuarioID,
    Nome,
    NumeroTelefone,
    Email,
    Senha,
    TipoUsuarioID
)
VALUES
(
    NEWID(),
    'Administrador Sistema',
    '11900000001',
    'admin@agendapet.com',
    HASHBYTES('SHA2_256','admin123'),

    (
        SELECT TipoUsuarioID
        FROM TipoUsuario
        WHERE NomeTipo='Administrador'
    )
)

GO


-------------------------------------------------
-- FUNCIONÁRIO
-------------------------------------------------

INSERT INTO Usuario
(
    UsuarioID,
    Nome,
    NumeroTelefone,
    Email,
    Senha,
    TipoUsuarioID
)
VALUES
(
    NEWID(),
    'João Banhista',
    '11977771000',
    'funcionario@agendapet.com',
    HASHBYTES('SHA2_256','123'),

    (
        SELECT TipoUsuarioID
        FROM TipoUsuario
        WHERE NomeTipo='Funcionário'
    )
)

GO


-------------------------------------------------
-- CLIENTE
-------------------------------------------------

INSERT INTO Usuario
(
    UsuarioID,
    Nome,
    NumeroTelefone,
    Email,
    Senha,
    TipoUsuarioID
)
VALUES
(
    NEWID(),
    'Carlos Henrique',
    '11988888888',
    'cliente@agendapet.com',
    HASHBYTES('SHA2_256','123'),

    (
        SELECT TipoUsuarioID
        FROM TipoUsuario
        WHERE NomeTipo='Tutor'
    )
)

GO


-------------------------------------------------
-- PET 1
-------------------------------------------------

INSERT INTO Pet
(
    PetID,
    Nome,
    TipoAnimalID,
    ComportamentoID,
    RacaID,
    PorteID,
    UsuarioID
)
VALUES
(
    NEWID(),

    'Rex',

    (
        SELECT TipoAnimalID
        FROM TipoAnimal
        WHERE NomeTipo='Cachorro'
    ),

    (
        SELECT ComportamentoID
        FROM ComportamentoPet
        WHERE NomeComportamento='Brincalhão'
    ),

    (
        SELECT RacaID
        FROM RacaPet
        WHERE NomeRaca='Labrador'
    ),

    (
        SELECT PorteID
        FROM PortePet
        WHERE NomePorte='Grande'
    ),

    (
        SELECT UsuarioID
        FROM Usuario
        WHERE Email='cliente@agendapet.com'
    )
)

GO


-------------------------------------------------
-- PET 2
-------------------------------------------------

INSERT INTO Pet
(
    PetID,
    Nome,
    TipoAnimalID,
    ComportamentoID,
    RacaID,
    PorteID,
    UsuarioID
)
VALUES
(
    NEWID(),

    'Mel',

    (
        SELECT TipoAnimalID
        FROM TipoAnimal
        WHERE NomeTipo='Gato'
    ),

    (
        SELECT ComportamentoID
        FROM ComportamentoPet
        WHERE NomeComportamento='Calmo'
    ),

    (
        SELECT RacaID
        FROM RacaPet
        WHERE NomeRaca='Persa'
    ),

    (
        SELECT PorteID
        FROM PortePet
        WHERE NomePorte='Pequeno'
    ),

    (
        SELECT UsuarioID
        FROM Usuario
        WHERE Email='cliente@agendapet.com'
    )
)

GO


-------------------------------------------------
-- PET 3
-------------------------------------------------

INSERT INTO Pet
(
    PetID,
    Nome,
    TipoAnimalID,
    ComportamentoID,
    RacaID,
    PorteID,
    UsuarioID
)
VALUES
(
    NEWID(),

    'Thor',

    (
        SELECT TipoAnimalID
        FROM TipoAnimal
        WHERE NomeTipo='Cachorro'
    ),

    (
        SELECT ComportamentoID
        FROM ComportamentoPet
        WHERE NomeComportamento='Protetor'
    ),

    (
        SELECT RacaID
        FROM RacaPet
        WHERE NomeRaca='Border Collie'
    ),

    (
        SELECT PorteID
        FROM PortePet
        WHERE NomePorte='Médio'
    ),

    (
        SELECT UsuarioID
        FROM Usuario
        WHERE Email='cliente@agendapet.com'
    )
)

GO


-------------------------------------------------
-- PET 4
-------------------------------------------------

INSERT INTO Pet
(
    PetID,
    Nome,
    TipoAnimalID,
    ComportamentoID,
    RacaID,
    PorteID,
    UsuarioID
)
VALUES
(
    NEWID(),

    'Nina',

    (
        SELECT TipoAnimalID
        FROM TipoAnimal
        WHERE NomeTipo='Coelho'
    ),

    (
        SELECT ComportamentoID
        FROM ComportamentoPet
        WHERE NomeComportamento='Dócil'
    ),

    (
        SELECT RacaID
        FROM RacaPet
        WHERE NomeRaca='Mini Lionhead'
    ),

    (
        SELECT PorteID
        FROM PortePet
        WHERE NomePorte='Pequeno'
    ),

    (
        SELECT UsuarioID
        FROM Usuario
        WHERE Email='cliente@agendapet.com'
    )
)

GO

select * from Pet
go

-------------------------------------------------
-- VALIDAR GUIDS
-------------------------------------------------

SELECT
PetID,
Nome
FROM Pet

SELECT
UsuarioID,
Nome
FROM Usuario

GO


-------------------------------------------------
-- VALIDAR
-------------------------------------------------

SELECT
u.Nome AS Tutor,
u.Email,
p.Nome AS Pet
FROM Usuario u
LEFT JOIN Pet p
ON p.UsuarioID = u.UsuarioID
WHERE u.Email = 'cliente@agendapet.com'

GO



-------------------------------------------------
-- SERVIÇOS
-------------------------------------------------

INSERT INTO Servico
(
    NomeServico,
    Preco,
    TempoServico
)
VALUES
('Banho',50.00,45),
('Tosa',60.00,50),
('Corte de Unha',20.00,15)

GO


-------------------------------------------------
-- AGENDAMENTO
-------------------------------------------------

INSERT INTO Agendamento
(
    DataAgendamento,
    HoraAgendamento,
    ValorTotal,
    StatusAgendamentoID,
    FuncionarioID,
    PetID,
    TempoTotal
)
VALUES
(
    '20260620',
    '10:30:00',

    (
        SELECT
            SUM(Preco)
        FROM Servico
        WHERE NomeServico IN
        (
            'Banho',
            'Tosa'
        )
    ),

    (
        SELECT StatusAgendamentoID
        FROM StatusAgendamento
        WHERE NomeStatus='Pendente'
    ),

    (
        SELECT UsuarioID
        FROM Usuario
        WHERE Email='funcionario@agendapet.com'
    ),

    (
        SELECT PetID
        FROM Pet
        WHERE Nome='Max'
    ),

    (
        SELECT
            SUM(TempoServico)
        FROM Servico
        WHERE NomeServico IN
        (
            'Banho',
            'Tosa'
        )
    )
)

GO


-------------------------------------------------
-- RELACIONAMENTO AGENDAMENTO x SERVIÇO
-------------------------------------------------

INSERT INTO AgendamentoServico
VALUES
(
(
SELECT ServicoID
FROM Servico
WHERE NomeServico='Banho'
),
(
SELECT TOP 1 AgendamentoID
FROM Agendamento
ORDER BY DataAgendamento DESC
)
)

INSERT INTO AgendamentoServico
VALUES
(
(
SELECT ServicoID
FROM Servico
WHERE NomeServico='Tosa'
),
(
SELECT TOP 1 AgendamentoID
FROM Agendamento
ORDER BY DataAgendamento DESC
)
)

GO


-------------------------------------------------
-- LOG DO AGENDAMENTO
-------------------------------------------------

INSERT INTO LogAgendamento
(
    DataAnteriorAgendameto,
    StatusAgendamentoAnterior,
    ServicosPorAgendamento,
    AgendamentoID
)
VALUES
(
    CONVERT(DATETIME,'2026-06-20 10:30:00',120),

    'Pendente',

    'Banho, Tosa',

    (
        SELECT TOP 1 AgendamentoID
        FROM Agendamento
        ORDER BY DataAgendamento DESC
    )
)

GO