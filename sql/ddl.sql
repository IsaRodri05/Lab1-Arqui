-- Crear base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'arq_per_db')
BEGIN
    CREATE DATABASE arq_per_db;
END
GO

USE arq_per_db;
GO

-- Drop table
--drop table telefono;
--drop table estudios;
--drop table profesion;
--drop table persona;

-- Tabla persona
IF OBJECT_ID('persona', 'U') IS NOT NULL DROP TABLE persona;
CREATE TABLE persona (
    cc INT NOT NULL PRIMARY KEY,
    nombre VARCHAR(45) NOT NULL,
    apellido VARCHAR(45) NOT NULL,
    genero CHAR(1) NOT NULL CHECK (genero IN ('M', 'F')),
    edad INT NULL
);
GO

-- Tabla profesion
IF OBJECT_ID('profesion', 'U') IS NOT NULL DROP TABLE profesion;
CREATE TABLE profesion (
    id INT NOT NULL PRIMARY KEY IDENTITY(1,1),  -- Cambiado para que sea autoincremental
    nom VARCHAR(90) NOT NULL,
    des TEXT NULL
);
GO

-- Tabla estudios
IF OBJECT_ID('estudios', 'U') IS NOT NULL DROP TABLE estudios;
CREATE TABLE estudios (
    id_prof INT NOT NULL,
    cc_per INT NOT NULL,
    fecha DATE NULL,
    univer VARCHAR(50) NULL,
    PRIMARY KEY (id_prof, cc_per),
    FOREIGN KEY (id_prof) REFERENCES profesion(id) ON DELETE CASCADE,  -- Eliminación en cascada
    FOREIGN KEY (cc_per) REFERENCES persona(cc) ON DELETE CASCADE  -- Eliminación en cascada
);
GO

-- Tabla telefono
IF OBJECT_ID('telefono', 'U') IS NOT NULL DROP TABLE telefono;
CREATE TABLE telefono (
    num VARCHAR(15) NOT NULL PRIMARY KEY,
    oper VARCHAR(45) NOT NULL,
    duenio INT NOT NULL,
    FOREIGN KEY (duenio) REFERENCES persona(cc) ON DELETE CASCADE  -- Eliminación en cascada
);
GO
