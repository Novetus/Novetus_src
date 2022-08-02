local this = {}

--https://www.tutorialspoint.com/how-to-search-for-an-item-in-a-lua-list
function Set(list)
	local set = {}
	for _, l in ipairs(list) do set[l] = true end
	return set
end

-- DONT EDIT ANYTHING ELSE ABOVE

-- add player tripcodes here in quotes sepertaed by commas
local tripcodeList = Set {}
-- enable this to trigger the whitelist
local enabled = false

-- DONT EDIT ANYTHING ELSE BELOW

function this:Name()
	return "Server Whitelist"
end

-- executes before the game starts (server, solo, studio)
-- arguments: Script - returns the script type name (Server, Solo, Studio), Client - returns the Client name.
function this:PreInit(Script, Client)
	if (Script ~= "Server") then
		enabled = false
	end
end

-- executes after a player joins (server)
-- arguments: Player - the Player joining
function this:OnPlayerAdded(Player)
	if (enabled == true) then
		local hasTripcode = false

		for _,newVal in pairs(Player:children()) do
			if (newVal.Name == "Tripcode") then
				if (newVal.Value ~= "") then
					hasTripcode = true
				end
			end
		end

		if (hasTripcode and tripcodeList[Player.Tripcode.Value]) then
			print("Player '" .. Player.Name .. "' is in whitelist!")
		else
			print("Player '" .. Player.Name .. "' not in whitelist. Kicking.")

			Server = game:GetService("NetworkServer")
			for _,Child in pairs(Server:children()) do
				name = "ServerReplicator|"..Player.Name.."|"..Player.userId.."|"..Player.AnonymousIdentifier.Value
				if (Server:findFirstChild(name) ~= nil and Child.Name == name) then
					Child:CloseConnection()
					print("Player '" .. Player.Name .. "' Kicked. Reason: Not in whitelist")
				end
			end
		end
	end
end

-- DO NOT REMOVE THIS. this is required to load this addon into the game.

function AddModule(t)
	print("AddonLoader: Adding " .. this:Name())
	table.insert(t, this)
end

_G.CSScript_AddModule=AddModule