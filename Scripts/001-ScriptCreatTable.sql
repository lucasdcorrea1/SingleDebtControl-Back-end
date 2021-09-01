CREATE DATABASE "DebitControl"
GO 

CREATE DATABASE Hangfire
GO

USE  "DebitControl"

CREATE TABLE Debits(
	Id					INTEGER IDENTITY(1, 1) NOT NULL,
	Value				BIGINT NOT NULL,
	Description			VARCHAR(MAX) NOT NULL,
	Active				BIT NOT NULL,
	CreationDate		DATETIME NOT NULL,
	LastUpdateDate		DATETIME NULL,
	CONSTRAINT PK_Debit PRIMARY KEY(Id),
)
GO

 CREATE TABLE Payments(
	Id					INTEGER IDENTITY(1, 1) NOT NULL,
	Id_Debit			INTEGER NOT NULL,
	Value				BIGINT NOT NULL,
	Description			VARCHAR(MAX) NULL,
	CreationDate		DATETIME,
	CONSTRAINT PK_Payment PRIMARY KEY(Id),
	CONSTRAINT FK_Debit_Payment FOREIGN KEY (Id_Debit) REFERENCES Debit(Id),
)
GO

 CREATE TABLE Logs(
	Id					INTEGER IDENTITY(1, 1) NOT NULL,
	Id_Debit			INTEGER NOT NULL,
	Id_Payment          INTEGER NULL,
	Date				DATETIME NOT NULL,
	Observation			NVARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_DebitHistory PRIMARY KEY(Id),
	CONSTRAINT FK_Debit_DebitHistory FOREIGN KEY (Id_Debit) REFERENCES Debit(Id),
	CONSTRAINT FK_Payment_DebitHistorys FOREIGN KEY (Id_Payment) REFERENCES Debit(Id),
 )
 GO

        -- [MaxLength(1000)]
        --public string Description { get; set; }

        --[MaxLength(1000)]
        --public string Observation { get; set; }


