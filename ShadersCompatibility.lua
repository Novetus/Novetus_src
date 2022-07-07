-- allows 2007M, 2006S, and 2007E users to join Shaders servers, and vice versa

local this = {}
local ClientName = "N/A"

function this:Name()
	return "Shaders MP Compatibility"
end

-- executes before the game starts (server, solo, studio)
-- arguments: Script - returns the script type name (Server, Solo, Studio), Client - returns the Client name.
function this:PreInit(Script, Client)
	ClientName = Client
end

function IsShaderSupportingClient()
	-- hate this so much
	if (ClientName == "2007E" or ClientName == "2007M" or ClientName == "2006S" or ClientName == "2007E-Shaders" or ClientName == "2007M-Shaders" or ClientName == "2006S-Shaders") then
		return true
	end
end

-- executes before a player gets kicked (server)
-- arguments: Player - the Player getting kicked, Reason - the reason the player got kicked
function this:OnPrePlayerKicked(Player, Reason)
	if (game.Lighting:FindFirstChild("SkipSecurity") ~= nil) then
		do return end
	end
	
	if (IsShaderSupportingClient()) then
		validLauncher = false

		for _,newVal in pairs(Player.Security:GetChildren()) do	
			if (newVal.Name == "LauncherMD5") then
				if (newVal.Value == game.Lighting.Security.LauncherMD5.Value or newVal.Value == "") then
					validLauncher = true
				end
			end
		end
		
		if (validLauncher == true) then
			print(Player.Name .. " is using a valid modified client!")
			local ver = Instance.new("StringValue",game.Lighting)
			ver.Name = "SkipSecurity"
			ver.Value = "temp"
		end
	end
end

-- executes after a character loads (server, solo, studio)
-- arguments: Player - Player getting a character loaded, Appearance - The object containing the appearance values 
-- notes: in play solo, you may have to respawn once to see any print outputs.
function this:OnLoadCharacter(Player, Appearance)
	if (game.Lighting.SkipSecurity.Value == "temp" then
		game.Lighting.SkipSecurity:remove()
	end
end

-- DO NOT MODIFY THIS. this is required to load this addon into the game.

function AddModule(t)
	table.insert(t, this)
end

_G.CSScript_AddModule=AddModule