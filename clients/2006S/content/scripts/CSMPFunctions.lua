settings().Rendering.frameRateManager = 2
settings().Rendering.graphicsMode = 2
settings().Network.MaxSendBuffer = 1000000
settings().Network.PhysicsReplicationUpdateRate = 1000000
settings().Network.SendRate = 1000000
settings().Network.PhysicsSend = 1 -- 1==RoundRobin

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
	local charparts = {[1] = newWaitForChild(newChar,"Head"),[2] = newWaitForChild(newChar,"Torso"),[3] = newWaitForChild(newChar,"Left Arm"),[4] = newWaitForChild(newChar,"Right Arm"),[5] = newWaitForChild(newChar,"Left Leg"),[6] = newWaitForChild(newChar,"Right Leg")}
	for _,newVal in pairs(playerApp:GetChildren()) do
		local customtype = newVal.CustomizationType.Value
		if (customtype == 1) then 
			pcall(function() 
				charparts[newVal.ColorIndex.Value].BrickColor = newVal.Value 
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
	Server = game:GetService("NetworkServer")
	RunService = game:GetService("RunService")
	Server:start(Port, 20)
	RunService:run()
	game.Workspace:InsertContent("rbxasset://Fonts//libraries.rbxm")
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
			LoadSecurity(newWaitForChild(Player,"Security"),Player,game.Lighting)
			if (Player.Character ~= nil) then
				LoadCharacterNew(newWaitForChild(Player,"Appearance"),Player.Character,RemoveTeapotTurret)
			end
		end
			while true do 
				wait(0.001)
				if (Player.Character ~= nil) then
					if (Player.Character.Humanoid.Health == 0) then
						wait(5)
						Player:LoadCharacter()
						LoadCharacterNew(newWaitForChild(Player,"Appearance"),Player.Character,RemoveTeapotTurret)
					elseif (Player.Character.Parent == nil) then 
						wait(5)
						Player:LoadCharacter() -- to make sure nobody is deleted.
						LoadCharacterNew(newWaitForChild(Player,"Appearance"),Player.Character,RemoveTeapotTurret)
					end
				end
			end
		end)
	game:GetService("Players").PlayerRemoving:connect(function(Player)
		print("Player '" .. Player.Name .. "' with ID '" .. Player.userId .. "' leaving")	
	end)
	game:GetService("RunService"):Run()
	pcall(function() game.Close:connect(function() Server:Stop() end) end)
	InitalizeSecurityValues(game.Lighting,LauncherMD5,ClientEXEMD5,ClientScriptMD5)
	Server.IncommingConnection:connect(IncommingConnection)
end

function CSConnect(UserID,ServerIP,ServerPort,PlayerName,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID,IconType,ClientEXEMD5,LauncherMD5,ClientScriptMD5,Ticket)
	local suc, err = pcall(function()
		client = game:GetService("NetworkClient")
		player = game:GetService("Players"):CreateLocalPlayer(UserID) 
		player:SetSuperSafeChat(false)
		pcall(function() player:SetUnder13(false) end)
		pcall(function() player:SetMembershipType(Enum.MembershipType.BuildersClub) end)
		pcall(function() player:SetAccountAge(365) end)
		player.CharacterAppearance=0
		pcall(function() player.Name=PlayerName or "" end)
		game:GetService("Visit")
		InitalizeClientAppearance(player,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID)
		InitalizeSecurityValues(player,LauncherMD5,ClientEXEMD5,ClientScriptMD5)
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
		game.GuiRoot.RightPalette.ReportAbuse:Remove()
		game.GuiRoot.ChatMenuPanel:Remove()
	end)

	if not suc then
		local x = Instance.new("Message")
		x.Text = err
		x.Parent = workspace
		wait(math.huge)
	end
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