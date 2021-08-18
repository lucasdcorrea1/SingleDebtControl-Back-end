CREATE DATABASE "DebitControl"
GO 

USE  "DebitControl"

CREATE TABLE Debit(
	Id					INTEGER IDENTITY(1, 1),
	Value				BIGINT,
	Description			VARCHAR(MAX),
	CreationDate		DATETIME,
	LastUpdateDate		DATETIME,
	CONSTRAINT PK_Debit PRIMARY KEY(Id)
)
GO

 CREATE TABLE DebitHistory(
	Id					INTEGER IDENTITY(1, 1),
	Id_Debit			INTEGER,
	RegisterDate		DATETIME,
	Observation			NVARCHAR(MAX),
	CONSTRAINT PK_DebitHistory PRIMARY KEY(Id),
	CONSTRAINT FK_DebitHistory_DebitHistory FOREIGN KEY (Id_Debit) REFERENCES Debit(Id)
 )
 GO