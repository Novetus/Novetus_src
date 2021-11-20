dofile("rbxasset://scripts\\cores\\StarterScript.lua")

pcall(function() game:GetService("ScriptContext").ScriptsDisabled = false end)
game.CoreGui.RobloxGui.TopLeftControl:Remove()
game.CoreGui.RobloxGui.BottomLeftControl:Remove()
game.CoreGui.RobloxGui.BottomRightControl:Remove()
game.GuiRoot.RightPalette:Remove()
game.GuiRoot.ChatMenuPanel:Remove()
game.GuiRoot.ScoreHud:Remove()
game.GuiRoot.ChatHud:Remove()

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

function LoadCharacterNew(playerApp,newChar)
	local path = "rbxasset://../../../shareddata/charcustom/"

	local charparts = {[1] = newWaitForChild(newChar,"Head"),[2] = newWaitForChild(newChar,"Torso"),[3] = newWaitForChild(newChar,"Left Arm"),[4] = newWaitForChild(newChar,"Right Arm"),[5] = newWaitForChild(newChar,"Left Leg"),[6] = newWaitForChild(newChar,"Right Leg")}
	for _,newVal in pairs(playerApp:GetChildren()) do
		local customtype = newVal.CustomizationType.Value
		if (customtype == 1) then 
			pcall(function() 
				charparts[newVal.ColorIndex.Value].BrickColor = newVal.Value 
			end)
		elseif (customtype == 2)  then
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
		elseif (customtype == 3)  then
			pcall(function()
				local newTShirt = "";
				if (string.match(newVal.Value, "http") == "http") then
					if (pcall(function()
						newTShirt = game.Workspace:InsertContent(newVal.Value)
						if newTShirt[1] then 
							if newTShirt[1].className == "ShirtGraphic" then
								newTShirt[1].Parent = newChar
								local oldTexture = newTShirt[1].Graphic;
								OldURL,OldID = oldTexture:match("(.+)=(.+)")
								NewURL,NewID = newVal.Value:match("(.+)=(.+)")
								newTShirt[1].Graphic = NewURL .. '=' .. OldID
							else
								newTShirt[1]:remove()
							end
						end
					end)) then
						--nothing
						print("success");
					else
						print("fail");
						newTShirt = Instance.new("ShirtGraphic")
						newTShirt.Graphic = newVal.Value
						newTShirt.Parent = newChar
					end
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
		elseif (customtype == 4)  then
			pcall(function()
				local newShirt = "";
				if (string.match(newVal.Value, "http") == "http") then
					if (pcall(function()
						newShirt = game.Workspace:InsertContent(newVal.Value)
						if newShirt[1] then 
							if newShirt[1].className == "Shirt" then
								newShirt[1].Parent = newChar
								local oldTexture = newShirt[1].ShirtTemplate;
								OldURL,OldID = oldTexture:match("(.+)=(.+)")
								NewURL,NewID = newVal.Value:match("(.+)=(.+)")
								newShirt[1].ShirtTemplate = NewURL .. '=' .. OldID
							else
								newShirt[1]:remove()
							end
						end
					end)) then
						--nothing
						print("success");
					else
						print("fail");
						newShirt = Instance.new("Shirt")
						newShirt.ShirtTemplate = newVal.Value
						newShirt.Parent = newChar
					end
				else
					newShirt = game.Workspace:InsertContent(path.."shirts/"..newVal.Value)
					if newShirt[1] then 
						if newShirt[1].className == "Shirt" then
							newShirt[1].Parent = newChar
						else
							newShirt[1]:remove()
						end
					end
				end
			end)
		elseif (customtype == 5)  then
			pcall(function()
				local newPants = "";
				if (string.match(newVal.Value, "http") == "http") then
					if (pcall(function()
						newPants = game.Workspace:InsertContent(newVal.Value)
						if newPants[1] then 
							if newPants[1].className == "Pants" then
								newPants[1].Parent = newChar
								local oldTexture = newPants[1].PantsTemplate;
								OldURL,OldID = oldTexture:match("(.+)=(.+)")
								NewURL,NewID = newVal.Value:match("(.+)=(.+)")
								newPants[1].PantsTemplate = NewURL .. '=' .. OldID
							else
								newPants[1]:remove()
							end
						end
					end)) then
						--nothing
						print("success");
					else
						print("fail");
						newPants = Instance.new("Pants")
						newPants.PantsTemplate = newVal.Value
						newPants.Parent = newChar
					end
				else
					newPants = game.Workspace:InsertContent(path.."pants/"..newVal.Value)
					if newPants[1] then 
						if newPants[1].className == "Pants" then
							newPants[1].Parent = newChar
						else
							newPants[1]:remove()
						end
					end
				end
			end)
		elseif (customtype == 6)  then
			pcall(function()
				local newFace = "";
				if (string.match(newVal.Value, "http") == "http") then
					if (pcall(function()
						newFace = game.Workspace:InsertContent(newVal.Value)
						if newFace[1] then 
							if newFace[1].className == "Decal" then
								newWaitForChild(charparts[1],"face"):remove()
								newFace[1].Parent = charparts[1]
								newFace[1].Face = "Front"
								local oldTexture = newFace[1].Texture;
								OldURL,OldID = oldTexture:match("(.+)=(.+)")
								NewURL,NewID = newVal.Value:match("(.+)=(.+)")
								newFace[1].Texture = NewURL .. '=' .. OldID
							else
								newFace[1]:remove()
							end
						end
					end)) then
						--nothing
						print("success");
					else
						print("fail");
						newWaitForChild(charparts[1],"face"):remove()
						newFace = Instance.new("Decal")
						newFace.Texture = newVal.Value
						newFace.Face = "Front"
						newFace.Parent = charparts[1]
					end
				else
					newFace = game.Workspace:InsertContent(path.."faces/"..newVal.Value)
					if newFace[1] then 
						if newFace[1].className == "Decal" then
							newWaitForChild(charparts[1],"face"):remove()
							newFace[1].Parent = charparts[1]
							newFace[1].Face = "Front"
						else
							newFace[1]:remove()
						end
					end
				end
			end)
		elseif (customtype == 7)  then
			pcall(function()
				local newPart = game.Workspace:InsertContent(path.."heads/"..newVal.Value)
				if newPart[1] then 
					if newPart[1].className == "SpecialMesh" or newPart[1].className == "CylinderMesh" or newPart[1].className == "BlockMesh" then
						newWaitForChild(charparts[1],"Mesh"):remove()
						newPart[1].Parent = charparts[1]
					else
						newPart[1]:remove()
					end
				end
			end)
		elseif (customtype == 8)  then
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

function InitalizeClientAppearance(Player,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID,ItemID,IconType)
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
			BodyColor.Name = "Head Color"
		elseif (i == 2) then
			if (TorsoColorID ~= nil) then
				BodyColor.Value = BrickColor.new(TorsoColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
			BodyColor.Name = "Torso Color"
		elseif (i == 3) then
			if (LeftArmColorID ~= nil) then
				BodyColor.Value = BrickColor.new(LeftArmColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
			BodyColor.Name = "Left Arm Color"
		elseif (i == 4) then
			if (RightArmColorID ~= nil) then
				BodyColor.Value = BrickColor.new(RightArmColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
			BodyColor.Name = "Right Arm Color"
		elseif (i == 5) then
			if (LeftLegColorID ~= nil) then
				BodyColor.Value = BrickColor.new(LeftLegColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
			BodyColor.Name = "Left Leg Color"
		elseif (i == 6) then
			if (RightLegColorID ~= nil) then
				BodyColor.Value = BrickColor.new(RightLegColorID)
			else
				BodyColor.Value = BrickColor.new(1)
			end
			BodyColor.Name = "Right Leg Color"
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
			if (Hat1ID ~= nil) then
				newHat.Value = Hat1ID
				newHat.Name = "Hat 1 - "..Hat1ID
			else
				newHat.Value = "NoHat.rbxm"
				newHat.Name = "Hat 1 - NoHat.rbxm"
			end
		elseif (i == 2) then
			if (Hat2ID ~= nil) then
				newHat.Value = Hat2ID
				newHat.Name = "Hat 2 - "..Hat2ID
			else
				newHat.Value = "NoHat.rbxm"
				newHat.Name = "Hat 2 - NoHat.rbxm"
			end
		elseif (i == 3) then
			if (Hat3ID ~= nil) then
				newHat.Value = Hat3ID
				newHat.Name = "Hat 3 - "..Hat3ID
			else
				newHat.Value = "NoHat.rbxm"
				newHat.Name = "Hat 3 - NoHat.rbxm"
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
		newTShirt.Name = "T-Shirt - "..TShirtID
	else
		newTShirt.Value = "NoTShirt.rbxm"
		newTShirt.Name = "T-Shirt - NoTShirt.rbxm"
	end
	local typeValue = Instance.new("NumberValue")
	typeValue.Name = "CustomizationType"
	typeValue.Parent = newTShirt
	typeValue.Value = 3
	--SHIRT
	local newShirt = Instance.new("StringValue",newCharApp)
	if (ShirtID ~= nil) then
		newShirt.Value = ShirtID
		newShirt.Name = "Shirt - "..ShirtID
	else
		newShirt.Value = "NoShirt.rbxm"
		newShirt.Name = "Shirt - NoShirt.rbxm"
	end
	local typeValue = Instance.new("NumberValue")
	typeValue.Name = "CustomizationType"
	typeValue.Parent = newShirt
	typeValue.Value = 4
	--PANTS
	local newPants = Instance.new("StringValue",newCharApp)
	if (PantsID ~= nil) then
		newPants.Value = PantsID
		newPants.Name = "Pants - "..PantsID
	else
		newPants.Value = "NoPants.rbxm"
		newPants.Name = "Pants - NoPants.rbxm"
	end
	local typeValue = Instance.new("NumberValue")
	typeValue.Name = "CustomizationType"
	typeValue.Parent = newPants
	typeValue.Value = 5
	--FACE
	local newFace = Instance.new("StringValue",newCharApp)
	if (FaceID ~= nil) then
		newFace.Value = FaceID
		newFace.Name = "Face - "..FaceID
	else
		newFace.Value = "DefaultFace.rbxm"
		newFace.Name = "Face - DefaultFace.rbxm"
	end
	local typeValue = Instance.new("NumberValue")
	typeValue.Name = "CustomizationType"
	typeValue.Parent = newFace
	typeValue.Value = 6
	--HEADS
	local newHead = Instance.new("StringValue",newCharApp)
	if (HeadID ~= nil) then
		newHead.Value = HeadID
		newHead.Name = "Head - "..HeadID
	else
		newHead.Value = "DefaultHead.rbxm"
		newHead.Name = "Head - DefaultHead.rbxm"
	end
	local typeValue = Instance.new("NumberValue")
	typeValue.Name = "CustomizationType"
	typeValue.Parent = newHead
	typeValue.Value = 7
	--EXTRA
	local newItem = Instance.new("StringValue",newCharApp)
	if (ItemID ~= nil) then
		newItem.Value = ItemID
		newItem.Name = "Extra - "..ItemID
	else
		newItem.Value = "NoExtra.rbxm"
		newItem.Name = "Extra - NoExtra.rbxm"
	end
	local typeValue = Instance.new("NumberValue")
	typeValue.Name = "CustomizationType"
	typeValue.Parent = newItem
	typeValue.Value = 8
	--ICON
	local newIcon = Instance.new("StringValue",newCharApp)
	if (IconType ~= nil) then
		newIcon.Value = IconType
	else
		newIcon.Value = "NBC"
	end
	newIcon.Name = "Icon"
	local typeValue = Instance.new("NumberValue")
	typeValue.Name = "CustomizationType"
	typeValue.Parent = newIcon
	typeValue.Value = 9
end

print("3DView loaded. Nerd.")

function CS3DView(UserID,PlayerName,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID,IconType,ItemID)
	game:GetService("RunService"):Run()
	game:SetMessage("Loading Player...")
	
	local plr = game.Players:CreateLocalPlayer(UserID)
	plr.Name = PlayerName
	plr:LoadCharacter()
	if (IconType == "BC") then
		plr:SetMembershipType(Enum.MembershipType.BuildersClub)
	elseif (IconType == "TBC") then
		plr:SetMembershipType(Enum.MembershipType.TurboBuildersClub)
	elseif  (IconType == "OBC") then
		plr:SetMembershipType(Enum.MembershipType.OutrageousBuildersClub)
	elseif  (IconType == "NBC" or string.match(IconType, "http") == "http") then
		plr:SetMembershipType(Enum.MembershipType.None)
	end
	plr.CharacterAppearance=0
	InitalizeClientAppearance(plr,Hat1ID,Hat2ID,Hat3ID,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,TShirtID,ShirtID,PantsID,FaceID,HeadID,ItemID,IconType)
	wait(0.7)
	LoadCharacterNew(newWaitForChild(plr,"Appearance"),plr.Character,false)
	
	local target = game.Workspace.Base.SpawnLocation
	local camera = game.Workspace.CurrentCamera
	camera.CameraType = Enum.CameraType.Watch
	local cf = CFrame.new(0, 10, 18) * CFrame.Angles(math.rad(180), 0, 0)
	camera.CoordinateFrame = cf;
	camera.CameraSubject = target
	
	i = true

	local function fixJump(prop)
		plr.Character.Torso.Velocity = plr.Character.Torso.Velocity * Vector3.new (1, 0, 1)
		if i == true then
			plr.Character.Torso.CFrame = plr.Character.Torso.CFrame - Vector3.new(0, 1.8, 0)
			i = false
		else
			i = true
		end
	end
	
	local human = plr.Character.Humanoid
	
	human.WalkSpeed = 0
	human.Jumping:connect(fixJump)
	
	game:GetService("Visit"):SetUploadUrl("")
	game:ClearMessage()
end

function CS3DViewEdit()
end

_G.CS3DView=CS3DView
_G.CS3DViewEdit=CS3DViewEdit