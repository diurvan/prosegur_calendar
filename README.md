# prosegur_calendar
Calendar project for Prosegur.

Solución CRUD que utiliza la librería fullCalendar (JavaScript) para registrar y mostrar eventos desde una Base de Datos SqlServer

<h2>PASOS PARA LA INSTALACIÓN</h2>

<table>
<thead>
<tr>
<th>Paso</th>
<th>Descripción</th>
</tr>
</thead>
<tbody>
<tr>
<td>1</td>
  <td align="left">Abrir el archivo <b>prosegur_sql.sql</b> y ejecutarlo en SqlServer (2014 en adelante)</td>
</tr>
<tr>
<td>2</td>
  <td align="center">Para depurar, abrir la solución <b>\prosegur_calendar\prosegur_calendar.sln</b> dentro de la carpeta app</td>
</tr>
  <tr>
<td>3</td>
    <td align="left">Cambiar la cadena de conexión en el archivo \prosegur_calendar\appsettings.json</td>
</tr>
<tr>
<td>4</td>
<td align="left">Ctrl+F5</td>
</tr>
<tr>
  <tr>
<td>2</td>
  <td align="center">Para integrar en Docker, ejecutar el copiado de archivos vía makefile, build y seguidamente el release (para la compilación y generación de compilados)</td>
</tr>
</tbody>
</table>
