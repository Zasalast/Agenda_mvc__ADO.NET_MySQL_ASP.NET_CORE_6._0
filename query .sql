Consultar eventos:
sql
 
SELECT a.IdAgenda, s.Nombre AS Servicio, sed.Direccion AS Sede, h.HoraInicio, h.HoraFin
FROM agendas a
JOIN servicios s ON a.IdServicio = s.IdServicio
JOIN sedes sed ON a.IdSede = sed.IdSede
JOIN horarios h ON a.IdHorario = h.IdHorario;
Insertar un evento (para administrador):
sql
 
-- Primero, asegúrate de tener los IDs correspondientes para servicio, sede y horario.
-- Reemplaza los valores entre corchetes [] con los valores correctos.
INSERT INTO agendas (IdSede, IdServicio, IdHorario)
VALUES ([ID_SEDE], [ID_SERVICIO], [ID_HORARIO]);
Eliminar un evento (para administrador):
sql
 
-- Reemplaza [ID_EVENTO] con el ID del evento que deseas eliminar.
DELETE FROM agendas
WHERE IdAgenda = [ID_EVENTO];
Actualizar un evento (para administrador):
sql
 
-- Reemplaza [ID_EVENTO], [ID_SEDE], [ID_SERVICIO] y [ID_HORARIO] con los valores correspondientes.
UPDATE agendas
SET IdSede = [ID_SEDE], IdServicio = [ID_SERVICIO], IdHorario = [ID_HORARIO]
WHERE IdAgenda = [ID_EVENTO];
Agendar un evento (para usuario):
sql
 
-- Reemplaza [ID_EVENTO] con el ID del evento al que deseas agendar.
-- Reemplaza [ID_CLIENTE], [FECHA] y [HORA] con los valores correspondientes.
INSERT INTO agendamientos (IdCliente, Fecha, Hora, Estado, IdAgenda)
VALUES ([ID_CLIENTE], [FECHA], [HORA], 'A', [ID_EVENTO]);