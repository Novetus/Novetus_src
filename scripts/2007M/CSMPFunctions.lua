--function made by rbxbanland
function newWaitForChild(newParent,name)
	local returnable = nil
	if newParent:FindFirstChild(name) then
		returnable = newParent:FindFirstChild(name)
	else 
		repeat wait() returnable = newParent:FindFirstChild(name)  until returnable ~= nil
	end
	return returnable
end

function KickPlayer(Player,reason)
	if (Player ~= nil) then
		local message = Instance.new("Message")
		message.Text = "You were kicked. Reason: "..reason
		message.Parent = Player
		wait(2)
		Player:remove()
		print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' kicked. Reason: "..reason)
	end
end

function newWaitForChildSecurity(newParent,name)
	local returnable = nil
	local loadAttempts = 0
	local maxAttempts = 5
	while loadAttempts < maxAttempts do
		if newParent:FindFirstChild(name) then
			returnable = newParent:FindFirstChild(name)
			break
		end
		wait()
		loadAttempts = loadAttempts + 1
		print("Player '" .. newParent.Name .. "' trying to connect. Number of attempts: "..loadAttempts)
	end
		
	if (loadAttempts == maxAttempts) then
		KickPlayer(newParent, "Modified Client")
	end
		
	return returnable
end

function LoadCharacterNew(playerApp,newChar)
	PlayerService = game:GetService("Players")
	Player = PlayerService:GetPlayerFromCharacter(newChar)
	
	local function kick()
		KickPlayer(Player, "Modified Client")
	end
	
	if (playerApp == nil) then
		kick()
	end
	
	if (not Player:FindFirstChild("Appearance")) then
		kick()
	end
	
	if ((playerApp:GetChildren() == 0) or (playerApp:GetChildren() == nil)) then
		kick()
	end
	
	local path = "rbxasset://../../../shareddata/charcustom/"
	
	local charparts = {[1] = newWaitForChild(newChar,"Head"),[2] = newWaitForChild(newChar,"Torso"),[3] = newWaitForChild(newChar,"Left Arm"),[4] = newWaitForChild(newChar,"Right Arm"),[5] = newWaitForChild(newChar,"Left Leg"),[6] = newWaitForChild(newChar,"Right Leg")}
	for _,newVal in pairs(playerApp:GetChildren()) do
		if (newVal.Name == "Body Color") then 
			pcall(function() 
				charparts[newVal.ColorIndex.Value].BrickColor = newVal.Value 
			end)
		elseif (newVal.Name == "Hat")  then
			pcall(function()
				local newHat = game.Workspace:InsertContent(path.."hats/"..newVal.Value)
				if newHat[1] then 
					if newHat[1].className == "Hat" then
						newHat[1].Parent = newChar
					else
						newHat[1]:remove()
					end
				end
			end)
		elseif (newVal.Name == "T-Shirt")  then
			pcall(function()
				local newTShirt = "";
				if (string.match(newVal.Value, "finobe") == "finobe") then
					newTShirt = Instance.new("ShirtGraphic")
					newTShirt.Graphic = newVal.Value
					newTShirt.Parent = newChar
				elseif (string.match(newVal.Value, "epicgamers") == "epicgamers") then
					newTShirt = Instance.new("ShirtGraphic")
					newTShirt.Graphic = newVal.Value
					newTShirt.Parent = newChar
				else
					newTShirt = game.Workspace:InsertContent(path.."tshirts/"..newVal.Value)
					if newTShirt[1] then 
						if newTShirt[1].className == "ShirtGraphic" then
							newTShirt[1].Parent = newChar
						else
							newTShirt[1]:remove()
						end
					end
				end
			end)
		elseif (newVal.Name == "Extra")  then
			pcall(function()
				local newHat = game.Workspace:InsertContent(path.."hats/"..newVal.Value)
				if newHat[1] then 
					if newHat[1].className == "Hat" then
						newHat[1].Parent = newChar
					else
						newHat[1]:remove()
					end
				end
			end)
			
			pcall(function()
				local newItem = game.Workspace:InsertContent(path.."custom/"..newVal.Value)
				if newItem[1] then 
					if newItem[1].className == "Decal" then
						newWaitForChild(charparts[1],"face"):remove()
						newItem[1].Parent = charparts[1]
						newItem[1].Face = "Front"
					elseif newPart[1].className == "SpecialMesh" or newPart[1].className == "CylinderMesh" or newPart[1].className == "BlockMesh" then
						newWaitForChild(charparts[1],"Mesh"):remove()
						newItem[1].Parent = charparts[1]
					else
						newItem[1].Parent = newChar
					end
				end
			end)
		end
	end
