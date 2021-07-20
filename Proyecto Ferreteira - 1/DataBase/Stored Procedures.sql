
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

declare @Encriptacion VARBINARY(MAX) = (SELECT ENCRYPTBYPASSPHRASE('password', @Contraseña))

declare @CodigoEstado int = 0
if(@Estado = 1)
  set @CodigoEstado = 1
else
  set @CodigoEstado = 2
--Actualizar Datos Personles
update Recursos_humanos.Empleado set Nombre_Empleado = @Nombre_Empleado,
                                     Apellido_empleado = @Apellido_empleado,
									 Correo = @Correo,
									 cod_estado = @CodigoEstado,
									 identidad = @DNI
									 where Codigo_Empleado = @id_Empleado

--Actualizar Datos del Usuario
update Recursos_humanos.Usuario set Nick_Name = @Usuario,
                                    Contrasenia = @Encriptacion,
									fecha_modificacion = getdate()
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

declare @CodigoEstado int = 0
if(@estado = 1)
  set @CodigoEstado = 1
else
  set @CodigoEstado = 2

 update [Compras].[Proveedor] set 
 Nombre_Proveedor = @nombre,
 Telefono = @telefono,
 Direccion = @direccion,
 Correo = @correo,
 cod_estado = @CodigoEstado

 where Codigo_Proveedor = @codigo

 set @mensaje = 'Se Actulizaron los datos del proveedor!'

end
else
 set @mensaje = 'No se pudo Actualizar'


end


if(@accion = 'E')
begin

 update [Compras].[Proveedor] set 
 cod_estado = 2

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
@cambio money,
@monto money,
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
(GETDATE(),@codigoProveedor,@codigoEmpleado,@flete,@subTotal,@isv,@descuento,@cambio,@monto)
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

declare @encriptacion VARBINARY(MAX) = (SELECT ENCRYPTBYPASSPHRASE('password', @contraseña))

if(exists(select * from [Recursos_humanos].[Empleado] where Codigo_Empleado = @codigo) and not exists(select * from [Recursos_humanos].[Usuario] where Codigo_Empleado = @codigo))
begin

insert into [Recursos_humanos].[Usuario] values
(@usuario,@encriptacion,@codigo,getdate(),null,1)

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

declare @Contraseña_Encriptada varbinary(max) = (select Contrasenia from Recursos_humanos.Usuario R where R.Nick_Name = @user)
declare @Contra nvarchar(50) = CONVERT(nvarchar(MAX), DECRYPTBYPASSPHRASE('password', @Contraseña_Encriptada)) 

