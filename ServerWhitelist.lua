local this = {}

--https://www.tutorialspoint.com/how-to-search-for-an-item-in-a-lua-list
function Set(list)
	local set = {}
	for _, l in ipairs(list) do set[l] = true end
	return set
end

-- DONT EDIT ANYTHING ELSE ABOVE

-- add player tripcodes here in quotes seperated by commas
local tripcodeList = Set {}
-- set this to true to enable the whitelist
local whitelistEnabled = false

-- DONT EDIT ANYTHING ELSE BELOW

function this:Name()
	return "Server Whitelist"
end

function this:IsEnabled(Script, Client)
	if (Script == "Server") then
		return true
	else
		return false
	end
end

function this:OnPlayerAdded(Player)
	if (whitelistEnabled == true) then
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
					--delayed to fix 2011 client crash
					delay(0.3, function() Child:CloseConnection() print("Player '" .. Player.Name .. "' Kicked. Reason: Not in whitelist") end)
				end
			end
		end
	end
end

function AddModule(t)
	print("AddonLoader: Adding " .. this:Name())
	table.insert(t, this)
end

_G.CSScript_AddModule=AddModule