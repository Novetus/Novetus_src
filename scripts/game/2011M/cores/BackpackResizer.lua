-- RBXGUI START --
local RbxGuiLib = {}

local function ScopedConnect(parentInstance, instance, event, signalFunc, syncFunc, removeFunc)
	local eventConnection = nil

	--Connection on parentInstance is scoped by parentInstance (when destroyed, it goes away)
	local tryConnect = function()
		if game:IsAncestorOf(parentInstance) then
			--Entering the world, make sure we are connected/synced
			if not eventConnection then
				eventConnection = instance[event]:connect(signalFunc)
				if syncFunc then syncFunc() end
			end
		else
			--Probably leaving the world, so disconnect for now
			if eventConnection then
				eventConnection:disconnect()
				if removeFunc then removeFunc() end
			end
		end
	end

	--Hook it up to ancestryChanged signal
	local connection = parentInstance.AncestryChanged:connect(tryConnect)
	
	--Now connect us if we're already in the world
	tryConnect()
	
	return connection
end

local function CreateButtons(frame, buttons, yPos, ySize)
	local buttonNum = 1
	local buttonObjs = {}
	for i, obj in ipairs(buttons) do 
		local button = Instance.new("TextButton")
		button.Name = "Button" .. buttonNum
		button.Font = Enum.Font.Arial
		button.FontSize = Enum.FontSize.Size18
		button.AutoButtonColor = true
		button.Style = Enum.ButtonStyle.RobloxButtonDefault 
		button.Text = obj.Text
		button.TextColor3 = Color3.new(1,1,1)
		button.MouseButton1Click:connect(obj.Function)
		button.Parent = frame
		buttonObjs[buttonNum] = button

		buttonNum = buttonNum + 1
	end
	local numButtons = buttonNum-1

	if numButtons == 1 then
		frame.Button1.Position = UDim2.new(0.35, 0, yPos.Scale, yPos.Offset)
		frame.Button1.Size = UDim2.new(.4,0,ySize.Scale, ySize.Offset)
	elseif numButtons == 2 then
		frame.Button1.Position = UDim2.new(0.1, 0, yPos.Scale, yPos.Offset)
		frame.Button1.Size = UDim2.new(.8/3,0, ySize.Scale, ySize.Offset)

		frame.Button2.Position = UDim2.new(0.55, 0, yPos.Scale, yPos.Offset)
		frame.Button2.Size = UDim2.new(.35,0, ySize.Scale, ySize.Offset)
	elseif numButtons >= 3 then
		local spacing = .1 / numButtons
		local buttonSize = .9 / numButtons

		buttonNum = 1
		while buttonNum <= numButtons do
			buttonObjs[buttonNum].Position = UDim2.new(spacing*buttonNum + (buttonNum-1) * buttonSize, 0, yPos.Scale, yPos.Offset)
			buttonObjs[buttonNum].Size = UDim2.new(buttonSize, 0, ySize.Scale, ySize.Offset)
			buttonNum = buttonNum + 1
		end
	end
end

local function setSliderPos(newAbsPosX,slider,sliderPosition,bar,steps)

	local newStep = steps - 1 --otherwise we really get one more step than we want
	
	local relativePosX = math.min(1, math.max(0, (newAbsPosX - bar.AbsolutePosition.X) / bar.AbsoluteSize.X ) )
	local wholeNum, remainder = math.modf(relativePosX * newStep)
	if remainder > 0.5 then
		wholeNum = wholeNum + 1
	end
	relativePosX = wholeNum/newStep

	local result = math.ceil(relativePosX * newStep)
	if sliderPosition.Value ~= (result + 1) then --onky update if we moved a step
		sliderPosition.Value = result + 1
		
		if relativePosX == 1 then
			slider.Position = UDim2.new(1,-slider.AbsoluteSize.X,slider.Position.Y.Scale,slider.Position.Y.Offset)
		else
			slider.Position = UDim2.new(relativePosX,0,slider.Position.Y.Scale,slider.Position.Y.Offset)
		end
	end
	
end

local function cancelSlide(areaSoak)
	areaSoak.Visible = false
	if areaSoakMouseMoveCon then areaSoakMouseMoveCon:disconnect() end
end

RbxGuiLib.CreateStyledMessageDialog = function(title, message, style, buttons)
	local frame = Instance.new("Frame")
	frame.Size = UDim2.new(0.5, 0, 0, 165)
	frame.Position = UDim2.new(0.25, 0, 0.5, -72.5)
	frame.Name = "MessageDialog"
	frame.Active = true
	frame.Style = Enum.FrameStyle.RobloxRound	
	
	local styleImage = Instance.new("ImageLabel")
	styleImage.Name = "StyleImage"
	styleImage.BackgroundTransparency = 1
	styleImage.Position = UDim2.new(0,5,0,15)
	if style == "error" or style == "Error" then
		styleImage.Size = UDim2.new(0, 71, 0, 71)
		styleImage.Image = "rbxasset://textures/ui/Error.png"
	elseif style == "notify" or style == "Notify" then
		styleImage.Size = UDim2.new(0, 71, 0, 71)
		styleImage.Image = "rbxasset://textures/ui/Notify.png"
	elseif style == "confirm" or style == "Confirm" then
		styleImage.Size = UDim2.new(0, 74, 0, 76)
		styleImage.Image = "rbxasset://textures/ui/Confirm.png"
	else
		return RbxGuiLib.CreateMessageDialog(title,message,buttons)
	end
	styleImage.Parent = frame
	
	local titleLabel = Instance.new("TextLabel")
	titleLabel.Name = "Title"
	titleLabel.Text = title
	titleLabel.BackgroundTransparency = 1
	titleLabel.TextColor3 = Color3.new(221/255,221/255,221/255)
	titleLabel.Position = UDim2.new(0, 80, 0, 0)
	titleLabel.Size = UDim2.new(1, -80, 0, 40)
	titleLabel.Font = Enum.Font.ArialBold
	titleLabel.FontSize = Enum.FontSize.Size36
	titleLabel.TextXAlignment = Enum.TextXAlignment.Center
	titleLabel.TextYAlignment = Enum.TextYAlignment.Center
	titleLabel.Parent = frame

	local messageLabel = Instance.new("TextLabel")
	messageLabel.Name = "Message"
	messageLabel.Text = message
	messageLabel.TextColor3 = Color3.new(221/255,221/255,221/255)
	messageLabel.Position = UDim2.new(0.025, 80, 0, 45)
	messageLabel.Size = UDim2.new(0.95, -80, 0, 55)
	messageLabel.BackgroundTransparency = 1
	messageLabel.Font = Enum.Font.Arial
	messageLabel.FontSize = Enum.FontSize.Size18
	messageLabel.TextWrap = true
	messageLabel.TextXAlignment = Enum.TextXAlignment.Left
	messageLabel.TextYAlignment = Enum.TextYAlignment.Top
	messageLabel.Parent = frame

	CreateButtons(frame, buttons, UDim.new(0, 105), UDim.new(0, 40) )

	return frame
end

RbxGuiLib.CreateMessageDialog = function(title, message, buttons)
	local frame = Instance.new("Frame")
	frame.Size = UDim2.new(0.5, 0, 0.5, 0)
	frame.Position = UDim2.new(0.25, 0, 0.25, 0)
	frame.Name = "MessageDialog"
	frame.Active = true
	frame.Style = Enum.FrameStyle.RobloxRound

	local titleLabel = Instance.new("TextLabel")
	titleLabel.Name = "Title"
	titleLabel.Text = title
	titleLabel.BackgroundTransparency = 1
	titleLabel.TextColor3 = Color3.new(221/255,221/255,221/255)
	titleLabel.Position = UDim2.new(0, 0, 0, 0)
	titleLabel.Size = UDim2.new(1, 0, 0.15, 0)
	titleLabel.Font = Enum.Font.ArialBold
	titleLabel.FontSize = Enum.FontSize.Size36
	titleLabel.TextXAlignment = Enum.TextXAlignment.Center
	titleLabel.TextYAlignment = Enum.TextYAlignment.Center
	titleLabel.Parent = frame

	local messageLabel = Instance.new("TextLabel")
	messageLabel.Name = "Message"
	messageLabel.Text = message
	messageLabel.TextColor3 = Color3.new(221/255,221/255,221/255)
	messageLabel.Position = UDim2.new(0.025, 0, 0.175, 0)
	messageLabel.Size = UDim2.new(0.95, 0, .55, 0)
	messageLabel.BackgroundTransparency = 1
	messageLabel.Font = Enum.Font.Arial
	messageLabel.FontSize = Enum.FontSize.Size18
	messageLabel.TextWrap = true
	messageLabel.TextXAlignment = Enum.TextXAlignment.Left
	messageLabel.TextYAlignment = Enum.TextYAlignment.Top
	messageLabel.Parent = frame

	CreateButtons(frame, buttons, UDim.new(0.8,0), UDim.new(0.15, 0))

	return frame
end

