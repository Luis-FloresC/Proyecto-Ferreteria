
USE [Ferreteria]
GO
/****** Object:  StoredProcedure [dbo].[BuscarEmpleado]    Script Date: 15/4/2021 18:24:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[BuscarEmpleado](@codigo int)
as begin
select
  E.Codigo_Empleado [Id],
  E.identidad [Identidad],
  E.Nombre_Empleado[Nombre],
  E.Apellido_empleado [Apellido],
  E.Telefono,
  E.Correo,
  convert(date,E.Fecha_Nacimiento) [Fecha Nac],
  P.Descripcion [Cargo],
  E.Direccion
  from [Recursos_humanos].[Empleado] E
  join [Recursos_humanos].[Puesto] P  on  E.Codigo_Puesto = P.Codigo_Puesto
  where E.[Codigo_Empleado] = @codigo

  end
GO
/****** Object:  StoredProcedure [dbo].[EditarEmpleado]    Script Date: 15/4/2021 18:24:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[EditarEmpleado]
(
@nombre nvarchar(75),
@apellido nvarchar(75),
@codigo int,
@dni nvarchar(20),
@telefono nvarchar(20),
@correo nvarchar(75),
@puesto int,
@fecha_nac datetime,
@direccion nvarchar(75),
@mensaje nvarchar(150) output
)
as begin


if(EXISTS(SELECT * FROM [Recursos_humanos].[Empleado] WHERE Codigo_Empleado = @codigo))
begin

update [Recursos_humanos].[Empleado]
set Nombre_Empleado = @nombre,
    Apellido_empleado = @apellido,
	identidad = @dni,
	Codigo_Puesto = @puesto,
	Telefono = @telefono,
	Correo = @correo,
	Fecha_Nacimiento = @fecha_nac,
	Direccion = @direccion
where Codigo_Empleado = @codigo


SET @mensaje = 'Se actulizaron los datos de: ' + CONCAT(@nombre,' ',@apellido)

end

else 

SET @mensaje = 'No se pudo actulizar el Empleado'


end
GO
/****** Object:  StoredProcedure [dbo].[EditarPerfilUsuario]    Script Date: 15/4/2021 18:24:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[EditarPerfilUsuario]
(
       @id_Empleado int
      ,@Nombre_Empleado nvarchar(50)
	  ,@DNI varchar(20)
      ,@Apellido_empleado nvarchar(50)
      ,@Correo nvarchar(100)
	  ,@Usuario nvarchar(50)
	  ,@Contraseña nvarchar(50)
      ,@Estado bit
	  ,@Mensaje nvarchar(150) output
)
as begin

--Actualizar Datos Personles
update Recursos_humanos.Empleado set Nombre_Empleado = @Nombre_Empleado,
                                     Apellido_empleado = @Apellido_empleado,
									 Correo = @Correo,
									 Estado = @Estado,
									 identidad = @DNI
									 where Codigo_Empleado = @id_Empleado

--Actualizar Datos del Usuario
update Recursos_humanos.Usuario set Nick_Name = @Usuario,
                                    Contrasenia = @Contraseña
									where Codigo_Empleado = @id_Empleado


set @Mensaje = 'El Usuario ' + @Nombre_Empleado +  ' ' + @Apellido_empleado + ' Se Actualizo con exito'



end
GO
/****** Object:  StoredProcedure [dbo].[ManteniemtoProveedores]    Script Date: 15/4/2021 18:24:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Procedure ManteniemtoProveedores
(
@codigo int,
@nombre nvarchar(75),
@telefono nvarchar(20),
@direccion nvarchar(250),
@correo nvarchar(32),
@accion nvarchar(20),
@estado bit,
@mensaje nvarchar(150) output
)
as begin

if(@accion = 'G')
begin

if(not exists(select * from [Compras].[Proveedor] where Nombre_Proveedor = @nombre))
begin

insert into [Compras].[Proveedor] 
values (@nombre,@telefono,@direccion,@correo,@estado)

set @mensaje = 'El Proveedor se registro con Exito!'
end

else
set @mensaje = 'El Proveedor ya Existe!'


end

if(@accion = 'M')
begin

if(exists(select * from [Compras].[Proveedor] where [Codigo_Proveedor] = @codigo))
begin
 update [Compras].[Proveedor] set 
 Nombre_Proveedor = @nombre,
 Telefono = @telefono,
 Direccion = @direccion,
 Correo = @correo,
 Estado = @estado

 where Codigo_Proveedor = @codigo

 set @mensaje = 'Se Actulizaron los datos del proveedor!'

end
else
 set @mensaje = 'No se pudo Actualizar'


end


if(@accion = 'E')
begin

 update [Compras].[Proveedor] set 
 Estado = @estado

 where Codigo_Proveedor = @codigo
set @mensaje = 'Se Elimino el Proveedor Correctamente' 

end


end
GO
/****** Object:  StoredProcedure [dbo].[RegistrarCompras]    Script Date: 15/4/2021 18:24:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[RegistrarCompras]
(
@codigoProveedor int,
@codigoEmpleado int,
@codigoCompra int,
@subTotal money,
@flete money,
@isv float,
@descuento money,
@codigoProducto int,
@precionUnitario money,
@cantidad int,
@mensaje nvarchar(150) output
)
as begin



if(@cantidad <= 0)

set @mensaje = 'No se pueden comprar unidades menores o iguales a 0'

else if(@precionUnitario <= 0)

set @mensaje = 'El precio unitario no puede ser menor o igual a 0'

else
begin

if(not exists(select * from [Compras].[Compra] WHERE Codigo_Compra = @codigoCompra))
begin
INSERT INTO [Compras].[Compra] VAlues 
(GETDATE(),@codigoProveedor,@codigoEmpleado,@flete,@subTotal,@isv,@descuento)
end


insert into [Compras].[Detalle_Compra] values 
(@codigoCompra,@codigoProducto,@precionUnitario,@cantidad)


update [Productos].[Producto] set Existencia = Existencia + @cantidad 
where Codigo_Producto = @codigoProducto

set @mensaje = 'La compra se realizo correctamente'

end

end
GO
/****** Object:  StoredProcedure [dbo].[RegistrarEmpleado]    Script Date: 15/4/2021 18:24:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RegistrarEmpleado]
(
@nombre nvarchar(75),
@apellido varchar(75),
@codigoPuesto int,
@DNI varchar(20),
@telefono nvarchar(20),
@correo nvarchar(75),
@fechaNacimiento datetime,
@estado bit,
@direccion nvarchar(75),
@mensaje nvarchar(150) output
)
as
begin

if(exists(select * from [Recursos_humanos].[Empleado] where identidad = @DNI))
set @mensaje = 'El empleado ya existe'
else
begin
insert into [Recursos_humanos].[Empleado] values
(@nombre,@apellido,@DNI,@codigoPuesto,@telefono,@correo,@fechaNacimiento,GETDATE(),@estado,@direccion)

set @mensaje = 'Se registró con éxito'

end

end

GO
/****** Object:  StoredProcedure [dbo].[RegistrarUsuario]    Script Date: 15/4/2021 18:24:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[RegistrarUsuario]
(
@usuario nvarchar(50),
@contraseña nvarchar(50),
@codigo int,
@mensaje nvarchar(150) output
)
as
begin


if(exists(select * from [Recursos_humanos].[Empleado] where Codigo_Empleado = @codigo) and not exists(select * from [Recursos_humanos].[Usuario] where Codigo_Empleado = @codigo))
begin

insert into [Recursos_humanos].[Usuario] values
(@usuario,@contraseña,@codigo)

set @mensaje = 'Usuario Registrado con exito'

end
else 

set @mensaje = 'El Empleado ya tiene un usuario Disponible'




end
GO
/****** Object:  StoredProcedure [dbo].[Verificar_Usuario]    Script Date: 15/4/2021 18:24:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Verificar_Usuario]
(
@user nvarchar(50),
@contrasenia nvarchar(50)
)
as begin

Select E.Codigo_Empleado [Id],
U.Nick_Name [Usuario],
U.Contrasenia,
E.Nombre_Empleado,
E.Apellido_empleado,
P.Descripcion [Cargo],
E.Correo [Email],
E.Estado,
E.identidad
from [Recursos_humanos].[Empleado] E
join [Recursos_humanos].[Puesto] P on P.Codigo_Puesto = E.Codigo_Puesto
join [Recursos_humanos].[Usuario] U on U.Codigo_Empleado = E.Codigo_Empleado
where U.Nick_Name = @user and U.Contrasenia = @contrasenia

end
GO

--Ingresar Clientes
create PROCEDURE IngresarCliente
@nombres as nvarchar(100),
@apellidos as nvarchar(100),
@identidad as varchar(20),
@fechaNacimiento as datetime,
@telefono as varchar(20),
@rtn as varchar(20),
@msj nvarchar(150) output
AS

BEGIN


if(exists(select * from [Ventas].[Cliente] where [identidad] = @identidad))

set  @msj = 'El Cliente ya existe...'

else 
begin
INSERT INTO [Ventas].[Cliente]
([nombres], 
[apellidos], 
[identidad], 
[fecha_nacimiento], 
[telefono], 
[rtn], 
[estado])
Values
(
@nombres,
@apellidos,
@identidad,
@fechaNacimiento,
@telefono,
@rtn,
CONVERT(bit,'True')
)

set @msj = 'Cliente agregado con exito'


end
END

Go

--Modificar el cliente
CREATE PROCEDURE ModificarCliente
@codigo as int,
@nombres as nvarchar(100),
@apellidos as nvarchar(100),
@identidad as varchar(20),
@fechaNacimiento as datetime,
@telefono as varchar(20),
@rtn as varchar(20)
AS
BEGIN

UPDATE [Ventas].[Cliente] SET 
[nombres] = @nombres,
[apellidos] = @apellidos,
[identidad] = @identidad,
[fecha_nacimiento] = @fechaNacimiento,
[telefono] = @telefono,
[rtn] = @rtn
WHERE codigo_cliente = @codigo

END
Go
--Agregar factura
CREATE PROCEDURE Facturar
@codigoEmpleado as int,
@codigoCliente as int,
@tipoPago as nvarchar(100),
@subtotal as money,
@isv as float,
@descuento as float
AS
BEGIN

INSERT INTO [Ventas].[venta]
(
[fecha_venta],
[codigo_empleado],
[codigo_cliente],
[tipo_pago],
[subtotal],
[isv],
[descuento]
)
VALUES
(
GETDATE(),
@codigoEmpleado,
@codigoCliente,
@tipoPago,
@subtotal,
@isv,
@descuento
)

END
Go
--Agregar detalles de la venta
CREATE PROCEDURE AgregarDetalle
@codigoProducto as int,
@precio as money,
@cantidad as int
AS
BEGIN

INSERT INTO [Ventas].[detalle_venta]
(
[codigo_venta],
[codigo_producto],
[precio_unitario],
[cantidad]
)
VALUES
(
(SELECT MAX(codigo_venta) FROM [Ventas].[venta]),
@codigoProducto,
@precio,
@cantidad
)

UPDATE [Productos].[Producto] 
SET [Existencia] = Existencia - @cantidad
Where [Codigo_Producto] = @codigoProducto

END
Go

Create procedure Factura
(@codigoV int,
@codigoC int)
as begin

select 
concat('FACT-',v.[codigo_venta])[Cod],
v.[fecha_venta],
p.Nombre_Producto,
dv.cantidad,
dv.precio_unitario,
sum(dv.cantidad*dv.precio_unitario)"Total"
from [Ventas].[venta] v
inner join [Ventas].[detalle_venta] dv on v.codigo_venta = dv.codigo_venta
join [Ventas].[Cliente] c on c.[codigo_cliente] = v.[codigo_cliente]
join [Productos].[Producto] p on p.[Codigo_Producto] = dv.[codigo_producto]
where 
v.[codigo_venta] = @codigoV and 
c.codigo_cliente = @codigoC
group by p.Nombre_Producto,dv.cantidad,dv.precio_unitario,v.fecha_venta,v.codigo_venta




end
go

create procedure ClienteFactura
(@codigoC int)
as begin



select CONCAT(c.nombres,' ',c.apellidos) "Nombre",
c.rtn,
c.telefono
from [Ventas].[Cliente] c
where c.codigo_cliente=@codigoC


end
go

create procedure TotalFacturas
(
@codigoV int,
@codigoC int
)
as begin

select
sum(dv.cantidad*dv.precio_unitario)"SubTotal",
sum(dv.cantidad*dv.precio_unitario*v.isv)"ISV",
v.descuento [Descuento],
SUM((dv.cantidad*dv.precio_unitario) + (dv.cantidad*dv.precio_unitario*v.[isv]) - v.descuento) [Total]
from [Ventas].[venta] v
inner join [Ventas].[detalle_venta] dv on v.codigo_venta = dv.codigo_venta
join [Ventas].[Cliente] c on c.[codigo_cliente] = v.[codigo_cliente]
join [Productos].[Producto] p on p.[Codigo_Producto] = dv.[codigo_producto]
where 
v.[codigo_venta] = @codigoV and 
c.codigo_cliente = @codigoC
group by p.Nombre_Producto,dv.cantidad,dv.precio_unitario,v.fecha_venta,v.codigo_venta,v.descuento


end
Go