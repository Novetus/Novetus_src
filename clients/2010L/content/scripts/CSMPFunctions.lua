settings().Rendering.FrameRateManager = 2
settings().Network.DataSendRate = 30
settings().Network.PhysicsSendRate = 20
settings().Network.ReceiveRate = 60
pcall(function() game:GetService("ScriptContext").ScriptsDisabled = false end)

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

function LoadCharacterNew(playerApp,newChar,RemoveTeapotTurret)
	if (playerApp==nil) then
		local message = Instance.new("Message")
		message.Text = "You were kicked. Reason: Modified Client"
		message.Parent = Player
		wait(2)
		Player:remove()
		print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' kicked. Reason: Modified Client")
	else
		if ((playerApp:GetChildren() == 0) or (playerApp:GetChildren() == nil)) then
			local message = Instance.new("Message")
			message.Text = "You were kicked. Reason: Modified Client"
			message.Parent = Player
			wait(2)
			Player:remove()
			print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' kicked. Reason: Modified Client")
		end
	end

	local charparts = {[1] = newWaitForChild(newChar,"Head"),[2] = newWaitForChild(newChar,"Torso"),[3] = newWaitForChild(newChar,"Left Arm"),[4] = newWaitForChild(newChar,"Right Arm"),[5] = newWaitForChild(newChar,"Left Leg"),[6] = newWaitForChild(newChar,"Right Leg")}
	for _,newVal in pairs(playerApp:GetChildren()) do
		local customtype = newVal.CustomizationType.Value
		if (customtype == 1) then 
			pcall(function() 
				charparts[newVal.ColorIndex.Value].BrickColor = newVal.Value 
			end)
		elseif (customtype == 2)  then
			pcall(function()
				local newHat = game.Workspace:InsertContent("rbxasset://../../../charcustom/hats/"..newVal.Value)
				if newHat[1] then 
					if newHat[1].className == "Hat" then
						if (RemoveTeapotTurret == true) then
							if (newHat[1].Name ~= "TeapotTurret.rbxm") then
								newHat[1].Parent = newChar
							else
								newHat[1]:remove()
							end
						else
							newHat[1].Parent = newChar
						end
					else
						newHat[1]:remove()
					end
				end
			end)
		elseif (customtype == 3)  then
			pcall(function()
				local newTShirt = game.Workspace:InsertContent("rbxasset://../../../charcustom/tshirts/"..newVal.Value)
				if newTShirt[1] then 
					if newTShirt[1].className == "ShirtGraphic" then
						newTShirt[1].Parent = newChar
					else
						newTShirt[1]:remove()
					end
				end
			end)
		elseif (customtype == 4)  then
			pcall(function()
				local newShirt = game.Workspace:InsertContent("rbxasset://../../../charcustom/shirts/"..newVal.Value)
				if newShirt[1] then 
					if newShirt[1].className == "Shirt" then
						newShirt[1].Parent = newChar
					else
						newShirt[1]:remove()
					end
				end
			end)
		elseif (customtype == 5)  then
			pcall(function()
				local newPants = game.Workspace:InsertContent("rbxasset://../../../charcustom/pants/"..newVal.Value)
				if newPants[1] then 
					if newPants[1].className == "Pants" then
						newPants[1].Parent = newChar
					else
						newPants[1]:remove()
					end
				end
			end)
		elseif (customtype == 6)  then
			pcall(function()
				local newFace = game.Workspace:InsertContent("rbxasset://../../../charcustom/faces/"..newVal.Value)
				if newFace[1] then 
					if newFace[1].className == "Decal" then
						newWaitForChild(charparts[1],"face"):remove()
						newFace[1].Parent = charparts[1]
						newFace[1].Face = "Front"
					else
						newFace[1]:remove()
					end
				end
			end)
		elseif (customtype == 7)  then
			pcall(function()
				local newPart = game.Workspace:InsertContent("rbxasset://../../../charcustom/heads/"..newVal.Value)
				if newPart[1] then 
					if newPart[1].className == "SpecialMesh" or newPart[1].className == "CylinderMesh" or newPart[1].className == "BlockMesh" then
						newWaitForChild(charparts[1],"Mesh"):remove()
						newPart[1].Parent = charparts[1]
					else
						newPart[1]:remove()
					end
				end
			end)
		end
	end
