CREATE DATABASE DB_HobbyTracker
USE DB_HobbyTracker

--******************************************************** TABLAS ********************************************************************
CREATE TABLE Usuarios 
(IDUsuario INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
nombre VARCHAR(50) NOT NULL,
apellido VARCHAR(50) NOT NULL,
email VARCHAR(50) NOT NULL,
contrasena VARCHAR(50) NOT NULL,
activo BIT NOT NULL
)

CREATE TABLE Clientes 
(IDCliente INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
nombre VARCHAR(50) NOT NULL,
apellido VARCHAR(50) NOT NULL,
email VARCHAR(50) NOT NULL,
contrasena VARCHAR(50) NOT NULL,
restablecerContrasena BIT DEFAULT 0
)

CREATE TABLE Libros 
(IDLibro INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
nombre VARCHAR(150) NOT NULL,
autor VARCHAR(50) NULL,
editorial VARCHAR(50) NULL,
leido BIT NULL, -- 1 Leído 0 Sin leer
cliente INT NOT NULL references Clientes(IDCliente)
)

CREATE TABLE CategoriaPelicula 
(IDCategoria INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
descripcion VARCHAR(50) NOT NULL,
activa BIT NULL,
cliente INT NOT NULL references Clientes(IDCliente)
)

CREATE TABLE Peliculas 
(IDPelicula INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
nombre VARCHAR(150) NOT NULL,
categoria INT NULL references CategoriaPelicula(IDCategoria),
--FOREIGN KEY (categoria) REFERENCES CategoriaPelicula(IDCategoria),
vista BIT NULL, -- 1 Terminiada 0 Pendiente
observaciones VARCHAR(200) NULL,
cliente INT NOT NULL references Clientes(IDCliente)
)

CREATE TABLE Series 
(IDSerie INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
nombre VARCHAR(150) NOT NULL,
estado BIT NULL, -- 1 Terminiada 0 Pendiente
registro VARCHAR(100) NULL,
cliente INT NOT NULL references Clientes(IDCliente)
)

CREATE TABLE Vinilos 
(IDVinilo INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
vinilo VARCHAR(150) NOT NULL,
banda VARCHAR(50) NOT NULL,
estado BIT NULL, -- 1 Adquirido 0 Por comprar
observaciones VARCHAR(200) NULL,
cliente INT NOT NULL references Clientes(IDCliente)
)

CREATE TABLE Conciertos 
(IDConcierto INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
concierto VARCHAR(150) NOT NULL,
lugar VARCHAR(100) NOT NULL,
fecha DATETIME NOT NULL, --año mes día
estado BIT NULL, -- 1 pendiente 0 vencido
observaciones VARCHAR(300) NULL,
cliente INT NOT NULL references Clientes(IDCliente)
)

CREATE TABLE JuegosSwitch 
(IDJuegoSwitch INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
juego VARCHAR(150) NOT NULL,
formato INT NULL, -- 0 pendiente 1 físico 2 digital
progreso INT NULL, -- 0 pendiente 1 termindado 2 sin terminar
estado BIT NULL, -- 1 Adquirido 0 Por comprar 
observaciones VARCHAR(100),
cliente INT NOT NULL references Clientes(IDCliente)
)

CREATE TABLE Animes 
(IDAnime INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
nombre VARCHAR(200) NOT NULL,
estado BIT NULL, -- 1 Terminiada 0 Pendiente
registro VARCHAR(100) NULL,
cliente INT NOT NULL references Clientes(IDCliente)
)

CREATE TABLE AnimePeliculas 
(IDAnimePelicula INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
nombre VARCHAR(200) NOT NULL,
estado BIT NULL, -- 1 Terminiada 0 Pendiente
observaciones VARCHAR(200) NULL,
cliente INT NOT NULL references Clientes(IDCliente)
)

--***************************************** PROCEDIMIENTOS ALMACENADOS USUARIOS ******************************************************

--******************************************************* USUARIOS *******************************************************************
--LISTAR USUARIOS
CREATE PROCEDURE SP_ListarUsuarios
AS
BEGIN
    SELECT IDUsuario, nombre, apellido, email, contrasena, activo FROM Usuarios
END