RbxGuiLib.CreateDropDownMenu = function(items, onSelect, forRoblox)
	local width = UDim.new(0, 100)
	local height = UDim.new(0, 32)

	local xPos = 0.055
	local frame = Instance.new("Frame")
	frame.Name = "DropDownMenu"
	frame.BackgroundTransparency = 1
	frame.Size = UDim2.new(width, height)

	local dropDownMenu = Instance.new("TextButton")
	dropDownMenu.Name = "DropDownMenuButton"
	dropDownMenu.TextWrap = true
	dropDownMenu.TextColor3 = Color3.new(1,1,1)
	dropDownMenu.Text = "Choose One"
	dropDownMenu.Font = Enum.Font.ArialBold
	dropDownMenu.FontSize = Enum.FontSize.Size18
	dropDownMenu.TextXAlignment = Enum.TextXAlignment.Left
	dropDownMenu.TextYAlignment = Enum.TextYAlignment.Center
	dropDownMenu.BackgroundTransparency = 1
	dropDownMenu.AutoButtonColor = true
	dropDownMenu.Style = Enum.ButtonStyle.RobloxButton
	dropDownMenu.Size = UDim2.new(1,0,1,0)
	dropDownMenu.Parent = frame
	dropDownMenu.ZIndex = 2

	local dropDownIcon = Instance.new("ImageLabel")
	dropDownIcon.Name = "Icon"
	dropDownIcon.Active = false
	dropDownIcon.Image = "rbxasset://textures/ui/DropDown.png"
	dropDownIcon.BackgroundTransparency = 1
	dropDownIcon.Size = UDim2.new(0,11,0,6)
	dropDownIcon.Position = UDim2.new(1,-11,0.5, -2)
	dropDownIcon.Parent = dropDownMenu
	dropDownIcon.ZIndex = 2
	
	local itemCount = #items
	local dropDownItemCount = #items
	local useScrollButtons = false
	if dropDownItemCount > 6 then
		useScrollButtons = true
		dropDownItemCount = 6
	end
	
	local droppedDownMenu = Instance.new("TextButton")
	droppedDownMenu.Name = "List"
	droppedDownMenu.Text = ""
	droppedDownMenu.BackgroundTransparency = 1
	--droppedDownMenu.AutoButtonColor = true
	droppedDownMenu.Style = Enum.ButtonStyle.RobloxButton
	droppedDownMenu.Visible = false
	droppedDownMenu.Active = true	--Blocks clicks
	droppedDownMenu.Position = UDim2.new(0,0,0,0)
	droppedDownMenu.Size = UDim2.new(1,0, (1 + dropDownItemCount)*.8, 0)
	droppedDownMenu.Parent = frame
	droppedDownMenu.ZIndex = 2

	local choiceButton = Instance.new("TextButton")
	choiceButton.Name = "ChoiceButton"
	choiceButton.BackgroundTransparency = 1
	choiceButton.BorderSizePixel = 0
	choiceButton.Text = "ReplaceMe"
	choiceButton.TextColor3 = Color3.new(1,1,1)
	choiceButton.TextXAlignment = Enum.TextXAlignment.Left
	choiceButton.TextYAlignment = Enum.TextYAlignment.Center
	choiceButton.BackgroundColor3 = Color3.new(1, 1, 1)
	choiceButton.Font = Enum.Font.Arial
	choiceButton.FontSize = Enum.FontSize.Size18
	if useScrollButtons then
		choiceButton.Size = UDim2.new(1,-13, .8/((dropDownItemCount + 1)*.8),0) 
	else
		choiceButton.Size = UDim2.new(1, 0, .8/((dropDownItemCount + 1)*.8),0) 
	end
	choiceButton.TextWrap = true
	choiceButton.ZIndex = 2

	local dropDownSelected = false

	local scrollUpButton 
	local scrollDownButton
	local scrollMouseCount = 0

	local setZIndex = function(baseZIndex)
		droppedDownMenu.ZIndex = baseZIndex +1
		if scrollUpButton then
			scrollUpButton.ZIndex = baseZIndex + 3
		end
		if scrollDownButton then
			scrollDownButton.ZIndex = baseZIndex + 3
		end
		
		local children = droppedDownMenu:GetChildren()
		if children then
			for i, child in ipairs(children) do
				if child.Name == "ChoiceButton" then
					child.ZIndex = baseZIndex + 2
				elseif child.Name == "ClickCaptureButton" then
					child.ZIndex = baseZIndex
				end
			end
		end
	end

	local scrollBarPosition = 1
	local updateScroll = function()
		if scrollUpButton then
			scrollUpButton.Active = scrollBarPosition > 1 
		end
		if scrollDownButton then
			scrollDownButton.Active = scrollBarPosition + dropDownItemCount <= itemCount 
		end

		local children = droppedDownMenu:GetChildren()
		if not children then return end

		local childNum = 1			
		for i, obj in ipairs(children) do
			if obj.Name == "ChoiceButton" then
				if childNum < scrollBarPosition or childNum >= scrollBarPosition + dropDownItemCount then
					obj.Visible = false
				else
					obj.Position = UDim2.new(0,0,((childNum-scrollBarPosition+1)*.8)/((dropDownItemCount+1)*.8),0)
					obj.Visible = true
				end
				obj.TextColor3 = Color3.new(1,1,1)
				obj.BackgroundTransparency = 1

				childNum = childNum + 1
			end
		end
	end
	local toggleVisibility = function()
		dropDownSelected = not dropDownSelected

		dropDownMenu.Visible = not dropDownSelected
		droppedDownMenu.Visible = dropDownSelected
		if dropDownSelected then
			setZIndex(4)
		else
			setZIndex(2)
		end
		if useScrollButtons then
			updateScroll()
		end
	end
	droppedDownMenu.MouseButton1Click:connect(toggleVisibility)

	local updateSelection = function(text)
		local foundItem = false
		local children = droppedDownMenu:GetChildren()
		local childNum = 1
		if children then
			for i, obj in ipairs(children) do
				if obj.Name == "ChoiceButton" then
					if obj.Text == text then
						obj.Font = Enum.Font.ArialBold
						foundItem = true			
						scrollBarPosition = childNum
					else
						obj.Font = Enum.Font.Arial
					end
					childNum = childNum + 1
				end
			end
		end
		if not text then
			dropDownMenu.Text = "Choose One"
			scrollBarPosition = 1
		else
			if not foundItem then
				error("Invalid Selection Update -- " .. text)
			end

			if scrollBarPosition + dropDownItemCount > itemCount + 1 then
				scrollBarPosition = itemCount - dropDownItemCount + 1
			end

			dropDownMenu.Text = text
		end
	end
	
	local function scrollDown()
		if scrollBarPosition + dropDownItemCount <= itemCount then
			scrollBarPosition = scrollBarPosition + 1
			updateScroll()
			return true
		end
		return false
	end
	local function scrollUp()
		if scrollBarPosition > 1 then
			scrollBarPosition = scrollBarPosition - 1
			updateScroll()
			return true
		end
		return false
	end
	
	if useScrollButtons then
		--Make some scroll buttons
		scrollUpButton = Instance.new("ImageButton")
		scrollUpButton.Name = "ScrollUpButton"
		scrollUpButton.BackgroundTransparency = 1
		scrollUpButton.Image = "rbxasset://textures/ui/scrollbuttonUp.png"
		scrollUpButton.Size = UDim2.new(0,17,0,17) 
		scrollUpButton.Position = UDim2.new(1,-11,(1*.8)/((dropDownItemCount+1)*.8),0)
		scrollUpButton.MouseButton1Click:connect(
			function()
				scrollMouseCount = scrollMouseCount + 1
			end)
		scrollUpButton.MouseLeave:connect(
			function()
				scrollMouseCount = scrollMouseCount + 1
			end)
		scrollUpButton.MouseButton1Down:connect(
			function()
				scrollMouseCount = scrollMouseCount + 1
	
				scrollUp()
				local val = scrollMouseCount
				wait(0.5)
				while val == scrollMouseCount do
					if scrollUp() == false then
						break
					end
					wait(0.1)
				end				
			end)

		scrollUpButton.Parent = droppedDownMenu

		scrollDownButton = Instance.new("ImageButton")
		scrollDownButton.Name = "ScrollDownButton"
		scrollDownButton.BackgroundTransparency = 1
		scrollDownButton.Image = "rbxasset://textures/ui/scrollbuttonDown.png"
		scrollDownButton.Size = UDim2.new(0,17,0,17) 
		scrollDownButton.Position = UDim2.new(1,-11,1,-11)
		scrollDownButton.Parent = droppedDownMenu
		scrollDownButton.MouseButton1Click:connect(
			function()
				scrollMouseCount = scrollMouseCount + 1
			end)
		scrollDownButton.MouseLeave:connect(
			function()
				scrollMouseCount = scrollMouseCount + 1
			end)
		scrollDownButton.MouseButton1Down:connect(
			function()
				scrollMouseCount = scrollMouseCount + 1

				scrollDown()
				local val = scrollMouseCount
				wait(0.5)
				while val == scrollMouseCount do
					if scrollDown() == false then
						break
					end
					wait(0.1)
				end				
			end)	

		local scrollbar = Instance.new("ImageLabel")
		scrollbar.Name = "ScrollBar"
		scrollbar.Image = "rbxasset://textures/ui/scrollbar.png"
		scrollbar.BackgroundTransparency = 1
		scrollbar.Size = UDim2.new(0, 18, (dropDownItemCount*.8)/((dropDownItemCount+1)*.8), -(17) - 11 - 4)
		scrollbar.Position = UDim2.new(1,-11,(1*.8)/((dropDownItemCount+1)*.8),17+2)
		scrollbar.Parent = droppedDownMenu
	end

	for i,item in ipairs(items) do
		-- needed to maintain local scope for items in event listeners below
		local button = choiceButton:clone()
		if forRoblox then
			button.RobloxLocked = true
		end		
		button.Text = item
		button.Parent = droppedDownMenu
		button.MouseButton1Click:connect(function()
			--Remove Highlight
			button.TextColor3 = Color3.new(1,1,1)
			button.BackgroundTransparency = 1

			updateSelection(item)
			onSelect(item)

			toggleVisibility()
		end)
		button.MouseEnter:connect(function()
			--Add Highlight	
			button.TextColor3 = Color3.new(0,0,0)
			button.BackgroundTransparency = 0
		end)

		button.MouseLeave:connect(function()
			--Remove Highlight
			button.TextColor3 = Color3.new(1,1,1)
			button.BackgroundTransparency = 1
		end)
	end

	--This does the initial layout of the buttons	
	updateScroll()

	local bigFakeButton = Instance.new("TextButton")
	bigFakeButton.BackgroundTransparency = 1
	bigFakeButton.Name = "ClickCaptureButton"
	bigFakeButton.Size = UDim2.new(0, 4000, 0, 3000)
	bigFakeButton.Position = UDim2.new(0, -2000, 0, -1500)
	bigFakeButton.ZIndex = 1
	bigFakeButton.Text = ""
	bigFakeButton.Parent = droppedDownMenu
	bigFakeButton.MouseButton1Click:connect(toggleVisibility)

	dropDownMenu.MouseButton1Click:connect(toggleVisibility)
	return frame, updateSelection
end

RbxGuiLib.CreatePropertyDropDownMenu = function(instance, property, enum)

	local items = enum:GetEnumItems()
	local names = {}
	local nameToItem = {}
	for i,obj in ipairs(items) do
		names[i] = obj.Name
		nameToItem[obj.Name] = obj
	end

	local frame
	local updateSelection
	frame, updateSelection = RbxGuiLib.CreateDropDownMenu(names, function(text) instance[property] = nameToItem[text] end)

	ScopedConnect(frame, instance, "Changed", 
		function(prop)
			if prop == property then
				updateSelection(instance[property].Name)
			end
		end,
		function()
			updateSelection(instance[property].Name)
		end)

	return frame
end

RbxGuiLib.GetFontHeight = function(font, fontSize)
	if font == nil or fontSize == nil then
		error("Font and FontSize must be non-nil")
	end

	if font == Enum.Font.Legacy then
		if fontSize == Enum.FontSize.Size8 then
			return 12
		elseif fontSize == Enum.FontSize.Size9 then
			return 14
		elseif fontSize == Enum.FontSize.Size10 then
			return 15
		elseif fontSize == Enum.FontSize.Size11 then
			return 17
		elseif fontSize == Enum.FontSize.Size12 then
			return 18
		elseif fontSize == Enum.FontSize.Size14 then
			return 21
		elseif fontSize == Enum.FontSize.Size18 then
			return 27
		elseif fontSize == Enum.FontSize.Size24 then
			return 36
		elseif fontSize == Enum.FontSize.Size36 then
			return 54
		elseif fontSize == Enum.FontSize.Size48 then
			return 72
		else
			error("Unknown FontSize")
		end
	elseif font == Enum.Font.Arial or font == Enum.Font.ArialBold then
		if fontSize == Enum.FontSize.Size8 then
			return 8
		elseif fontSize == Enum.FontSize.Size9 then
			return 9
		elseif fontSize == Enum.FontSize.Size10 then
			return 10
		elseif fontSize == Enum.FontSize.Size11 then
			return 11
		elseif fontSize == Enum.FontSize.Size12 then
			return 12
		elseif fontSize == Enum.FontSize.Size14 then
			return 14
		elseif fontSize == Enum.FontSize.Size18 then
			return 18
		elseif fontSize == Enum.FontSize.Size24 then
			return 24
		elseif fontSize == Enum.FontSize.Size36 then
			return 36
		elseif fontSize == Enum.FontSize.Size48 then
			return 48
		else
			error("Unknown FontSize")
		end
	else
		error("Unknown Font " .. font)
	end
end

