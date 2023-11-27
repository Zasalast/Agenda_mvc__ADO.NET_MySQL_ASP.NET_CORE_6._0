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