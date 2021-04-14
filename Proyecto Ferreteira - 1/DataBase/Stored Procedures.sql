
--- Proceso Almacenado para verificar los Usuarios
create procedure Verificar_Usuario
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
E.Estado
from [Recursos_humanos].[Empleado] E
join [Recursos_humanos].[Puesto] P on P.Codigo_Puesto = E.Codigo_Puesto
join [Recursos_humanos].[Usuario] U on U.Codigo_Empleado = E.Codigo_Empleado
where U.Nick_Name = @user and U.Contrasenia = @contrasenia

end

---- Proceso Almacenado para Editar Los Usuarios


Create Procedure EditarPerfilUsuario
(
       @id_Empleado int
      ,@Nombre_Empleado nvarchar(50)
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
									 Estado = @Estado
									 where Codigo_Empleado = @id_Empleado

--Actualizar Datos del Usuario
update Recursos_humanos.Usuario set Nick_Name = @Usuario,
                                    Contrasenia = @Contraseña
									where Codigo_Empleado = @id_Empleado


set @Mensaje = 'El Usuario ' + @Nombre_Empleado +  ' ' + @Apellido_empleado + ' Se Actualizo con exito'



end

-- Proceso ALmacenado para compras

create procedure RegistrarCompras
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

--Registro de Empleados

create procedure RegistrarEmpleado
(
@nombre nvarchar(75),
@apellido varchar(75),
@codigoPuesto int,
@telefono nvarchar(20),
@correo nvarchar(75),
@fechaNacimiento nvarchar(50),
@estado bit,
@direccion nvarchar(75),
@mensaje nvarchar(150) output
)
as
begin

if(exists(select * from [Recursos_humanos].[Empleado] where Nombre_Empleado = @nombre) and exists(select * from [Recursos_humanos].[Empleado] where Apellido_empleado = @apellido) )
set @mensaje = 'El Empleado ya existe'
else
insert into [Recursos_humanos].[Empleado] values
(@nombre,@apellido,@codigoPuesto,@telefono,@correo,convert(date,@fechaNacimiento,103),GETDATE(),@estado,@direccion)

set @mensaje = 'Se Registro con Existo'

end

--Registrar Usuario

create procedure RegistrarUsuario
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