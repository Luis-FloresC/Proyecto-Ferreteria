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
Go

CREATE TABLE Ventas.detalle_venta(
codigo_venta int not null,
codigo_producto int not null,
precio_unitario money not null,
cantidad int not null
)
Go

CREATE TABLE Ventas.envio(
codigo_envio int not null,
codigo_venta int not null,
fecha_entrega datetime not null,
estado_envio nvarchar(50) not null,
direccion nvarchar(255) not null,
codigo_repartidor int not null
)
Go

CREATE TABLE Ventas.Cliente(
codigo_cliente int not null IDENTITY(1,1),
nombres nvarchar(100) not null,
apellidos nvarchar(100) not null,
fecha_nacimiento datetime not null,
telefono varchar(20),
rtn varchar(20)
)
Go

CREATE TABLE Compras.Compras(
Codigo_Compra int not null IDENTITY (1,1),
Fecha_Compra datetime not null,
Codigo_Proveedor int not null,
Codigo_Empleado int not null,
Flete_Envio money not null,
Subtotal money not null,
ISV float not null,
Descuento money not null
Constraint PK_Codigo_Compra
Primary Key Clustered (Codigo_Compra)
)
Go

CREATE TABLE Compras.Detalle_Compras(
Codigo_Compra int not null,
Codigo_Producto int not null,
Precio_Unitario money not null,
Cantidad int not null
)
Go

CREATE TABLE Compras.Proveedor(
Codigo_Proveedor int not null IDENTITY(1,1),
Nombre_Proveedor nvarchar(75)not null,
Telefono nvarchar (20) not null ,
Direccion nvarchar (250) not null,
Correo nvarchar (32) not null
Constraint PK_Codigo_Proveedor
Primary Key Clustered (Codigo_Proveedor)
)
Go

CREATE TABLE Productos.Productos(
Codigo_Producto int not null IDENTITY (1,1),
Nombre_Producto nvarchar(100) not null,
Existencia int not null,
Precio_Estandar money not null,
Codigo_Categoria int not null,
Estado bit not null
Constraint PK_Codigo_Producto
Primary Key Clustered (Codigo_Producto)
)
Go

CREATE TABLE Recursos_humanos.Empleados(
Codigo_Empleado int not null IDENTITY(1,1),
Nombre_Empleado nvarchar(75)not null,
Apellido_empleado nvarchar(75)not null,
Codigo_Puesto int not null,
Telefono nvarchar(20)not null,
Correo nvarchar(75)not null,
Fecha_Nacimiento datetime not null,
Fecha_Contratacion datetime not null,
Estado bit not null,
Direccion nvarchar(75) not null,
Constraint PK_Codigo_Empleado
Primary Key Clustered (Codigo_Empleado)
)
Go

-- Tabla de Categorias
create Table Productos.Categoria
(
Codigo_Categoria int not null identity(1,1),
Nombre_Categoria nvarchar(50) not null,
constraint Pk_Codigo_Categoria
primary key clustered(Codigo_Categoria)
)
Go

-- Llave Foranea de Codigo_Categoria para Productos.Producto 
alter Table Productos.Productos
  add constraint Fk_Productos_Producto$TieneUna$Productos_Categoria
  foreign key(Codigo_Categoria) references Productos.Categoria(Codigo_Categoria)
  on update no action
  on delete no action
Go

-- llave fóranea de Codigo_Proveedor para Compras.Compras
ALTER TABLE Compras.Compras
	Add Constraint FK_Compras_Compras$TieneUna$Compras_Proveedor
	Foreign Key(Codigo_Proveedor) References Compras.Proveedor(Codigo_Proveedor)
	On UPDATE No Action
	On DELETE No Action
Go

-- llave fóranea de Codigo_Empleados para Compras.Compras !Se realiza teniendo la creacion de la tabla empleados
ALTER TABLE Compras.Compras
	Add Constraint FK_Compras_Compras$TieneUna$Recursos_humanos_Empleado
	Foreign Key(Codigo_Empleado) References Recursos_humanos.Empleado(Codigo_Empleado)
	On UPDATE No Action
	On DELETE No Action
Go

--Llave fóranea para detalle_compras (Codigo_Compra)
ALTER TABLE Compras.Detalle_Compras
	Add Constraint FK_Compras_Detalle_Compras$TieneUn$Compras_Compras
	Foreign Key(Codigo_Compra) References Compras.Compras(Codigo_Compra)
	On UPDATE No Action
	On DELETE No Action
Go

--Llave fóranea para detalle_compras (Codigo_Producto)
ALTER TABLE Compras.Detalle_Compras
	Add Constraint FK_Compras_Detalle_Compras$TieneUn$Productos_Nombre_Producto
	Foreign Key(Codigo_Producto) References Productos.Productos(Codigo_Producto)
	On UPDATE No Action
	On DELETE No Action

