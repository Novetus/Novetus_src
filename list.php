<?php
/*
Novetus' Master Server is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Novetus' Master Server is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Novetus' Master Server.  If not, see <https://www.gnu.org/licenses/>.

Read it here https://github.com/Novetus/Novetus_src/blob/master/LICENSE-MASTER-SERVER or in LICENSE-MASTER-SERVER.txt.
*/

//NOVETUS MASTER SERVER QUERY CODE
//thanks to idkwhatnametoget for the port fix

//name
$name = $_GET["name"];
//port
$port = $_GET["port"];
//client
$client = $_GET["client"];
//version
$version = $_GET["version"];
//id
$id = $_GET["id"];

if (!empty($port) and $port < 65535 and is_numeric($port) and !empty($name) and !empty($client) and !empty($version) and !empty($id))
{
	//server ip
	$ip = $_GET["ip"];
	
	$file = 'serverlist.txt';

	//ONLY the $name and $client arguments will show up in the master server!
	$text = $id.'|'.base64_encode(base64_encode($name).'|'.base64_encode($ip).'|'.base64_encode($port).'|'.base64_encode($client).'|'.base64_encode($version))."\r\n";

	file_put_contents($file, $text, FILE_APPEND);
}
?>