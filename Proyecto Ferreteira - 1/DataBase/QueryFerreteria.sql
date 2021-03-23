--Query de la base de datos de Ferreteria

Use tempdb
Go

CREATE DATABASE Ferreteria
Go

USE Ferreteria
Go

CREATE SCHEMA Ventas
Go

CREATE SCHEMA Compras
Go

CREATE SCHEMA Productos
Go

CREATE SCHEMA Recursos_humanos
Go

CREATE TABLE Ventas.venta(
codigo_venta int not null IDENTITY(1,1),
fecha_venta datetime not null,
codigo_empleado int not null,
codigo_cliente int not null,
tipo_pago nvarchar(100) not null,
subtotal money not null,
isv float not null,
descuento float not null
)

CREATE TABLE Ventas.detalle_venta(
codigo_venta int not null,
codigo_producto int not null,
precio_unitario money not null,
cantidad int not null
)

CREATE TABLE Ventas.envio(
codigo_envio int not null,
codigo_venta int not null,
fecha_entrega datetime not null,
estado_envio nvarchar(50) not null,
direccion nvarchar(255) not null,
codigo_repartidor int not null
)

CREATE TABLE Ventas.Cliente(
codigo_cliente int not null IDENTITY(1,1),
nombres nvarchar(100) not null,
apellidos nvarchar(100) not null,
fecha_nacimiento datetime not null,
telefono varchar(20),
rtn varchar(20)
)