local function layoutGuiObjectsHelper(frame, guiObjects, settingsTable)
	local totalPixels = frame.AbsoluteSize.Y
	local pixelsRemaining = frame.AbsoluteSize.Y
	for i, child in ipairs(guiObjects) do
		if child:IsA("TextLabel") or child:IsA("TextButton") then
			local isLabel = child:IsA("TextLabel")
			if isLabel then
				pixelsRemaining = pixelsRemaining - settingsTable["TextLabelPositionPadY"]
			else
				pixelsRemaining = pixelsRemaining - settingsTable["TextButtonPositionPadY"]
			end
			child.Position = UDim2.new(child.Position.X.Scale, child.Position.X.Offset, 0, totalPixels - pixelsRemaining)
			child.Size = UDim2.new(child.Size.X.Scale, child.Size.X.Offset, 0, pixelsRemaining)

			if child.TextFits and child.TextBounds.Y < pixelsRemaining then
				child.Visible = true
				if isLabel then
					child.Size = UDim2.new(child.Size.X.Scale, child.Size.X.Offset, 0, child.TextBounds.Y + settingsTable["TextLabelSizePadY"])
				else 
					child.Size = UDim2.new(child.Size.X.Scale, child.Size.X.Offset, 0, child.TextBounds.Y + settingsTable["TextButtonSizePadY"])
				end

				while not child.TextFits do
					child.Size = UDim2.new(child.Size.X.Scale, child.Size.X.Offset, 0, child.AbsoluteSize.Y + 1)
				end
				pixelsRemaining = pixelsRemaining - child.AbsoluteSize.Y		

				if isLabel then
					pixelsRemaining = pixelsRemaining - settingsTable["TextLabelPositionPadY"]
				else
					pixelsRemaining = pixelsRemaining - settingsTable["TextButtonPositionPadY"]
				end
			else
				child.Visible = false
				pixelsRemaining = -1
			end			

		else
			--GuiObject
			child.Position = UDim2.new(child.Position.X.Scale, child.Position.X.Offset, 0, totalPixels - pixelsRemaining)
			pixelsRemaining = pixelsRemaining - child.AbsoluteSize.Y
			child.Visible = (pixelsRemaining >= 0)
		end
	end
end

RbxGuiLib.LayoutGuiObjects = function(frame, guiObjects, settingsTable)
	if not frame:IsA("GuiObject") then
		error("Frame must be a GuiObject")
	end
	for i, child in ipairs(guiObjects) do
		if not child:IsA("GuiObject") then
			error("All elements that are layed out must be of type GuiObject")
		end
	end

	if not settingsTable then
		settingsTable = {}
	end

	if not settingsTable["TextLabelSizePadY"] then
		settingsTable["TextLabelSizePadY"] = 0
	end
	if not settingsTable["TextLabelPositionPadY"] then
		settingsTable["TextLabelPositionPadY"] = 0
	end
	if not settingsTable["TextButtonSizePadY"] then
		settingsTable["TextButtonSizePadY"] = 12
	end
	if not settingsTable["TextButtonPositionPadY"] then
		settingsTable["TextButtonPositionPadY"] = 2
	end

	--Wrapper frame takes care of styled objects
	local wrapperFrame = Instance.new("Frame")
	wrapperFrame.Name = "WrapperFrame"
	wrapperFrame.BackgroundTransparency = 1
	wrapperFrame.Size = UDim2.new(1,0,1,0)
	wrapperFrame.Parent = frame

	for i, child in ipairs(guiObjects) do
		child.Parent = wrapperFrame
	end

	local recalculate = function()
		wait()
		layoutGuiObjectsHelper(wrapperFrame, guiObjects, settingsTable)
	end
	
	frame.Changed:connect(
		function(prop)
			if prop == "AbsoluteSize" then
				--Wait a heartbeat for it to sync in
				recalculate()
			end
		end)
	frame.AncestryChanged:connect(recalculate)

	layoutGuiObjectsHelper(wrapperFrame, guiObjects, settingsTable)
end


RbxGuiLib.CreateSlider = function(steps,width,position)
	local sliderGui = Instance.new("Frame")
	sliderGui.Size = UDim2.new(1,0,1,0)
	sliderGui.BackgroundTransparency = 1
	sliderGui.Name = "SliderGui"
	
	local areaSoak = Instance.new("TextButton")
	areaSoak.Name = "AreaSoak"
	areaSoak.Text = ""
	areaSoak.BackgroundTransparency = 1
	areaSoak.Active = false
	areaSoak.Size = UDim2.new(1,0,1,0)
	areaSoak.Visible = false
	areaSoak.ZIndex = 4
	areaSoak.Parent = sliderGui
	
	local sliderPosition = Instance.new("IntValue")
	sliderPosition.Name = "SliderPosition"
	sliderPosition.Value = 0
	sliderPosition.Parent = sliderGui
	
	local id = math.random(1,100)
	
	local bar = Instance.new("Frame")
	bar.Name = "Bar"
	bar.BackgroundColor3 = Color3.new(0,0,0)
	if type(width) == "number" then
		bar.Size = UDim2.new(0,width,0,5)
	else
		bar.Size = UDim2.new(0,200,0,5)
	end
	bar.BorderColor3 = Color3.new(95/255,95/255,95/255)
	bar.ZIndex = 2
	bar.Parent = sliderGui
	
	if position["X"] and position["X"]["Scale"] and position["X"]["Offset"] and position["Y"] and position["Y"]["Scale"] and position["Y"]["Offset"] then
		bar.Position = position
	end
	
	local slider = Instance.new("ImageButton")
	slider.Name = "Slider"
	slider.BackgroundTransparency = 1
	slider.Image = "rbxasset://textures/ui/Slider.png"
	slider.Position = UDim2.new(0,0,0.5,-10)
	slider.Size = UDim2.new(0,20,0,20)
	slider.ZIndex = 3
	slider.Parent = bar
	
	local areaSoakMouseMoveCon = nil
	
	areaSoak.MouseLeave:connect(function()
		if areaSoak.Visible then
			cancelSlide(areaSoak)
		end
	end)
	areaSoak.MouseButton1Up:connect(function()
		if areaSoak.Visible then
			cancelSlide(areaSoak)
		end
	end)
	
	slider.MouseButton1Down:connect(function()
		areaSoak.Visible = true
		if areaSoakMouseMoveCon then areaSoakMouseMoveCon:disconnect() end
		areaSoakMouseMoveCon = areaSoak.MouseMoved:connect(function(x,y)
			setSliderPos(x,slider,sliderPosition,bar,steps)
		end)
	end)
	
	slider.MouseButton1Up:connect(function() cancelSlide(areaSoak) end)
	
	sliderPosition.Changed:connect(function(prop)
		sliderPosition.Value = math.min(steps, math.max(1,sliderPosition.Value))
		local relativePosX = (sliderPosition.Value) / steps
		if relativePosX >= 1 then
			slider.Position = UDim2.new(relativePosX,-20,slider.Position.Y.Scale,slider.Position.Y.Offset)
		else
			slider.Position = UDim2.new(relativePosX,0,slider.Position.Y.Scale,slider.Position.Y.Offset)
		end
	end)
	
	return sliderGui, sliderPosition

end


