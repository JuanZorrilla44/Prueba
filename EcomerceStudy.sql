CREATE DATABASE Ecomerce;

USE Ecomerce;

CREATE TABLE Roles(
 RoleId int IDENTITY(1,1) PRIMARY KEY,
 NameRol varchar(50) NOT NULL,
 StatusRol BIT NOT NULL DEFAULT 1
);

CREATE TABLE Users(
	UserId int IDENTITY(1,1) PRIMARY KEY,
	Email varchar(100) not null UNIQUE,
	Password varchar(100) not null,
	FullName varchar(50) not null,
	UserName varchar(15) not null,
	Phone bigint,
	RoleId int not null,
	StatusUser BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

DROP TABLE Roles;

DROP TABLE Users;

UPDATE Users SET StatusUser = 0 WHERE UserId = 1;

UPDATE Users SET RoleId = @RoleId WHERE UserId = @UserId

SELECT  FROM Users U WHERE StatusUser = 1;

UPDATE Categories SET StatusCategory = @Status WHERE CategoryID = @CategoryID

UPDATE Roles SET StatusRol = @Status WHERE RoleId = @RoleId;


INSERT INTO Users (Email, FullName, Password, Phone, RoleId, UserName) VALUES ('user@example.com', 'string', '7V2FsUwIy5+BrK4zhdT7Cg==', 3165580810, 1, 'Elreydemonio');

INSERT INTO Roles (NameRol) VALUES (@NameRol);

INSERT INTO Categories (NameCategory) VALUES (@NameCategory)

UPDATE Users SET FullName = @FullName, Email = @Email,Phone = @phone, UserName = @UserName  WHERE UserId = @userId;

UPDATE Roles SET NameRol = @NameRol WHERE RoleId = @RoleId;

UPDATE Categories SET NameCategory = @NameCategory WHERE CategoryID = @CategoryID

SELECT * FROM Users WHERE RoleId = @RoleId 


CREATE TABLE Categories(
	CategoryID int IDENTITY(1,1) PRIMARY KEY,
	NameCategory varchar(50) not null,
	StatusCategory BIT NOT NULL DEFAULT 1,
); 

CREATE TABLE Products(
	ProductId int IDENTITY(1,1) PRIMARY KEY,
	NameProduct varchar(50) not null,
	Price float not null,
	Quantity int not null,
	CategoryId int not null,
	StatusProduct BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryID)
);

INSERT INTO Products (NameProduct, CategoryId, Price, Quantity ) VALUES (@NameProduct,@CategoryId,@Price,@Quantity);

UPDATE Products SET NameProduct = @NameProduct, CategoryId = @CategoryId ,Price = @Price, Quantity = @Quantity  WHERE ProductId = @ProductId;

SELECT P.CategoryId, P.NameProduct, P.Price, P.ProductId, P.Quantity, P.StatusProduct, C.NameCategory 
FROM Products P 
INNER JOIN Categories C ON P.CategoryId = C.CategoryID
WHERE P.CategoryId = @CategoryId


CREATE TABLE InputsOutputs(
	InputsOutputsId int IDENTITY(1,1) PRIMARY KEY,
	Quantity int not null,
	ProductId int not null,
	UserId int not null,
	CreationDate DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
	FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
