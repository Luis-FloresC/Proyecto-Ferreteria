
--- Proceso Almacenado para verificar los Uusuarios
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
