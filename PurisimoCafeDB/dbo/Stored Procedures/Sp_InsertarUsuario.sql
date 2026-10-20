
-- INSERTAR
CREATE PROCEDURE [dbo].[Sp_InsertarUsuario]
	@Nombre NVARCHAR(100) ,
	@Correo NVARCHAR(100) ,
	@Contraseña NVARCHAR(255) ,
	@RolID INT 

AS
BEGIN
INSERT INTO Usuarios(Nombre,Correo,Contraseña,RolID)
VALUES (@Nombre, @Correo, @Contraseña,@RolID);
END