RbxGuiLib.CreateScrollingFrame = function(orderList,scrollStyle)
	local frame = Instance.new("Frame")
	frame.Name = "ScrollingFrame"
	frame.BackgroundTransparency = 1
	frame.Size = UDim2.new(1,0,1,0)
	
	local scrollUpButton = Instance.new("ImageButton")
	scrollUpButton.Name = "ScrollUpButton"
	scrollUpButton.BackgroundTransparency = 1
	scrollUpButton.Image = "rbxasset://textures/ui/scrollbuttonUp.png"
	scrollUpButton.Size = UDim2.new(0,17,0,17) 

	
	local scrollDownButton = Instance.new("ImageButton")
	scrollDownButton.Name = "ScrollDownButton"
	scrollDownButton.BackgroundTransparency = 1
	scrollDownButton.Image = "rbxasset://textures/ui/scrollbuttonDown.png"
	scrollDownButton.Size = UDim2.new(0,17,0,17) 

	local style = "simple"
	if scrollStyle and tostring(scrollStyle) then
		style = scrollStyle
	end
	
	local scrollPosition = 1
	local rowSize = 1
	
	local layoutGridScrollBar = function()
		local guiObjects = {}
		if orderList then
			for i, child in ipairs(orderList) do
				if child.Parent == frame then
					table.insert(guiObjects, child)
				end
			end
		else
			local children = frame:GetChildren()
			if children then
				for i, child in ipairs(children) do 
					if child:IsA("GuiObject") then
						table.insert(guiObjects, child)
					end
				end
			end
		end
		if #guiObjects == 0 then
			scrollUpButton.Active = false
			scrollDownButton.Active = false
			scrollPosition = 1
			return
		end

		if scrollPosition > #guiObjects then
			scrollPosition = #guiObjects
		end
		
		local totalPixelsY = frame.AbsoluteSize.Y
		local pixelsRemainingY = frame.AbsoluteSize.Y
		
		local totalPixelsX  = frame.AbsoluteSize.X
		
		local xCounter = 0
		local rowSizeCounter = 0
		local setRowSize = true

		local pixelsBelowScrollbar = 0
		local pos = #guiObjects
		while pixelsBelowScrollbar < totalPixelsY and pos >= 1 do
			if pos >= scrollPosition then
				pixelsBelowScrollbar = pixelsBelowScrollbar + guiObjects[pos].AbsoluteSize.Y
			else
				xCounter = xCounter + guiObjects[pos].AbsoluteSize.X
				rowSizeCounter = rowSizeCounter + 1
				if xCounter >= totalPixelsX then
					if setRowSize then
						rowSize = rowSizeCounter - 1
						setRowSize = false
					end
					
					rowSizeCounter = 0
					xCounter = 0
					if pixelsBelowScrollbar + guiObjects[pos].AbsoluteSize.Y <= totalPixelsY then
						--It fits, so back up our scroll position
						pixelsBelowScrollbar = pixelsBelowScrollbar + guiObjects[pos].AbsoluteSize.Y
						if scrollPosition <= rowSize then
							scrollPosition = rowSize 
							break
						else
							--print("Backing up ScrollPosition from -- " ..scrollPosition)
							scrollPosition = scrollPosition - rowSize 
						end
					else
						break
					end
				end
			end
			pos = pos - 1
		end

		xCounter = 0
		--print("ScrollPosition = " .. scrollPosition)
		pos = scrollPosition
		rowSizeCounter = 0
		setRowSize = true
		local lastChildSize = 0
		
		local xOffset,yOffset = 0
		if guiObjects[1] then
			yOffset = math.ceil(math.floor(math.fmod(totalPixelsY,guiObjects[1].AbsoluteSize.X))/2)
			xOffset = math.ceil(math.floor(math.fmod(totalPixelsX,guiObjects[1].AbsoluteSize.Y))/2)
		end
		
		for i, child in ipairs(guiObjects) do
			if i < scrollPosition then
				--print("Hiding " .. child.Name)
				child.Visible = false
			else
				if pixelsRemainingY < 0 then
					--print("Out of Space " .. child.Name)
					child.Visible = false
				else
					--print("Laying out " .. child.Name)
					--GuiObject
					if setRowSize then rowSizeCounter = rowSizeCounter + 1 end
					if xCounter + child.AbsoluteSize.X >= totalPixelsX then
						if setRowSize then
							rowSize = rowSizeCounter - 1
							setRowSize = false
						end
						xCounter = 0
						pixelsRemainingY = pixelsRemainingY - child.AbsoluteSize.Y
					end
					child.Position = UDim2.new(child.Position.X.Scale,xCounter + xOffset, 0, totalPixelsY - pixelsRemainingY + yOffset)
					xCounter = xCounter + child.AbsoluteSize.X
					child.Visible = ((pixelsRemainingY - child.AbsoluteSize.Y) >= 0)
					lastChildSize = child.AbsoluteSize				
				end
			end
		end

		scrollUpButton.Active = (scrollPosition > 1)
		if lastChildSize == 0 then 
			scrollDownButton.Active = false
		else
			scrollDownButton.Active = ((pixelsRemainingY - lastChildSize.Y) < 0)
		end
	end


	local layoutSimpleScrollBar = function()
		local guiObjects = {}	
		
		if orderList then
			for i, child in ipairs(orderList) do
				if child.Parent == frame then
					table.insert(guiObjects, child)
				end
			end
		else
			local children = frame:GetChildren()
			if children then
				for i, child in ipairs(children) do 
					if child:IsA("GuiObject") then
						table.insert(guiObjects, child)
					end
				end
			end
		end
		if #guiObjects == 0 then
			scrollUpButton.Active = false
			scrollDownButton.Active = false
			scrollPosition = 1
			return
		end

		if scrollPosition > #guiObjects then
			scrollPosition = #guiObjects
		end
		
		local totalPixels = frame.AbsoluteSize.Y
		local pixelsRemaining = frame.AbsoluteSize.Y

		local pixelsBelowScrollbar = 0
		local pos = #guiObjects
		while pixelsBelowScrollbar < totalPixels and pos >= 1 do
			if pos >= scrollPosition then
				pixelsBelowScrollbar = pixelsBelowScrollbar + guiObjects[pos].AbsoluteSize.Y
			else
				if pixelsBelowScrollbar + guiObjects[pos].AbsoluteSize.Y <= totalPixels then
					--It fits, so back up our scroll position
					pixelsBelowScrollbar = pixelsBelowScrollbar + guiObjects[pos].AbsoluteSize.Y
					if scrollPosition <= 1 then
						scrollPosition = 1
						break
					else
						--print("Backing up ScrollPosition from -- " ..scrollPosition)
						scrollPosition = scrollPosition - 1
					end
				else
					break
				end
			end
			pos = pos - 1
		end

		--print("ScrollPosition = " .. scrollPosition)
		pos = scrollPosition
		for i, child in ipairs(guiObjects) do
			if i < scrollPosition then
				--print("Hiding " .. child.Name)
				child.Visible = false
			else
				if pixelsRemaining < 0 then
					--print("Out of Space " .. child.Name)
					child.Visible = false
				else
					--print("Laying out " .. child.Name)
					--GuiObject
					child.Position = UDim2.new(child.Position.X.Scale, child.Position.X.Offset, 0, totalPixels - pixelsRemaining)
					pixelsRemaining = pixelsRemaining - child.AbsoluteSize.Y
					child.Visible = (pixelsRemaining >= 0)				
				end
			end
		end
		scrollUpButton.Active = (scrollPosition > 1)
		scrollDownButton.Active = (pixelsRemaining < 0)
	end

	local reentrancyGuard = false
	local recalculate = function()
		if reentrancyGuard then
			return
		end
		reentrancyGuard = true
		wait()
		local success, err = nil
		if style == "grid" then
			success, err = pcall(function() layoutGridScrollBar(frame) end)
		elseif style == "simple" then
			success, err = pcall(function() layoutSimpleScrollBar(frame) end)
		end
		if not success then print(err) end

		reentrancyGuard = false
	end

	local scrollUp = function()
		if scrollUpButton.Active then
			scrollPosition = scrollPosition - rowSize
			recalculate()
		end
	end

	local scrollDown = function()
		if scrollDownButton.Active then
			scrollPosition = scrollPosition + rowSize
			recalculate()
		end
	end

	local scrollMouseCount = 0
	scrollUpButton.MouseButton1Click:connect(
		function()
			--print("Up-MouseButton1Click")
			scrollMouseCount = scrollMouseCount + 1
		end)
	scrollUpButton.MouseLeave:connect(
		function()
			--print("Up-Leave")
			scrollMouseCount = scrollMouseCount + 1
		end)

	scrollUpButton.MouseButton1Down:connect(
		function()
			--print("Up-Down")
			scrollMouseCount = scrollMouseCount + 1

			scrollUp()
			local val = scrollMouseCount
			wait(0.5)
			while val == scrollMouseCount do
				if scrollUp() == false then
					break
				end
				wait(0.1)
			end				
		end)

	scrollDownButton.MouseButton1Click:connect(
		function()
			--print("Down-Click")
			scrollMouseCount = scrollMouseCount + 1
		end)
	scrollDownButton.MouseLeave:connect(
		function()
			--print("Down-Leave")
			scrollMouseCount = scrollMouseCount + 1
		end)
	scrollDownButton.MouseButton1Down:connect(
		function()
			--print("Down-Down")
			scrollMouseCount = scrollMouseCount + 1

			scrollDown()
			local val = scrollMouseCount
			wait(0.5)
			while val == scrollMouseCount do
				if scrollDown() == false then
					break
				end
				wait(0.1)
			end				
		end)


	frame.ChildAdded:connect(function()
		recalculate()
	end)

	frame.ChildRemoved:connect(function()
		recalculate()
	end)
	
	frame.Changed:connect(
		function(prop)
			if prop == "AbsoluteSize" then
				--Wait a heartbeat for it to sync in
				recalculate()
			end
		end)
	frame.AncestryChanged:connect(recalculate)

	return frame, scrollUpButton, scrollDownButton, recalculate
end
local function binaryGrow(min, max, fits)
	if min > max then
		return min
	end
	local biggestLegal = min

	while min <= max do
		local mid = min + math.floor((max - min) / 2)
		if fits(mid) and (biggestLegal == nil or biggestLegal < mid) then
			biggestLegal = mid
			
			--Try growing
			min = mid + 1
		else
			--Doesn't fit, shrink
			max = mid - 1
		end
	end
	return biggestLegal
end


local function binaryShrink(min, max, fits)
	if min > max then
		return min
	end
	local smallestLegal = max

	while min <= max do
		local mid = min + math.floor((max - min) / 2)
		if fits(mid) and (smallestLegal == nil or smallestLegal > mid) then
			smallestLegal = mid
			
			--It fits, shrink
			max = mid - 1			
		else
			--Doesn't fit, grow
			min = mid + 1
		end
	end
	return smallestLegal
end


local function getGuiOwner(instance)
	while instance ~= nil do
		if instance:IsA("ScreenGui") or instance:IsA("BillboardGui")  then
			return instance
		end
		instance = instance.Parent
	end
	return nil
end

RbxGuiLib.AutoTruncateTextObject = function(textLabel)
	local text = textLabel.Text

	local fullLabel = textLabel:Clone()
	fullLabel.Name = "Full" .. textLabel.Name 
	fullLabel.BorderSizePixel = 0
	fullLabel.BackgroundTransparency = 0
	fullLabel.Text = text
	fullLabel.TextXAlignment = Enum.TextXAlignment.Center
	fullLabel.Position = UDim2.new(0,-3,0,0)
	fullLabel.Size = UDim2.new(0,100,1,0)
	fullLabel.Visible = false
	fullLabel.Parent = textLabel

	local shortText = nil
	local mouseEnterConnection = nil
	local mouseLeaveConnection= nil

	local checkForResize = function()
		if getGuiOwner(textLabel) == nil then
			return
		end
		textLabel.Text = text
		if textLabel.TextFits then 
			--Tear down the rollover if it is active
			if mouseEnterConnection then
				mouseEnterConnection:disconnect()
				mouseEnterConnection = nil
			end
			if mouseLeaveConnection then
				mouseLeaveConnection:disconnect()
				mouseLeaveConnection = nil
			end
		else
			local len = string.len(text)
			textLabel.Text = text .. "~"

			--Shrink the text
			local textSize = binaryGrow(0, len, 
				function(pos)
					if pos == 0 then
						textLabel.Text = "~"
					else
						textLabel.Text = string.sub(text, 1, pos) .. "~"
					end
					return textLabel.TextFits
				end)
			shortText = string.sub(text, 1, textSize) .. "~"
			textLabel.Text = shortText
			
			--Make sure the fullLabel fits
			if not fullLabel.TextFits then
				--Already too small, grow it really bit to start
				fullLabel.Size = UDim2.new(0, 10000, 1, 0)
			end
			
			--Okay, now try to binary shrink it back down
			local fullLabelSize = binaryShrink(textLabel.AbsoluteSize.X,fullLabel.AbsoluteSize.X, 
				function(size)
					fullLabel.Size = UDim2.new(0, size, 1, 0)
					return fullLabel.TextFits
				end)
			fullLabel.Size = UDim2.new(0,fullLabelSize+6,1,0)

			--Now setup the rollover effects, if they are currently off
			if mouseEnterConnection == nil then
				mouseEnterConnection = textLabel.MouseEnter:connect(
					function()
						fullLabel.ZIndex = textLabel.ZIndex + 1
						fullLabel.Visible = true
						--textLabel.Text = ""
					end)
			end
			if mouseLeaveConnection == nil then
				mouseLeaveConnection = textLabel.MouseLeave:connect(
					function()
						fullLabel.Visible = false
						--textLabel.Text = shortText
					end)
			end
		end
	end
	textLabel.AncestryChanged:connect(checkForResize)
	textLabel.Changed:connect(
		function(prop) 
			if prop == "AbsoluteSize" then 
				checkForResize() 	
			end 
		end)

	checkForResize()

	local function changeText(newText)
		text = newText
		fullLabel.Text = text
		checkForResize()
	end

	return textLabel, changeText
end

local function TransitionTutorialPages(fromPage, toPage, transitionFrame, currentPageValue)	
	if fromPage then
		fromPage.Visible = false
		if transitionFrame.Visible == false then
			transitionFrame.Size = fromPage.Size
			transitionFrame.Position = fromPage.Position
		end
	else
		if transitionFrame.Visible == false then
			transitionFrame.Size = UDim2.new(0.0,50,0.0,50)
			transitionFrame.Position = UDim2.new(0.5,-25,0.5,-25)
		end
	end
	transitionFrame.Visible = true
	currentPageValue.Value = nil

	local newsize, newPosition
	if toPage then
		--Make it visible so it resizes
		toPage.Visible = true

		newSize = toPage.Size
		newPosition = toPage.Position

		toPage.Visible = false
	else
		newSize = UDim2.new(0.0,50,0.0,50)
		newPosition = UDim2.new(0.5,-25,0.5,-25)
	end
	transitionFrame:TweenSizeAndPosition(newSize, newPosition, Enum.EasingDirection.InOut, Enum.EasingStyle.Quad, 0.3, true,
		function(state)
			if state == Enum.TweenStatus.Completed then
				transitionFrame.Visible = false
				if toPage then
					toPage.Visible = true
					currentPageValue.Value = toPage
				end
			end
		end)
end