--INSERTAR USUARIO
CREATE PROCEDURE SP_InsertarUsuario
(@nombre VARCHAR(50),
@apellido VARCHAR(50),
@email VARCHAR(50),
@contrasena VARCHAR(50),
@activo BIT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Usuarios WHERE email = @email)
	BEGIN
		INSERT INTO Usuarios (nombre, apellido, email, contrasena, activo)
		VALUES (@nombre, @apellido, @email, @contrasena, @activo)

		SET @resultado = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		SET @mensaje = 'El usuario ya existe'
		PRINT('El usuario ya existe')
	END
END

--EDITAR USUARIO
CREATE PROCEDURE SP_EditarUsuario
(@IDUsuario INT,
@nombre VARCHAR(50),
@apellido VARCHAR(50),
@email VARCHAR(50),
@activo BIT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Usuarios WHERE email = @email AND
	IDUsuario != @IDUsuario)
	BEGIN
		UPDATE TOP(1) Usuarios SET
		nombre = @nombre,
		apellido = @apellido,
		email = @email,
		activo = @activo
		WHERE IDUsuario = @IDUsuario

		SET @resultado = 1
	END
	ELSE
	BEGIN
		SET @mensaje = 'No se pudo actualizar el usuario'
		PRINT('No se pudo actualizar el usuario')
	END
END

--ELIMINAR USUARIO
CREATE PROCEDURE SP_EliminarUsuario
    @id INT,
    @resultado INT OUTPUT,
	@mensaje VARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DELETE TOP (1) FROM Usuarios WHERE IDUsuario = @id;
        SET @resultado = 1
    END TRY
    BEGIN CATCH
        SET @mensaje = 'No se pudo eliminar el usuario'
		PRINT('No se pudo eliminar el usuario')
    END CATCH
END

--******************************************************* CLIENTES *******************************************************************
--LISTAR CLIENTES
CREATE PROCEDURE SP_ListarClientes
AS
BEGIN
    SELECT IDCliente, nombre, apellido, email, contrasena, restablecerContrasena FROM Clientes
END

--LISTAR CLIENTES POR PERFIL
CREATE PROCEDURE SP_ListarClientesPerfil
(@IDCliente INT)
AS
BEGIN
    SELECT IDCliente, nombre, apellido, email, contrasena, restablecerContrasena FROM Clientes
	WHERE IDCliente = @IDCliente
END

--INSERTAR CLIENTES
CREATE PROCEDURE SP_InsertarCliente
(@nombre VARCHAR(50),
@apellido VARCHAR(50),
@email VARCHAR(50),
@contrasena VARCHAR(50),
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Clientes WHERE email = @email)
	BEGIN
		INSERT INTO Clientes(nombre, apellido, email, contrasena, restablecerContrasena)
		VALUES (@nombre, @apellido, @email, @contrasena, 0)

		SET @resultado = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		SET @mensaje = 'El cliente ya existe'
		PRINT('El cliente ya existe')
	END
END

--EDITAR CLIENTES
CREATE PROCEDURE SP_EditarCliente
(@IDCliente INT,
@nombre VARCHAR(50),
@apellido VARCHAR(50),
@email VARCHAR(50),
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Clientes WHERE email = @email AND IDCliente != @IDCliente)
	BEGIN
		UPDATE TOP(1) Clientes SET
		nombre = @nombre,
		apellido = @apellido,
		email = @email
		WHERE IDCliente = @IDCliente

		SET @resultado = 1
	END
	ELSE
	BEGIN
		SET @mensaje = 'No se pudo actualizar el cliente'
		PRINT('No se pudo actualizar el cliente')
	END
END

--CAMBIAR CONTRASEÑA EN EL PERFIL
CREATE PROCEDURE SP_CambiarContrasenaPerfil
    @IDCliente INT,
	@contrasena VARCHAR(50),
    @resultado INT OUTPUT,
	@mensaje VARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE TOP(1) Clientes SET
		contrasena = @contrasena
		WHERE IDCliente = @IDCliente

        SET @resultado = 1
    END TRY
    BEGIN CATCH
        SET @mensaje = 'No se pudo cambiar la contraseña'
		PRINT('No se pudo cambiar la contraseña')
    END CATCH
END

--CAMBIAR CONTRASEÑA
CREATE PROCEDURE SP_CambiarContrasena
    @IDCliente INT,
	@contrasena VARCHAR(50),
    @resultado INT OUTPUT,
	@mensaje VARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE TOP(1) Clientes SET
		contrasena = @contrasena,
		restablecerContrasena = 0
		WHERE IDCliente = @IDCliente

        SET @resultado = 1
    END TRY
    BEGIN CATCH
        SET @mensaje = 'No se pudo cambiar la contraseña'
		PRINT('No se pudo cambiar la contraseña')
    END CATCH
