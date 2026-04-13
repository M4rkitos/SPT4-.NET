-- Criação da base de dados EasyAccess
-- Compatível com Azure SQL Database

CREATE TABLE Tb_Moradores (
    Id_Morador INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(100) NOT NULL,
    Apartamento VARCHAR(10) NOT NULL,
    Bloco VARCHAR(5)
);

CREATE TABLE Tb_Vagas (
    Id_Vaga INT PRIMARY KEY IDENTITY(1,1),
    Numero_Vaga VARCHAR(10) UNIQUE NOT NULL,
    Tipo_Vaga VARCHAR(20) CHECK (Tipo_Vaga IN ('Morador', 'Visitante', 'PCD')),
    Status_Vaga VARCHAR(20) DEFAULT 'Disponível'
);

CREATE TABLE Tb_Reservas_Visitantes (
    Id_Reserva INT PRIMARY KEY IDENTITY(1,1),
    Placa_Veiculo VARCHAR(10) NOT NULL,
    Data_Reserva DATETIME DEFAULT GETDATE(),
    Id_Morador_Responsavel INT,
    Id_Vaga_Alocada INT,
    FOREIGN KEY (Id_Morador_Responsavel) REFERENCES Tb_Moradores(Id_Morador),
    FOREIGN KEY (Id_Vaga_Alocada) REFERENCES Tb_Vagas(Id_Vaga)
);