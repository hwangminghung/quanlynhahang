create database qlnh
go
use qlnh
go
create table account (
username varchar(50) primary key,
pass varchar(50)

)
insert into account values('hungdz','123456')
-- Bảng lưu thông tin khách hàng
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    NameCustomer nvarchar(50),
    Phone NVARCHAR(15),
);

-- Bảng lưu thông tin bàn--
CREATE TABLE Tables (
    TableID INT PRIMARY KEY,
    TableName NVARCHAR(50),
    Capacity INT
);
-- Bảng lưu thông tin món ăn
CREATE TABLE MenuItems (
    ItemID INT PRIMARY KEY,
    ItemName NVARCHAR(100),
    Price DECIMAL(10, 2),
	ProductImage image
);
select * from MenuItems
-- Bảng lưu thông tin đơn hàng
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    TableID INT,
    OrderDate DATE,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (TableID) REFERENCES Tables(TableID)
);
alter table orders alter column orderdate date
select * from Orders
select GETDATE()

-- Bảng chi tiết đơn hàng
CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY,
    OrderID INT,
    ItemID INT,
    Quantity INT,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ItemID) REFERENCES MenuItems(ItemID)
);

-- Bảng theo dõi trạng thái thanh toán
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY,
    OrderID INT,
    AmountPaid DECIMAL(10, 2),
    PaymentDate DATE,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);
alter table payments alter column paymentdate date
-- Chèn thông tin khách hàng
INSERT INTO Customers (CustomerID, NameCustomer,Phone)
VALUES (1, 'John', '0866900933');
-- Chèn thông tin bàn
INSERT INTO Tables (TableID, TableName, Capacity)
VALUES (1, 'Table 1', 4);
-- Chèn thông tin món ăn
INSERT INTO MenuItems (ItemID, ItemName, Price)
VALUES (1, 'Pizza', 12.99);
-- Chèn thông tin đơn hàng
INSERT INTO Orders (OrderID, CustomerID, TableID, OrderDate)
VALUES (1, 1, 1, GETDATE());
-- Chèn chi tiết đơn hàng
INSERT INTO OrderDetails (OrderDetailID, OrderID, ItemID, Quantity)
VALUES (1, 1, 1, 2);
-- Chèn thông tin thanh toán
INSERT INTO Payments (PaymentID, OrderID, AmountPaid, PaymentDate)
VALUES (1, 1, 25.98, GETDATE());

select * from MenuItems