END

--RESTABLECER CONTRASEÑA
CREATE PROCEDURE SP_RestablecerContrasaena
    @IDCliente INT,
	@contrasena VARCHAR(50),
    @resultado INT OUTPUT,
	@mensaje VARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE TOP(1) Clientes SET
		contrasena = @contrasena,
		restablecerContrasena = 1
		WHERE IDCliente = @IDCliente

        SET @resultado = 1
    END TRY
    BEGIN CATCH
        SET @mensaje = 'No se pudo restablecer la contraseña'
		PRINT('No se pudo restablecer la contraseña')
    END CATCH
END

EXEC SP_ListarClientes

--******************************************************** LIBROS ********************************************************************
--LISTAR LIBROS
CREATE PROCEDURE SP_ListarLibros
(@IDCliente INT)
AS
BEGIN
    SELECT IDLibro, nombre, autor, editorial, leido, cliente FROM Libros
	WHERE cliente = @IDCliente
END

--INSERTAR LIBRO
CREATE PROCEDURE SP_InsertarLibro
(@nombre VARCHAR(200),
@autor VARCHAR(50),
@editorial VARCHAR(50),
@leido BIT,
@IDCliente INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Usuarios WHERE nombre = @nombre)
	BEGIN
		INSERT INTO Libros(nombre, autor, editorial, leido, cliente)
		VALUES (@nombre, @autor, @editorial, @leido, @IDCliente)

		SET @resultado = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		SET @mensaje = 'El libro ya existe'
		PRINT('El libro ya existe')
	END
END

--EDITAR LIBRO
CREATE PROCEDURE SP_EditarLibro
(@IDLibro INT,
@nombre VARCHAR(200),
@autor VARCHAR(50),
@editorial VARCHAR(50),
@leido BIT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Libros WHERE nombre = @nombre AND IDLibro != @IDLibro)
	BEGIN
		UPDATE TOP(1) Libros SET
		nombre = @nombre,
		autor = @autor,
		editorial = @editorial,
		leido = @leido
		WHERE IDLibro = @IDLibro

		SET @resultado = 1
	END
	ELSE
	BEGIN
		SET @mensaje = 'No se pudo actualizar el libro'
		PRINT('No se pudo actualizar el libro')
	END
END

--ELIMINAR LIBRO
CREATE PROCEDURE SP_EliminarLibro
    @IDLibro INT,
    @resultado INT OUTPUT,
	@mensaje VARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DELETE TOP (1) FROM Libros WHERE IDLibro = @IDLibro;
        SET @resultado = 1
    END TRY
    BEGIN CATCH
        SET @mensaje = 'No se pudo eliminar el libro'
		PRINT('No se pudo eliminar el libro')
    END CATCH
END

--****************************************************** PELÍCULAS *******************************************************************
--LISTAR PELÍCULAS
CREATE PROCEDURE SP_ListarPeliculas
    @IDCliente INT
AS
BEGIN
    SELECT P.*, CP.IDCategoria, CP.descripcion  -- Seleccionar también la columna IDCategoria de la tabla CategoriaPelicula
    FROM Peliculas P
    INNER JOIN CategoriaPelicula CP ON P.categoria = CP.IDCategoria
    INNER JOIN Clientes C ON P.cliente = C.IDCliente
    WHERE C.IDCliente = @IDCliente
END 

EXEC SP_ListarPeliculas 1

--INSERTAR PELÍCULA
CREATE PROCEDURE SP_InsertarPelicula
(@nombre VARCHAR(200),
@IDCategoria INT,
@vista BIT,
@observaciones VARCHAR(200),
@IDCliente INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Peliculas WHERE nombre = @nombre)
	BEGIN
		INSERT INTO Peliculas(nombre, categoria, vista, observaciones, cliente)
		VALUES (@nombre, @IDCategoria, @vista, @observaciones, @IDCliente)

		SET @resultado = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		SET @mensaje = 'La película ya existe'
		PRINT('La película ya existe')
	END
END

