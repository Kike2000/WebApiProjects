CREATE TABLE Roles(
Id UNIQUEIDENTIFIER PRIMARY KEY,
Name varchar(50)
)


CREATE TABLE Users(
Id UNIQUEIDENTIFIER PRIMARY KEY,
Name varchar(50),
Email varchar(40),
RolId UNIQUEIDENTIFIER REFERENCES Roles(Id)
)

CREATE TABLE Comments (
Id UNIQUEIDENTIFIER PRIMARY KEY,
Comment varchar(150),
TaskId UNIQUEIDENTIFIER REFERENCES Tasks(Id)
)
drop table Comments

CREATE TABLE Tasks (
Id UNIQUEIDENTIFIER PRIMARY KEY,
Title varchar(50),
Description varchar(40),
IsCompleted BIT,
UserId UNIQUEIDENTIFIER REFERENCES Users(Id),
StartDate Datetime NULL,
EndDate Datetime NULL,
CreationTaskDate DATETIME DEFAULT GETDATE(),
CreatorId UNIQUEIDENTIFIER REFERENCES Users(Id)
)

