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
Constraint PK_codigo_venta
Primary Key Clustered (codigo_venta)
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
codigo_empleado int not null
Constraint PK_codigo_envio
Primary Key Clustered (codigo_envio)
)
Go

CREATE TABLE Ventas.Cliente(
codigo_cliente int not null IDENTITY(1,1),
nombres nvarchar(100) not null,
apellidos nvarchar(100) not null,
identidad varchar(20) not null,
fecha_nacimiento datetime not null,
telefono varchar(20),
rtn varchar(20),
estado bit
Constraint PK_codigo_cliente
Primary Key Clustered (codigo_cliente),
Constraint AK_Cliente_Identidad
unique nonclustered (identidad)
)
Go

CREATE TABLE Compras.Compra(
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

CREATE TABLE Compras.Detalle_Compra(
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

CREATE TABLE Productos.Producto(
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

CREATE TABLE Recursos_humanos.Empleado(
Codigo_Empleado int not null IDENTITY(1,1),
Nombre_Empleado nvarchar(75)not null,
Apellido_empleado nvarchar(75)not null,
identidad varchar(20) not null,
Codigo_Puesto int not null,
Telefono nvarchar(20)not null,
Correo nvarchar(75)not null,
Fecha_Nacimiento datetime not null,
Fecha_Contratacion datetime not null,
Estado bit not null,
Direccion nvarchar(75) not null,
Constraint PK_Codigo_Empleado
Primary Key Clustered (Codigo_Empleado),
Constraint AK_Empleado_Identidad
unique nonclustered (identidad)
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

--Tabla de Puestos
CREATE TABLE Recursos_humanos.Puesto
(
Codigo_Puesto int not null IDENTITY(1,1),
Descripcion nvarchar(150) not null
CONSTRAINT PK_Codigo_Puesto
PRIMARY KEY CLUSTERED(Codigo_Puesto)
)
Go

--Tabla de Usuarios
CREATE TABLE Recursos_humanos.Usuario
(
Codigo_Usuario int not null IDENTITY(1,1),
Nick_Name nvarchar(50) not null,
Contrasenia nvarchar(50) not null,
Codigo_Empleado int not null 
CONSTRAINT PK_Codigo_Usuario
PRIMARY KEY CLUSTERED(Codigo_Usuario)
)
Go

--Llave fóranea para venta (Codigo_empleado) hace referencia a la tabla Empleado
ALTER TABLE Ventas.venta
	Add Constraint FK_Ventas_venta$TieneUn$Recursos_humanos_Empleado
	Foreign Key(codigo_empleado) References Recursos_humanos.Empleado(Codigo_Empleado)
	On UPDATE No Action
	On DELETE No Action

--Llave fóranea para venta (Codigo_cliente) hace referencia a la tabla cliente
ALTER TABLE Ventas.venta
	Add Constraint FK_Ventas_venta$TieneUn$Ventas_Cliente
	Foreign Key(codigo_cliente) References Ventas.Cliente(codigo_cliente)
	On UPDATE No Action
	On DELETE No Action

--Llave fóranea para detalle_venta (codigo_venta) hace referencia a la tabla venta
ALTER TABLE Ventas.detalle_venta
	Add Constraint FK_Ventas_detalle_venta$TieneUn$Ventas_venta
	Foreign Key(codigo_venta) References Ventas.venta(codigo_venta)
	On UPDATE No Action
	On DELETE No Action

--Llave fóranea para detalle_venta (codigo_producto) hace referencia a la tabla productos
ALTER TABLE Ventas.detalle_venta
	Add Constraint FK_Ventas_detalle_venta$TieneUn$Productos_producto
	Foreign Key(codigo_producto) References Productos.producto(codigo_producto)
	On UPDATE No Action
	On DELETE No Action

--Llave fóranea para Envio (codigo_venta) hace referencia a la tabla venta
ALTER TABLE Ventas.envio
	Add Constraint FK_Ventas_Envio$TieneUn$Ventas_venta
	Foreign Key(codigo_venta) References Ventas.venta(codigo_venta)
	On UPDATE No Action
	On DELETE No Action

--Llave fóranea para Envio (codigo_empleado) hace referencia a la tabla empleado
ALTER TABLE Ventas.envio
	Add Constraint FK_Ventas_Envio$TieneUn$Recursos_humanos_Empleado
	Foreign Key(codigo_empleado) References Recursos_humanos.Empleado(codigo_empleado)
	On UPDATE No Action
	On DELETE No Action

--Llave Fóranea de Codigo_Empleado para Recursos_humanos.Usuario
ALTER TABLE Recursos_humanos.Usuario
ADD CONSTRAINT FK_Recursos_humanos_Usuario$TieneUn$Codigo_Empleado
FOREIGN KEY(Codigo_Empleado) REFERENCES Recursos_humanos.Empleado(Codigo_Empleado)
ON UPDATE NO ACTION
ON DELETE NO ACTION
Go

--Llave Fóranea de Codigo_Puesto para Recursos_humanos.Empleados
ALTER TABLE	Recursos_humanos.Empleado
ADD CONSTRAINT FK_Recursos_humanos_Empleado$TieneUn$Codigo_Puesto
FOREIGN KEY(Codigo_Puesto) REFERENCES Recursos_humanos.Puesto(Codigo_Puesto)
ON UPDATE NO ACTION
ON DELETE NO ACTION
Go

-- Llave Foranea de Codigo_Categoria para Productos.Producto 
alter Table Productos.Producto
  add constraint Fk_Productos_Producto$TieneUna$Productos_Categoria
  foreign key(Codigo_Categoria) references Productos.Categoria(Codigo_Categoria)
  on update no action
  on delete no action
Go

-- llave fóranea de Codigo_Proveedor para Compras.Compras
ALTER TABLE Compras.Compra
	Add Constraint FK_Compras_Compra$TieneUna$Compras_Proveedor
	Foreign Key(Codigo_Proveedor) References Compras.Proveedor(Codigo_Proveedor)
	On UPDATE No Action
	On DELETE No Action
Go

-- llave fóranea de Codigo_Empleados para Compras.Compras !Se realiza teniendo la creacion de la tabla empleados
ALTER TABLE Compras.Compra
	Add Constraint FK_Compras_Compra$TieneUna$Recursos_humanos_Empleado
	Foreign Key(Codigo_Empleado) References Recursos_humanos.Empleado(Codigo_Empleado)
	On UPDATE No Action
	On DELETE No Action
Go

--Llave fóranea para detalle_compras (Codigo_Compra)
ALTER TABLE Compras.Detalle_Compra
	Add Constraint FK_Compras_Detalle_Compra$TieneUn$Compras_Compra
	Foreign Key(Codigo_Compra) References Compras.Compra(Codigo_Compra)
	On UPDATE No Action
	On DELETE No Action
Go

--Llave fóranea para detalle_compras (Codigo_Producto)
ALTER TABLE Compras.Detalle_Compra
	Add Constraint FK_Compras_Detalle_Compra$TieneUn$Productos_Nombre_Producto
	Foreign Key(Codigo_Producto) References Productos.Producto(Codigo_Producto)
	On UPDATE No Action
	On DELETE No Action
Go

--Creacion de llave Compuesta en la tabla de Detalle de Compra
ALTER TABLE Compras.Detalle_Compra
	Add Constraint PK_LlaveCompuesta_Detalle_Compra
	primary key(Codigo_Compra,Codigo_Producto)
Go

--Creacion de llave Compuesta en la tabla de Detalle de Compra
ALTER TABLE Ventas.detalle_venta
	Add Constraint PK_LlaveCompuesta_Detalle_Venta
	primary key(codigo_venta,codigo_producto)
Go

--Creacion del indice unico para Nombre_Producto 
Alter table Productos.Producto
ADD UNIQUE (Nombre_Producto)



-- Insertar Datos de Categoria

insert into [Productos].[Categoria] 
values 
('Herramientas de mano'),
('Lubricantes')

-- Insertar Datos para los puestos
insert into [Recursos_humanos].[Puesto]
values
('Administrador'),
('Gerente'),
('Vendedor')

-- Insertar Empleado/Usuario 

insert into [Recursos_humanos].[Empleado]
values('Luis','Flores','0703200003793',1,'96362917','lf016158@gmail.com',2000-06-07,GetDate(),1,'Danli')


insert into  [Recursos_humanos].[Usuario]
values ('Luis','12345678',1)