RbxGuiLib.CreateTutorial = function(name, tutorialKey, createButtons)
	local frame = Instance.new("Frame")
	frame.Name = "Tutorial-" .. name
	frame.BackgroundTransparency = 1
	frame.Size = UDim2.new(0.6, 0, 0.6, 0)
	frame.Position = UDim2.new(0.2, 0, 0.2, 0)

	local transitionFrame = Instance.new("Frame")
	transitionFrame.Name = "TransitionFrame"
	transitionFrame.Style = Enum.FrameStyle.RobloxRound
	transitionFrame.Size = UDim2.new(0.6, 0, 0.6, 0)
	transitionFrame.Position = UDim2.new(0.2, 0, 0.2, 0)
	transitionFrame.Visible = false
	transitionFrame.Parent = frame

	local currentPageValue = Instance.new("ObjectValue")
	currentPageValue.Name = "CurrentTutorialPage"
	currentPageValue.Value = nil
	currentPageValue.Parent = frame

	local boolValue = Instance.new("BoolValue")
	boolValue.Name = "Buttons"
	boolValue.Value = createButtons
	boolValue.Parent = frame

	local pages = Instance.new("Frame")
	pages.Name = "Pages"
	pages.BackgroundTransparency = 1
	pages.Size = UDim2.new(1,0,1,0)
	pages.Parent = frame

	local function getVisiblePageAndHideOthers()
		local visiblePage = nil
		local children = pages:GetChildren()
		if children then
			for i,child in ipairs(children) do
				if child.Visible then
					if visiblePage then
						child.Visible = false
					else
						visiblePage = child
					end
				end
			end
		end
		return visiblePage
	end

	local showTutorial = function(alwaysShow)
		if alwaysShow or UserSettings().GameSettings:GetTutorialState(tutorialKey) == false then
			print("Showing tutorial-",tutorialKey)
			local currentTutorialPage = getVisiblePageAndHideOthers()

			local firstPage = pages:FindFirstChild("TutorialPage1")
			if firstPage then
				TransitionTutorialPages(currentTutorialPage, firstPage, transitionFrame, currentPageValue)	
			else
				error("Could not find TutorialPage1")
			end
		end
	end

	local dismissTutorial = function()
		local currentTutorialPage = getVisiblePageAndHideOthers()

		if currentTutorialPage then
			TransitionTutorialPages(currentTutorialPage, nil, transitionFrame, currentPageValue)
		end

		UserSettings().GameSettings:SetTutorialState(tutorialKey, true)
	end

	local gotoPage = function(pageNum)
		local page = pages:FindFirstChild("TutorialPage" .. pageNum)
		local currentTutorialPage = getVisiblePageAndHideOthers()
		TransitionTutorialPages(currentTutorialPage, page, transitionFrame, currentPageValue)
	end

	return frame, showTutorial, dismissTutorial, gotoPage
end 

local function CreateBasicTutorialPage(name, handleResize, skipTutorial)
	local frame = Instance.new("Frame")
	frame.Name = "TutorialPage"
	frame.Style = Enum.FrameStyle.RobloxRound
	frame.Size = UDim2.new(0.6, 0, 0.6, 0)
	frame.Position = UDim2.new(0.2, 0, 0.2, 0)
	frame.Visible = false
	
	local frameHeader = Instance.new("TextLabel")
	frameHeader.Name = "Header"
	frameHeader.Text = name
	frameHeader.BackgroundTransparency = 1
	frameHeader.FontSize = Enum.FontSize.Size24
	frameHeader.Font = Enum.Font.ArialBold
	frameHeader.TextColor3 = Color3.new(1,1,1)
	frameHeader.TextXAlignment = Enum.TextXAlignment.Center
	frameHeader.TextWrap = true
	frameHeader.Size = UDim2.new(1,-55, 0, 22)
	frameHeader.Position = UDim2.new(0,0,0,0)
	frameHeader.Parent = frame

	local skipButton = Instance.new("ImageButton")
	skipButton.Name = "SkipButton"
	skipButton.AutoButtonColor = false
	skipButton.BackgroundTransparency = 1
	skipButton.Image = "rbxasset://textures/ui/Skip.png"	
	skipButton.MouseButton1Click:connect(function()
		skipButton.Image = "rbxasset://textures/ui/Skip.png"	
		skipTutorial()
	end)
	skipButton.MouseEnter:connect(function()
		skipButton.Image = "rbxasset://textures/ui/SkipEnter.png"	
	end)
	skipButton.MouseLeave:connect(function()
		skipButton.Image = "rbxasset://textures/ui/Skip.png"	
	end)
	skipButton.Size = UDim2.new(0, 55, 0, 22)
	skipButton.Position = UDim2.new(1, -55, 0, 0)
	skipButton.Parent = frame

	local innerFrame = Instance.new("Frame")
	innerFrame.Name = "ContentFrame"
	innerFrame.BackgroundTransparency = 1
	innerFrame.Position = UDim2.new(0,0,0,22)
	innerFrame.Parent = frame

	local nextButton = Instance.new("TextButton")
	nextButton.Name = "NextButton"
	nextButton.Text = "Next"
	nextButton.TextColor3 = Color3.new(1,1,1)
	nextButton.Font = Enum.Font.Arial
	nextButton.FontSize = Enum.FontSize.Size18
	nextButton.Style = Enum.ButtonStyle.RobloxButtonDefault
	nextButton.Size = UDim2.new(0,80, 0, 32)
	nextButton.Position = UDim2.new(0.5, 5, 1, -32)
	nextButton.Active = false
	nextButton.Visible = false
	nextButton.Parent = frame

	local prevButton = Instance.new("TextButton")
	prevButton.Name = "PrevButton"
	prevButton.Text = "Previous"
	prevButton.TextColor3 = Color3.new(1,1,1)
	prevButton.Font = Enum.Font.Arial
	prevButton.FontSize = Enum.FontSize.Size18
	prevButton.Style = Enum.ButtonStyle.RobloxButton
	prevButton.Size = UDim2.new(0,80, 0, 32)
	prevButton.Position = UDim2.new(0.5, -85, 1, -32)
	prevButton.Active = false
	prevButton.Visible = false
	prevButton.Parent = frame

	innerFrame.Size = UDim2.new(1,0,1,-22-35)

	local parentConnection = nil

	local function basicHandleResize()
		if frame.Visible and frame.Parent then
			local maxSize = math.min(frame.Parent.AbsoluteSize.X, frame.Parent.AbsoluteSize.Y)
			handleResize(200,maxSize)
		end
	end

	frame.Changed:connect(
		function(prop)
			if prop == "Parent" then
				if parentConnection ~= nil then
					parentConnection:disconnect()
					parentConnection = nil
				end
				if frame.Parent and frame.Parent:IsA("GuiObject") then
					parentConnection = frame.Parent.Changed:connect(
						function(parentProp)
							if parentProp == "AbsoluteSize" then
								wait()
								basicHandleResize()
							end
						end)
					basicHandleResize()
				end
			end

			if prop == "Visible" then 
				basicHandleResize()
			end
		end)

	return frame, innerFrame
end

RbxGuiLib.CreateTextTutorialPage = function(name, text, skipTutorialFunc)
	local frame = nil
	local contentFrame = nil

	local textLabel = Instance.new("TextLabel")
	textLabel.BackgroundTransparency = 1
	textLabel.TextColor3 = Color3.new(1,1,1)
	textLabel.Text = text
	textLabel.TextWrap = true
	textLabel.TextXAlignment = Enum.TextXAlignment.Left
	textLabel.TextYAlignment = Enum.TextYAlignment.Center
	textLabel.Font = Enum.Font.Arial
	textLabel.FontSize = Enum.FontSize.Size14
	textLabel.Size = UDim2.new(1,0,1,0)

	local function handleResize(minSize, maxSize)
		size = binaryShrink(minSize, maxSize,
			function(size)
				frame.Size = UDim2.new(0, size, 0, size)
				return textLabel.TextFits
			end)
		frame.Size = UDim2.new(0, size, 0, size)
		frame.Position = UDim2.new(0.5, -size/2, 0.5, -size/2)
	end

	frame, contentFrame = CreateBasicTutorialPage(name, handleResize, skipTutorialFunc)
	textLabel.Parent = contentFrame

	return frame
end

RbxGuiLib.CreateImageTutorialPage = function(name, imageAsset, x, y, skipTutorialFunc)
	local frame = nil
	local contentFrame = nil

	local imageLabel = Instance.new("ImageLabel")
	imageLabel.BackgroundTransparency = 1
	imageLabel.Image = imageAsset
	imageLabel.Size = UDim2.new(0,x,0,y)
	imageLabel.Position = UDim2.new(0.5,-x/2,0.5,-y/2)

	local function handleResize(minSize, maxSize)
		size = binaryShrink(minSize, maxSize,
			function(size)
				return size >= x and size >= y
			end)
		if size >= x and size >= y then
			imageLabel.Size = UDim2.new(0,x, 0,y)
			imageLabel.Position = UDim2.new(0.5,-x/2, 0.5, -y/2)
		else
			if x > y then
				--X is limiter, so 
				imageLabel.Size = UDim2.new(1,0,y/x,0)
				imageLabel.Position = UDim2.new(0,0, 0.5 - (y/x)/2, 0)
			else
				--Y is limiter
				imageLabel.Size = UDim2.new(x/y,0,1, 0)
				imageLabel.Position = UDim2.new(0.5-(x/y)/2, 0, 0, 0)
			end
		end
		frame.Size = UDim2.new(0, size, 0, size)
		frame.Position = UDim2.new(0.5, -size/2, 0.5, -size/2)
	end

	frame, contentFrame = CreateBasicTutorialPage(name, handleResize, skipTutorialFunc)
	imageLabel.Parent = contentFrame

	return frame
end

