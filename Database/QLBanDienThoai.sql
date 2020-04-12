use master 
go
--drop database QLBanDienThoai
go
create database QLBanDienThoai
go
use QLBanDienThoai

-- CREATE ALL TABLE

go

create table ACCOUNT
(
	AccountID int primary key identity(1,1),
	AccountName Nvarchar(100),
	Password Nvarchar(100),
	PhoneNumber Nvarchar(100),
	Address Nvarchar(100),
	Email Nvarchar(100),
	AccountType Nvarchar(100),
)
go
create table PRODUCT
(
	ProductID int primary key identity(1,1),
	Name Nvarchar(100),
	Detail Nvarchar(100),
	Price Money,
	Quantity int,
	Image Nvarchar(100),
	Is_Visible int,
	Is_Active int
)
go
create table PRODUCT_CATEGORY
(
	ID int primary key identity(1,1),
	ProductID int,
	CategoryID int,
)
go
create table CATEGORY 
(
	CategoryID int primary key identity(1,1),
	CategoryName Nvarchar(100),
	Quantity int,
	Is_Active int,
	Archive int
)

-- CREATE ALL CONSTRAINT
go
ALTER TABLE PRODUCT_CATEGORY
ADD CONSTRAINT FK_PRODUCTCATEGORY_CATEGORY
FOREIGN KEY (CategoryID)
REFERENCES CATEGORY (CategoryID)
go
ALTER TABLE PRODUCT_CATEGORY
ADD CONSTRAINT FK_PRODUCTCATEGORY_PRODUCT
FOREIGN KEY (ProductID)
REFERENCES PRODUCT (ProductID)


-- INSERT VALUE

go
insert into ACCOUNT(AccountName,Password,PhoneNumber,Address,Email,AccountType)
			 values('admin','admin','328193728',N'123 đường 123 TPHCM','abc@gmail.com','admin')
insert into ACCOUNT(AccountName,Password,PhoneNumber,Address,Email,AccountType)
			 values('customer1','customer1','328193728',N'123 đường 123 TPHCM','abc@gmail.com','customer'),
				   ('customer2','customer2','328193728',N'123 đường 123 TPHCM','abc@gmail.com','customer')
insert into PRODUCT(Name,Detail,Price,Quantity,Image,Is_Visible,Is_Active)
			 values('IphoneX',N'Màn hình: 6.3", Full HD+ Camera: 16 MP và 2 MP (2 camera), Selfie: 25 MP - PIN: 3500 mAh',10000000,100,'001.jpg',1,1),
				   ('Galaxy S9',N'Màn hình: 6.3", Full HD+ Camera: 16 MP và 2 MP (2 camera), Selfie: 25 MP - PIN: 3500 mAh',8500000,100,'002.jpg',1,1),
				   ('Oppo Find X',N'Màn hình: 6.3", Full HD+ Camera: 16 MP và 2 MP (2 camera), Selfie: 25 MP - PIN: 3500 mAh',6700000,100,'003.jpg',1,1),
			       ('Huawei Nova',N'Màn hình: 6.3", Full HD+ Camera: 16 MP và 2 MP (2 camera), Selfie: 25 MP - PIN: 3500 mAh',12000000,100,'004.jpg',1,1),
				   ('Oppo F9',N'Màn hình: 6.3", Full HD+ Camera: 16 MP và 2 MP (2 camera), Selfie: 25 MP - PIN: 3500 mAh',6900000,100,'005.jpg',1,1),
				   ('Samsung Galaxy J8',N'Màn hình: 6.3", Full HD+ Camera: 16 MP và 2 MP (2 camera), Selfie: 25 MP - PIN: 3500 mAh',8000000,100,'006.jpg',1,1),
				   ('Samsung Galaxy A6 ',N'Màn hình: 6.3", Full HD+ Camera: 16 MP và 2 MP (2 camera), Selfie: 25 MP - PIN: 3500 mAh',4800000,100,'007.jpg',1,1),
				   ('Massgo One 2 ',N'Màn hình: 6.3", Full HD+ Camera: 16 MP và 2 MP (2 camera), Selfie: 25 MP - PIN: 3500 mAh',9500000,100,'008.jpg',1,1)





--------------------------------------------------------------------------
create table TAGS
(
	ID varchar(10) primary key,
	Tag_key Nvarchar(100),
	tag_Value Nvarchar(100),
)
go
create table RATING
(
	RatingID varchar(10) primary key,
	ProductID varchar(10),
	AccountID varchar(10),
	StarNum int,
	Comment Nvarchar(100),
	Is_Active int
)
go
go
create table RECEIPT_NOTE
(
	ReceiptID varchar(10) primary key,
	AccountID varchar(10),
	Amount int,
	State int,
	SuccessAt Datetime,
	Is_Active int,
	Archive int,
)
go
create table DETAIL_RECEIPT
(
	ID INT primary key,
	ReceiptID varchar(10),
	ProductID varchar(10),
	Quantity int,
	State int,
)
create table ORDER_BILL
(
	OrderID varchar(10) primary key,
	AccountID varchar(10),
	Amount int,
	Address Nvarchar(100),
	Phone Nvarchar(100),
	State int,
	Manner int,
	Is_Paid int,
	DateOrder Datetime,
	DateRequired Datetime,
	Is_Active int,
	Archive int,
)
create table ORDER_BILL_DETAIL
(
	ID INT primary key,
	OrderID varchar(10),
	ProductID varchar(10),
	Quantity int,
	Price Money,
)
go
ALTER TABLE ORDER_BILL_DETAIL
ADD CONSTRAINT FK_ORDER_BILL_TO_DETAIL
FOREIGN KEY (OrderID)
REFERENCES ORDER_BILL(OrderID)
go
ALTER TABLE ORDER_BILL_DETAIL
ADD CONSTRAINT FK_ORDER_BILL_DETAIL_PRODUCT
FOREIGN KEY (ProductID)
REFERENCES Product(ProductID)
go
ALTER TABLE ORDER_BILL
ADD CONSTRAINT FK_ORDER_BILL_PRODUCT
FOREIGN KEY (AccountID)
REFERENCES ACCOUNT(AccountID)
go
ALTER TABLE RECEIPT_NOTE
ADD CONSTRAINT FK_RECEIPT_NOTE_ACCOUNT
FOREIGN KEY (AccountID)
REFERENCES ACCOUNT(AccountID)
go
ALTER TABLE DETAIL_RECEIPT
ADD CONSTRAINT FK_DETAIL_RECEIPT_RECEIPT
FOREIGN KEY (ReceiptID)
REFERENCES RECEIPT_NOTE(ReceiptID)
go
ALTER TABLE DETAIL_RECEIPT
ADD CONSTRAINT FK_DETAIL_RECEIPT_PRODUCT
FOREIGN KEY (ProductID)
REFERENCES PRODUCT(ProductID)
go
ALTER TABLE RATING
ADD CONSTRAINT FK_RATING_ACCOUNT
FOREIGN KEY (AccountID)
REFERENCES ACCOUNT(AccountID)
go
ALTER TABLE RATING
ADD CONSTRAINT FK_RATING_PRODUCT
FOREIGN KEY (ProductID)
REFERENCES PRODUCT(ProductID)