end

function InitalizeClientAppearance(Player,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID,ItemID)
	local newCharApp = Instance.new("IntValue",Player)
	newCharApp.Name = "Appearance"
	--BODY COLORS
	for i=1,6,1 do
		local BodyColor = Instance.new("BrickColorValue",newCharApp)
		if (i == 1) then
			if (HeadColorID ~= nil) then
				BodyColor.Value = BrickColor.new(HeadColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
		elseif (i == 2) then
			if (TorsoColorID ~= nil) then
				BodyColor.Value = BrickColor.new(TorsoColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
		elseif (i == 3) then
			if (LeftArmColorID ~= nil) then
				BodyColor.Value = BrickColor.new(LeftArmColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
		elseif (i == 4) then
			if (RightArmColorID ~= nil) then
				BodyColor.Value = BrickColor.new(RightArmColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
		elseif (i == 5) then
			if (LeftLegColorID ~= nil) then
				BodyColor.Value = BrickColor.new(LeftLegColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
		elseif (i == 6) then
			if (RightLegColorID ~= nil) then
				BodyColor.Value = BrickColor.new(RightLegColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
		end
		BodyColor.Name = "Body Color"
		local indexValue = Instance.new("NumberValue")
		indexValue.Name = "ColorIndex"
		indexValue.Parent = BodyColor
		indexValue.Value = i
	end
	--HATS
	for i=1,3,1 do
		local newHat = Instance.new("StringValue",newCharApp)
		if (i == 1) then
			if (Hat1ID ~= nil) then
				newHat.Value = Hat1ID
			else
				newHat.Value = "NoHat.rbxm"
			end
		elseif (i == 2) then
			if (Hat2ID ~= nil) then
				newHat.Value = Hat2ID
			else
				newHat.Value = "NoHat.rbxm"
			end
		elseif (i == 3) then
			if (Hat3ID ~= nil) then
				newHat.Value = Hat3ID
			else
				newHat.Value = "NoHat.rbxm"
			end
		end
		newHat.Name = "Hat"
	end
	--T-SHIRT
	local newTShirt = Instance.new("StringValue",newCharApp)
	if (TShirtID ~= nil) then
		newTShirt.Value = TShirtID
	else
		newTShirt.Value = "NoTShirt.rbxm"
	end
	newTShirt.Name = "T-Shirt"
	--EXTRA
	local newItem = Instance.new("StringValue",newCharApp)
	if (ItemID ~= nil) then
		newItem.Value = ItemID
	else
		newItem.Value = "NoExtra.rbxm"
	end
	newItem.Name = "Extra"
end

function LoadSecurity(playerApp,Player,ServerSecurityLocation)
	local function kick()
		KickPlayer(Player, "Modified Client")
	end
	
	if (playerApp == nil) then
		kick()
	end
	
	if (not Player:FindFirstChild("Security")) then
		kick()
	end
	
	if (not playerApp:FindFirstChild("ClientEXEMD5") or not playerApp:FindFirstChild("LauncherMD5") or not playerApp:FindFirstChild("ClientScriptMD5")) then
		kick()
	end
	
	for _,newVal in pairs(playerApp:GetChildren()) do
		if (newVal.Name == "ClientEXEMD5") then
			if (newVal.Value ~= ServerSecurityLocation.Security.ClientEXEMD5.Value or newVal.Value == "") then
				kick()
				break
			end
		end
				
		if (newVal.Name == "LauncherMD5") then
			if (newVal.Value ~= ServerSecurityLocation.Security.LauncherMD5.Value or newVal.Value == "") then
				kick()
				break
			end
		end
				
		if (newVal.Name == "ClientScriptMD5") then
			if (newVal.Value ~= ServerSecurityLocation.Security.ClientScriptMD5.Value or newVal.Value == "") then
				kick()
				break
			end
		end
	end
end

function InitalizeSecurityValues(Location,ClientEXEMD5,LauncherMD5,ClientScriptMD5)
	Location = Instance.new("IntValue", Location)
	Location.Name = "Security"
	
	local clientValue = Instance.new("StringValue", Location)
	clientValue.Value = ClientEXEMD5 or ""
	clientValue.Name = "ClientEXEMD5"

	local launcherValue = Instance.new("StringValue", Location)
	launcherValue.Value = LauncherMD5 or ""
	launcherValue.Name = "LauncherMD5"

	local scriptValue = Instance.new("StringValue", Location)
	scriptValue.Value = ClientScriptMD5 or ""
	scriptValue.Name = "ClientScriptMD5"
end

function InitalizeTripcode(Location,Tripcode)
	local code = Instance.new("StringValue", Location)
	code.Value = Tripcode or ""
	code.Name = "Tripcode"
end

function LoadTripcode(Player)
	local function kick()
		KickPlayer(Player, "Modified Client")
	end
	
	if (not Player:FindFirstChild("Tripcode")) then
		kick()
	end
	
	for _,newVal in pairs(Player:GetChildren()) do
		if (newVal.Name == "Tripcode") then
			if (newVal.Value == "") then
				kick()
				break
			end
		end
	end
end

function InitalizeClientName(Location)
	local newName = Instance.new("StringValue",Location)
	newName.Value = "2007M"
	newName.Name = "Name"
end

print("ROBLOX Client version '0.3.512.0' loaded.")

function CSServer(Port,PlayerLimit,ClientEXEMD5,LauncherMD5,ClientScriptMD5)
	Server = game:GetService("NetworkServer")
	RunService = game:GetService("RunService")
	PlayerService = game:GetService("Players")
	Server:start(Port, 20)
	RunService:run()
	game.Workspace:InsertContent("rbxasset://Fonts//libraries.rbxm")
	PlayerService.MaxPlayers = PlayerLimit
	PlayerService.PlayerAdded:connect(function(Player)
		Player.Chatted:connect(function(msg)
			print(Player.Name.."; "..msg)
		end)
		
		if (PlayerService.NumPlayers > PlayerService.MaxPlayers) then
			KickPlayer(Player, "Too many players on server.")
		else
			print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' added")
			Player:LoadCharacter()
			LoadSecurity(newWaitForChildSecurity(Player,"Security"),Player,game.Lighting)
			newWaitForChildSecurity(Player,"Tripcode")
			LoadTripcode(Player)
			pcall(function() print("Player '" .. Player.Name .. "-" .. Player.userId .. "' security check success. Tripcode: '" .. Player.Tripcode.Value .. "'") end)
			if (Player.Character ~= nil) then
				LoadCharacterNew(newWaitForChildSecurity(Player,"Appearance"),Player.Character)
			end
		end
		
		-- rename all Server replicators in NetworkServer to "ServerReplicator"
		for _,Child in pairs(Server:GetChildren()) do
			Child.Name = "ServerReplicator"
		end
		
		coroutine.resume(coroutine.create(function()
			while Player ~= nil do
				wait(0.1)
				if (Player.Character ~= nil) then
					if (Player.Character.Humanoid.Health == 0) then
						wait(5)
						Player:LoadCharacter()
						LoadCharacterNew(newWaitForChildSecurity(Player,"Appearance"),Player.Character)
					elseif (Player.Character.Parent == nil) then 
						wait(5)
						Player:LoadCharacter()
						LoadCharacterNew(newWaitForChildSecurity(Player,"Appearance"),Player.Character)
					end
				end
			end
		end))
	end)
	PlayerService.PlayerRemoving:connect(function(Player)
		print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' leaving")	
	end)
	pcall(function() game.Close:connect(function() Server:Stop() end) end)
	InitalizeSecurityValues(game.Lighting,ClientEXEMD5,LauncherMD5,ClientScriptMD5)
	InitalizeClientName(game.Lighting)
	Server.IncommingConnection:connect(IncommingConnection)
end

function CSConnect(UserID,ServerIP,ServerPort,PlayerName,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID,IconType,ItemID,ClientEXEMD5,LauncherMD5,ClientScriptMD5,Tripcode,Ticket)
	local suc, err = pcall(function()
		client = game:GetService("NetworkClient")
		player = game:GetService("Players"):CreateLocalPlayer(UserID)
		InitalizeSecurityValues(player,ClientEXEMD5,LauncherMD5,ClientScriptMD5)
		InitalizeTripcode(player,Tripcode)
		player:SetSuperSafeChat(false)
		pcall(function() player:SetUnder13(false) end)
		pcall(function() player:SetAccountAge(365) end)
		pcall(function() player.Name=PlayerName or "" end)
		game:GetService("Visit")
		InitalizeClientAppearance(player,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID,ItemID)		
	end)

	local function dieerror(errmsg)
		game:SetMessage(errmsg)
		wait(math.huge)
	end

	if not suc then
		dieerror(err)
	end

	local function disconnect(peer,lostconnection)
		game:SetMessage("You have lost connection to the game")
	end

	local function connected(url, replicator)
		replicator.Disconnection:connect(disconnect)
		local marker = nil
		local suc, err = pcall(function()
			game:SetMessageBrickCount()
			marker = replicator:SendMarker()
		end)
		if not suc then
			dieerror(err)
		end
		marker.Received:connect(function()
			local suc, err = pcall(function()
				game:ClearMessage()
			end)
			if not suc then
				dieerror(err)
			end
		end)
	end

	local function rejected()
		dieerror("Failed to connect to the Game. (Connection rejected)")
	end

	local function failed(peer, errcode, why)
		dieerror("Failed to connect to the Game. (ID="..errcode.." ["..why.."])")
	end

	local suc, err = pcall(function()
		game:SetMessage("Connecting to server...")
		client.ConnectionAccepted:connect(connected)
		client.ConnectionRejected:connect(rejected)
		client.ConnectionFailed:connect(failed)
		client:Connect(ServerIP,ServerPort, 0, 20)
		game.GuiRoot.MainMenu["Toolbox"]:Remove()
		game.GuiRoot.MainMenu["Edit Mode"]:Remove()
	end)

	if not suc then
		local x = Instance.new("Message")
		x.Text = err
		x.Parent = workspace
		wait(math.huge)
	end
end

function CSSolo(UserID,PlayerName,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID,IconType,ItemID)
	local plr = game.Players:CreateLocalPlayer(UserID)
	game:GetService("RunService"):run()
	game.Workspace:InsertContent("rbxasset://Fonts//libraries.rbxm")
	plr.Name = PlayerName
	plr:LoadCharacter()
	InitalizeClientAppearance(plr,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID,ItemID)
	LoadCharacterNew(newWaitForChild(plr,"Appearance"),plr.Character,false)
	game:GetService("Visit")
	while true do 
		wait(0.001)
		if (plr.Character ~= nil) then
			if (plr.Character.Humanoid.Health == 0) then
				wait(5)
				plr:LoadCharacter()
				LoadCharacterNew(newWaitForChild(plr,"Appearance"),plr.Character)
			elseif (plr.Character.Parent == nil) then 
				wait(5)
				plr:LoadCharacter() -- to make sure nobody is deleted.
				LoadCharacterNew(newWaitForChild(plr,"Appearance"),plr.Character)
			end
		end
	end
end

function CSStudio()
end

_G.CSServer=CSServer
_G.CSConnect=CSConnect
_G.CSSolo=CSSolo
_G.CSStudio=CSStudio
