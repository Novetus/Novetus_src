-- allows 2007M, 2006S, 2007E, and 2009E users to join Shaders/HD servers, and vice versa

local this = {}
local ClientName = "N/A"

function this:Name()
	return "Shader Client MP Compatibility"
end

-- executes before the game starts (server, solo, studio)
-- arguments: Script - returns the script type name (Server, Solo, Studio), Client - returns the Client name.
function this:PreInit(Script, Client)
	ClientName = Client
end

function IsShaderSupportingClient()
	-- hate this so much
	if (ClientName == "2007E" or ClientName == "2007M" or ClientName == "2006S" or ClientName == "2007E-Shaders" or ClientName == "2007M-Shaders" or ClientName == "2006S-Shaders" or ClientName == "2009E" or ClientName == "2009E-HD") then
		return true
	end
end

-- executes before a player gets kicked (server)
-- arguments: Player - the Player getting kicked, Reason - the reason the player got kicked
function this:OnPrePlayerKicked(Player, Reason)
	if (game.Lighting:findFirstChild("SkipSecurity") ~= nil) then
		do return end
	end
	
	if (IsShaderSupportingClient()) then
		invalidSecurityVals = 0
		
		for _,newVal in pairs(Player.Security:children()) do	
			if (newVal.Name == "ClientEXEMD5") then
				if (newVal.Value ~= game.Lighting.Security.ClientEXEMD5.Value or newVal.Value == "") then
					invalidSecurityVals = invalidSecurityVals + 1
				end
			end
					
			if (newVal.Name == "LauncherMD5") then
				if (newVal.Value ~= game.Lighting.Security.LauncherMD5.Value or newVal.Value == "") then
					invalidSecurityVals = invalidSecurityVals + 1
				end
			end
					
			if (newVal.Name == "ClientScriptMD5") then
				if (newVal.Value ~= game.Lighting.Security.ClientScriptMD5.Value or newVal.Value == "") then
					invalidSecurityVals = invalidSecurityVals + 1
				end
			end
		end
	
		if (invalidSecurityVals < 3) then
			print(Player.Name .. " has "..invalidSecurityVals.." invalid security values! Verifying...")
			validLauncher = false
			hasTripcode = false
			securityValues = 0

			for _,newVal in pairs(Player.Security:children()) do	
				if (newVal.Name == "LauncherMD5") then
					if (newVal.Value == game.Lighting.Security.LauncherMD5.Value) then
						validLauncher = true
					end
				end
				
				securityValues = securityValues + 1
			end
			
			for _,newVal in pairs(Player:children()) do
				if (newVal.Name == "Tripcode") then
					if (newVal.Value ~= "") then
						hasTripcode = true
					end
				end
			end
			
			if (validLauncher == true and hasTripcode == true and securityValues == 3) then
				print(Player.Name .. " is using a valid modified client!")
				local ver = Instance.new("StringValue",game.Lighting)
				ver.Name = "SkipSecurity"
				local tempTag = Instance.new("StringValue",ver)
				tempTag.Name = "Temp"
			else
				print(Player.Name .. " is using an invalid modified client! Kicking...")
			end
		else
			print(Player.Name .. " is using an invalid modified client! Kicking...")
		end
	end
end

-- executes after a character loads (server, solo, studio)
-- arguments: Player - Player getting a character loaded, Appearance - The object containing the appearance values 
-- notes: in play solo, you may have to respawn once to see any print outputs.
function this:OnLoadCharacter(Player, Appearance)
	if (IsShaderSupportingClient()) then
		if (game.Lighting:findFirstChild("SkipSecurity") ~= nil) then
			if (game.Lighting.SkipSecurity:findFirstChild("Temp") ~= nil) then
				game.Lighting.SkipSecurity:remove()
			end
		end
	end
end

function AddModule(t)
	print("AddonLoader: Adding " .. this:Name())
	table.insert(t, this)
end

_G.CSScript_AddModule=AddModule