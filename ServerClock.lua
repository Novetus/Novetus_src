local this = {}
local ScriptName = "N/A"

function this:Name()
	return "Server Clock (Time Since Started)"
end

function this:PreInit(Script, Client)
	ScriptName = Script
end

function this:PostInit()
	if (ScriptName == "Server") then
		local ver = Instance.new("IntValue",game.Lighting)
		ver.Name = "ServerTicks"
		ver.Value = 0
	end
end

-- executes every 0.1 seconds. (server, solo, studio)
-- arguments: none
function this:Update()
	-- we already wait 0.1 seconds
	if (ScriptName == "Server") then
		game.Lighting.ServerTicks.Value = game.Lighting.ServerTicks.Value + 1
	end
end

-- DO NOT REMOVE THIS. this is required to load this addon into the game.

function AddModule(t)
	print("AddonLoader: Adding " .. this:Name())
	table.insert(t, this)
end

_G.CSScript_AddModule=AddModule