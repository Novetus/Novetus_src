<?php
//NOVETUS MASTER SERVER QUERY CODE

//server name
$name = $_GET["name"];
//server ip
$ip = $_GET["ip"];
//server port
$port = $_GET["port"];
//client name
$client = $_GET["client"];
//players
$players = $_GET["players"];
//maxplayers
$maxplayers = $_GET["maxplayers"];
//online status
$online = $_GET["online"];

//strings
$deleteentry = 1;
$status = "Offline";

//ONLY the $name and $client arguments will show up in the master server!
$file = 'serverlist.txt';
$text = "$name|$ip|$port|$client";

if ($online == 1)
{
	$deleteentry = 0;
	
	foreach(file($file) as $line) 
	{
		if (strpos($line, $text) !== false)
		{
			$file_contents = file_get_contents($file);
			$contents = str_replace($line, '', $file_contents);
			file_put_contents($file, $contents);
		}
	}
	
	$fulltext = $text."|$players|$maxplayers\r\n";
	file_put_contents($file, $fulltext, FILE_APPEND);
	
	$status = "Online";
}

if ($deleteentry == 1)
{
	foreach(file($file) as $line) 
	{
		if (strpos($line, $text) !== false)
		{
			$file_contents = file_get_contents($file);
			$contents = str_replace($line, '', $file_contents);
			file_put_contents($file, $contents);
		}
	}
}

// Display the server info to browsers.
echo "$name.<br>A $client server.<br>Server Status: $status";
?>