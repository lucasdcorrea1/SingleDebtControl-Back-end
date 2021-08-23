CREATE DATABASE "DebitControl"
GO 

USE  "DebitControl"

CREATE TABLE Debit(
	Id					INTEGER IDENTITY(1, 1),
	Value				BIGINT,
	Description			VARCHAR(MAX),
	CreationDate		DATETIME,
	LastUpdateDate		DATETIME,
	CONSTRAINT PK_Debit PRIMARY KEY(Id),
)
GO

 CREATE TABLE Payment(
	Id					INTEGER IDENTITY(1, 1),
	Id_Debit			INTEGER,
	Value				BIGINT,
	Description			VARCHAR(MAX) NULL,
	CreationDate		DATETIME,
	CONSTRAINT PK_Payment PRIMARY KEY(Id),
	CONSTRAINT FK_Debit_Payment FOREIGN KEY (Id_Debit) REFERENCES Debit(Id),
)
GO

 CREATE TABLE PaymentHistory(
	Id					INTEGER IDENTITY(1, 1),
	Id_Debit			INTEGER,
	Id_Payment          INTEGER NULL,
	RegisterDate		DATETIME,
	Observation			NVARCHAR(MAX),
	CONSTRAINT PK_DebitHistory PRIMARY KEY(Id),
	CONSTRAINT FK_Debit_DebitHistory FOREIGN KEY (Id_Debit) REFERENCES Debit(Id),
	CONSTRAINT FK_Payment_DebitHistory FOREIGN KEY (Id_Payment) REFERENCES Debit(Id),
 )
 GO