
CREATE DATABASE IF NOT EXISTS `agenda_empresa3` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `agenda_empresa3`;

-- Volcando estructura para tabla agenda_empresa3.agendamientos
CREATE TABLE IF NOT EXISTS `agendamientos` (
  `IdAgendamiento` int NOT NULL AUTO_INCREMENT,
  `IdCliente` int DEFAULT NULL,
  `Fecha` date DEFAULT NULL,
  `Hora` time DEFAULT NULL,
  `Estado` varchar(1) DEFAULT NULL,
  `IdAgenda` int DEFAULT NULL,
  PRIMARY KEY (`IdAgendamiento`),
  KEY `Agendamientos_ibfk_1` (`IdCliente`),
  KEY `Agendamientos_ibfk_2` (`IdAgenda`),
  CONSTRAINT `Agendamientos_ibfk_1` FOREIGN KEY (`IdCliente`) REFERENCES `personas` (`IdPersona`),
  CONSTRAINT `Agendamientos_ibfk_2` FOREIGN KEY (`IdAgenda`) REFERENCES `agendas` (`IdAgenda`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla agenda_empresa3.agendas
CREATE TABLE IF NOT EXISTS `agendas` (
  `IdAgenda` int NOT NULL AUTO_INCREMENT,
  `IdSede` int DEFAULT NULL,
  `IdServicio` int DEFAULT NULL,
  `IdHorario` int DEFAULT NULL,
  `IdProfesional` int DEFAULT NULL,
  PRIMARY KEY (`IdAgenda`),
  KEY `Agendas_ibfk_1` (`IdSede`),
  KEY `Agendas_ibfk_2` (`IdServicio`),
  KEY `Agendas_ibfk_3` (`IdHorario`),
  KEY `Agendas_ibfk_4` (`IdProfesional`),
  CONSTRAINT `Agendas_ibfk_1` FOREIGN KEY (`IdSede`) REFERENCES `sedes` (`IdSede`),
  CONSTRAINT `Agendas_ibfk_2` FOREIGN KEY (`IdServicio`) REFERENCES `servicios` (`IdServicio`),
  CONSTRAINT `Agendas_ibfk_3` FOREIGN KEY (`IdHorario`) REFERENCES `horarios` (`IdHorario`),
  CONSTRAINT `Agendas_ibfk_4` FOREIGN KEY (`IdProfesional`) REFERENCES `personas` (`IdPersona`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla agenda_empresa3.asistencias
CREATE TABLE IF NOT EXISTS `asistencias` (
  `IdAsistencia` int NOT NULL AUTO_INCREMENT,
  `EstadoAsistencia` varchar(1) DEFAULT NULL,
  `IdAgendamiento` int DEFAULT NULL,
  PRIMARY KEY (`IdAsistencia`),
  KEY `Asistencias_ibfk_1` (`IdAgendamiento`),
  CONSTRAINT `Asistencias_ibfk_1` FOREIGN KEY (`IdAgendamiento`) REFERENCES `agendamientos` (`IdAgendamiento`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla agenda_empresa3.cancelaciones
CREATE TABLE IF NOT EXISTS `cancelaciones` (
  `IdCancelacion` int NOT NULL AUTO_INCREMENT,
  `FechaHora` datetime DEFAULT NULL,
  `Motivo` varchar(200) DEFAULT NULL,
  `IdAgendamiento` int DEFAULT NULL,
  PRIMARY KEY (`IdCancelacion`),
  KEY `Cancelaciones_ibfk_1` (`IdAgendamiento`),
  CONSTRAINT `Cancelaciones_ibfk_1` FOREIGN KEY (`IdAgendamiento`) REFERENCES `agendamientos` (`IdAgendamiento`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla agenda_empresa3.horarios
CREATE TABLE IF NOT EXISTS `horarios` (
  `IdHorario` int NOT NULL AUTO_INCREMENT,
  `HoraInicio` time DEFAULT NULL,
  `HoraFin` time DEFAULT NULL,
  PRIMARY KEY (`IdHorario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla agenda_empresa3.permisos
CREATE TABLE IF NOT EXISTS `permisos` (
  `IdPermiso` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IdPermiso`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla agenda_empresa3.personas
CREATE TABLE IF NOT EXISTS `personas` (
  `IdPersona` int NOT NULL AUTO_INCREMENT,
  `Nombres` varchar(50) DEFAULT NULL,
  `Apellidos` varchar(50) DEFAULT NULL,
  `Identificacion` varchar(15) DEFAULT NULL,
  `IdRol` int NOT NULL,
  PRIMARY KEY (`IdPersona`),
  KEY `Personas_ibfk_1` (`IdRol`),
  CONSTRAINT `Personas_ibfk_1` FOREIGN KEY (`IdRol`) REFERENCES `roles` (`IdRol`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla agenda_empresa3.roles
CREATE TABLE IF NOT EXISTS `roles` (
  `IdRol` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IdRol`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla agenda_empresa3.rolespermisos
CREATE TABLE IF NOT EXISTS `rolespermisos` (
  `IdRol` int NOT NULL,
  `IdPermiso` int NOT NULL,
  PRIMARY KEY (`IdRol`,`IdPermiso`),
  KEY `RolesPermisos_ibfk_2` (`IdPermiso`),
  CONSTRAINT `RolesPermisos_ibfk_1` FOREIGN KEY (`IdRol`) REFERENCES `roles` (`IdRol`),
  CONSTRAINT `RolesPermisos_ibfk_2` FOREIGN KEY (`IdPermiso`) REFERENCES `permisos` (`IdPermiso`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla agenda_empresa3.sedes
CREATE TABLE IF NOT EXISTS `sedes` (
  `IdSede` int NOT NULL AUTO_INCREMENT,
  `Direccion` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`IdSede`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla agenda_empresa3.servicios
CREATE TABLE IF NOT EXISTS `servicios` (
  `IdServicio` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`IdServicio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


DELIMITER //
CREATE PROCEDURE spCreateReservation
(
    IN cliente INT,
    IN fecha DATE,
    IN hora TIME
)
BEGIN
   INSERT INTO agendamientos (IdCliente, Fecha, Hora) 
   VALUES (cliente, fecha, hora);
   
   SELECT LAST_INSERT_ID(); 
END //
DELIMITER ;

Consultar eventos:

SELECT a.IdAgenda, s.Nombre AS Servicio, sed.Direccion AS Sede, h.HoraInicio, h.HoraFin
FROM agendas a
JOIN servicios s ON a.IdServicio = s.IdServicio
JOIN sedes sed ON a.IdSede = sed.IdSede
JOIN horarios h ON a.IdHorario = h.IdHorario;
Insertar un evento (para administrador):

-- Primero, asegúrate de tener los IDs correspondientes para servicio, sede y horario.
-- Reemplaza los valores entre corchetes [] con los valores correctos.
INSERT INTO agendas (IdSede, IdServicio, IdHorario)
VALUES ([ID_SEDE], [ID_SERVICIO], [ID_HORARIO]);
Eliminar un evento (para administrador):

-- Reemplaza [ID_EVENTO] con el ID del evento que deseas eliminar.
DELETE FROM agendas
WHERE IdAgenda = [ID_EVENTO];
Actualizar un evento (para administrador):

-- Reemplaza [ID_EVENTO], [ID_SEDE], [ID_SERVICIO] y [ID_HORARIO] con los valores correspondientes.
UPDATE agendas
SET IdSede = [ID_SEDE], IdServicio = [ID_SERVICIO], IdHorario = [ID_HORARIO]
WHERE IdAgenda = [ID_EVENTO];
Agendar un evento (para usuario):

-- Reemplaza [ID_EVENTO] con el ID del evento al que deseas agendar.
-- Reemplaza [ID_CLIENTE], [FECHA] y [HORA] con los valores correspondientes.
INSERT INTO agendamientos (IdCliente, Fecha, Hora, Estado, IdAgenda)
VALUES ([ID_CLIENTE], [FECHA], [HORA], 'A', [ID_EVENTO]);