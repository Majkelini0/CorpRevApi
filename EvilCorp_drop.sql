-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2024-06-28 17:03:29.426

-- foreign keys
ALTER TABLE AvailableDiscounts DROP CONSTRAINT AvailableDiscounts_Discount;

ALTER TABLE AvailableDiscounts DROP CONSTRAINT AvailableDiscounts_Software;

ALTER TABLE Company DROP CONSTRAINT Company_Client;

ALTER TABLE Individual DROP CONSTRAINT Individual_Client;

ALTER TABLE Payment DROP CONSTRAINT Payment_SingleSale;

ALTER TABLE SingleSale DROP CONSTRAINT SingleSale_Client;

ALTER TABLE SingleSale DROP CONSTRAINT SingleSale_Software;

ALTER TABLE Version DROP CONSTRAINT Version_Software;

-- tables
DROP TABLE AvailableDiscounts;

DROP TABLE Client;

DROP TABLE Company;

DROP TABLE Discount;

DROP TABLE Individual;

DROP TABLE Payment;

DROP TABLE SingleSale;

DROP TABLE Software;

DROP TABLE "User";

DROP TABLE Version;

-- End of file.

