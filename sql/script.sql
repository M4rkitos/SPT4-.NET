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
    
    -- Configuração da Chave Estrangeira (Relacionamento 1:N)
    CONSTRAINT FK_VagasReservas_Moradores FOREIGN KEY (MoradorId) 
        REFERENCES Moradores(Id) ON DELETE CASCADE
);