end

function InitalizeClientAppearance(Player,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID)
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
			BodyColor.Name = "HeadColor"
		elseif (i == 2) then
			if (TorsoColorID ~= nil) then
				BodyColor.Value = BrickColor.new(TorsoColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
			BodyColor.Name = "TorsoColor"
		elseif (i == 3) then
			if (LeftArmColorID ~= nil) then
				BodyColor.Value = BrickColor.new(LeftArmColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
			BodyColor.Name = "LeftArmColor"
		elseif (i == 4) then
			if (RightArmColorID ~= nil) then
				BodyColor.Value = BrickColor.new(RightArmColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
			BodyColor.Name = "RightArmColor"
		elseif (i == 5) then
			if (LeftLegColorID ~= nil) then
				BodyColor.Value = BrickColor.new(LeftLegColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
			BodyColor.Name = "LeftLegColor"
		elseif (i == 6) then
			if (RightLegColorID ~= nil) then
				BodyColor.Value = BrickColor.new(RightLegColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
			BodyColor.Name = "RightLegColor"
		end
		local indexValue = Instance.new("NumberValue")
		indexValue.Name = "ColorIndex"
		indexValue.Parent = BodyColor
		indexValue.Value = i
		local typeValue = Instance.new("NumberValue")
		typeValue.Name = "CustomizationType"
		typeValue.Parent = BodyColor
		typeValue.Value = 1
	end
	--HATS
	for i=1,3,1 do
		local newHat = Instance.new("StringValue",newCharApp)
		if (i == 1) then
			if (RightLegColorID ~= nil) then
				newHat.Value = Hat1ID
				newHat.Name = Hat1ID
			else
				newHat.Value = "NoHat.rbxm"
				newHat.Name = "NoHat.rbxm"
			end
		elseif (i == 2) then
			if (RightLegColorID ~= nil) then
				newHat.Value = Hat2ID
				newHat.Name = Hat2ID
			else
				newHat.Value = "NoHat.rbxm"
				newHat.Name = "NoHat.rbxm"
			end
		elseif (i == 3) then
			if (RightLegColorID ~= nil) then
				newHat.Value = Hat3ID
				newHat.Name = Hat3ID
			else
				newHat.Value = "NoHat.rbxm"
				newHat.Name = "NoHat.rbxm"
			end
		end
		local typeValue = Instance.new("NumberValue")
		typeValue.Name = "CustomizationType"
		typeValue.Parent = newHat
		typeValue.Value = 2
	end
	--T-SHIRT
	local newTShirt = Instance.new("StringValue",newCharApp)
	if (TShirtID ~= nil) then
		newTShirt.Value = TShirtID
		newTShirt.Name = TShirtID
	else
		newTShirt.Value = "NoTShirt.rbxm"
		newTShirt.Name = "NoTShirt.rbxm"
	end
	local typeValue = Instance.new("NumberValue")
	typeValue.Name = "CustomizationType"
	typeValue.Parent = newTShirt
	typeValue.Value = 3
	--SHIRT
	local newShirt = Instance.new("StringValue",newCharApp)
	if (ShirtID ~= nil) then
		newShirt.Value = ShirtID
		newShirt.Name = ShirtID
	else
		newShirt.Value = "NoShirt.rbxm"
		newShirt.Name = "NoShirt.rbxm"
	end
	local typeValue = Instance.new("NumberValue")
	typeValue.Name = "CustomizationType"
	typeValue.Parent = newShirt
	typeValue.Value = 4
	--PANTS
	local newPants = Instance.new("StringValue",newCharApp)
	if (PantsID ~= nil) then
		newPants.Value = PantsID
		newPants.Name = PantsID
	else
		newPants.Value = "NoPants.rbxm"
		newPants.Name = "NoPants.rbxm"
	end
	local typeValue = Instance.new("NumberValue")
	typeValue.Name = "CustomizationType"
	typeValue.Parent = newPants
	typeValue.Value = 5
	--FACE
	local newFace = Instance.new("StringValue",newCharApp)
	if (FaceID ~= nil) then
		newFace.Value = FaceID
		newFace.Name = FaceID
	else
		newFace.Value = "DefaultFace.rbxm"
		newFace.Name = "DefaultFace.rbxm"
	end
	local typeValue = Instance.new("NumberValue")
	typeValue.Name = "CustomizationType"
	typeValue.Parent = newFace
	typeValue.Value = 6
	--HEADS
	local newHead = Instance.new("StringValue",newCharApp)
	if (HeadID ~= nil) then
		newHead.Value = HeadID
		newHead.Name = HeadID
	else
		newHead.Value = "DefaultHead.rbxm"
		newHead.Name = "DefaultHead.rbxm"
	end
	local typeValue = Instance.new("NumberValue")
	typeValue.Name = "CustomizationType"
	typeValue.Parent = newHead
	typeValue.Value = 7
end

function LoadSecurity(playerApp,Player,ServerSecurityLocation)
	if (playerApp==nil) then
		local message = Instance.new("Message")
		message.Text = "You were kicked. Reason: Modified Client"
		message.Parent = Player
		wait(2)
		Player:remove()
		print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' kicked. Reason: Modified Client")
	else
		if ((playerApp:GetChildren() ~= 0) or (playerApp:GetChildren() ~= nil)) then
			for _,newVal in pairs(playerApp:GetChildren()) do
				if (playerApp:FindFirstChild("ClientEXEMD5")) then
					if ((newVal.Name == "ClientEXEMD5")) then
						if ((newVal.Value ~= ServerSecurityLocation.Security.ClientEXEMD5.Value) or (newVal.Value == nil) or (newVal.Value == "")) then
							local message = Instance.new("Message")
							message.Text = "You were kicked. Reason: Modified Client"
							message.Parent = Player
							wait(2)
							Player:remove()
							print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' kicked. Reason: Modified Client")
						end
					end
				else
					local message = Instance.new("Message")
					message.Text = "You were kicked. Reason: Modified Client"
					message.Parent = Player
					wait(2)
					Player:remove()
					print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' kicked. Reason: Modified Client")
				end
				
				if (playerApp:FindFirstChild("LauncherMD5")) then
					if ((newVal.Name == "LauncherMD5")) then
						if ((newVal.Value ~= ServerSecurityLocation.Security.LauncherMD5.Value) or (newVal.Value == nil) or (newVal.Value == "")) then
							local message = Instance.new("Message")
							message.Text = "You were kicked. Reason: Modified Client"
							message.Parent = Player
							wait(2)
							Player:remove()
							print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' kicked. Reason: Modified Client")
						end
					end
				else
					local message = Instance.new("Message")
					message.Text = "You were kicked. Reason: Modified Client"
					message.Parent = Player
					wait(2)
					Player:remove()
					print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' kicked. Reason: Modified Client")
				end
				
				if (playerApp:FindFirstChild("ClientScriptMD5")) then
					if ((newVal.Name == "ClientScriptMD5")) then
						if ((newVal.Value ~= ServerSecurityLocation.Security.ClientScriptMD5.Value) or (newVal.Value == nil) or (newVal.Value == "")) then
							local message = Instance.new("Message")
							message.Text = "You were kicked. Reason: Modified Client"
							message.Parent = Player
							wait(2)
							Player:remove()
							print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' kicked. Reason: Modified Client")
						end
					end
				else
					local message = Instance.new("Message")
					message.Text = "You were kicked. Reason: Modified Client"
					message.Parent = Player
					wait(2)
					Player:remove()
					print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' kicked. Reason: Modified Client")
				end
			end
		else
			local message = Instance.new("Message")
			message.Text = "You were kicked. Reason: Modified Client"
			message.Parent = Player
			wait(2)
			Player:remove()
			print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' kicked. Reason: Modified Client")
		end
	end
end

function InitalizeSecurityValues(Location,ClientEXEMD5,LauncherMD5,ClientScriptMD5)
	local newCharApp = Instance.new("IntValue",Location)
	newCharApp.Name = "Security"
	local newClientMD5 = Instance.new("StringValue",newCharApp)
	if (ClientEXEMD5 ~= nil) then
		newClientMD5.Value = ClientEXEMD5
	else
		newClientMD5.Value = ""
	end
	newClientMD5.Name = "ClientEXEMD5"
	local newLauncherMD5 = Instance.new("StringValue",newCharApp)
	if (LauncherMD5 ~= nil) then
		newLauncherMD5.Value = LauncherMD5
	else
		newLauncherMD5.Value = ""
	end
	newLauncherMD5.Name = "LauncherMD5"
	local newClientScriptMD5 = Instance.new("StringValue",newCharApp)
	if (ClientScriptMD5 ~= nil) then
		newClientScriptMD5.Value = ClientScriptMD5
	else
		newClientScriptMD5.Value = ""
	end
	newClientScriptMD5.Name = "ClientScriptMD5"
end

rbxversion = version()
print("ROBLOX Client version '" .. rbxversion .. "' loaded.")

function CSServer(Port,PlayerLimit,ClientEXEMD5,LauncherMD5,ClientScriptMD5,RemoveTeapotTurret)
	assert((type(Port)~="number" or tonumber(Port)~=nil or Port==nil),"CSRun Error: Port must be nil or a number.")
	local NetworkServer=game:GetService("NetworkServer")
	pcall(NetworkServer.Stop,NetworkServer)
	NetworkServer:Start(Port)
	game:GetService("Players").MaxPlayers = PlayerLimit
	game:GetService("Players").PlayerAdded:connect(function(Player)
		if (game:GetService("Players").NumPlayers > game:GetService("Players").MaxPlayers) then
			local message = Instance.new("Message")
			message.Text = "You were kicked. Reason: Too many players on server."
			message.Parent = Player
			wait(2)
			Player:remove()
			print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' kicked. Reason: Too many players on server.")
		else
			print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' added")
			Player:LoadCharacter()
		end
		Player.CharacterAdded:connect(function(char)
			LoadSecurity(newWaitForChild(Player,"Security"),Player,game.Lighting)
			if (Player.Character ~= nil) then
				LoadCharacterNew(newWaitForChild(Player,"Appearance"),Player.Character,RemoveTeapotTurret)
			end
		end)
		Player.Changed:connect(function(Property)
			if (Property=="Character") and (Player.Character~=nil) then
				local Character=Player.Character
				local Humanoid=Character:FindFirstChild("Humanoid")
				if (Humanoid~=nil) then
					Humanoid.Died:connect(function() delay(5,function() Player:LoadCharacter() LoadCharacterNew(newWaitForChild(Player,"Appearance"),Player.Character,RemoveTeapotTurret) end) end)
				end
			end
		end)
	end)
	game:GetService("Players").PlayerRemoving:connect(function(Player)
		print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' leaving")	
	end)
	game:GetService("RunService"):Run()
	game.Workspace:InsertContent("rbxasset://Fonts//libraries.rbxm")
	InitalizeSecurityValues(game.Lighting,LauncherMD5,ClientEXEMD5,ClientScriptMD5)
	pcall(function() game.Close:connect(function() NetworkServer:Stop() end) end)
	NetworkServer.IncommingConnection:connect(IncommingConnection)
end

function CSConnect(UserID,ServerIP,ServerPort,PlayerName,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID,IconType,ClientEXEMD5,LauncherMD5,ClientScriptMD5,Ticket)
	pcall(function() game:SetPlaceID(-1, false) end)
	pcall(function() game:GetService("Players"):SetChatStyle(Enum.ChatStyle.ClassicAndBubble) end)

	pcall(function()
		game:GetService("GuiService").Changed:connect(function()
			pcall(function() game:GetService("GuiService").ShowLegacyPlayerList=true end)
			pcall(function() game.CoreGui.RobloxGui.PlayerListScript:Remove() end)
			pcall(function() game.CoreGui.RobloxGui.PlayerListTopRightFrame:Remove() end)
			pcall(function() game.CoreGui.RobloxGui.BigPlayerListWindowImposter:Remove() end)
			pcall(function() game.CoreGui.RobloxGui.BigPlayerlist:Remove() end)
		end)
	end)
	game:GetService("RunService"):Run()
	assert((ServerIP~=nil and ServerPort~=nil),"CSConnect Error: ServerIP and ServerPort must be defined.")
	local function SetMessage(Message) game:SetMessage(Message) end
	local Visit,NetworkClient,PlayerSuccess,Player,ConnectionFailedHook=game:GetService("Visit"),game:GetService("NetworkClient")

	local function GetClassCount(Class,Parent)
		local Objects=Parent:GetChildren()
		local Number=0
		for Index,Object in pairs(Objects) do
			if (Object.className==Class) then
				Number=Number+1
			end
			Number=Number+GetClassCount(Class,Object)
		end
		return Number
	end

	local function RequestCharacter(Replicator)
		local Connection
		Connection=Player.Changed:connect(function(Property)
			if (Property=="Character") then
				game:ClearMessage()
			end
		end)
		SetMessage("Requesting character...")
		Replicator:RequestCharacter()
		SetMessage("Waiting for character...")
	end

	local function Disconnection(Peer,LostConnection)
		SetMessage("You have lost connection to the game")
	end

	local function ConnectionAccepted(Peer,Replicator)
		Replicator.Disconnection:connect(Disconnection)
		local RequestingMarker=true
		game:SetMessageBrickCount()
		local Marker=Replicator:SendMarker()
		Marker.Received:connect(function()
			RequestingMarker=false
			RequestCharacter(Replicator)
		end)
		while RequestingMarker do
			Workspace:ZoomToExtents()
			wait(0.5)
		end
	end

	local function ConnectionFailed(Peer, Code, why)
		SetMessage("Failed to connect to the Game. (ID="..Code.." ["..why.."])")
	end

	pcall(function() settings().Diagnostics:LegacyScriptMode() end)
	pcall(function() game:SetRemoteBuildMode(true) end)
	SetMessage("Connecting to server...")
	NetworkClient.ConnectionAccepted:connect(ConnectionAccepted)
	ConnectionFailedHook=NetworkClient.ConnectionFailed:connect(ConnectionFailed)
	NetworkClient.ConnectionRejected:connect(function()
		pcall(function() ConnectionFailedHook:disconnect() end)
		SetMessage("Failed to connect to the Game. (Connection rejected)")
	end)

	pcall(function() NetworkClient.Ticket=Ticket or "" end) -- 2008 client has no ticket :O
	PlayerSuccess,Player=pcall(function() return NetworkClient:PlayerConnect(UserID,ServerIP,ServerPort) end)

	if (not PlayerSuccess) then
		SetMessage("Failed to connect to the Game. (Invalid IP Address)")
		NetworkClient:Disconnect()
	end

	if (not PlayerSuccess) then
		local Error,Message=pcall(function()
			Player=game:GetService("Players"):CreateLocalPlayer(UserID)
			NetworkClient:Connect(ServerIP,ServerPort)
		end)
		if (not Error) then
			SetMessage("Failed to connect to the Game.")
		end
	end
	
	InitalizeClientAppearance(Player,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID)
	InitalizeSecurityValues(Player,LauncherMD5,ClientEXEMD5,ClientScriptMD5)
	pcall(function() Player:SetUnder13(false) end)
	pcall(function() Player:SetMembershipType(Enum.MembershipType.BuildersClub) end)
	pcall(function() Player:SetAccountAge(365) end)
	Player:SetSuperSafeChat(false)
	Player.CharacterAppearance=0
	pcall(function() Player.Name=PlayerName or "" end)
	pcall(function() Visit:SetUploadUrl("") end)
	game:GetService("Visit")
end

function CSSolo(UserID,PlayerName,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID,IconType)
	local plr = game.Players:CreateLocalPlayer(UserID)
	game:GetService("RunService"):run()
	game.Workspace:InsertContent("rbxasset://Fonts//libraries.rbxm")
	plr.Name = PlayerName
	plr:LoadCharacter()
	plr.CharacterAppearance=0
	InitalizeClientAppearance(plr,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID)
	LoadCharacterNew(newWaitForChild(plr,"Appearance"),plr.Character,false)
	game:GetService("Visit")
	while true do wait()
		if (plr.Character.Humanoid.Health == 0) then
			wait(5)
			plr:LoadCharacter()
			LoadCharacterNew(newWaitForChild(plr,"Appearance"),plr.Character,false)
		end
	end
end

_G.CSServer=CSServer
_G.CSConnect=CSConnect
_G.CSSolo=CSSolo