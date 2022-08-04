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

//id
$id = $_GET["id"];

if (!empty($id))
{
	$file = 'serverlist.txt';

	foreach(file($file) as $line) 
	{
		if (strpos($line, $id) !== false)
		{
			$file_contents = file_get_contents($file);
			$contents = str_replace($line, '', $file_contents);
			file_put_contents($file, $contents);
		}
	}
}
?>