RbxGuiLib.AddTutorialPage = function(tutorial, tutorialPage)
	local transitionFrame = tutorial.TransitionFrame
	local currentPageValue = tutorial.CurrentTutorialPage

	if not tutorial.Buttons.Value then
		tutorialPage.ContentFrame.Size = UDim2.new(1,0,1,-22)
		tutorialPage.NextButton.Parent = nil
		tutorialPage.PrevButton.Parent = nil
	end

	local children = tutorial.Pages:GetChildren()
	if children and #children > 0 then
		tutorialPage.Name = "TutorialPage" .. (#children+1)
		local previousPage = children[#children]
		if not previousPage:IsA("GuiObject") then
			error("All elements under Pages must be GuiObjects")
		end

		if tutorial.Buttons.Value then
			if previousPage.NextButton.Active then
				error("NextButton already Active on previousPage, please only add pages with RbxGui.AddTutorialPage function")
			end
			previousPage.NextButton.MouseButton1Click:connect(
				function()
					TransitionTutorialPages(previousPage, tutorialPage, transitionFrame, currentPageValue)
				end)
			previousPage.NextButton.Active = true
			previousPage.NextButton.Visible = true

			if tutorialPage.PrevButton.Active then
				error("PrevButton already Active on tutorialPage, please only add pages with RbxGui.AddTutorialPage function")
			end
			tutorialPage.PrevButton.MouseButton1Click:connect(
				function()
					TransitionTutorialPages(tutorialPage, previousPage, transitionFrame, currentPageValue)
				end)
			tutorialPage.PrevButton.Active = true
			tutorialPage.PrevButton.Visible = true
		end

		tutorialPage.Parent = tutorial.Pages
	else
		--First child
		tutorialPage.Name = "TutorialPage1"
		tutorialPage.Parent = tutorial.Pages
	end
end 

RbxGuiLib.Help = 
	function(funcNameOrFunc) 
		--input argument can be a string or a function.  Should return a description (of arguments and expected side effects)
		if funcNameOrFunc == "CreatePropertyDropDownMenu" or funcNameOrFunc == RbxGuiLib.CreatePropertyDropDownMenu then
			return "Function CreatePropertyDropDownMenu.  " ..
				   "Arguments: (instance, propertyName, enumType).  " .. 
				   "Side effect: returns a container with a drop-down-box that is linked to the 'property' field of 'instance' which is of type 'enumType'" 
		end 
		if funcNameOrFunc == "CreateDropDownMenu" or funcNameOrFunc == RbxGuiLib.CreateDropDownMenu then
			return "Function CreateDropDownMenu.  " .. 
			       "Arguments: (items, onItemSelected).  " .. 
				   "Side effect: Returns 2 results, a container to the gui object and a 'updateSelection' function for external updating.  The container is a drop-down-box created around a list of items" 
		end 
		if funcNameOrFunc == "CreateMessageDialog" or funcNameOrFunc == RbxGuiLib.CreateMessageDialog then
			return "Function CreateMessageDialog.  " .. 
			       "Arguments: (title, message, buttons). " .. 
			       "Side effect: Returns a gui object of a message box with 'title' and 'message' as passed in.  'buttons' input is an array of Tables contains a 'Text' and 'Function' field for the text/callback of each button"
		end		
		if funcNameOrFunc == "CreateStyledMessageDialog" or funcNameOrFunc == RbxGuiLib.CreateStyledMessageDialog then
			return "Function CreateStyledMessageDialog.  " .. 
			       "Arguments: (title, message, style, buttons). " .. 
			       "Side effect: Returns a gui object of a message box with 'title' and 'message' as passed in.  'buttons' input is an array of Tables contains a 'Text' and 'Function' field for the text/callback of each button, 'style' is a string, either Error, Notify or Confirm"
		end
		if funcNameOrFunc == "GetFontHeight" or funcNameOrFunc == RbxGuiLib.GetFontHeight then
			return "Function GetFontHeight.  " .. 
			       "Arguments: (font, fontSize). " .. 
			       "Side effect: returns the size in pixels of the given font + fontSize"
		end
		if funcNameOrFunc == "LayoutGuiObjects" or funcNameOrFunc == RbxGuiLib.LayoutGuiObjects then
		
		end
		if funcNameOrFunc == "CreateScrollingFrame" or funcNameOrFunc == RbxGuiLib.CreateScrollingFrame then
			return "Function CreateScrollingFrame.  " .. 
			   "Arguments: (orderList, style) " .. 
			   "Side effect: returns 4 objects, (scrollFrame, scrollUpButton, scrollDownButton, recalculateFunction).  'scrollFrame' can be filled with GuiObjects.  It will lay them out and allow scrollUpButton/scrollDownButton to interact with them.  Orderlist is optional (and specifies the order to layout the children.  Without orderlist, it uses the children order. style is also optional, and allows for a 'grid' styling if style is passed 'grid' as a string.  recalculateFunction can be called when a relayout is needed (when orderList changes)"
		end
		if funcNameOrFunc == "AutoTruncateTextObject" or funcNameOrFunc == RbxGuiLib.AutoTruncateTextObject then
			return "Function AutoTruncateTextObject.  " .. 
			   "Arguments: (textLabel) " .. 
			   "Side effect: returns 2 objects, (textLabel, changeText).  The 'textLabel' input is modified to automatically truncate text (with ellipsis), if it gets too small to fit.  'changeText' is a function that can be used to change the text, it takes 1 string as an argument"
		end
		if funcNameOrFunc == "CreateSlider" or funcNameOrFunc == RbxGuiLib.CreateSlider then
			return "Function CreateSlider.  " ..
				"Arguments: (steps, width, position) " ..
				"Side effect: returns 2 objects, (sliderGui, sliderPosition).  The 'steps' argument specifies how many different positions the slider can hold along the bar.  'width' specifies in pixels how wide the bar should be (modifiable afterwards if desired). 'position' argument should be a UDim2 for slider positioning. 'sliderPosition' is an IntValue whose current .Value specifies the specific step the slider is currently on."
		end
	end
-- RBXGUI END --

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

waitForChild(game,"Players")
waitForProperty(game.Players,"LocalPlayer")
local player = game.Players.LocalPlayer

local RbxGui,msg = RbxGuiLib
if not RbxGui then print("could not find RbxGui!") return end

--- Begin Locals
waitForChild(game,"Players")

-- don't do anything if we are in an empty game
if #game.Players:GetChildren() < 1 then
	game.Players.ChildAdded:wait()
end

local tilde = "~"
local backquote = "`"
game:GetService("GuiService"):AddKey(tilde) -- register our keys
game:GetService("GuiService"):AddKey(backquote)

local player = game.Players.LocalPlayer

local backpack = game:GetService("CoreGui").RobloxGui:FindFirstChild("Backpack")
local screen = game:GetService("CoreGui").RobloxGui
local closeButton = backpack.Tabs.CloseButton

local openCloseDebounce = false

local backpackItems = {}

local buttons = {}

local debounce = false

local guiTweenSpeed = 1

local backpackOldStateVisible = false
local browsingMenu = false

local mouseEnterCons = {}
local mouseClickCons = {}

local characterChildAddedCon = nil
local characterChildRemovedCon = nil
local backpackAddCon = nil
local humanoidDiedCon = nil
local backpackButtonClickCon = nil
local guiServiceKeyPressCon = nil

waitForChild(player,"Backpack")
local playerBackpack = player.Backpack

waitForChild(backpack,"Gear")
waitForChild(backpack.Gear,"GearPreview")
local gearPreview = backpack.Gear.GearPreview

waitForChild(backpack.Gear,"GearGridScrollingArea")
local scroller = backpack.Gear.GearGridScrollingArea

waitForChild(backpack.Parent,"CurrentLoadout")
local currentLoadout = backpack.Parent.CurrentLoadout

waitForChild(backpack.Parent,"ControlFrame")
waitForChild(backpack.Parent.ControlFrame,"BackpackButton")
local backpackButton = backpack.Parent.ControlFrame.BackpackButton

waitForChild(backpack.Gear,"GearGrid")
waitForChild(backpack.Gear.GearGrid,"GearButton")
local gearButton = backpack.Gear.GearGrid.GearButton
local grid = backpack.Gear.GearGrid

waitForChild(backpack.Gear.GearGrid,"SearchFrame")
waitForChild(backpack.Gear.GearGrid.SearchFrame,"SearchBoxFrame")
waitForChild(backpack.Gear.GearGrid.SearchFrame.SearchBoxFrame,"SearchBox")
local searchBox = backpack.Gear.GearGrid.SearchFrame.SearchBoxFrame.SearchBox

waitForChild(backpack.Gear.GearGrid.SearchFrame,"SearchButton")
local searchButton = backpack.Gear.GearGrid.SearchFrame.SearchButton

waitForChild(backpack.Gear.GearGrid,"ResetFrame")
local resetFrame = backpack.Gear.GearGrid.ResetFrame

waitForChild(backpack.Gear.GearGrid.ResetFrame,"ResetButtonBorder")
local resetButton = backpack.Gear.GearGrid.ResetFrame.ResetButtonBorder

waitForChild(backpack,"SwapSlot")
local swapSlot = backpack.SwapSlot


-- creating scroll bar early as to make sure items get placed correctly
local scrollFrame, scrollUp, scrollDown, recalculateScroll = RbxGui.CreateScrollingFrame(nil,"grid")

scrollFrame.Position = UDim2.new(0,0,0,30)
scrollFrame.Size = UDim2.new(1,0,1,-30)
scrollFrame.Parent = backpack.Gear.GearGrid

local scrollBar = Instance.new("Frame")
scrollBar.Name = "ScrollBar"
scrollBar.BackgroundTransparency = 0.9
scrollBar.BackgroundColor3 = Color3.new(1,1,1)
scrollBar.BorderSizePixel = 0
scrollBar.Size = UDim2.new(0, 17, 1, -36)
scrollBar.Position = UDim2.new(0,0,0,18)
scrollBar.Parent = scroller

scrollDown.Position = UDim2.new(0,0,1,-17)

scrollUp.Parent = scroller
scrollDown.Parent = scroller

local scrollFrameLoadout, scrollUpLoadout, scrollDownLoadout, recalculateScrollLoadout = RbxGui.CreateScrollingFrame()

scrollFrameLoadout.Position = UDim2.new(0,0,0,0)
scrollFrameLoadout.Size = UDim2.new(1,0,1,0)
scrollFrameLoadout.Parent = backpack.Gear.GearLoadouts.LoadoutsList

local LoadoutButton = Instance.new("TextButton")
LoadoutButton.RobloxLocked = true
LoadoutButton.Name = "LoadoutButton"
LoadoutButton.Font = Enum.Font.ArialBold
LoadoutButton.FontSize = Enum.FontSize.Size14
LoadoutButton.Position = UDim2.new(0,0,0,0)
LoadoutButton.Size = UDim2.new(1,0,0,32)
LoadoutButton.Style = Enum.ButtonStyle.RobloxButton
LoadoutButton.Text = "Loadout #1"
LoadoutButton.TextColor3 = Color3.new(1,1,1)
LoadoutButton.Parent = scrollFrameLoadout

local LoadoutButtonTwo = LoadoutButton:clone()
LoadoutButtonTwo.Text = "Loadout #2"
LoadoutButtonTwo.Parent = scrollFrameLoadout

local LoadoutButtonThree = LoadoutButton:clone()
LoadoutButtonThree.Text = "Loadout #3"
LoadoutButtonThree.Parent = scrollFrameLoadout

local LoadoutButtonFour = LoadoutButton:clone()
LoadoutButtonFour.Text = "Loadout #4"
LoadoutButtonFour.Parent = scrollFrameLoadout

local scrollBarLoadout = Instance.new("Frame")
scrollBarLoadout.Name = "ScrollBarLoadout"
scrollBarLoadout.BackgroundTransparency = 0.9
scrollBarLoadout.BackgroundColor3 = Color3.new(1,1,1)
scrollBarLoadout.BorderSizePixel = 0
scrollBarLoadout.Size = UDim2.new(0, 17, 1, -36)
scrollBarLoadout.Position = UDim2.new(0,0,0,18)
scrollBarLoadout.Parent = backpack.Gear.GearLoadouts.GearLoadoutsScrollingArea

scrollDownLoadout.Position = UDim2.new(0,0,1,-17)

scrollUpLoadout.Parent = backpack.Gear.GearLoadouts.GearLoadoutsScrollingArea
scrollDownLoadout.Parent = backpack.Gear.GearLoadouts.GearLoadoutsScrollingArea


-- Begin Functions
function removeFromMap(map,object)
	for i = 1, #map do
		if map[i] == object then
			table.remove(map,i)
			break
		end
	end
end

function robloxLock(instance)
  instance.RobloxLocked = true
  children = instance:GetChildren()
  if children then
	 for i, child in ipairs(children) do
		robloxLock(child)
	 end
  end
end

function resize()
	local size = 0
	if gearPreview.AbsoluteSize.Y > gearPreview.AbsoluteSize.X then
		size = gearPreview.AbsoluteSize.X * 0.75
	else
		size = gearPreview.AbsoluteSize.Y * 0.75
	end

	gearPreview.GearImage.Size = UDim2.new(0,size,0,size)
	gearPreview.GearImage.Position = UDim2.new(0,gearPreview.AbsoluteSize.X/2 - size/2,0.75,-size)
	
	resizeGrid()
end

function addToGrid(child)
	if not child:IsA("Tool") then
		if not child:IsA("HopperBin") then 
			return
		end
	end
	if child:FindFirstChild("RobloxBuildTool") then return end
	
	for i,v in pairs(backpackItems) do  -- check to see if we already have this gear registered
		if v == child then return end
	end

	table.insert(backpackItems,child)
	
	local changeCon = child.Changed:connect(function(prop)
		if prop == "Name" then
			if buttons[child] then
				if buttons[child].Image == "" then
					buttons[child].GearText.Text = child.Name
				end
			end
		end
	end)
	local ancestryCon = nil
	ancestryCon = child.AncestryChanged:connect(function(theChild,theParent)
		local thisObject = nil
		for k,v in pairs(backpackItems) do
			if v == child then
				thisObject = v
				break
			end
		end
		
		waitForProperty(player,"Character")
		waitForChild(player,"Backpack")
		if (child.Parent ~= player.Backpack and child.Parent ~= player.Character) then
			if ancestryCon then ancestryCon:disconnect() end
			if changeCon then changeCon:disconnect() end
			
			for k,v in pairs(backpackItems) do
				if v == thisObject then
					if mouseEnterCons[buttons[v]] then mouseEnterCons[buttons[v]]:disconnect() end
					if mouseClickCons[buttons[v]] then mouseClickCons[buttons[v]]:disconnect() end
					buttons[v].Parent = nil
					buttons[v] = nil
					break
				end
			end

			removeFromMap(backpackItems,thisObject)
			
			resizeGrid()
		else
			resizeGrid()
		end
		updateGridActive()
	end)
	resizeGrid()
end

function buttonClick(button)
	if button:FindFirstChild("UnequipContextMenu") and not button.Active then
		button.UnequipContextMenu.Visible = true
		browsingMenu = true
	end
end

function previewGear(button)
	if not browsingMenu then
		gearPreview.GearImage.Image = button.Image
		gearPreview.GearStats.GearName.Text = button.GearReference.Value.Name
	end
end

function checkForSwap(button,x,y)
	local loadoutChildren = currentLoadout:GetChildren()
	for i = 1, #loadoutChildren do
		if loadoutChildren[i]:IsA("Frame") and string.find(loadoutChildren[i].Name,"Slot") then
			if x >= loadoutChildren[i].AbsolutePosition.x and x <= (loadoutChildren[i].AbsolutePosition.x + loadoutChildren[i].AbsoluteSize.x) then
				if y >= loadoutChildren[i].AbsolutePosition.y and y <= (loadoutChildren[i].AbsolutePosition.y + loadoutChildren[i].AbsoluteSize.y) then
					local slot = tonumber(string.sub(loadoutChildren[i].Name,5))
					swapGearSlot(slot,button)
					return true
				end
			end
		end
	end
	return false
end

function resizeGrid()
	for k,v in pairs(backpackItems) do
		if not v:FindFirstChild("RobloxBuildTool") then
			if not buttons[v] then
				local buttonClone = gearButton:clone()
				buttonClone.Parent = grid.ScrollingFrame
				buttonClone.Visible = true
				buttonClone.Image = v.TextureId
				if buttonClone.Image == "" then
					buttonClone.GearText.Text = v.Name
				end
				--print("v =",v)
				buttonClone.GearReference.Value = v
				buttonClone.Draggable = true 
				buttons[v] = buttonClone
				
				local unequipMenu = getGearContextMenu()
				
				unequipMenu.Visible = false
				unequipMenu.Parent = buttonClone
				
				local beginPos = nil
				buttonClone.DragBegin:connect(function(value)
					buttonClone.ZIndex = 9
					beginPos = value
				end)
				buttonClone.DragStopped:connect(function(x,y)
					if beginPos ~= buttonClone.Position then
						buttonClone.ZIndex = 1
						if not checkForSwap(buttonClone,x,y) then
							buttonClone:TweenPosition(beginPos,Enum.EasingDirection.Out, Enum.EasingStyle.Quad, 0.5, true)
							buttonClone.Draggable = false
							delay(0.5,function()
								buttonClone.Draggable = true
							end)
						else
							buttonClone.Position = beginPos
						end
					end	
				end)
				
				mouseEnterCons[buttonClone] = buttonClone.MouseEnter:connect(function() previewGear(buttonClone) end)
				mouseClickCons[buttonClone] = buttonClone.MouseButton1Click:connect(function() buttonClick(buttonClone) end)
			end
		end
	end
	recalculateScroll()
end

function showPartialGrid(subset)

	resetFrame.Visible = true

	for k,v in pairs(buttons) do
		v.Parent = nil
	end
	for k,v in pairs(subset) do
		v.Parent =  grid.ScrollingFrame
	end
	recalculateScroll()
end

function showEntireGrid()
	resetFrame.Visible = false
	
	for k,v in pairs(buttons) do
		v.Parent = grid.ScrollingFrame
	end
	recalculateScroll()
end

function inLoadout(gear)
	local children = currentLoadout:GetChildren()
	for i = 1, #children do
		if children[i]:IsA("Frame") then
			local button = children[i]:GetChildren()
			if #button > 0 then
				if button[1].GearReference.Value and button[1].GearReference.Value == gear then
					return true
				end
			end
		end
	end
	return false
end	

function updateGridActive()
	for k,v in pairs(backpackItems) do
		if buttons[v] then
			local gear = nil
			local gearRef = buttons[v]:FindFirstChild("GearReference")
			
			if gearRef then gear = gearRef.Value end
			
			if not gear then
				buttons[v].Active = false
			elseif inLoadout(gear) then
				buttons[v].Active = false
			else
				buttons[v].Active = true
			end
		end
	end
end

function centerGear(loadoutChildren)
	local gearButtons = {}
	local lastSlotAdd = nil
	for i = 1, #loadoutChildren do
		if loadoutChildren[i]:IsA("Frame") and #loadoutChildren[i]:GetChildren() > 0 then
			if loadoutChildren[i].Name == "Slot0" then 
				lastSlotAdd = loadoutChildren[i]
			else
				table.insert(gearButtons, loadoutChildren[i])
			end
		end
	end
	if lastSlotAdd then table.insert(gearButtons,lastSlotAdd) end
	
	local startPos = ( 1 - (#gearButtons * 0.1) ) / 2
	for i = 1, #gearButtons do	
		gearButtons[i]:TweenPosition(UDim2.new(startPos + ((i - 1) * 0.1),0,0,0), Enum.EasingDirection.Out, Enum.EasingStyle.Quad, 0.25, true)
	end
end

function spreadOutGear(loadoutChildren)
	for i = 1, #loadoutChildren do
		if loadoutChildren[i]:IsA("Frame") then
			local slot = tonumber(string.sub(loadoutChildren[i].Name,5))
			if slot == 0 then slot = 10 end
			loadoutChildren[i]:TweenPosition(UDim2.new((slot - 1)/10,0,0,0), Enum.EasingDirection.Out, Enum.EasingStyle.Quad, 0.25, true)
		end
	end
end

function openCloseBackpack()
	if openCloseDebounce then return end
	openCloseDebounce = true
	
	local visible = not backpack.Visible
	if visible then
		updateGridActive()
		local centerDialogSupported, msg = pcall(function() game.GuiService:AddCenterDialog(backpack, Enum.CenterDialogType.PlayerInitiatedDialog, 
			function()
				backpack.Visible = true
				loadoutChildren = currentLoadout:GetChildren()
				for i = 1, #loadoutChildren do
					if loadoutChildren[i]:IsA("Frame") then
						loadoutChildren[i].BackgroundTransparency = 0.5
					end
				end 
				spreadOutGear(loadoutChildren)
			end)
		end)
		backpackButton.Selected = true
		backpack:TweenSizeAndPosition(UDim2.new(0.55, 0, 0.6, 0),UDim2.new(0.225, 0, 0.2, 0), Enum.EasingDirection.Out, Enum.EasingStyle.Quad, guiTweenSpeed/2, true)
		delay(guiTweenSpeed/2 + 0.01,
			function()
				local children = backpack:GetChildren()
				for i = 1, #children do
					if children[i]:IsA("Frame") then
						children[i].Visible = true
					end
				end
				resizeGrid()
				resize()
				openCloseDebounce = false
			end)
	else
		backpackButton.Selected = false
		local children = backpack:GetChildren()
		for i = 1, #children do
			if children[i]:IsA("Frame") then
				children[i].Visible = false
			end
		end
		loadoutChildren = currentLoadout:GetChildren()
		for i = 1, #loadoutChildren do
			if loadoutChildren[i]:IsA("Frame") then
				loadoutChildren[i].BackgroundTransparency = 1
			end
		end
		centerGear(loadoutChildren)
	
		backpack:TweenSizeAndPosition(UDim2.new(0,0,0,0),UDim2.new(0.5,0,0.5,0), Enum.EasingDirection.Out, Enum.EasingStyle.Quad, guiTweenSpeed/2, true)
		delay(guiTweenSpeed/2 + 0.01,
			function()
				backpack.Visible = visible
				resizeGrid()
				resize()
				pcall(function() game.GuiService:RemoveCenterDialog(backpack) end)
				openCloseDebounce = false
			end)
	end
end

function loadoutCheck(child, selectState)
	if not child:IsA("ImageButton") then return end
	for k,v in pairs(backpackItems) do
		if buttons[v] then
			if child:FindFirstChild("GearReference") then
				if buttons[v].GearReference.Value == child.GearReference.Value then
					buttons[v].Active = selectState
					break
				end
			end
		end
	end
end

function clearPreview()
	gearPreview.GearImage.Image = ""
	gearPreview.GearStats.GearName.Text = ""
end

function removeAllEquippedGear(physGear)
	local stuff = player.Character:GetChildren()
	for i = 1, #stuff do
		if ( stuff[i]:IsA("Tool") or stuff[i]:IsA("HopperBin") ) and stuff[i] ~= physGear then
			stuff[i].Parent = playerBackpack
		end
	end
end

function equipGear(physGear)
	removeAllEquippedGear(physGear)
	physGear.Parent = player.Character
	updateGridActive()
end

function unequipGear(physGear)
	physGear.Parent = playerBackpack
	updateGridActive()
end

function highlight(button)
	button.TextColor3 = Color3.new(0,0,0)
	button.BackgroundColor3 = Color3.new(0.8,0.8,0.8)
end
function clearHighlight(button)
	button.TextColor3 = Color3.new(1,1,1)
	button.BackgroundColor3 = Color3.new(0,0,0)
end

function swapGearSlot(slot,gearButton)
	if not swapSlot.Value then -- signal loadout to swap a gear out
		swapSlot.Slot.Value = slot
		swapSlot.GearButton.Value = gearButton
		swapSlot.Value = true
		updateGridActive()
	end
end


local UnequipGearMenuClick = function(element, menu)
	if type(element.Action) ~= "number" then return end
	local num = element.Action
	if num == 1 then -- remove from loadout
		unequipGear(menu.Parent.GearReference.Value)
		local inventoryButton = menu.Parent
		local gearToUnequip = inventoryButton.GearReference.Value
		local loadoutChildren = currentLoadout:GetChildren()
		local slot = -1
		for i = 1, #loadoutChildren do
			if loadoutChildren[i]:IsA("Frame") then
				local button = loadoutChildren[i]:GetChildren()
				if button[1] and button[1].GearReference.Value == gearToUnequip then
					slot = button[1].SlotNumber.Text
					break
				end
			end
		end
		swapGearSlot(slot,nil)
	end
end

-- these next two functions are used to stop any use of backpack while the player is dead (can cause issues)
function activateBackpack()
	backpack.Visible = backpackOldStateVisible
	
	backpackButtonClickCon = backpackButton.MouseButton1Click:connect(function() openCloseBackpack() end)
	guiServiceKeyPressCon = game:GetService("GuiService").KeyPressed:connect(function(key)
		if key == tilde or key == backquote then
			openCloseBackpack()
		end
	end)
end
function deactivateBackpack()
	if backpackButtonClickCon then backpackButtonClickCon:disconnect() end
	if guiServiceKeyPressCon then guiServiceKeyPressCon:disconnect() end

	backpackOldStateVisible = backpack.Visible
	backpack.Visible = false
end

function setupCharacterConnections()

	if backpackAddCon then backpackAddCon:disconnect() end
	backpackAddCon = game.Players.LocalPlayer.Backpack.ChildAdded:connect(function(child) addToGrid(child) end)
	
	-- make sure we get all the children
	local backpackChildren = game.Players.LocalPlayer.Backpack:GetChildren()
	for i = 1, #backpackChildren do
		addToGrid(backpackChildren[i])
	end

	if characterChildAddedCon then characterChildAddedCon:disconnect() end
	characterChildAddedCon = 
		game.Players.LocalPlayer.Character.ChildAdded:connect(function(child)
			addToGrid(child)
			updateGridActive()
		end)
		
	if characterChildRemovedCon then characterChildRemovedCon:disconnect() end
	characterChildRemovedCon = 
		game.Players.LocalPlayer.Character.ChildRemoved:connect(function(child)
			updateGridActive()
		end)
		
			
	if humanoidDiedCon then humanoidDiedCon:disconnect() end
	local localPlayer = game.Players.LocalPlayer
	waitForProperty(localPlayer,"Character")
	waitForChild(localPlayer.Character,"Humanoid")
	humanoidDiedCon = game.Players.LocalPlayer.Character.Humanoid.Died:connect(function() deactivateBackpack() end)
	
	activateBackpack()
end

function removeCharacterConnections()
	if characterChildAddedCon then characterChildAddedCon:disconnect() end
	if characterChildRemovedCon then characterChildRemovedCon:disconnect() end
	if backpackAddCon then backpackAddCon:disconnect() end
end

function trim(s)
  return (s:gsub("^%s*(.-)%s*$", "%1"))
end

function splitByWhiteSpace(text)
	if type(text) ~= "string" then return nil end
	
	local terms = {}
	for token in string.gmatch(text, "[^%s]+") do
	   if string.len(token) > 2 then
			table.insert(terms,token)
	   end
	end
	return terms
end

function filterGear(searchTerm)
	string.lower(searchTerm)
	searchTerm = trim(searchTerm)
	if string.len(searchTerm) < 2 then return nil end
	local terms = splitByWhiteSpace(searchTerm)
	
	local filteredGear = {}
	for k,v in pairs(backpackItems) do
		if buttons[v] then
			local gearString = string.lower(buttons[v].GearReference.Value.Name)
			gearString = trim(gearString)
			for i = 1, #terms do
				if string.match(gearString,terms[i]) then
					table.insert(filteredGear,buttons[v])
					break
				end
			end
		end
	end
	
	return filteredGear
end


function showSearchGear()
	local searchText = searchBox.Text
	searchBox.Text = "Search..."
	local filteredButtons = filterGear(searchText)
	if filteredButtons and #filteredButtons > 0 then
		showPartialGrid(filteredButtons)
	else
		showEntireGrid()
	end
end

function nukeBackpack()
	while #buttons > 0 do
		table.remove(buttons)
	end
	buttons = {}
	while #backpackItems > 0 do
		table.remove(backpackItems)
	end
	backpackItems = {}
	local scrollingFrameChildren = grid.ScrollingFrame:GetChildren()
	for i = 1, #scrollingFrameChildren do
		scrollingFrameChildren[i]:remove()
	end
end

function getGearContextMenu()
	local gearContextMenu = Instance.new("Frame")
	gearContextMenu.Active = true
	gearContextMenu.Name = "UnequipContextMenu"
	gearContextMenu.Size = UDim2.new(0,115,0,70)
	gearContextMenu.Position = UDim2.new(0,-16,0,-16)
	gearContextMenu.BackgroundTransparency = 1
	gearContextMenu.Visible = false

	local gearContextMenuButton = Instance.new("TextButton")
	gearContextMenuButton.Name = "UnequipContextMenuButton"
	gearContextMenuButton.Text = ""
	gearContextMenuButton.Style = Enum.ButtonStyle.RobloxButtonDefault
	gearContextMenuButton.ZIndex = 4
	gearContextMenuButton.Size = UDim2.new(1, 0, 1, -20)
	gearContextMenuButton.Visible = true
	gearContextMenuButton.Parent = gearContextMenu
	
	local elementHeight = 12
	
	local contextMenuElements = {}		
	local contextMenuElementsName = {"Remove Hotkey"}

	for i = 1, #contextMenuElementsName do
		local element = {}
		element.Type = "Button"
		element.Text = contextMenuElementsName[i]
		element.Action = i
		element.DoIt = UnequipGearMenuClick
		table.insert(contextMenuElements,element)
	end

	for i, contextElement in ipairs(contextMenuElements) do
		local element = contextElement
		if element.Type == "Button" then
			local button = Instance.new("TextButton")
			button.Name = "UnequipContextButton" .. i
			button.BackgroundColor3 = Color3.new(0,0,0)
			button.BorderSizePixel = 0
			button.TextXAlignment = Enum.TextXAlignment.Left
			button.Text = " " .. contextElement.Text
			button.Font = Enum.Font.Arial
			button.FontSize = Enum.FontSize.Size14
			button.Size = UDim2.new(1, 8, 0, elementHeight)
			button.Position = UDim2.new(0,0,0,elementHeight * i)
			button.TextColor3 = Color3.new(1,1,1)
			button.ZIndex = 4
			button.Parent = gearContextMenuButton

			button.MouseButton1Click:connect(function()
				if button.Active and not gearContextMenu.Parent.Active then
					local success, result = pcall(function() element.DoIt(element, gearContextMenu) end)
					browsingMenu = false
					gearContextMenu.Visible = false
					clearHighlight(button)
					clearPreview()
				end
			end)
			
			button.MouseEnter:connect(function()
				if button.Active and gearContextMenu.Parent.Active then
					highlight(button)
				end
			end)
			button.MouseLeave:connect(function()
				if button.Active and gearContextMenu.Parent.Active then
					clearHighlight(button)
				end
			end)
			
			contextElement.Button = button
			contextElement.Element = button
		elseif element.Type == "Label" then
			local frame = Instance.new("Frame")
			frame.Name = "ContextLabel" .. i
			frame.BackgroundTransparency = 1
			frame.Size = UDim2.new(1, 8, 0, elementHeight)

			local label = Instance.new("TextLabel")	
			label.Name = "Text1"
			label.BackgroundTransparency = 1
			label.BackgroundColor3 = Color3.new(1,1,1)
			label.BorderSizePixel = 0
			label.TextXAlignment = Enum.TextXAlignment.Left
			label.Font = Enum.Font.ArialBold
			label.FontSize = Enum.FontSize.Size14
			label.Position = UDim2.new(0.0, 0, 0, 0)
			label.Size = UDim2.new(0.5, 0, 1, 0)
			label.TextColor3 = Color3.new(1,1,1)
			label.ZIndex = 4
			label.Parent = frame
			element.Label1 = label
		
			if element.GetText2 then
				label = Instance.new("TextLabel")	
				label.Name = "Text2"
				label.BackgroundTransparency = 1
				label.BackgroundColor3 = Color3.new(1,1,1)
				label.BorderSizePixel = 0
				label.TextXAlignment = Enum.TextXAlignment.Right
				label.Font = Enum.Font.Arial
				label.FontSize = Enum.FontSize.Size14
				label.Position = UDim2.new(0.5, 0, 0, 0)
				label.Size = UDim2.new(0.5, 0, 1, 0)
				label.TextColor3 = Color3.new(1,1,1)
				label.ZIndex = 4
				label.Parent = frame
				element.Label2 = label
			end
			frame.Parent = gearContextMenuButton
			element.Label = frame
			element.Element =  frame
		end
	end

	gearContextMenu.ZIndex = 4
	gearContextMenu.MouseLeave:connect(function()
		browsingMenu = false
		gearContextMenu.Visible = false
		clearPreview()
	end)
	robloxLock(gearContextMenu)
	
	return gearContextMenu
end

local backpackChildren = player.Backpack:GetChildren()
for i = 1, #backpackChildren do
	addToGrid(backpackChildren[i])
end

------------------------- Start Lifelong Connections -----------------------
screen.Changed:connect(function(prop)
	if prop == "AbsoluteSize" then
		if debounce then return end
		debounce = true
		wait()
		resize()
		resizeGrid()
		debounce = false
	end
end)

currentLoadout.ChildAdded:connect(function(child) loadoutCheck(child, false) end)
currentLoadout.ChildRemoved:connect(function(child) loadoutCheck(child, true) end)

currentLoadout.DescendantAdded:connect(function(descendant)
	if not backpack.Visible and ( descendant:IsA("ImageButton") or descendant:IsA("TextButton") ) then
		centerGear(currentLoadout:GetChildren())
	end
end)
currentLoadout.DescendantRemoving:connect(function(descendant)
	if not backpack.Visible and ( descendant:IsA("ImageButton") or descendant:IsA("TextButton") ) then
		wait()
		centerGear(currentLoadout:GetChildren())
	end
end)
	
grid.MouseEnter:connect(function() clearPreview() end)
grid.MouseLeave:connect(function() clearPreview() end)

player.CharacterRemoving:connect(function()
	removeCharacterConnections()
	nukeBackpack()
end)
player.CharacterAdded:connect(function() setupCharacterConnections() end)

player.ChildAdded:connect(function(child)
	if child:IsA("Backpack") then
		playerBackpack = child
		if backpackAddCon then backpackAddCon:disconnect() end
		backpackAddCon = game.Players.LocalPlayer.Backpack.ChildAdded:connect(function(child) addToGrid(child) end)
	end
end)

swapSlot.Changed:connect(function()
	if not swapSlot.Value then
		updateGridActive()
	end
end)

searchBox.FocusLost:connect(function(enterPressed)
	if enterPressed then
		showSearchGear()
	end
end)

local loadoutChildren = currentLoadout:GetChildren()
for i = 1, #loadoutChildren do
	if loadoutChildren[i]:IsA("Frame") and string.find(loadoutChildren[i].Name,"Slot") then
		loadoutChildren[i].ChildRemoved:connect(function()
			updateGridActive()
		end)
		loadoutChildren[i].ChildAdded:connect(function()
			updateGridActive()
		end)
	end
end

closeButton.MouseButton1Click:connect(function() openCloseBackpack() end)

searchButton.MouseButton1Click:connect(function() showSearchGear() end)
resetButton.MouseButton1Click:connect(function() showEntireGrid() end)
------------------------- End Lifelong Connections -----------------------

resize()
resizeGrid()

-- make sure any items in the loadout are accounted for in inventory
local loadoutChildren = currentLoadout:GetChildren()
for i = 1, #loadoutChildren do
	loadoutCheck(loadoutChildren[i], false)
end
if not backpack.Visible then centerGear(currentLoadout:GetChildren()) end

-- make sure that inventory is listening to gear reparenting
if characterChildAddedCon == nil and game.Players.LocalPlayer["Character"] then
	setupCharacterConnections()
end
if not backpackAddCon then
	backpackAddCon = game.Players.LocalPlayer.Backpack.ChildAdded:connect(function(child) addToGrid(child) end)
end

-- flip it on if we are good
if game.CoreGui.Version >= 3 then
	backpackButton.Visible = true
end

recalculateScrollLoadout()

end)