-- allows 2007M, 2006S, 2007E, and 2009E users to join Shaders/HD servers, and vice versa

local this = {}

function this:Name()
	return "Shader Client MP Compatibility"
end

function this:IsEnabled(Script, Client)
	-- hate this so much
	if (Client == "2007E" or Client == "2007M" or Client == "2006S" or Client == "2007E-Shaders" or Client == "2007M-Shaders" or Client == "2006S-Shaders" or Client == "2009E" or Client == "2009E-HD") then
		return true
	else
		return false
	end
end

function this:OnPrePlayerKicked(Player, Reason)
	if (game.Lighting:findFirstChild("SkipSecurity") ~= nil) then
		do return end
	end
	
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

	if (invalidSecurityVals > 0) then
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

function this:OnLoadCharacter(Player, Appearance)
	if (game.Lighting:findFirstChild("SkipSecurity") ~= nil) then
		if (game.Lighting.SkipSecurity:findFirstChild("Temp") ~= nil) then
			game.Lighting.SkipSecurity:remove()
		end
	end
end

function AddModule(t)
	print("AddonLoader: Adding " .. this:Name())
	table.insert(t, this)
end

_G.CSScript_AddModule=AddModule