Select E.Codigo_Empleado [Id],
U.Nick_Name [Usuario],
@Contra,
E.Nombre_Empleado,
E.Apellido_empleado,
P.Descripcion [Cargo],
E.Correo [Email],
case when E.cod_estado = 1 then convert(bit ,1) else convert(bit ,0) end 'Estado',
E.identidad
from [Recursos_humanos].[Empleado] E
join [Recursos_humanos].[Puesto] P on P.Codigo_Puesto = E.Codigo_Puesto
join [Recursos_humanos].[Usuario] U on U.Codigo_Empleado = E.Codigo_Empleado
where U.Nick_Name = @user and @Contra = @contrasenia


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
[cod_estado])
Values
(
@nombres,
@apellidos,
@identidad,
@fechaNacimiento,
@telefono,
@rtn,
1
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
create PROCEDURE Facturar
@codigoEmpleado as int,
@codigoCliente as int,
@tipoPago as nvarchar(100),
@subtotal as money,
@isv as float,
@descuento as float,
@cambio money,
@monto money

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
[descuento],
cambio,
monto_pago
)
VALUES
(
GETDATE(),
@codigoEmpleado,
@codigoCliente,
@tipoPago,
@subtotal,
@isv,
@descuento,
@cambio,
@monto
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

create procedure IngresarProducto
(
@nombre nvarchar(100),
@precio money,
@codigo int,
@msj nvarchar(150) output
)
as begin

if(not exists(select * from [Productos].[Producto] P where P.Nombre_Producto = @nombre))
begin

INSERT INTO Productos.Producto
VALUES (@nombre, 0 ,@precio,0, @codigo, 1,GETDATE(),null)
set @msj = 'Se ha insertado correctamente el producto'

end
else
set @msj = 'Verifique que no este ingresando un producto ya existente'


end
GO

DECLARE	@return_value int,
		@mensaje nvarchar(150)

EXEC	@return_value = [dbo].[RegistrarUsuario]
		@usuario = N'Admin',
		@contraseña = N'12345678',
		@codigo = 1,
		@mensaje = @mensaje OUTPUT

SELECT	@mensaje as N'@mensaje'

SELECT	'Return Value' = @return_value

GO

create procedure DeatalleProveedores
(@FromDate nvarchar(50),@ToDate nvarchar(50))
as begin
declare @FechaDesde1 datetime = CONVERT (datetime, @FromDate, 103)
declare @Fechahasta2 datetime = convert(datetime,@ToDate,103)

select P.Nombre_Proveedor 'Nombre',
sum(DC.Cantidad) 'Cantidad',
DC.Precio_Unitario,
SUM(DC.Cantidad*DC.Precio_Unitario)'TotalDeCompras'

from Compras.Compra C
join Compras.Detalle_Compra DC on C.Codigo_Compra = DC.Codigo_Compra
join Compras.Proveedor P on P.Codigo_Proveedor = C.Codigo_Proveedor
where C.Fecha_Compra between @FechaDesde1 and @Fechahasta2

group by P.Nombre_Proveedor,DC.Precio_Unitario

end
go

create procedure ReporteCompras
(@FromDate nvarchar(50),@ToDate nvarchar(50))
as begin
declare @FechaDesde1 datetime = CONVERT (datetime, @FromDate, 103)
declare @Fechahasta2 datetime = convert(datetime,@ToDate,103)


select 
row_number() over( order by C.Codigo_Compra desc)'N',
CONCAT('FACT-',C.Codigo_Compra)'FacturaId',
C.Fecha_Compra 'Fecha',
concat(E.Nombre_Empleado,' ',E.Apellido_empleado) 'Empleado',
P.Nombre_Producto 'Producto',
Ct.Nombre_Categoria 'Categoria',
Pr.Nombre_Proveedor 'Proveedor',
DC.Cantidad,
DC.Precio_Unitario,
SUm(DC.Cantidad*DC.Precio_Unitario)'Total',
CONCAT(@FechaDesde1,' - ',@Fechahasta2) 'Rango'
from Compras.Compra C
join Compras.Detalle_Compra DC on C.Codigo_Compra = DC.Codigo_Compra
join Compras.Proveedor Pr on Pr.Codigo_Proveedor = C.Codigo_Proveedor
join Productos.Producto P on P.Codigo_Producto = DC.Codigo_Producto
join Productos.Categoria Ct on Ct.Codigo_Categoria = P.Codigo_Categoria
join Recursos_humanos.Empleado E on E.Codigo_Empleado = C.Codigo_Empleado
where C.Fecha_Compra between @FechaDesde1 and @Fechahasta2
GROUP BY C.Codigo_Compra,C.Fecha_Compra,E.Nombre_Empleado,E.Apellido_empleado,P.Nombre_Producto,Ct.Nombre_Categoria,Pr.Nombre_Proveedor,DC.Cantidad,DC.Precio_Unitario

end
go

create Procedure ReporteVentas
@FechaDesde varchar(50),
@FechaHasta varchar(50)
As
begin
declare @FechaDesde1 datetime = CONVERT (datetime, @FechaDesde, 103)
declare @Fechahasta2 datetime = convert(datetime,@FechaHasta,103)

SELECT 
row_number() over( order by DV.codigo_venta desc)'N',
DV.codigo_venta AS 'VentaID',
	   V.fecha_venta AS 'FechadeVenta',
	   CONCAT(C.nombres,' ', C.apellidos) AS 'Cliente',
	   P.Nombre_Producto AS 'Producto',
	   DV.precio_unitario AS 'PrecioUnitario',
	   DV.cantidad AS 'Cantidad',
	   V.descuento AS 'Descuento',
	   (V.subtotal-V.descuento) AS 'Total',
	   CONCAT(@FechaDesde,' - ',@Fechahasta) 'Rango'
	   FROM Ventas.detalle_venta DV 
	   INNER JOIN Ventas.venta V       ON V.codigo_venta    = DV.codigo_venta
	   INNER JOIN Productos.Producto P ON P.Codigo_Producto = DV.codigo_producto
	   INNER JOIN Ventas.Cliente C     ON C.codigo_cliente  = V.codigo_cliente

Where V.fecha_venta between @FechaDesde1 and  @Fechahasta2

end
go

create procedure VentasDias
(
@FechaDesde varchar(50),
@FechaHasta varchar(50)
)
As
begin
declare @FechaDesde1 datetime = CONVERT (datetime, @FechaDesde, 103)
declare @Fechahasta2 datetime = convert(datetime,@FechaHasta,103)


select PP.Dia,PP.Total from
(select v.fecha_venta 'Fecha',
case when 
datename(dw,v.fecha_venta) = 'Monday' then 'Lunes'
when datename(dw,v.fecha_venta) = 'Tuesday' then 'Martes'  
when datename(dw,v.fecha_venta) = 'Wednesday' then 'Miercoles' 
when datename(dw,v.fecha_venta) = 'Thursday' then 'Jueves' 
when datename(dw,v.fecha_venta) = 'friday' then 'Viernes' 
when datename(dw,v.fecha_venta) = 'Saturday' then 'Sabado' 
else
'Domingo'
end 'Dia',
sum(dv.cantidad*dv.precio_unitario)[Total] from ventas.venta v
join ventas.detalle_venta dv on dv.codigo_venta = v.codigo_venta
where v.fecha_venta between @FechaDesde1 and @Fechahasta2
group by datename(dw,v.fecha_venta),v.fecha_venta
) PP 
group by PP.Dia,PP.Total,PP.Fecha
order by Datepart(DW,PP.Fecha)

end
go

