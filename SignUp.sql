create database signup

use [signup]

CREATE TABLE Users
(
    Id INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);

select* from Users


