-- put script names here

Addons = {"ClientNamePrinter", "ShadersCompatibility"}

-- DONT EDIT ANYTHING ELSE BELOW

Scripts = {}

function AddScript(name)
	table.insert(Scripts, name)
end

for i,v in pairs(Addons) do
	local fullname = "rbxasset://..//..//..//addons//".. v ..".lua"
	AddScript(fullname)
end

Modules = {}

for i,v in pairs(Scripts) do
	local success, response = pcall(function() dofile(v) end)
	
	if (not success) then
		print("AddonLoader: Failed to load script: " .. response)
	else
		_G.CSScript_AddModule(Modules)
	end
end

function PreInit(Script, Client)
	for i,v in pairs(Modules) do
		pcall(function() v:PreInit(Script, Client) end)
	end
end

function PostInit()
	for i,v in pairs(Modules) do
		pcall(function() v:PostInit() end)
	end
end

function OnLoadCharacter(Player, Appearance)
	for i,v in pairs(Modules) do
		pcall(function() v:OnLoadCharacter(Player, Appearance) end)
	end
end

function OnPlayerAdded(Player)
	for i,v in pairs(Modules) do
		pcall(function() v:OnPlayerAdded(Player) end)
	end
end

function OnPlayerRemoved(Player)
	for i,v in pairs(Modules) do
		pcall(function() v:OnPlayerRemoved(Player) end)
	end
end

function OnPlayerKicked(Player, Reason)
	for i,v in pairs(Modules) do
		pcall(function() v:OnPlayerKicked(Player, Reason) end)
	end
end

function OnPrePlayerKicked(Player, Reason)
	for i,v in pairs(Modules) do
		pcall(function() v:OnPrePlayerKicked(Player, Reason) end)
	end
end

_G.CSScript_PreInit=PreInit
_G.CSScript_PostInit=PostInit
_G.CSScript_OnLoadCharacter=OnLoadCharacter
_G.CSScript_OnPlayerAdded=OnPlayerAdded
_G.CSScript_OnPlayerRemoved=OnPlayerRemoved
_G.CSScript_OnPlayerKicked=OnPlayerKicked
_G.CSScript_OnPrePlayerKicked=OnPrePlayerKicked