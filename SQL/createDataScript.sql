INSERT INTO PERSON (FIRSTNAME, LASTNAME, GENDER, IDNUM, DATEOFBIRTH)
VALUES
('John', 'Doe', 1, '12345678901', '1985-01-15'),
('Jane', 'Smith', 2, '10987654321', '1990-07-22'),
('Emily', 'Johnson', 2, '11122334455', '1978-12-30'),
('Michael', 'Brown', 1, '12345678902', '1982-05-10'),
('Sarah', 'Davis', 2, '23456789012', '1992-09-25'),
('David', 'Wilson', 1, '34567890123', '1988-03-17'),
('Laura', 'Taylor', 2, '45678901234', '1995-06-01'),
('James', 'Anderson', 1, '56789012345', '1980-11-08'),
('Anna', 'Thomas', 2, '67890123456', '1993-02-20'),
('Robert', 'Jackson', 1, '78901234567', '1987-12-30'),
('Olivia', 'White', 2, '89012345678', '1991-08-14'),
('William', 'Harris', 1, '90123456789', '1983-07-25'),
('Sophia', 'Martin', 2, '01234567890', '1989-10-05'),
('Daniel', 'Moore', 1, '12345678903', '1984-04-22');

DECLARE @Counter INT = 1;

WHILE @Counter <= 20
BEGIN
    INSERT INTO PHONE (TYPE, NUMBER, PERSONID)
    VALUES (
        ABS(CHECKSUM(NEWID())) % 3 + 1, -- PHONETYPE between 1 and 3
        CAST(ABS(CHECKSUM(NEWID())) % 10 + 1000 AS NVARCHAR(50)), -- NUMBER between length of 4 and 50
        ABS(CHECKSUM(NEWID())) % 14 + 1 -- PERSONID between 1 and 14
    );

    SET @Counter = @Counter + 1;
END

INSERT INTO PERSONALRELATIONS (PERSONID, RELATEDPERSONID, TYPE)
VALUES
(1, 2, 1),
(2, 1, 1),
(1, 3, 2),
(3, 1, 2),
(2, 4, 1),
(4, 2, 1),
(3, 5, 3),
(5, 3, 3),
(4, 6, 2),
(6, 4, 2),
(5, 7, 4),
(7, 5, 4),
(6, 8, 1),
(8, 6, 1),
(7, 9, 3),
(9, 7, 3),
(8, 10, 2),
(10, 8, 2),
(9, 11, 4),
(11, 9, 4),
(10, 12, 1),
(12, 10, 1);