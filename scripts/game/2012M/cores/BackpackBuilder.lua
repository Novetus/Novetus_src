
--- ATTENTION!!! There are site-specific ids at the end of this script!!!!!!!!!!!!!!!!!!!!

delay(0, function()

if game.CoreGui.Version < 3 then return end -- peace out if we aren't using the right client

local gui = game:GetService("CoreGui").RobloxGui

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

waitForChild(game,"Players")
waitForProperty(game.Players,"LocalPlayer")
local player = game.Players.LocalPlayer

-- First up is the current loadout
local CurrentLoadout = Instance.new("Frame")
CurrentLoadout.Name = "CurrentLoadout"
CurrentLoadout.Position = UDim2.new(0.5, -240, 1, -85)
CurrentLoadout.Size = UDim2.new(0, 480, 0, 48)
CurrentLoadout.BackgroundTransparency = 1
CurrentLoadout.RobloxLocked = true
CurrentLoadout.Parent = gui

local Debounce = Instance.new("BoolValue")
Debounce.Name = "Debounce"
Debounce.RobloxLocked = true
Debounce.Parent = CurrentLoadout

local BackpackButton = Instance.new("ImageButton")
BackpackButton.RobloxLocked = true
BackpackButton.Visible = false
BackpackButton.Name = "BackpackButton"
BackpackButton.BackgroundTransparency = 1
BackpackButton.Image = "rbxasset://textures/ui/backpackButton.png"
BackpackButton.Position = UDim2.new(0.5, -195, 1, -30)
BackpackButton.Size = UDim2.new(0,107,0,26)
waitForChild(gui,"ControlFrame")
BackpackButton.Parent = gui.ControlFrame

for i = 0, 9 do
	local slotFrame = Instance.new("Frame")
	slotFrame.RobloxLocked = true
	slotFrame.BackgroundColor3 = Color3.new(0,0,0)
	slotFrame.BackgroundTransparency = 1
	slotFrame.BorderColor3 = Color3.new(1,1,1)
	slotFrame.Name = "Slot" .. tostring(i)
	if i == 0 then
		slotFrame.Position = UDim2.new(0.9,0,0,0)
	else
		slotFrame.Position = UDim2.new((i - 1) * 0.1,0,0,0)
	end
	slotFrame.Size = UDim2.new(0.1,0,1,0)
	slotFrame.Parent = CurrentLoadout
end

local TempSlot = Instance.new("ImageButton")
TempSlot.Name = "TempSlot"
TempSlot.Active = true
TempSlot.Size = UDim2.new(1,0,1,0)
TempSlot.Style = Enum.ButtonStyle.RobloxButton
TempSlot.Visible = false
TempSlot.RobloxLocked = true
TempSlot.Parent = CurrentLoadout

	-- TempSlot Children
	local GearReference = Instance.new("ObjectValue")
	GearReference.Name = "GearReference"
	GearReference.RobloxLocked = true
	GearReference.Parent = TempSlot

	local ToolTipLabel = Instance.new("TextLabel")
	ToolTipLabel.Name = "ToolTipLabel"
	ToolTipLabel.RobloxLocked = true
	ToolTipLabel.Text = ""
	ToolTipLabel.BackgroundTransparency = 0.5
	ToolTipLabel.BorderSizePixel = 0
	ToolTipLabel.Visible = false
	ToolTipLabel.TextColor3 = Color3.new(1,1,1)
	ToolTipLabel.BackgroundColor3 = Color3.new(0,0,0)
	ToolTipLabel.TextStrokeTransparency = 0
	ToolTipLabel.Font = Enum.Font.ArialBold
	ToolTipLabel.FontSize = Enum.FontSize.Size14
	--ToolTipLabel.TextWrap = true
	ToolTipLabel.Size = UDim2.new(1,60,0,20)
	ToolTipLabel.Position = UDim2.new(0,-30,0,-30)
	ToolTipLabel.Parent = TempSlot

	local Kill = Instance.new("BoolValue")
	Kill.Name = "Kill"
	Kill.RobloxLocked = true
	Kill.Parent = TempSlot

	local GearImage = Instance.new("ImageLabel")
	GearImage.Name = "GearImage"
	GearImage.BackgroundTransparency = 1
	GearImage.Position = UDim2.new(0,-7,0,-7)
	GearImage.Size = UDim2.new(1,14,1,14)
	GearImage.ZIndex = 2
	GearImage.RobloxLocked = true
	GearImage.Parent = TempSlot

	local SlotNumber = Instance.new("TextLabel")
	SlotNumber.Name = "SlotNumber"
	SlotNumber.BackgroundTransparency = 1
	SlotNumber.BorderSizePixel = 0
	SlotNumber.Font = Enum.Font.ArialBold
	SlotNumber.FontSize = Enum.FontSize.Size18
	SlotNumber.Position = UDim2.new(0,-7,0,-7)
	SlotNumber.Size = UDim2.new(0,10,0,15)
	SlotNumber.TextColor3 = Color3.new(1,1,1)
	SlotNumber.TextTransparency = 0
	SlotNumber.TextXAlignment = Enum.TextXAlignment.Left
	SlotNumber.TextYAlignment = Enum.TextYAlignment.Bottom
	SlotNumber.ZIndex = 4
	SlotNumber.RobloxLocked = true
	SlotNumber.Parent = TempSlot
	
	local SlotNumberDownShadow = SlotNumber:clone()
	SlotNumberDownShadow.Name = "SlotNumberDownShadow"
	SlotNumberDownShadow.TextColor3 = Color3.new(0,0,0)
	SlotNumberDownShadow.ZIndex = 3
	SlotNumberDownShadow.Position = UDim2.new(0,-6,0,-6)
	SlotNumberDownShadow.Parent = TempSlot
	
	local SlotNumberUpShadow = SlotNumberDownShadow:clone()
	SlotNumberUpShadow.Name = "SlotNumberUpShadow"
	SlotNumberUpShadow.Position = UDim2.new(0,-8,0,-8)
	SlotNumberUpShadow.Parent = TempSlot

	local GearText = Instance.new("TextLabel")
	GearText.RobloxLocked = true
	GearText.Name = "GearText"
	GearText.BackgroundTransparency = 1
	GearText.Font = Enum.Font.Arial
	GearText.FontSize = Enum.FontSize.Size14
	GearText.Position = UDim2.new(0,-8,0,-8)
	GearText.ZIndex = 2
	GearText.Size = UDim2.new(1,16,1,16)
	GearText.Text = ""
	GearText.TextColor3 = Color3.new(1,1,1)
	GearText.TextWrap = true
	GearText.Parent = TempSlot

--- Great, now lets make the inventory!

local Backpack = Instance.new("Frame")
Backpack.Visible = false
Backpack.Name = "Backpack"
Backpack.Position = UDim2.new(0.5,0,0.5,0)
Backpack.Size = UDim2.new(0,0,0,0)
Backpack.Style = Enum.FrameStyle.RobloxSquare
Backpack.RobloxLocked = true
Backpack.Parent = gui
Backpack.Active = true

	-- Backpack Children
	local SwapSlot = Instance.new("BoolValue")
	SwapSlot.RobloxLocked = true
	SwapSlot.Name = "SwapSlot"
	SwapSlot.Parent = Backpack
		
		-- SwapSlot Children
		local Slot = Instance.new("IntValue")
		Slot.RobloxLocked = true
		Slot.Name = "Slot"
		Slot.Parent = SwapSlot
		
		local GearButton = Instance.new("ObjectValue")
		GearButton.RobloxLocked = true
		GearButton.Name = "GearButton"
		GearButton.Parent = SwapSlot
	
	local Tabs = Instance.new("Frame")
	Tabs.Name = "Tabs"
	Tabs.Visible = false
	Tabs.RobloxLocked = true
	Tabs.BackgroundColor3 = Color3.new(0,0,0)
	Tabs.BackgroundTransparency = 0.5
	Tabs.BorderSizePixel = 0
	Tabs.Position = UDim2.new(0,-8,-0.1,-8)
	Tabs.Size = UDim2.new(1,16,0.1,0)
	Tabs.Parent = Backpack
	
		-- Tabs Children
		local InventoryButton = Instance.new("ImageButton")
		InventoryButton.RobloxLocked = true
		InventoryButton.Name = "InventoryButton"
		InventoryButton.Size = UDim2.new(0.12,0,1,0)
		InventoryButton.Style = Enum.ButtonStyle.RobloxButton
		InventoryButton.Selected = true
		InventoryButton.Parent = Tabs
		
			-- InventoryButton Children
			local InventoryText = Instance.new("TextLabel")
			InventoryText.RobloxLocked = true
			InventoryText.Name = "InventoryText"
			InventoryText.BackgroundTransparency = 1
			InventoryText.Font = Enum.Font.ArialBold
			InventoryText.FontSize = Enum.FontSize.Size18
			InventoryText.Position = UDim2.new(0,-7,0,-7)
			InventoryText.Size = UDim2.new(1,14,1,14)
			InventoryText.Text = "Gear"
			InventoryText.TextColor3 = Color3.new(1,1,1)
			InventoryText.Parent = InventoryButton
			
		local closeButton = Instance.new("TextButton")
		closeButton.RobloxLocked = true
		closeButton.Name = "CloseButton"
		closeButton.Font = Enum.Font.ArialBold
		closeButton.FontSize = Enum.FontSize.Size24
		closeButton.Position = UDim2.new(1,-33,0,2)
		closeButton.Size = UDim2.new(0,30,0,30)
		closeButton.Style = Enum.ButtonStyle.RobloxButton
		closeButton.Text = "X"
		closeButton.TextColor3 = Color3.new(1,1,1)
		closeButton.Parent = Tabs
	
	local Gear = Instance.new("Frame")
	Gear.Name = "Gear"
	Gear.RobloxLocked = true
	Gear.BackgroundTransparency = 1
	Gear.Size  = UDim2.new(1,0,1,0)
	Gear.Parent = Backpack

		-- Gear Children
		local GearGrid = Instance.new("Frame")
		GearGrid.RobloxLocked = true
		GearGrid.Name = "GearGrid"
		GearGrid.Size = UDim2.new(0.69,0,1,0)
		GearGrid.Style = Enum.FrameStyle.RobloxSquare
		GearGrid.Parent = Gear
		
			-- GearGrid Children
			local ResetFrame = Instance.new("Frame")
			ResetFrame.RobloxLocked = true
			ResetFrame.Name = "ResetFrame"
			ResetFrame.Visible = false
			ResetFrame.BackgroundTransparency = 1
			ResetFrame.Size = UDim2.new(0,32,0,64)
			ResetFrame.Parent = GearGrid
			
				-- ResetFrame Children
				local ResetImageLabel = Instance.new("ImageLabel")
				ResetImageLabel.RobloxLocked = true
				ResetImageLabel.Name = "ResetImageLabel"
				ResetImageLabel.Image = "rbxasset://textures/ui/ResetIcon.png"
				ResetImageLabel.BackgroundTransparency = 1
				ResetImageLabel.Position = UDim2.new(0,0,0,-12)
				ResetImageLabel.Size = UDim2.new(0.8,0,0.79,0)
				ResetImageLabel.ZIndex = 2
				ResetImageLabel.Parent = ResetFrame
				
				local ResetButtonBorder = Instance.new("TextButton")
				ResetButtonBorder.RobloxLocked = true
				ResetButtonBorder.Name = "ResetButtonBorder"
				ResetButtonBorder.Position = UDim2.new(0,-3,0,-7)
				ResetButtonBorder.Size = UDim2.new(1,0,0.64,0)
				ResetButtonBorder.Style = Enum.ButtonStyle.RobloxButton
				ResetButtonBorder.Text = ""
				ResetButtonBorder.Parent = ResetFrame
				
				
			local SearchFrame = Instance.new("Frame")
			SearchFrame.RobloxLocked = true
			SearchFrame.Name = "SearchFrame"
			SearchFrame.BackgroundTransparency = 1
			SearchFrame.Position = UDim2.new(1,-150,0,0)
			SearchFrame.Size = UDim2.new(0,150,0,25)
			SearchFrame.Parent = GearGrid
			
				-- SearchFrame Children
				local SearchButton = Instance.new("ImageButton")
				SearchButton.RobloxLocked = true
				SearchButton.Name = "SearchButton"
				SearchButton.Size = UDim2.new(0,25,0,25)
				SearchButton.BackgroundTransparency = 1
				SearchButton.Image = "rbxasset://textures/ui/SearchIcon.png"
				SearchButton.Parent = SearchFrame
				
				local SearchBoxFrame = Instance.new("TextButton")
				SearchBoxFrame.RobloxLocked = true
				SearchBoxFrame.Position = UDim2.new(0,26,0,0)
				SearchBoxFrame.Size = UDim2.new(0,121,0,25)
				SearchBoxFrame.Name = "SearchBoxFrame"
				SearchBoxFrame.Text = ""
				SearchBoxFrame.Style = Enum.ButtonStyle.RobloxButton
				SearchBoxFrame.Parent = SearchFrame
				
					-- SearchBoxFrame Children
					local SearchBox = Instance.new("TextBox")
					SearchBox.RobloxLocked = true
					SearchBox.Name = "SearchBox"
					SearchBox.BackgroundTransparency = 1
					SearchBox.Font = Enum.Font.ArialBold
					SearchBox.FontSize = Enum.FontSize.Size12
					SearchBox.Position = UDim2.new(0,-5,0,-5)
					SearchBox.Size = UDim2.new(1,10,1,10)
					SearchBox.TextColor3 = Color3.new(1,1,1)
					SearchBox.TextXAlignment = Enum.TextXAlignment.Left
					SearchBox.ZIndex = 2
					SearchBox.TextWrap = true
					SearchBox.Text = "Search..."
					SearchBox.Parent = SearchBoxFrame
			
			local GearButton = Instance.new("ImageButton")
			GearButton.RobloxLocked = true
			GearButton.Visible = false
			GearButton.Name = "GearButton"
			GearButton.Size = UDim2.new(0,64,0,64)
			GearButton.Style = Enum.ButtonStyle.RobloxButton
			GearButton.Parent = GearGrid

				-- GearButton Children
				local GearReference = Instance.new("ObjectValue")
				GearReference.RobloxLocked = true
				GearReference.Name = "GearReference"
				GearReference.Parent = GearButton
				
				local GreyOutButton = Instance.new("Frame")
				GreyOutButton.RobloxLocked = true
				GreyOutButton.Name = "GreyOutButton"
				GreyOutButton.BackgroundTransparency = 0.5
				GreyOutButton.Size = UDim2.new(1,0,1,0)
				GreyOutButton.Active = true
				GreyOutButton.Visible = false
				GreyOutButton.ZIndex = 3
				GreyOutButton.Parent = GearButton
				
				local GearText = Instance.new("TextLabel")
				GearText.RobloxLocked = true
				GearText.Name = "GearText"
				GearText.BackgroundTransparency = 1
				GearText.Font = Enum.Font.Arial
				GearText.FontSize = Enum.FontSize.Size14
				GearText.Position = UDim2.new(0,-8,0,-8)
				GearText.Size = UDim2.new(1,16,1,16)
				GearText.Text = ""
				GearText.ZIndex = 2
				GearText.TextColor3 = Color3.new(1,1,1)
				GearText.TextWrap = true
				GearText.Parent = GearButton

		local GearGridScrollingArea = Instance.new("Frame")
		GearGridScrollingArea.RobloxLocked = true
		GearGridScrollingArea.Name = "GearGridScrollingArea"
		GearGridScrollingArea.Position = UDim2.new(0.7,0,0,0)
		GearGridScrollingArea.Size = UDim2.new(0,17,1,0)
		GearGridScrollingArea.BackgroundTransparency = 1
		GearGridScrollingArea.Parent = Gear

		local GearLoadouts = Instance.new("Frame")
		GearLoadouts.RobloxLocked = true
		GearLoadouts.Name = "GearLoadouts"
		GearLoadouts.BackgroundTransparency = 1
		GearLoadouts.Position = UDim2.new(0.7,23,0.5,1)
		GearLoadouts.Size = UDim2.new(0.3,-23,0.5,-1)
		GearLoadouts.Parent = Gear
		GearLoadouts.Visible = false
		
			-- GearLoadouts Children
			local GearLoadoutsHeader = Instance.new("Frame")
			GearLoadoutsHeader.RobloxLocked = true
			GearLoadoutsHeader.Name = "GearLoadoutsHeader"
			GearLoadoutsHeader.BackgroundColor3 = Color3.new(0,0,0)
			GearLoadoutsHeader.BackgroundTransparency = 0.2
			GearLoadoutsHeader.BorderColor3 = Color3.new(1,0,0)
			GearLoadoutsHeader.Size = UDim2.new(1,2,0.15,-1)
			GearLoadoutsHeader.Parent = GearLoadouts

				-- GearLoadoutsHeader Children
				local LoadoutsHeaderText = Instance.new("TextLabel")
				LoadoutsHeaderText.RobloxLocked = true
				LoadoutsHeaderText.Name = "LoadoutsHeaderText"
				LoadoutsHeaderText.BackgroundTransparency = 1
				LoadoutsHeaderText.Font = Enum.Font.ArialBold
				LoadoutsHeaderText.FontSize = Enum.FontSize.Size18
				LoadoutsHeaderText.Size = UDim2.new(1,0,1,0)
				LoadoutsHeaderText.Text = "Loadouts"
				LoadoutsHeaderText.TextColor3 = Color3.new(1,1,1)
				LoadoutsHeaderText.Parent = GearLoadoutsHeader
	
				local GearLoadoutsScrollingArea = GearGridScrollingArea:clone()
				GearLoadoutsScrollingArea.RobloxLocked = true
				GearLoadoutsScrollingArea.Name = "GearLoadoutsScrollingArea"
				GearLoadoutsScrollingArea.Position = UDim2.new(1,-15,0.15,2)
				GearLoadoutsScrollingArea.Size = UDim2.new(0,17,0.85,-2)
				GearLoadoutsScrollingArea.Parent = GearLoadouts

				local LoadoutsList = Instance.new("Frame")
				LoadoutsList.RobloxLocked = true
				LoadoutsList.Name = "LoadoutsList"
				LoadoutsList.Position = UDim2.new(0,0,0.15,2)
				LoadoutsList.Size = UDim2.new(1,-17,0.85,-2)
				LoadoutsList.Style = Enum.FrameStyle.RobloxSquare
				LoadoutsList.Parent = GearLoadouts


		local GearPreview = Instance.new("Frame")
		GearPreview.RobloxLocked = true
		GearPreview.Name = "GearPreview"
		GearPreview.Position = UDim2.new(0.7,23,0,0)
		GearPreview.Size = UDim2.new(0.3,-23,0.5,-1)
		GearPreview.Style = Enum.FrameStyle.RobloxRound
		GearPreview.Parent = Gear
		
			-- GearPreview Children
			local GearStats = Instance.new("Frame")
			GearStats.RobloxLocked = true
			GearStats.Name = "GearStats"
			GearStats.BackgroundTransparency = 1
			GearStats.Position = UDim2.new(0,0,0.75,0)
			GearStats.Size = UDim2.new(1,0,0.25,0)
			GearStats.Parent = GearPreview
				
				-- GearStats Children
				local GearName = Instance.new("TextLabel")
				GearName.RobloxLocked = true
				GearName.Name = "GearName"
				GearName.BackgroundTransparency = 1
				GearName.Font = Enum.Font.ArialBold
				GearName.FontSize = Enum.FontSize.Size18
				GearName.Position = UDim2.new(0,-3,0,0)
				GearName.Size = UDim2.new(1,6,1,5)
				GearName.Text = ""
				GearName.TextColor3 = Color3.new(1,1,1)
				GearName.TextWrap = true
				GearName.Parent = GearStats
				
			local GearImage = Instance.new("ImageLabel")
			GearImage.RobloxLocked = true
			GearImage.Name = "GearImage"
			GearImage.Image = ""
			GearImage.BackgroundTransparency = 1
			GearImage.Position = UDim2.new(0.125,0,0,0)
			GearImage.Size = UDim2.new(0.75,0,0.75,0)
			GearImage.Parent = GearPreview
			
				--GearImage Children
				local GearIcons = Instance.new("Frame")
				GearIcons.BackgroundColor3 = Color3.new(0,0,0)
				GearIcons.BackgroundTransparency = 0.5
				GearIcons.BorderSizePixel = 0
				GearIcons.RobloxLocked = true
				GearIcons.Name = "GearIcons"
				GearIcons.Position = UDim2.new(0.4,2,0.85,-2)
				GearIcons.Size = UDim2.new(0.6,0,0.15,0)
				GearIcons.Visible = false
				GearIcons.Parent = GearImage
				
					-- GearIcons Children
					local GenreImage = Instance.new("ImageLabel")
					GenreImage.RobloxLocked = true
					GenreImage.Name = "GenreImage"
					GenreImage.BackgroundColor3 = Color3.new(102/255,153/255,1)
					GenreImage.BackgroundTransparency = 0.5
					GenreImage.BorderSizePixel = 0
					GenreImage.Size = UDim2.new(0.25,0,1,0)
					GenreImage.Parent = GearIcons
					
					local AttributeOneImage = GenreImage:clone()
					AttributeOneImage.RobloxLocked = true
					AttributeOneImage.Name = "AttributeOneImage"
					AttributeOneImage.BackgroundColor3 = Color3.new(1,51/255,0)
					AttributeOneImage.Position = UDim2.new(0.25,0,0,0)
					AttributeOneImage.Parent = GearIcons
					
					local AttributeTwoImage = GenreImage:clone()
					AttributeTwoImage.RobloxLocked = true
					AttributeTwoImage.Name = "AttributeTwoImage"
					AttributeTwoImage.BackgroundColor3 = Color3.new(153/255,1,153/255)
					AttributeTwoImage.Position = UDim2.new(0.5,0,0,0)
					AttributeTwoImage.Parent = GearIcons
					
					local AttributeThreeImage = GenreImage:clone()
					AttributeThreeImage.RobloxLocked = true
					AttributeThreeImage.Name = "AttributeThreeImage"
					AttributeThreeImage.BackgroundColor3 = Color3.new(0,0.5,0.5)
					AttributeThreeImage.Position = UDim2.new(0.75,0,0,0)
					AttributeThreeImage.Parent = GearIcons
					

-- Backpack Resizer (handles all backpack resizing junk)
--game:GetService("ScriptContext"):AddCoreScript(53878053,Backpack,"BackpackResizer")
dofile("rbxasset://scripts\\cores\\BackpackResizer.lua")

end)