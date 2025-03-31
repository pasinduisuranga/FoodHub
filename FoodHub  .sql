
CREATE DATABASE FoodHub_Delivery_System;

CREATE TABLE CustomersTable(
	Cus_ID VARCHAR(20) PRIMARY KEY NOT NULL,
    Cus_name VARCHAR(100) NOT NULL,
    Cus_num VARCHAR(20) NOT NULL,
    BOD DATE,
    NIC VARCHAR(20),
    H_Address VARCHAR(200) NOT NULL,
    Loc_no VARCHAR(50),
    line VARCHAR(100),
    street VARCHAR(100) NOT NULL
);
CREATE TABLE RiderTable (
    Rider_id VARCHAR(20) PRIMARY KEY NOT NULL,
    Fir_name VARCHAR(50) NOT NULL,
	Mid_name VARCHAR(50),
    Las_name VARCHAR(50),
    Con_num VARCHAR(20) NOT NULL,
    Lic_no VARCHAR(20) NOT NULL,
    R_Address VARCHAR(200) NOT NULL,
    BOF DATE NOT NULL,
    NIC VARCHAR(20) NOT NULL,
    Age INT
);
CREATE TABLE OrderTable (
    Order_ID VARCHAR(20) PRIMARY KEY NOT NULL,
    Order_status VARCHAR(50) ,
    Pay_method VARCHAR(50) NOT NULL,
    Order_time TIME NOT NULL,
    Order_date DATE NOT NULL,
    Tot_amount DECIMAL(10,2) NOT NULL,
    Dis_time TIME,
    Cus_ID VARCHAR(20) NOT NULL,
    Rider_id VARCHAR(20) NOT NULL,
    FOREIGN KEY (Cus_ID) REFERENCES CustomersTable(Cus_ID),
    FOREIGN KEY (Rider_id) REFERENCES RiderTable(Rider_id)
);
CREATE TABLE Motor_bike (
    Bike_id VARCHAR(20) PRIMARY KEY NOT NULL,
    brand VARCHAR(50) NOT NULL,
    model VARCHAR(50) NOT NULL,
    Reg_no VARCHAR(20) NOT NULL,
    Eng_no VARCHAR(50) NOT NULL,
    colours VARCHAR(100),
    Rider_id VARCHAR(20) NOT NULL
);
CREATE TABLE Assign (
	Rider_id VARCHAR(20) NOT NULL,
	Bike_id VARCHAR (20) NOT NULL
	FOREIGN KEY (Rider_id) REFERENCES RiderTable(Rider_id),
	FOREIGN KEY (Bike_id) REFERENCES Motor_bike(Bike_id)
);
CREATE TABLE Order_Item (
	Item_No VARCHAR(20) PRIMARY KEY NOT NULL,
	Item_Name VARCHAR(100) NOT NULL,
	Price DECIMAL(10,2) NOT NULL,
	Category VARCHAR(50) NOT NULL
);
CREATE TABLE Ingredient (
	Ing_No VARCHAR(20) PRIMARY KEY NOT NULL,
	Ing_Name VARCHAR (100) NOT NULL
);
CREATE TABLE Comprises (
	Item_No VARCHAR(20) NOT NULL, 
	Ing_No VARCHAR(20) NOT NULL,
	FOREIGN KEY (Item_No) REFERENCES Order_Item(Item_No),
	FOREIGN KEY (Ing_No) REFERENCES Ingredient(Ing_No)
);
CREATE TABLE R_Dependent (
	Rider_id VARCHAR(20) NOT NULL,
	Dep_Name VARCHAR(100) NOT NULL,
	BOD DATE ,
	Relationship VARCHAR (50) NOT NULL,
	FOREIGN KEY (Rider_id) REFERENCES RiderTable(Rider_id)
);