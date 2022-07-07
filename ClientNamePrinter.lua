--adds a StringValue that lists the client's version.

local this = {}

function this:Name()
	return "Client Name Printer"
end

function this:PreInit(Script, Client)
	local ver = Instance.new("StringValue",game.Lighting)
	ver.Name = "Version"
	ver.Value = Client
end

function AddModule(t)
	table.insert(t, this)
end

_G.CSScript_AddModule=AddModule