--EDITAR PELÍCULA
CREATE PROCEDURE SP_EditarPelicula
(@IDPelicula INT,
@nombre VARCHAR(200),
@IDCategoria INT,
@vista BIT,
@observaciones VARCHAR(200),
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Peliculas WHERE nombre = @nombre AND IDPelicula != @IDPelicula)
	BEGIN
		UPDATE TOP(1) Peliculas SET
		nombre = @nombre,
		categoria = @IDCategoria,
		vista = @vista,		
		observaciones = @observaciones
		WHERE IDPelicula = @IDPelicula

		SET @resultado = 1
	END
	ELSE
	BEGIN
		SET @mensaje = 'No se pudo actualizar la película'
		PRINT('No se pudo actualizar la película')
	END
END

--ELIMINAR PELÍCULA
CREATE PROCEDURE SP_EliminarPelicula
(@IDPelicula INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DELETE TOP (1) FROM Peliculas WHERE IDPelicula = @IDPelicula;
        SET @resultado = 1
    END TRY
    BEGIN CATCH
        SET @mensaje = 'No se pudo eliminar la película'
		PRINT('No se pudo eliminar la película')
    END CATCH
END

--***************************************************** CATERGORÍAS ******************************************************************
--LISTAR CATEGORÍAS
CREATE PROCEDURE SP_ListarCategorias
(@IDCliente INT)
AS
BEGIN
    SELECT IDCategoria, descripcion, activa, cliente FROM CategoriaPelicula 
	WHERE cliente = @IDCliente
END

--INSERTAR CATEGORÍAS
CREATE PROCEDURE SP_InsertarCategoria
(@descripcion VARCHAR(50),
@activa BIT,
@IDCliente INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM CategoriaPelicula WHERE descripcion = @descripcion)
	BEGIN
		INSERT INTO CategoriaPelicula(descripcion, activa, cliente)
		VALUES (@descripcion, @activa, @IDCliente)

		SET @resultado = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		SET @mensaje = 'La categoría ya existe'
		PRINT('La categoría ya existe')
	END
END

--EDITAR CATEGORÍAS
CREATE PROCEDURE SP_EditarCategoria
(@IDCategoria INT,
@descripcion VARCHAR(50),
@activa BIT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM CategoriaPelicula WHERE descripcion = @descripcion AND IDCategoria != @IDCategoria)
	BEGIN
		UPDATE TOP(1) CategoriaPelicula SET
		descripcion = @descripcion,
		activa = @activa
		WHERE IDCategoria = @IDCategoria

		SET @resultado = 1
	END
	ELSE
	BEGIN
		SET @mensaje = 'No se pudo actualizar la categoría'
		PRINT('No se pudo actualizar la categoría')
	END
END

--ELIMINAR CATEGORÍAS
CREATE PROCEDURE SP_EliminarCategoria
(@IDCategoria INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DELETE TOP (1) FROM CategoriaPelicula WHERE IDCategoria = @IDCategoria;
        SET @resultado = 1
    END TRY
    BEGIN CATCH
        SET @mensaje = 'No se pudo eliminar la categoría'
		PRINT('No se pudo eliminar la categoría')
    END CATCH
END

--******************************************************** SERIES ********************************************************************
--LISTAR SERIES
CREATE PROCEDURE SP_ListarSeries
(@IDCliente INT)
AS
BEGIN
    SELECT IDSerie, nombre, estado, registro, cliente FROM Series
	WHERE cliente = @IDCliente
END

--INSERTAR SERIES
CREATE PROCEDURE SP_InsertarSerie
(@nombre VARCHAR(200),
@estado BIT,
@registro VARCHAR(100),
@IDCliente INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Series WHERE nombre = @nombre)
	BEGIN
		INSERT INTO Series(nombre, estado, registro, cliente)
		VALUES (@nombre, @estado, @registro, @IDCliente)

		SET @resultado = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		SET @mensaje = 'La serie ya existe'
		PRINT('La serie ya existe')
	END
END

--EDITAR SERIES
CREATE PROCEDURE SP_EditarSerie
(@IDSerie INT,
@nombre VARCHAR(200),
@estado BIT,
@registro VARCHAR(200),
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Series WHERE nombre = @nombre AND IDSerie != @IDSerie)
	BEGIN
		UPDATE TOP(1) Series SET
		nombre = @nombre,
		estado = @estado,
		registro = @registro
		WHERE IDSerie = @IDSerie

		SET @resultado = 1
	END
	ELSE
	BEGIN
		SET @mensaje = 'No se pudo actualizar la serie'
		PRINT('No se pudo actualizar la serie')
	END
END

--ELIMINAR SERIES
CREATE PROCEDURE SP_EliminarSerie
(@IDSerie INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DELETE TOP (1) FROM Series WHERE IDSerie = @IDSerie;
        SET @resultado = 1
    END TRY
    BEGIN CATCH
        SET @mensaje = 'No se pudo eliminar la serie'
		PRINT('No se pudo eliminar la serie')
    END CATCH
END

--******************************************************** VINILOS *******************************************************************
--LISTAR VINILOS
CREATE PROCEDURE SP_ListarVinilos
(@IDCliente INT)
AS
BEGIN
    SELECT IDVinilo, vinilo, banda, estado, observaciones, cliente FROM Vinilos
	WHERE cliente = @IDCliente
END

--INSERTAR VINILOS
CREATE PROCEDURE SP_InsertarVinilo
(@vinilo VARCHAR(200),
@banda VARCHAR(50),
@estado BIT,
@observaciones VARCHAR(100),
@IDCliente INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Vinilos WHERE vinilo = @vinilo)
	BEGIN
		INSERT INTO Vinilos(vinilo, banda, estado, observaciones, cliente)
		VALUES (@vinilo, @banda, @estado, @observaciones, @IDCliente)

		SET @resultado = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		SET @mensaje = 'El vinilo ya existe'
		PRINT('El vinilo ya existe')
	END
END

--EDITAR VINILOS
CREATE PROCEDURE SP_EditarVinilo
(@IDVinilo INT,
@vinilo VARCHAR(200),
@banda VARCHAR(50),
@estado BIT,
@observaciones VARCHAR(100),
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Vinilos WHERE vinilo = @vinilo AND IDVinilo != @IDVinilo)
	BEGIN
		UPDATE TOP(1) Vinilos SET
		vinilo = @vinilo,
		banda = @banda,
		estado = @estado,
		observaciones = @observaciones
		WHERE IDVinilo = @IDVinilo

		SET @resultado = 1
	END
	ELSE
	BEGIN
		SET @mensaje = 'No se pudo actualizar el vinilo'
		PRINT('No se pudo actualizar el vinilo')
	END
END

--ELIMINAR VINILOS
CREATE PROCEDURE SP_EliminarVinilo
(@IDVinilo INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DELETE TOP (1) FROM Vinilos WHERE IDVinilo = @IDVinilo;
        SET @resultado = 1
    END TRY
    BEGIN CATCH
        SET @mensaje = 'No se pudo eliminar el vinilo'
		PRINT('No se pudo eliminar el vinilo')
    END CATCH
END

--****************************************************** CONCIERTOS ******************************************************************
--LISTAR CONCIERTOS
CREATE PROCEDURE SP_ListarConciertos
(@IDCliente INT)
AS
BEGIN
    UPDATE Conciertos
    SET estado = 0
    WHERE fecha < GETDATE(); -- Cambiar el estado a 0 si la fecha ha pasado

    SELECT IDConcierto, concierto, lugar, /*CONVERT(CHAR(10), fecha, 103)[fecha]*/ fecha, estado, observaciones, cliente
    FROM Conciertos
	WHERE cliente = @IDCliente
	ORDER BY fecha
END

--INSERTAR CONCIERTOS
CREATE PROCEDURE SP_InsertarConcierto
(@concierto VARCHAR(200),
@lugar VARCHAR(100),
@fecha DATETIME,
--@estado BIT,
@observaciones VARCHAR(300),
@IDCliente INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @resultado = 0;

    BEGIN TRY
		DECLARE @estado CHAR(1);

        -- Comprobar si la fecha del concierto es anterior a la fecha actual
        IF @fecha < GETDATE()
        BEGIN
            SET @estado = '0'; -- Estado 'o' para indicar que el concierto ya ha pasado
        END
        ELSE
        BEGIN
            SET @estado = '1'; -- Estado '1' para indicar que el concierto está programado
        END
		
        INSERT INTO Conciertos(concierto, lugar, fecha, estado, observaciones, cliente)
        VALUES (@concierto, @lugar, @fecha, 1, @observaciones, @IDCliente)

        SET @resultado = SCOPE_IDENTITY();
    END TRY
    BEGIN CATCH
        -- Captura de errores
        SET @mensaje = ERROR_MESSAGE(); -- Mensaje de error
        SET @resultado = -1; -- Código de resultado para indicar error
    END CATCH
END

--EDITAR CONCIERTOS
CREATE PROCEDURE SP_EditarConcierto
(@IDConcierto INT,
@concierto VARCHAR(200),
@lugar VARCHAR(100),
@fecha DATETIME,
--@estado BIT,
@observaciones VARCHAR(300),
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	BEGIN TRY
		DECLARE @estado CHAR(1);

        -- Comprobar si la fecha del concierto es anterior a la fecha actual
        IF @fecha < GETDATE()
        BEGIN
            SET @estado = '0'; -- Estado 'o' para indicar que el concierto ya ha pasado
        END
        ELSE
        BEGIN
            SET @estado = '1'; -- Estado '1' para indicar que el concierto está programado
        END
		
		UPDATE TOP(1) Conciertos SET
		concierto = @concierto,
		lugar = @lugar,
		fecha = @fecha,
		estado = @estado,
		observaciones = @observaciones
		WHERE IDConcierto = @IDConcierto

		SET @resultado = 1
	END TRY
    BEGIN CATCH
		-- Captura de errores
        SET @mensaje = ERROR_MESSAGE(); -- Mensaje de error
        SET @resultado = -1; -- Código de resultado para indicar error
    END CATCH
END

--ELIMINAR CONCIERTOS
CREATE PROCEDURE SP_EliminarConcierto
    @IDConcierto INT,
    @resultado INT OUTPUT,
	@mensaje VARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DELETE TOP (1) FROM Conciertos WHERE IDConcierto = @IDConcierto;
        SET @resultado = 1
    END TRY
    BEGIN CATCH
        SET @mensaje = 'No se pudo eliminar el concierto'
		PRINT('No se pudo eliminar el concierto')
    END CATCH
END

--**************************************************** JUEGOS SWITCH *****************************************************************
--LISTAR JUEGOS SWITCH
CREATE PROCEDURE SP_ListarJuegosSwitch
(@IDCliente INT)
AS
BEGIN
    SELECT IDJuegoSwitch, juego, formato, progreso, estado, observaciones, cliente 
    FROM JuegosSwitch
    WHERE cliente = @IDCliente
END

--INSERTAR JUEGOS SWITCH
CREATE PROCEDURE SP_InsertarJuegoSwitch
(@juego VARCHAR(200),
@formato INT,
@progreso INT,
@estado BIT,
@observaciones VARCHAR(100),
@IDCliente INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @resultado = 0;

    BEGIN TRY
        INSERT INTO JuegosSwitch(juego, progreso, formato, estado, observaciones, cliente)
        VALUES (@juego, @formato, @progreso, @estado, @observaciones, @IDCliente);

        SET @resultado = SCOPE_IDENTITY();
    END TRY
    BEGIN CATCH
        -- Captura de errores
        SET @mensaje = ERROR_MESSAGE(); -- Mensaje de error
        SET @resultado = -1; -- Código de resultado para indicar error
    END CATCH
END

--EDITAR JUEGOS SWITCH
CREATE PROCEDURE SP_EditarJuegoSwitch
(@IDJuegoSwitch INT,
@juego VARCHAR(200),
@formato INT,
@progreso INT,
@estado BIT,
@observaciones VARCHAR(300),
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	BEGIN TRY
		UPDATE TOP(1) JuegosSwitch SET
		juego = @juego,
		formato = @formato,
		progreso = @progreso,
		estado = @estado,
		observaciones = @observaciones
		WHERE IDJuegoSwitch = @IDJuegoSwitch

		SET @resultado = 1
	END TRY
    BEGIN CATCH
		-- Captura de errores
        SET @mensaje = ERROR_MESSAGE(); -- Mensaje de error
        SET @resultado = -1; -- Código de resultado para indicar error
    END CATCH
END

--ELIMINAR JUEGOS SWITCH
CREATE PROCEDURE SP_EliminarJuegoSwitch
    @IDJuegoSwitch INT,
    @resultado INT OUTPUT,
	@mensaje VARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DELETE TOP (1) FROM JuegosSwitch WHERE IDJuegoSwitch = @IDJuegoSwitch;
        SET @resultado = 1
    END TRY
    BEGIN CATCH
        SET @mensaje = 'No se pudo eliminar el juego'
		PRINT('No se pudo eliminar el juego')
    END CATCH
END

--******************************************************** ANIMES ********************************************************************
--LISTAR ANIMES
CREATE PROCEDURE SP_ListarAnimes
(@IDCliente INT)
AS
BEGIN
    SELECT IDAnime, nombre, estado, registro, cliente FROM Animes
	WHERE cliente = @IDCliente
END

--INSERTAR ANIMES
CREATE PROCEDURE SP_InsertarAnime
(@nombre VARCHAR(200),
@estado BIT,
@registro VARCHAR(100),
@IDCliente INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Series WHERE nombre = @nombre)
	BEGIN
		INSERT INTO Animes(nombre, estado, registro, cliente)
		VALUES (@nombre, @estado, @registro, @IDCliente)

		SET @resultado = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		SET @mensaje = 'El anime ya existe'
		PRINT('El anime  existe')
	END
END

--EDITAR ANIMES
CREATE PROCEDURE SP_EditarAnime
(@IDAnime INT,
@nombre VARCHAR(200),
@estado BIT,
@registro VARCHAR(100),
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM Animes WHERE nombre = @nombre AND IDAnime != @IDAnime)
	BEGIN
		UPDATE TOP(1) Animes SET
		nombre = @nombre,
		estado = @estado,
		registro = @registro
		WHERE IDAnime = @IDAnime

		SET @resultado = 1
	END
	ELSE
	BEGIN
		SET @mensaje = 'No se pudo actualizar el anime'
		PRINT('No se pudo actualizar el anime')
	END
END

--ELIMINAR ANIMES
CREATE PROCEDURE SP_EliminarAnime
(@IDAnime INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DELETE TOP (1) FROM Animes WHERE IDAnime = @IDAnime;
        SET @resultado = 1
    END TRY
    BEGIN CATCH
        SET @mensaje = 'No se pudo eliminar el anime'
		PRINT('No se pudo eliminar el anime')
    END CATCH
END

--******************************************************** ANIMES PELÍCULAS ********************************************************************
--LISTAR ANIMES PELÍCULAS
CREATE PROCEDURE SP_ListarAnimePeliculas
(@IDCliente INT)
AS
BEGIN
    SELECT IDAnimePelicula, nombre, estado, observaciones, cliente FROM AnimePeliculas
	WHERE cliente = @IDCliente
END

--INSERTAR ANIMES PELÍCULAS
CREATE PROCEDURE SP_InsertarAnimePelicula
(@nombre VARCHAR(200),
@estado BIT,
@observaciones VARCHAR(100),
@IDCliente INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM AnimePeliculas WHERE nombre = @nombre)
	BEGIN
		INSERT INTO AnimePeliculas(nombre, estado, observaciones, cliente)
		VALUES (@nombre, @estado, @observaciones, @IDCliente)

		SET @resultado = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		SET @mensaje = 'La pelíucla de anime ya existe'
		PRINT('La pelíucla de anime  existe')
	END
END

--EDITAR ANIMES PELÍCULAS
CREATE PROCEDURE SP_EditarAnimePelicula
(@IDAnimePelicula INT,
@nombre VARCHAR(200),
@estado BIT,
@observaciones VARCHAR(100),
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET @resultado = 0

	IF NOT EXISTS (SELECT *FROM AnimePeliculas WHERE nombre = @nombre AND IDAnimePelicula != @IDAnimePelicula)
	BEGIN
		UPDATE TOP(1) AnimePeliculas SET
		nombre = @nombre,
		estado = @estado,
		observaciones = @observaciones
		WHERE IDAnimePelicula = @IDAnimePelicula

		SET @resultado = 1
	END
	ELSE
	BEGIN
		SET @mensaje = 'No se pudo actualizar la pelíucla de anime'
		PRINT('No se pudo actualizar la pelíucla de anime')
	END
END

--ELIMINAR ANIMES PELÍCULAS
CREATE PROCEDURE SP_EliminarAnimePelicula
(@IDAnimePelicula INT,
@resultado INT OUTPUT,
@mensaje VARCHAR(50) OUTPUT)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DELETE TOP (1) FROM AnimePeliculas WHERE IDAnimePelicula = @IDAnimePelicula;
        SET @resultado = 1
    END TRY
    BEGIN CATCH
        SET @mensaje = 'No se pudo eliminar la pelíucla de anime'
		PRINT('No se pudo eliminar la pelíucla de anime')
    END CATCH
END



EXEC SP_ListarClientes 1
EXEC SP_ListarPeliculas 1