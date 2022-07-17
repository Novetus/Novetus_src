--adds a StringValue that lists the client's version.

local this = {}

function this:Name()
	return "Client Name and Script Printer"
end

function this:PreInit(Script, Client)
	local ver = Instance.new("StringValue",game.Lighting)
	ver.Name = "Version"
	ver.Value = Client
	
	local scr = Instance.new("StringValue",game.Lighting)
	scr.Name = "ScriptLoaded"
	scr.Value = Script
end

function AddModule(t)
	print("AddonLoader: Adding " .. this:Name())
	table.insert(t, this)
end

_G.CSScript_AddModule=AddModule