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
	pcall(function() dofile(v) end)
	pcall(function() _G.CSScript_AddModule(Modules) end)
end

function PreInit(Script, Client)
	for i,v in pairs(Modules) do
		pcall(function() v:PreInit(Script, Client) end)
		pcall(function() print(v:Name() .. " called PreInit") end)
	end
end

function PostInit()
	for i,v in pairs(Modules) do
		pcall(function() v:PostInit() end)
		pcall(function() print(v:Name() .. " called PostInit") end)
	end
end

function OnLoadCharacter(Player, Appearance)
	for i,v in pairs(Modules) do
		pcall(function() v:OnLoadCharacter(Player, Appearance) end)
		pcall(function() print(v:Name() .. " called OnLoadCharacter") end)
	end
end

function OnPlayerAdded(Player)
	for i,v in pairs(Modules) do
		pcall(function() v:OnPlayerAdded(Player) end)
		pcall(function() print(v:Name() .. " called OnPlayerAdded") end)
	end
end

function OnPlayerRemoved(Player)
	for i,v in pairs(Modules) do
		pcall(function() v:OnPlayerRemoved(Player) end)
		pcall(function() print(v:Name() .. " called OnPlayerRemoved") end)
	end
end

function OnPlayerKicked(Player, Reason)
	for i,v in pairs(Modules) do
		pcall(function() v:OnPlayerKicked(Player, Reason) end)
		pcall(function() print(v:Name() .. " called OnPlayerKicked") end)
	end
end

function OnPrePlayerKicked(Player, Reason)
	for i,v in pairs(Modules) do
		pcall(function() v:OnPrePlayerKicked(Player, Reason) end)
		pcall(function() print(v:Name() .. " called OnPrePlayerKicked") end)
	end
end

_G.CSScript_PreInit=PreInit
_G.CSScript_PostInit=PostInit
_G.CSScript_OnLoadCharacter=OnLoadCharacter
_G.CSScript_OnPlayerAdded=OnPlayerAdded
_G.CSScript_OnPlayerRemoved=OnPlayerRemoved
_G.CSScript_OnPlayerKicked=OnPlayerKicked
_G.CSScript_OnPrePlayerKicked=OnPrePlayerKicked