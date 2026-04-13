-- Tabela de Vagas
CREATE TABLE Vagas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NumeroVaga VARCHAR(10) NOT NULL,
    Status VARCHAR(20)
);

-- Tabela de Reservas (Relacionamento)
CREATE TABLE Reservas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PlacaVeiculo VARCHAR(10) NOT NULL,
    DataReserva DATETIME NOT NULL,
    VagaId INT,
    FOREIGN KEY (VagaId) REFERENCES Vagas(Id)
);