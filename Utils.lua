local this = {}

function this:Name()
	return "Novetus Utilities (Client Name Printer and Script Name Printer)"
end

function this:IsEnabled(Script, Client)
	if (Script ~= "Studio") then
		return true
	else
		return false
	end
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