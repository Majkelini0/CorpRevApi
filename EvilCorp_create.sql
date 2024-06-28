-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2024-06-28 17:03:29.426

-- tables
-- Table: AvailableDiscounts
CREATE TABLE AvailableDiscounts (
    Software_IdSoftware int  NOT NULL,
    Discount_IdDiscount int  NOT NULL,
    CONSTRAINT AvailableDiscounts_pk PRIMARY KEY  (Software_IdSoftware,Discount_IdDiscount)
);

-- Table: Client
CREATE TABLE Client (
    IdClient int  NOT NULL,
    Addres varchar(300)  NOT NULL,
    PhoneNum varchar(50)  NOT NULL,
    Email varchar(200)  NOT NULL,
    IsDeleted varchar(1)  NOT NULL,
    PrevClient varchar(1)  NOT NULL,
    CONSTRAINT Client_pk PRIMARY KEY  (IdClient)
);

-- Table: Company
CREATE TABLE Company (
    Name varchar(300)  NOT NULL,
    Krs varchar(10)  NOT NULL,
    Client_IdClient int  NOT NULL,
    CONSTRAINT Company_pk PRIMARY KEY  (Client_IdClient)
);

-- Table: Discount
CREATE TABLE Discount (
    IdDiscount int  NOT NULL,
    Name varchar(200)  NOT NULL,
    Info varchar(300)  NOT NULL,
    Value decimal(5,2)  NOT NULL,
    DateFrom datetime  NOT NULL,
    DateTo datetime  NOT NULL,
    CONSTRAINT Discount_pk PRIMARY KEY  (IdDiscount)
);

-- Table: Individual
CREATE TABLE Individual (
    FName varchar(200)  NOT NULL,
    LName varchar(200)  NOT NULL,
    PESEL varchar(11)  NOT NULL,
    Client_IdClient int  NOT NULL,
    CONSTRAINT Individual_pk PRIMARY KEY  (Client_IdClient)
);

-- Table: Payment
CREATE TABLE Payment (
    IdPayment int  NOT NULL,
    Amount decimal(8,2)  NOT NULL,
    SingleSale_IdSale int  NOT NULL,
    CONSTRAINT Payment_pk PRIMARY KEY  (IdPayment)
);

-- Table: SingleSale
CREATE TABLE SingleSale (
    IdSale int  NOT NULL,
    CreatedAt datetime  NOT NULL,
    ExpiresAt datetime  NOT NULL,
    FulfilledAt datetime  NULL,
    Price decimal(8,2)  NOT NULL,
    UpdatesInfo varchar(300)  NOT NULL,
    SupportTime int  NOT NULL,
    IdVersion int  NOT NULL,
    IsPaid varchar(1)  NOT NULL,
    Client_IdClient int  NOT NULL,
    Software_IdSoftware int  NOT NULL,
    CONSTRAINT SingleSale_pk PRIMARY KEY  (IdSale)
);

-- Table: Software
CREATE TABLE Software (
    IdSoftware int  NOT NULL,
    Name varchar(200)  NOT NULL,
    SoftInfo varchar(300)  NOT NULL,
    Category varchar(50)  NOT NULL,
    Price int  NOT NULL,
    CONSTRAINT Software_pk PRIMARY KEY  (IdSoftware)
);

-- Table: User
CREATE TABLE "User" (
    IdUser int  NOT NULL,
    Login varchar(100)  NOT NULL,
    Password varchar(100)  NOT NULL,
    Salt varchar(1000)  NOT NULL,
    RefreshToken varchar(1000)  NOT NULL,
    RefreshTokenExpTime  datetime  NOT NULL,
    CONSTRAINT User_pk PRIMARY KEY  (IdUser)
);

-- Table: Version
CREATE TABLE Version (
    Version varchar(9)  NOT NULL,
    VersInfo int  NOT NULL,
    Software_IdSoftware int  NOT NULL,
    CONSTRAINT Version_pk PRIMARY KEY  (Version)
);

-- foreign keys
-- Reference: AvailableDiscounts_Discount (table: AvailableDiscounts)
ALTER TABLE AvailableDiscounts ADD CONSTRAINT AvailableDiscounts_Discount
    FOREIGN KEY (Discount_IdDiscount)
    REFERENCES Discount (IdDiscount);

-- Reference: AvailableDiscounts_Software (table: AvailableDiscounts)
ALTER TABLE AvailableDiscounts ADD CONSTRAINT AvailableDiscounts_Software
    FOREIGN KEY (Software_IdSoftware)
    REFERENCES Software (IdSoftware);

-- Reference: Company_Client (table: Company)
ALTER TABLE Company ADD CONSTRAINT Company_Client
    FOREIGN KEY (Client_IdClient)
    REFERENCES Client (IdClient);

-- Reference: Individual_Client (table: Individual)
ALTER TABLE Individual ADD CONSTRAINT Individual_Client
    FOREIGN KEY (Client_IdClient)
    REFERENCES Client (IdClient);

-- Reference: Payment_SingleSale (table: Payment)
ALTER TABLE Payment ADD CONSTRAINT Payment_SingleSale
    FOREIGN KEY (SingleSale_IdSale)
    REFERENCES SingleSale (IdSale);

-- Reference: SingleSale_Client (table: SingleSale)
ALTER TABLE SingleSale ADD CONSTRAINT SingleSale_Client
    FOREIGN KEY (Client_IdClient)
    REFERENCES Client (IdClient);

-- Reference: SingleSale_Software (table: SingleSale)
ALTER TABLE SingleSale ADD CONSTRAINT SingleSale_Software
    FOREIGN KEY (Software_IdSoftware)
    REFERENCES Software (IdSoftware);

-- Reference: Version_Software (table: Version)
ALTER TABLE Version ADD CONSTRAINT Version_Software
    FOREIGN KEY (Software_IdSoftware)
    REFERENCES Software (IdSoftware);

-- End of file.

