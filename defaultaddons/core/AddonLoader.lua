-- put script names here

Addons = {"Utils", "ServerWhitelist", "URLSetup"}

-- DONT EDIT ANYTHING ELSE BELOW

CoreScriptName = "AddonLoader"
ParentClient = "2009E"
ParentFunctionScript = "Server"

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
	local success, response = pcall(function() dofile(v) end)
	
	if (not success) then
		print("AddonLoader: Failed to load script: " .. response)
	else
		local success2, response2 = pcall(function() _G.CSScript_AddModule(Modules) end)
		if (not success2) then
			print("AddonLoader: Failed to add script module: " .. response2)
		end
	end
end

function PreInit(Script, Client)
	ParentClient = Client
	ParentFunctionScript = Script

	for i,v in pairs(Modules) do
		local enabled = true
		local s, r = pcall(function() enabled = v:IsEnabled(Script, Client) end)

		if (enabled) then
			local success, response = pcall(function() v:PreInit(Script, Client) end)
			if (not success and not string.find(response, CoreScriptName)) then
				print("AddonLoader: Failed to call PreInit: " .. response)
			end
		end
	end
end

function PostInit()
	for i,v in pairs(Modules) do
		local enabled = true
		local s, r = pcall(function() enabled = v:IsEnabled(ParentFunctionScript, ParentClient) end)

		if (enabled) then
			local success, response = pcall(function() v:PostInit() end)
			if (not success and not string.find(response, CoreScriptName)) then
				print("AddonLoader: Failed to call PostInit: " .. response)
			end
		end
	end
end

function Update()
	for i,v in pairs(Modules) do
		local enabled = true
		local s, r = pcall(function() enabled = v:IsEnabled(ParentFunctionScript, ParentClient) end)

		if (enabled) then
			local success, response = pcall(function() v:Update() end)
			if (not success and not string.find(response, CoreScriptName)) then
				print("AddonLoader: Failed to call Update: " .. response)
			end
		end
	end
end

function OnLoadCharacter(Player, Appearance)
	for i,v in pairs(Modules) do
		local enabled = true
		local s, r = pcall(function() enabled = v:IsEnabled(ParentFunctionScript, ParentClient) end)

		if (enabled) then
			local success, response = pcall(function() v:OnLoadCharacter(Player, Appearance) end)
			if (not success and not string.find(response, CoreScriptName)) then
				print("AddonLoader: Failed to call OnLoadCharacter: " .. response)
			end
		end
	end
end

function OnPlayerAdded(Player)
	for i,v in pairs(Modules) do
		local enabled = true
		local s, r = pcall(function() enabled = v:IsEnabled(ParentFunctionScript, ParentClient) end)

		if (enabled) then
			local success, response = pcall(function() v:OnPlayerAdded(Player) end)
			if (not success and not string.find(response, CoreScriptName)) then
				print("AddonLoader: Failed to call OnPlayerAdded: " .. response)
			end
		end
	end
end

function OnPlayerRemoved(Player)
	for i,v in pairs(Modules) do
		local enabled = true
		local s, r = pcall(function() enabled = v:IsEnabled(ParentFunctionScript, ParentClient) end)

		if (enabled) then
			local success, response = pcall(function() v:OnPlayerRemoved(Player) end)
			if (not success and not string.find(response, CoreScriptName)) then
				print("AddonLoader: Failed to call OnPlayerRemoved: " .. response)
			end
		end
	end
end

function OnPlayerKicked(Player, Reason)
	for i,v in pairs(Modules) do
		local enabled = true
		local s, r = pcall(function() enabled = v:IsEnabled(ParentFunctionScript, ParentClient) end)

		if (enabled) then
			local success, response = pcall(function() v:OnPlayerKicked(Player, Reason) end)
			if (not success and not string.find(response, CoreScriptName)) then
				print("AddonLoader: Failed to call OnPlayerKicked: " .. response)
			end
		end
	end
end

function OnPrePlayerKicked(Player, Reason)
	for i,v in pairs(Modules) do
		local enabled = true
		local s, r = pcall(function() enabled = v:IsEnabled(ParentFunctionScript, ParentClient) end)

		if (enabled) then
			local success, response = pcall(function() v:OnPrePlayerKicked(Player, Reason) end)
			if (not success and not string.find(response, CoreScriptName)) then
				print("AddonLoader: Failed to call OnPrePlayerKicked: " .. response)
			end
		end
	end
end

_G.CSScript_PreInit=PreInit
_G.CSScript_PostInit=PostInit
_G.CSScript_Update=Update
_G.CSScript_OnLoadCharacter=OnLoadCharacter
_G.CSScript_OnPlayerAdded=OnPlayerAdded
_G.CSScript_OnPlayerRemoved=OnPlayerRemoved
_G.CSScript_OnPlayerKicked=OnPlayerKicked
_G.CSScript_OnPrePlayerKicked=OnPrePlayerKicked
