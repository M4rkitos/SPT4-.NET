-- --- 1. CRIAÇÃO DA TABELA PRINCIPAL (MORADOR) ---
CREATE TABLE Moradores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Apartamento VARCHAR(10) NOT NULL,
    Bloco VARCHAR(5) NOT NULL
);

-- --- 2. CRIAÇÃO DA TABELA RELACIONADA (RESERVA DE VAGA) ---
CREATE TABLE VagasReservas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PlacaVeiculo VARCHAR(10) NOT NULL,
    DataInicio DATETIME NOT NULL,
    DataFim DATETIME NOT NULL,
    MoradorId INT NOT NULL,
    ValorCobrado DECIMAL(18,2) NOT NULL, -- Coluna adicionada para espelhar a Entity

    CONSTRAINT FK_VagasReservas_Moradores FOREIGN KEY (MoradorId)
        REFERENCES Moradores(Id) ON DELETE CASCADE
);