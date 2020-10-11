<?php
//NOVETUS MASTER SERVER QUERY CODE
//maybe convert this shit to c# and implement it on the master server...

//so we can save the server list in shareddata so the server can load it.
chdir('shareddata');

//server name
$name = $argv[1];
//server ip
$ip = $argv[2];
//server port
$port = $argv[3];
//client name
$client = $argv[4];
//online status
$online = $argv[5];
//maxplayers
$players = $argv[6];
//maxplayers
$maxplayers = $argv[7];

//strings
$deleteentry = 1;
$status = "Offline";

//ONLY the $name and $client arguments will show up in the master server!
$file = 'serverlist.txt';
$text = "$name|$ip|$port|$client|$players|$maxplayers\r\n";

if ($online == 1)
{
	$deleteentry = 0;
	$file_contents = file_get_contents($file);
	if (strpos($file_contents, $text) === false)
	{
		file_put_contents($file, $text, FILE_APPEND);
	}
	$status = "Online";
}

if ($deleteentry == 1)
{
	$file_contents = file_get_contents($file);
	$contents = str_replace($text, '', $file_contents);
	file_put_contents($file, $contents);
}

// Display the server info to browsers.
echo "$name.<br>A $client server.<br>Server Status: $status<br>Players: $players/$maxplayers";
?>