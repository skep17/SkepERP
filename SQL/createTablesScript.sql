CREATE TABLE Person (
	Id INT NOT NULL PRIMARY KEY IDENTITY, -- იდენტიფიკატორი
	FirstName NVARCHAR(50) NOT NULL, -- სახელი
	LastName NVARCHAR(50) NOT NULL, -- გვარი
	Gender INT NOT NULL, -- სქესი
	PersonalId CHAR(11) NOT NULL, -- პირადი ნომერი
	DateOfBirth DATE NOT NULL, -- დაბადების თარიღი
);

Create Table Phone (
	ID INT NOT NULL PRIMARY KEY IDENTITY, -- იდენტიფიკატორი
	PhoneType INT NOT NULL, -- ტელეფონის ნომრის ტიპი
	Number NVARCHAR(50), -- ნომერი
	PersonId INT, -- ფიზიკური პირი
	CONSTRAINT FK_Person_Phone FOREIGN KEY (PersonId) REFERENCES Person(Id) -- Foreign key შეზღუდვა
);

Create Table PersonalRelations (
	ID INT NOT NULL PRIMARY KEY IDENTITY, -- იდენტიფიკატორი
	PersonId INT NOT NULL, -- ფიზიკური პირი
	RelatedPersonId INT NOT NULL, -- ფიზიკური პირი
	RelationType INT NOT NULL, -- კავშირის ტიპი
	CONSTRAINT UC_Person_Relation UNIQUE (PersonId, RelatedPersonId, RelationType),  -- უნიკალურობის შეზღუდვა
    CONSTRAINT FK_Person_PersonId FOREIGN KEY (PersonId) REFERENCES Person(Id),    -- Foreign key შეზღუდვა
    CONSTRAINT FK_Person_RelatedPersonId FOREIGN KEY (RelatedPersonId) REFERENCES Person(Id) -- Foreign key შეზღუდვა
);