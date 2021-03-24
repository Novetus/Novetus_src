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
//online status
$online = $_GET["online"];

//strings
$deleteentry = 1;
$status = "Offline";

//ONLY the $name and $client arguments will show up in the master server!
$file = 'serverlist.txt';
$text = base64_encode(base64_encode($name).'|'.base64_encode($ip).'|'.base64_encode($port).'|'.base64_encode($client))."\r\n";

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
	
	file_put_contents($file, $text, FILE_APPEND);
	
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