
delay(0, function()

if game.CoreGui.Version < 3 then return end -- peace out if we aren't using the right client

-- A couple of necessary functions
local function waitForChild(instance, name)
	while not instance:FindFirstChild(name) do
		instance.ChildAdded:wait()
	end
end
local function waitForProperty(instance, property)
	while not instance[property] do
		instance.Changed:wait()
	end
end





--- Begin Locals
waitForChild(game,"Players")
waitForProperty(game.Players,"LocalPlayer")
local player = game.Players.LocalPlayer

waitForChild(game, "LocalBackpack")
game.LocalBackpack:SetOldSchoolBackpack(false)

local currentLoadout = game:GetService("CoreGui").RobloxGui:FindFirstChild("CurrentLoadout")
local maxNumLoadoutItems = 10

local guiBackpack = currentLoadout.Parent.Backpack

local characterChildAddedCon = nil
local keyPressCon = nil
local backpackChildCon = nil

local debounce = currentLoadout.Debounce

local waitingOnEnlarge = nil

local enlargeFactor = 1.18
local buttonSizeEnlarge = UDim2.new(1 * enlargeFactor,0,1 * enlargeFactor,0)
local buttonSizeNormal = UDim2.new(1,0,1,0)
local enlargeOverride = true

local guiTweenSpeed = 0.5

for i = 0, 9 do
	game:GetService("GuiService"):AddKey(tostring(i)) -- register our keys
end

local gearSlots = {}
for i = 1, maxNumLoadoutItems do
	gearSlots[i] = "empty"
end

local inventory = {}
--- End Locals






-- Begin Functions
local function kill(prop,con,gear)
	if con then con:disconnect() end
	if prop == true and gear then
		reorganizeLoadout(gear,false)
	end
end

function removeGear(gear)
	local emptySlot = nil
	for i = 1, #gearSlots do
		if gearSlots[i] == gear and gear.Parent ~= nil then
			emptySlot = i
			break
		end
	end
	if emptySlot then
		if gearSlots[emptySlot].GearReference.Value then
			if gearSlots[emptySlot].GearReference.Value.Parent == game.Players.LocalPlayer.Character then -- if we currently have this equipped, unequip it
				gearSlots[emptySlot].GearReference.Value.Parent = game.Players.LocalPlayer.Backpack
			end
			
			if gearSlots[emptySlot].GearReference.Value:IsA("HopperBin") and gearSlots[emptySlot].GearReference.Value.Active then -- this is an active hopperbin
				gearSlots[emptySlot].GearReference.Value:Disable()
				gearSlots[emptySlot].GearReference.Value.Active = false
			end
		end
		
		gearSlots[emptySlot] = "empty"
		
		local centerizeX = gear.Size.X.Scale/2
		local centerizeY = gear.Size.Y.Scale/2
		gear:TweenSizeAndPosition(UDim2.new(0,0,0,0),
			UDim2.new(gear.Position.X.Scale + centerizeX,gear.Position.X.Offset,gear.Position.Y.Scale + centerizeY,gear.Position.Y.Offset),
			Enum.EasingDirection.Out, Enum.EasingStyle.Quad,guiTweenSpeed/4,true)
		delay(guiTweenSpeed/2,
			function()
				gear:remove()
			end)
	end
end

function insertGear(gear, addToSlot)
	local pos = nil
	if not addToSlot then
		for i = 1, #gearSlots do
			if gearSlots[i] == "empty" then
				pos = i
				break
			end
		end
		
		if pos == 1 and gearSlots[1] ~= "empty" then gear:remove() return end -- we are currently full, can't add in
	else
		pos = addToSlot
		-- push all gear down one slot
		local start = 1
		for i = 1, #gearSlots do
			if gearSlots[i] == "empty" then
				start = i
				break
			end
		end
		for i = start, pos + 1, -1 do
			gearSlots[i] = gearSlots[i - 1]
			if i == 10 then
				gearSlots[i].SlotNumber.Text = "0"
				gearSlots[i].SlotNumberDownShadow.Text = "0"
				gearSlots[i].SlotNumberUpShadow.Text = "0"
			else
				gearSlots[i].SlotNumber.Text = i
				gearSlots[i].SlotNumberDownShadow.Text = i
				gearSlots[i].SlotNumberUpShadow.Text = i
			end
		end
	end

	gearSlots[pos] = gear
	if pos ~= maxNumLoadoutItems then
		if(type(tostring(pos)) == "string") then
			local posString = tostring(pos) 
			gear.SlotNumber.Text = posString
			gear.SlotNumberDownShadow.Text = posString
			gear.SlotNumberUpShadow.Text = posString
		end
	else -- tenth gear doesn't follow mathematical pattern :(
		gear.SlotNumber.Text = "0"
		gear.SlotNumberDownShadow.Text = "0"
		gear.SlotNumberUpShadow.Text = "0"
	end
	gear.Visible = true

	local con = nil
	con = gear.Kill.Changed:connect(function(prop) kill(prop,con,gear) end)
end


function reorganizeLoadout(gear, inserting, equipped, addToSlot)
	if inserting then -- add in gear
		insertGear(gear, addToSlot)
	else
		removeGear(gear)
	end
	if gear ~= "empty" then gear.ZIndex = 1 end
end

function checkToolAncestry(child,parent)
	if child:FindFirstChild("RobloxBuildTool") then return end -- don't show roblox build tools
	if child:IsA("Tool") or child:IsA("HopperBin") then
		for i = 1, #gearSlots do
			if gearSlots[i] ~= "empty" and gearSlots[i].GearReference.Value == child then
				if parent == nil then 
					gearSlots[i].Kill.Value = true
					return false
				elseif child.Parent == player.Character then
					gearSlots[i].Selected = true
					return true
				elseif child.Parent == player.Backpack then
					if child:IsA("Tool") then gearSlots[i].Selected = false end
					return true
				else
					gearSlots[i].Kill.Value = true
					return false
				end
				return true
			end
		end
	end
end

function removeAllEquippedGear(physGear)
	local stuff = player.Character:GetChildren()
	for i = 1, #stuff do
		if ( stuff[i]:IsA("Tool") or stuff[i]:IsA("HopperBin") ) and stuff[i] ~= physGear then
			if stuff[i]:IsA("Tool") then stuff[i].Parent = player.Backpack end
			if stuff[i]:IsA("HopperBin") then
				stuff[i]:Disable()
			end
		end
	end
end

function hopperBinSwitcher(numKey, physGear)
	if not physGear then return end
	
	physGear:ToggleSelect()
	
	if gearSlots[numKey] == "empty" then return end
	
	if not physGear.Active then
		gearSlots[numKey].Selected = false
		normalizeButton(gearSlots[numKey])
	else
		gearSlots[numKey].Selected = true
		enlargeButton(gearSlots[numKey])
	end
end

function toolSwitcher(numKey)

	if not gearSlots[numKey] then return end
	local physGear = gearSlots[numKey].GearReference.Value
	if physGear == nil then return end

	removeAllEquippedGear(physGear) -- we don't remove this gear, as then we get a double switcheroo

	local key = numKey
	if numKey == 0 then key = 10 end
		
	for i = 1, #gearSlots do
		if gearSlots[i] and gearSlots[i] ~= "empty" and i ~= key then
			normalizeButton(gearSlots[i])
			gearSlots[i].Selected = false
			if gearSlots[i].GearReference.Value:IsA("HopperBin") and gearSlots[i].GearReference.Value.Active then
				gearSlots[i].GearReference.Value:ToggleSelect()
			end
		end
	end
	
	if physGear:IsA("HopperBin") then
		hopperBinSwitcher(numKey,physGear)
	else
		if physGear.Parent == player.Character then
			physGear.Parent = player.Backpack
			gearSlots[numKey].Selected = false
			
			normalizeButton(gearSlots[numKey])
		else
			physGear.Parent = player.Character
			gearSlots[numKey].Selected = true
			
			enlargeButton(gearSlots[numKey])
		end
	end
end


function activateGear(num)
	local numKey = nil
	if num == "0" then
		numKey = 10 -- why do lua indexes have to start at 1? :(
	else
		numKey = tonumber(num)
	end

	if(numKey == nil) then return end

	if gearSlots[numKey] ~= "empty" then
		toolSwitcher(numKey)
	end
end


enlargeButton = function(button)
	if button.Size.Y.Scale > 1 then return end
	if not button.Parent then return end
	if not button.Selected then return end

	for i = 1, #gearSlots do
		if gearSlots[i] == "empty" then break end
		if gearSlots[i] ~= button then
			normalizeButton(gearSlots[i])
		end
	end
	
	if not enlargeOverride then
		waitingOnEnlarge = button
		return
	end

	if button:IsA("ImageButton") or button:IsA("TextButton") then
		button.ZIndex = 2
		local centerizeX = -(buttonSizeEnlarge.X.Scale - button.Size.X.Scale)/2
		local centerizeY = -(buttonSizeEnlarge.Y.Scale - button.Size.Y.Scale)/2
		button:TweenSizeAndPosition(buttonSizeEnlarge,
			UDim2.new(button.Position.X.Scale + centerizeX,button.Position.X.Offset,button.Position.Y.Scale + centerizeY,button.Position.Y.Offset),
			Enum.EasingDirection.Out, Enum.EasingStyle.Quad,guiTweenSpeed/5,enlargeOverride)
	end
end

normalizeAllButtons = function()
	for i = 1, #gearSlots do
		if gearSlots[i] == "empty" then break end
		if gearSlots[i] ~= button then
			normalizeButton(gearSlots[i],0.1)
		end
	end
end


normalizeButton = function(button, speed)
	if not button then return end
	if button.Size.Y.Scale <= 1 then return end
	if button.Selected then return end
	if not button.Parent then return end
	
	local moveSpeed = speed
	if moveSpeed == nil or type(moveSpeed) ~= "number" then moveSpeed = guiTweenSpeed/5 end

	if button:IsA("ImageButton") or button:IsA("TextButton") then
		button.ZIndex = 1
		local inverseEnlarge = 1/enlargeFactor
		local centerizeX = -(buttonSizeNormal.X.Scale - button.Size.X.Scale)/2
		local centerizeY = -(buttonSizeNormal.Y.Scale - button.Size.Y.Scale)/2
		button:TweenSizeAndPosition(buttonSizeNormal,
			UDim2.new(button.Position.X.Scale + centerizeX,button.Position.X.Offset,button.Position.Y.Scale + centerizeY,button.Position.Y.Offset),
			Enum.EasingDirection.Out, Enum.EasingStyle.Quad,moveSpeed,enlargeOverride)
	end
end

waitForDebounce = function()
	if debounce.Value then
		debounce.Changed:wait()
	end
end

function pointInRectangle(point,rectTopLeft,rectSize)
	if point.x > rectTopLeft.x and point.x < (rectTopLeft.x + rectSize.x) then
		if point.y > rectTopLeft.y and point.y < (rectTopLeft.y + rectSize.y) then
			return true
		end
	end
	return false
end

function swapGear(gearClone,toFrame)
	local toFrameChildren = toFrame:GetChildren()
	if #toFrameChildren == 1 then
		if toFrameChildren[1]:FindFirstChild("SlotNumber") then
		
			local toSlot = tonumber(toFrameChildren[1].SlotNumber.Text)
			local gearCloneSlot = tonumber(gearClone.SlotNumber.Text)
			if toSlot == 0 then toSlot = 10 end
			if gearCloneSlot == 0 then gearCloneSlot = 10 end
			
			gearSlots[toSlot] = gearClone
			gearSlots[gearCloneSlot] = toFrameChildren[1]
			
			toFrameChildren[1].SlotNumber.Text = gearClone.SlotNumber.Text
			toFrameChildren[1].SlotNumberDownShadow.Text = gearClone.SlotNumber.Text
			toFrameChildren[1].SlotNumberUpShadow.Text = gearClone.SlotNumber.Text
			
			local subString = string.sub(toFrame.Name,5)
			gearClone.SlotNumber.Text = subString
			gearClone.SlotNumberDownShadow.Text = subString
			gearClone.SlotNumberUpShadow.Text = subString
			
			gearClone.Position = UDim2.new(gearClone.Position.X.Scale,0,gearClone.Position.Y.Scale,0)
			toFrameChildren[1].Position = UDim2.new(toFrameChildren[1].Position.X.Scale,0,toFrameChildren[1].Position.Y.Scale,0)
			
			toFrameChildren[1].Parent = gearClone.Parent
			gearClone.Parent = toFrame
		end
	else
		local slotNum = tonumber(gearClone.SlotNumber.Text)
		if slotNum == 0 then slotNum = 10 end
		gearSlots[slotNum] = "empty" -- reset this gear slot
		
		local subString = string.sub(toFrame.Name,5)
		gearClone.SlotNumber.Text = subString
		gearClone.SlotNumberDownShadow.Text = subString
		gearClone.SlotNumberUpShadow.Text = subString
			
		local toSlotNum = tonumber(gearClone.SlotNumber.Text)
		if toSlotNum == 0 then toSlotNum = 10 end
		gearSlots[toSlotNum] = gearClone
		gearClone.Position = UDim2.new(gearClone.Position.X.Scale,0,gearClone.Position.Y.Scale,0)
		gearClone.Parent = toFrame
	end
end

function resolveDrag(gearClone,x,y)
	local mousePoint = Vector2.new(x,y)
	
	local frame = gearClone.Parent
	local frames = frame.Parent:GetChildren()
	
	for i = 1, #frames do
		if frames[i]:IsA("Frame") then
			if pointInRectangle(mousePoint, frames[i].AbsolutePosition,frames[i].AbsoluteSize) then
				swapGear(gearClone,frames[i])
				return true
			end
		end
	end
	
	if x < frame.AbsolutePosition.x or x > ( frame.AbsolutePosition.x + frame.AbsoluteSize.x ) then
		reorganizeLoadout(gearClone,false)
		return false
	elseif y < frame.AbsolutePosition.y or y > ( frame.AbsolutePosition.y + frame.AbsoluteSize.y ) then
		reorganizeLoadout(gearClone,false)
		return false
	else
		if dragBeginPos then gearClone.Position = dragBeginPos end
		return -1
	end
end

function unequipAllItems(dontEquipThis)
	for i = 1, #gearSlots do
		if gearSlots[i] == "empty" then break end
		if gearSlots[i].GearReference.Value ~= dontEquipThis then
			if gearSlots[i].GearReference.Value:IsA("HopperBin") then
				gearSlots[i].GearReference.Value:Disable()
			elseif gearSlots[i].GearReference.Value:IsA("Tool") then
				gearSlots[i].GearReference.Value.Parent = game.Players.LocalPlayer.Backpack
			end
			gearSlots[i].Selected = false
		end
	end
end

local addingPlayerChild = function(child, equipped, addToSlot, inventoryGearButton)
	waitForDebounce()
	debounce.Value = true
	
	if child:FindFirstChild("RobloxBuildTool") then debounce.Value = false return end -- don't show roblox build tools
	if not child:IsA("Tool") then
		if not child:IsA("HopperBin") then
			debounce.Value = false 
			return  -- we don't care about anything besides tools (sigh...)
		end
	end
	
	if not addToSlot then
		for i = 1, #gearSlots do
			if gearSlots[i] ~= "empty" and gearSlots[i].GearReference.Value == child then -- we already have gear, do nothing
				debounce.Value = false
				return
			end
		end
	end
	

	local gearClone = currentLoadout.TempSlot:clone()
	gearClone.Name = child.Name
	gearClone.GearImage.Image = child.TextureId
	if gearClone.GearImage.Image == "" then
		gearClone.GearText.Text = child.Name
	end
	gearClone.GearReference.Value = child

	gearClone.RobloxLocked = true

	local slotToMod = -1
	
	if not addToSlot then
		for i = 1, #gearSlots do
			if gearSlots[i] == "empty" then
				slotToMod = i
				break
			end
		end
	else
		slotToMod = addToSlot
	end
	
	if slotToMod == - 1 then debounce.Value = false print("no slots!") return end -- No available slot to add in!
	
	local slotNum = slotToMod % 10
	local parent = currentLoadout:FindFirstChild("Slot"..tostring(slotNum))
	gearClone.Parent = parent
	
	if inventoryGearButton then
		local absolutePositionFinal = inventoryGearButton.AbsolutePosition
		local currentAbsolutePosition = gearClone.AbsolutePosition
		local diff = absolutePositionFinal - currentAbsolutePosition
		gearClone.Position = UDim2.new(gearClone.Position.X.Scale,diff.x,gearClone.Position.Y.Scale,diff.y)
		gearClone.ZIndex = 4
	end
	
	if addToSlot then
		reorganizeLoadout(gearClone, true, equipped, addToSlot)
	else
		reorganizeLoadout(gearClone, true)
	end

	if gearClone.Parent == nil then debounce.Value = false return end -- couldn't fit in (hopper is full!)

	if equipped then
		gearClone.Selected = true
		unequipAllItems(child)
		delay(guiTweenSpeed + 0.01,function() -- if our gear is equipped, we will want to enlarge it when done moving
			if (gearClone.GearReference.Value:IsA("Tool") and gearClone:FindFirstChild("GearReference") and gearClone.GearReference.Value.Parent == player.Character) or
				(gearClone.GearReference.Value:IsA("HopperBin") and gearClone.GearReference.Value.Active == true) then
					enlargeButton(gearClone)
			end
		end) 
	end

	local dragBeginPos = nil
	local clickCon, buttonDeleteCon, mouseEnterCon, mouseLeaveCon, dragStop, dragBegin = nil
	clickCon = gearClone.MouseButton1Click:connect(function() if not gearClone.Draggable then activateGear(gearClone.SlotNumber.Text) end end)
	mouseEnterCon = gearClone.MouseEnter:connect(function()
		if guiBackpack.Visible then
			gearClone.Draggable = true
		end
	end)
	dragBegin = gearClone.DragBegin:connect(function(pos)
		dragBeginPos = pos
		gearClone.ZIndex = 7
		local children = gearClone:GetChildren()
		for i = 1, #children do
			if children[i]:IsA("TextLabel") then
				if string.find(children[i].Name,"Shadow") then
					children[i].ZIndex = 8
				else
					children[i].ZIndex = 9
				end
			elseif children[i]:IsA("Frame") or children[i]:IsA("ImageLabel") then
				 children[i].ZIndex = 7
			end
		end
	end)
	dragStop = gearClone.DragStopped:connect(function(x,y)
		if gearClone.Selected then
			gearClone.ZIndex = 2
		else
			gearClone.ZIndex = 1
		end
		local children = gearClone:GetChildren()
		for i = 1, #children do
			if children[i]:IsA("TextLabel") then
				if string.find(children[i].Name,"Shadow") then
					children[i].ZIndex = 3
				else
					children[i].ZIndex = 4
				end
			elseif children[i]:IsA("Frame") or children[i]:IsA("ImageLabel") then
				 children[i].ZIndex = 2
			end
		end
		resolveDrag(gearClone,x,y)
	end)
	mouseLeaveCon = gearClone.MouseLeave:connect(function()
		gearClone.Draggable = false
	end)
	buttonDeleteCon = gearClone.AncestryChanged:connect(function()
			if gearClone.Parent and gearClone.Parent.Parent == currentLoadout then return end
			if clickCon then clickCon:disconnect() end
			if buttonDeleteCon then buttonDeleteCon:disconnect() end
			if mouseEnterCon then mouseEnterCon:disconnect() end
			if mouseLeaveCon then mouseLeaveCon:disconnect() end
			if dragStop then dragStop:disconnect() end
			if dragBegin then dragBegin:disconnect() end
	end) -- this probably isn't necessary since objects are being deleted (probably), but this might still leak just in case
	
	local childCon = nil
	local childChangeCon = nil
	childCon = child.AncestryChanged:connect(function(newChild,parent)
		if not checkToolAncestry(newChild,parent) then
			if childCon then childCon:disconnect() end
			if childChangeCon then childChangeCon:disconnect() end
			removeFromInventory(child)
		elseif parent == game.Players.LocalPlayer.Backpack then
			normalizeButton(gearClone)
		end
	end)
	
	childChangeCon = child.Changed:connect(function(prop)
		if prop == "Name" then
			if gearClone and gearClone.GearImage.Image == "" then
				gearClone.GearText.Text = child.Name
			end
		end
	end)
	
	debounce.Value = false
	
end

function addToInventory(child)
	if not child:IsA("Tool") or not child:IsA("HopperBin") then return end
	
	local slot = nil
	for i = 1, #inventory do
		if inventory[i] and inventory[i] == child then return end
		if not inventory[i] then slot = i end
	end
	if slot then 
		inventory[slot] = child 
	elseif #inventory < 1 then
		inventory[1] = child
	else
		inventory[#inventory + 1] = child
	end
end

function removeFromInventory(child)
	for i = 1, #inventory do
		if inventory[i] == child then
			table.remove(inventory,i)
			inventory[i] = nil
		end
	end
end

-- these next two functions are used for safe guarding 
-- when we are waiting for character to come back after dying
function activateLoadout()
	keyPressCon = game:GetService("GuiService").KeyPressed:connect(function(key) activateGear(key) end)
	currentLoadout.Visible = true
    --fixes a bug where weapons become draggable upon respawning
    guiBackpack.Visible = false
end

function deactivateLoadout()
	if keyPressCon then keyPressCon:disconnect() end
	currentLoadout.Visible = false
end

function setupBackpackListener()
	if backpackChildCon then backpackChildCon:disconnect() end
	backpackChildCon = player.Backpack.ChildAdded:connect(function(child)
		addingPlayerChild(child)
		addToInventory(child)
	end)
end

function playerCharacterChildAdded(child)
	addingPlayerChild(child,true)
	addToInventory(child)
end
-- End Functions






-- Begin Script
wait() -- let stuff initialize incase this is first heartbeat...
	
waitForChild(player,"Backpack")
local backpackChildren = player.Backpack:GetChildren()
local size = math.min(10,#backpackChildren)
for i = 1, size do
	addingPlayerChild(backpackChildren[i],false)
end
setupBackpackListener()

waitForProperty(player,"Character")
for i,v in ipairs(player.Character:GetChildren()) do
	playerCharacterChildAdded(v)
end
characterChildAddedCon = player.Character.ChildAdded:connect(function(child) playerCharacterChildAdded(child) end)

waitForChild(player.Character,"Humanoid")
humanoidDiedCon = player.Character.Humanoid.Died:connect(function() deactivateLoadout() end)

--commenting this seems to fix the backpack???
--[[player.CharacterRemoving:connect(function()
	if characterChildAddedCon then characterChildAddedCon:disconnect() end
	if humanoidDiedCon then humanoidDiedCon:disconnect() end
	if backpackChildCon then backpackChildCon:disconnect() end
	deactivateLoadout()
end)]]--
player.CharacterAdded:connect(function()
	player = game.Players.LocalPlayer -- make sure we are still looking at the correct character
	
	for i = 1, #gearSlots do
		if gearSlots[i] ~= "empty" then
			gearSlots[i].Parent = nil
			gearSlots[i] = "empty"
		end
	end

	waitForChild(player,"Backpack")
	local backpackChildren = player.Backpack:GetChildren()
	local size = math.min(10,#backpackChildren)
	for i = 1, size do
		addingPlayerChild(backpackChildren[i])
	end
	
	setupBackpackListener()
	
	characterChildAddedCon = 
		player.Character.ChildAdded:connect(function(child)
			addingPlayerChild(child,true)
		end)
		
	waitForChild(player.Character,"Humanoid")
	humanoidDiedCon = 
		player.Character.Humanoid.Died:connect(function()
			deactivateLoadout()
		end)
	activateLoadout()
end)

waitForChild(guiBackpack,"SwapSlot")
guiBackpack.SwapSlot.Changed:connect(function()
	if guiBackpack.SwapSlot.Value then
		local swapSlot = guiBackpack.SwapSlot
		local pos = swapSlot.Slot.Value
		if pos == 0 then pos = 10 end
		if gearSlots[pos] then
			reorganizeLoadout(gearSlots[pos],false)
		end
		if swapSlot.GearButton.Value then
			addingPlayerChild(swapSlot.GearButton.Value.GearReference.Value,false,pos)
		end
		guiBackpack.SwapSlot.Value = false
	end
end)

keyPressCon = game:GetService("GuiService").KeyPressed:connect(function(key) activateGear(key) end)

end)