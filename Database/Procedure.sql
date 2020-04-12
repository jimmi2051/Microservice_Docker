use master 
go
--drop database QLBanDienThoai
go
create database QLBanDienThoai
go
use QLBanDienThoai

-- CREATE ALL TABLE

go
create table TAGS
(
	ID varchar(10) primary key,
	Tag_key Nvarchar(100),
	tag_Value Nvarchar(100),
)
go
create table PRODUCT
(
	ProductID varchar(10) primary key,
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
	ID varchar(10) primary key,
	ProductID varchar(10),
	CategoryID varchar(10),
)
go
create table CATEGORY 
(
	CategoryID varchar(10) primary key,
	CategoryName Nvarchar(100),
	Qiantity int,
	Is_Active int,
	Archive int
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
create table ACCOUNT
(
	AccountID varchar(10) primary key,
	AccountName Nvarchar(100),
	Password Nvarchar(100),
	PhoneNumber Nvarchar(100),
	Address Nvarchar(100),
	Email Nvarchar(100),
	AccountType Nvarchar(100),
)
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

-- INSERT VALUE