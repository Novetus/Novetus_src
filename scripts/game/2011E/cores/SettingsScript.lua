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




RbxGuiLib.CreateScrollingFrame = function(orderList)
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


	local scrollPosition = 1


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
		local success, err = pcall(function() layoutSimpleScrollBar(frame) end)
		if not success then print(err) end

		reentrancyGuard = false
	end

	local scrollUp = function()
		if scrollUpButton.Active then
			scrollPosition = scrollPosition - 1
			recalculate()
		end
	end

	local scrollDown = function()
		if scrollDownButton.Active then
			scrollPosition = scrollPosition + 1
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
			--Doesn'RbxGuiLib fit, shrink
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
			--Doesn'RbxGuiLib fit, grow
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
			   "Arguments: (orderList) " .. 
			   "Side effect: returns 4 objects, (scrollFrame, scrollUpButton, scrollDownButton, recalculateFunction).  'scrollFrame' can be filled with GuiObjects.  It will lay them out and allow scrollUpButton/scrollDownButton to interact with them.  Orderlist is optional (and specifies the order to layout the children.  Without orderlist, it uses the children order.  recalculateFunction can be called when a relayout is needed (when orderList changes)"
		end
		if funcNameOrFunc == "AutoTruncateTextObject" or funcNameOrFunc == RbxGuiLib.AutoTruncateTextObject then
			return "Function AutoTruncateTextObject.  " .. 
			   "Arguments: (textLabel) " .. 
			   "Side effect: returns 2 objects, (textLabel, changeText).  The 'textLabel' input is modified to automatically truncate text (with ellipsis), if it gets too small to fit.  'changeText' is a function that can be used to change the text, it takes 1 string as an argument"
		end
	end

delay(0, function()

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

local function Color3I(r,g,b)
  return Color3.new(r/255,g/255,b/255)
end

local function robloxLock(instance)
  instance.RobloxLocked = true
  children = instance:GetChildren()
  if children then
	 for i, child in ipairs(children) do
		robloxLock(child)
	 end
  end
end

if LoadLibrary then
  RbxGui = RbxGuiLib
  local baseZIndex = 0
if UserSettings then
	local createSettingsDialog = function()
		waitForChild(game:GetService("CoreGui").RobloxGui.BottomLeftControl,"SettingsButton")
		settingsButton = game:GetService("CoreGui").RobloxGui.BottomLeftControl.SettingsButton

		local shield = Instance.new("TextButton")
		shield.Text = ""
		shield.Name = "UserSettingsShield"
		shield.Active = true
		shield.AutoButtonColor = false
		shield.Visible = false
		shield.Size = UDim2.new(1,0,1,0)
		shield.BackgroundColor3 = Color3I(51,51,51)
		shield.BorderColor3 = Color3I(27,42,53)
		shield.BackgroundTransparency = 0.4
		shield.ZIndex = baseZIndex + 1

		local frame = Instance.new("Frame")
		frame.Name = "Settings"
		frame.Position = UDim2.new(0.5, -262, 0.5, -150)
		frame.Size = UDim2.new(0, 525, 0, 290)
		frame.BackgroundTransparency = 1
		frame.Active = true
		frame.Parent = shield

		--[[local settingsBacker = Instance.new("Frame")
		settingsBacker.Size = UDim2.new(1, 0, 1, 0)
		settingsBacker.Style = Enum.FrameStyle.RobloxRound
		settingsBacker.Active = true
		settingsBacker.ZIndex = 1
		settinssBacker.Parent = frame]]--

		local settingsFrame = Instance.new("Frame")
		settingsFrame.Name = "SettingsStyle"
		settingsFrame.Size = UDim2.new(1, 0, 1, 0)
		settingsFrame.Style = Enum.FrameStyle.RobloxRound
		settingsFrame.Active = true
		settingsFrame.ZIndex = baseZIndex
		settingsFrame.Parent = frame

		local title = Instance.new("TextLabel")
		title.Name = "Title"
		title.Text = "Settings"
		title.TextColor3 = Color3I(221,221,221)
		title.Position = UDim2.new(0.5, 0, 0, 30)
		title.Font = Enum.Font.ArialBold
		title.FontSize = Enum.FontSize.Size36
		title.ZIndex = baseZIndex + 1
		--title.TextXAlignment = Enum.TextXAlignment.Center
		--title.TextYAlignment = Enum.TextYAlignment.Center
		title.Parent = settingsFrame

		local cameraLabel = Instance.new("TextLabel")
		cameraLabel.Name = "CameraLabel"
		cameraLabel.Text = "Character & Camera Controls (Test):"
		cameraLabel.Font = Enum.Font.Arial
		cameraLabel.FontSize = Enum.FontSize.Size18
		cameraLabel.Position = UDim2.new(0,20,0,105)
		cameraLabel.TextColor3 = Color3I(255,255,255)
		cameraLabel.TextXAlignment = Enum.TextXAlignment.Left
		cameraLabel.ZIndex = baseZIndex + 1
		cameraLabel.Parent = settingsFrame

		local mouseLockLabel = game.CoreGui.RobloxGui:FindFirstChild("MouseLockLabel",true)

		local enumItems = Enum.ControlMode:GetEnumItems()
		local enumNames = {}
		local enumNameToItem = {}
		for i,obj in ipairs(enumItems) do
			enumNames[i] = obj.Name
			enumNameToItem[obj.Name] = obj
		end

		local cameraDropDown
		local updateCameraDropDownSelection
		cameraDropDown, updateCameraDropDownSelection = RbxGui.CreateDropDownMenu(enumNames, 
			function(text) 
				UserSettings().GameSettings.ControlMode = enumNameToItem[text] 
				
				pcall(function()
					if mouseLockLabel and UserSettings().GameSettings.ControlMode == Enum.ControlMode["Mouse Lock Switch"] then
						mouseLockLabel.Visible = true
					else
						mouseLockLabel.Visible = false
					end
				end)
			end)
		cameraDropDown.Name = "CameraField"
		cameraDropDown.ZIndex = baseZIndex + 1
		cameraDropDown.Position = UDim2.new(0, 300, 0, 88)
		cameraDropDown.Size = UDim2.new(0,200,0,32)
		cameraDropDown.Parent = settingsFrame

		local exitButton = Instance.new("TextButton")
		exitButton.Name = "ExitBtn"
		exitButton.Font = Enum.Font.Arial
		exitButton.FontSize = Enum.FontSize.Size18
		exitButton.Position = UDim2.new(0.5, -100, 0, 200)
		exitButton.Size = UDim2.new(0,200,0,50)
		exitButton.AutoButtonColor = true
		exitButton.Style = Enum.ButtonStyle.RobloxButtonDefault 
		exitButton.Text = "OK"
		exitButton.TextColor3 = Color3I(255,255,255)
		exitButton.ZIndex = baseZIndex + 1

		exitButton.Parent = settingsFrame

		robloxLock(shield)

		exitButton.MouseButton1Click:connect(
			function() 
				shield.Visible = false
				settingsButton.Active = true
			end
		)
		
		settingsButton.MouseButton1Click:connect(
			function() 
				settingsButton.Active = false
				updateCameraDropDownSelection(UserSettings().GameSettings.ControlMode.Name)
				shield.Visible = true
			end
		)
			
		return shield
	end

	delay(0, function()
		createSettingsDialog().Parent = game:GetService("CoreGui").RobloxGui
		game:GetService("CoreGui").RobloxGui.BottomLeftControl.SettingsButton.Active = true
		
		local mouseLockLabel = game.CoreGui.RobloxGui:FindFirstChild("MouseLockLabel",true)
		if mouseLockLabel and UserSettings().GameSettings.ControlMode == Enum.ControlMode["Mouse Lock Switch"] then
			mouseLockLabel.Visible = true
		else
			mouseLockLabel.Visible = false
		end
	end)
	
end --UserSettings call

local function CreateTextButtons(frame, buttons, yPos, ySize)
	if #buttons < 1 then
		error("Must have more than one button")
	end

	local buttonNum = 1
	local buttonObjs = {}

	local function toggleSelection(button)
		for i, obj in ipairs(buttonObjs) do
			if obj == button then
				obj.Style = Enum.ButtonStyle.RobloxButtonDefault
			else
				obj.Style = Enum.ButtonStyle.RobloxButton
			end
		end
	end

	for i, obj in ipairs(buttons) do 
		local button = Instance.new("TextButton")
		button.Name = "Button" .. buttonNum
		button.Font = Enum.Font.Arial
		button.FontSize = Enum.FontSize.Size18
		button.AutoButtonColor = true
		button.Style = Enum.ButtonStyle.RobloxButton
		button.Text = obj.Text
		button.TextColor3 = Color3.new(1,1,1)
		button.MouseButton1Click:connect(function() toggleSelection(button) obj.Function() end)
		button.Parent = frame
		buttonObjs[buttonNum] = button

		buttonNum = buttonNum + 1
	end
	
	toggleSelection(buttonObjs[1])

	local numButtons = buttonNum-1

	if numButtons == 1 then
		frame.Button1.Position = UDim2.new(0.35, 0, yPos.Scale, yPos.Offset)
		frame.Button1.Size = UDim2.new(.4,0,ySize.Scale, ySize.Offset)
	elseif numButtons == 2 then
		frame.Button1.Position = UDim2.new(0.1, 0, yPos.Scale, yPos.Offset)
		frame.Button1.Size = UDim2.new(.35,0, ySize.Scale, ySize.Offset)

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

local function createHelpDialog()
	waitForChild(game:GetService("CoreGui").RobloxGui, "TopLeftControl")
	waitForChild(game:GetService("CoreGui").RobloxGui.TopLeftControl, "Help")

	local shield = Instance.new("Frame")
	shield.Name = "HelpDialogShield"
	shield.Active = true
	shield.Visible = false
	shield.Size = UDim2.new(1,0,1,0)
	shield.BackgroundColor3 = Color3I(51,51,51)
	shield.BorderColor3 = Color3I(27,42,53)
	shield.BackgroundTransparency = 0.4
	shield.ZIndex = baseZIndex + 1

	local helpDialog = Instance.new("Frame")
	helpDialog.Name = "HelpDialog"
	helpDialog.Style = Enum.FrameStyle.RobloxRound
	helpDialog.Position = UDim2.new(.2, 0, .2, 0)
	helpDialog.Size = UDim2.new(0.6, 0, 0.6, 0)
	helpDialog.Active = true
	helpDialog.Parent = shield

	local titleLabel = Instance.new("TextLabel")
	titleLabel.Name = "Title"
	titleLabel.Text = "Keyboard & Mouse Controls"
	titleLabel.Font = Enum.Font.ArialBold
	titleLabel.FontSize = Enum.FontSize.Size36
	titleLabel.Position = UDim2.new(0, 0, 0.025, 0)
	titleLabel.Size = UDim2.new(1, 0, 0, 40)
	titleLabel.TextColor3 = Color3.new(1,1,1)
	titleLabel.BackgroundTransparency = 1
	titleLabel.Parent = helpDialog

	local buttonRow = Instance.new("Frame")
	buttonRow.Name = "Buttons"
	buttonRow.Position = UDim2.new(0.1, 0, .07, 40)
	buttonRow.Size = UDim2.new(0.8, 0, 0, 45)
	buttonRow.BackgroundTransparency = 1
	buttonRow.Parent = helpDialog

	local imageFrame = Instance.new("Frame")
	imageFrame.Name = "ImageFrame"
	imageFrame.Position = UDim2.new(0.05, 0, 0.075, 80)
	imageFrame.Size = UDim2.new(0.9, 0, .9, -120)
	imageFrame.BackgroundTransparency = 1
	imageFrame.Parent = helpDialog

	local layoutFrame = Instance.new("Frame")
	layoutFrame.Name = "LayoutFrame"
	layoutFrame.Position = UDim2.new(0.5, 0, 0, 0)
	layoutFrame.Size = UDim2.new(1.5, 0, 1, 0)
	layoutFrame.BackgroundTransparency = 1
	layoutFrame.SizeConstraint = Enum.SizeConstraint.RelativeYY
	layoutFrame.Parent = imageFrame

	local image = Instance.new("ImageLabel")
	image.Name = "Image"
	image.Image = "rbxasset://textures/ui/tutorial_look.png"
	image.Position = UDim2.new(-0.5, 0, 0, 0)
	image.Size = UDim2.new(1, 0, 1, 0)
	image.BackgroundTransparency = 1
	image.Parent = layoutFrame

	local buttons = {}
	buttons[1] = {}
	buttons[1].Text = "Look"
	buttons[1].Function = function() 
		image.Image = "rbxasset://textures/ui/tutorial_look.png"
	end 
	buttons[2] = {}
	buttons[2].Text = "Move"
	buttons[2].Function = function() 
		image.Image = "rbxasset://textures/ui/tutorial_move.png"
	end 
	buttons[3] = {}
	buttons[3].Text = "Gear"
	buttons[3].Function = function() 
		image.Image = "rbxasset://textures/ui/tutorial_gear.png"
	end
	buttons[4] = {}
	buttons[4].Text = "Zoom"
	buttons[4].Function = function() 	
		image.Image = "rbxasset://textures/ui/tutorial_zoom.png"
	end 

	CreateTextButtons(buttonRow, buttons, UDim.new(0, 0), UDim.new(1,0))

	local okBtn = Instance.new("TextButton")
	okBtn.Name = "OkBtn"
	okBtn.Text = "OK"
	okBtn.Size = UDim2.new(0.3, 0, 0, 45)
	okBtn.Position = UDim2.new(0.35, 0, .975, -50)
	okBtn.Font = Enum.Font.Arial
	okBtn.FontSize = Enum.FontSize.Size18
	okBtn.BackgroundTransparency = 1
	okBtn.TextColor3 = Color3.new(1,1,1)
	okBtn.Style = Enum.ButtonStyle.RobloxButtonDefault
	okBtn.MouseButton1Click:connect(function()
		shield.Visible = false
	end)
	okBtn.Parent = helpDialog

	robloxLock(shield)
	return shield
end

local createSaveDialogs = function()
	local shield = Instance.new("TextButton")
	shield.Text = ""
	shield.AutoButtonColor = false
	shield.Name = "SaveDialogShield"
	shield.Active = true
	shield.Visible = false
	shield.Size = UDim2.new(1,0,1,0)
	shield.BackgroundColor3 = Color3I(51,51,51)
	shield.BorderColor3 = Color3I(27,42,53)
	shield.BackgroundTransparency = 0.4
	shield.ZIndex = baseZIndex+1

	local clearAndResetDialog
	local save
	local saveLocal
	local dontSave
	local cancel

	local messageBoxButtons = {}
	messageBoxButtons[1] = {}
	messageBoxButtons[1].Text = "Save"
	messageBoxButtons[1].Function = function() save() end 
	messageBoxButtons[2] = {}
	messageBoxButtons[2].Text = "Cancel"
	messageBoxButtons[2].Function = function() cancel() end 
	messageBoxButtons[3] = {}
	messageBoxButtons[3].Text = "Don't Save"
	messageBoxButtons[3].Function = function() dontSave() end 

	local saveDialogMessageBox = RbxGui.CreateStyledMessageDialog("Unsaved Changes", "Save your changes to ROBLOX before leaving?", "Confirm", messageBoxButtons)
	saveDialogMessageBox.Visible = true
	saveDialogMessageBox.Parent = shield


	local errorBoxButtons = {}

	local buttonOffset = 1
	if game.LocalSaveEnabled then
		errorBoxButtons[buttonOffset] = {}
		errorBoxButtons[buttonOffset].Text = "Save to Disk"
		errorBoxButtons[buttonOffset].Function = function() saveLocal() end 
		buttonOffset = buttonOffset + 1
	end
	errorBoxButtons[buttonOffset] = {}
	errorBoxButtons[buttonOffset].Text = "Keep Playing"
	errorBoxButtons[buttonOffset].Function = function() cancel() end 
	errorBoxButtons[buttonOffset+1] = {}
	errorBoxButtons[buttonOffset+1].Text = "Don't Save"
	errorBoxButtons[buttonOffset+1].Function = function() dontSave() end 

	local errorDialogMessageBox = RbxGui.CreateStyledMessageDialog("Upload Failed", "Sorry, we could not save your changes to ROBLOX.", "Error", errorBoxButtons)
	errorDialogMessageBox.Visible = false
	errorDialogMessageBox.Parent = shield

	local spinnerDialog = Instance.new("Frame")
	spinnerDialog.Name = "SpinnerDialog"
	spinnerDialog.Style = Enum.FrameStyle.RobloxRound
	spinnerDialog.Size = UDim2.new(0, 350, 0, 150)
	spinnerDialog.Position = UDim2.new(.5, -175, .5, -75)
	spinnerDialog.Visible = false
	spinnerDialog.Active = true
	spinnerDialog.Parent = shield

	local waitingLabel = Instance.new("TextLabel")
	waitingLabel.Name = "WaitingLabel"
	waitingLabel.Text = "Saving to ROBLOX..."
	waitingLabel.Font = Enum.Font.ArialBold
	waitingLabel.FontSize = Enum.FontSize.Size18
	waitingLabel.Position = UDim2.new(0.5, 25, 0.5, 0)
	waitingLabel.TextColor3 = Color3.new(1,1,1)
	waitingLabel.Parent = spinnerDialog

	local spinnerFrame = Instance.new("Frame")
	spinnerFrame.Name = "Spinner"
	spinnerFrame.Size = UDim2.new(0, 80, 0, 80)
	spinnerFrame.Position = UDim2.new(0.5, -150, 0.5, -40)
	spinnerFrame.BackgroundTransparency = 1
	spinnerFrame.Parent = spinnerDialog

	local spinnerIcons = {}
	local spinnerNum = 1
	while spinnerNum <= 8 do
		local spinnerImage = Instance.new("ImageLabel")
	   spinnerImage.Name = "Spinner"..spinnerNum
		spinnerImage.Size = UDim2.new(0, 16, 0, 16)
		spinnerImage.Position = UDim2.new(.5+.3*math.cos(math.rad(45*spinnerNum)), -8, .5+.3*math.sin(math.rad(45*spinnerNum)), -8)
		spinnerImage.BackgroundTransparency = 1
	   spinnerImage.Image = "rbxasset://textures/ui/Spinner.png"
		spinnerImage.Parent = spinnerFrame

	   spinnerIcons[spinnerNum] = spinnerImage
	   spinnerNum = spinnerNum + 1
	end

	save = function()
		saveDialogMessageBox.Visible = false
		
		--Show the spinner dialog
		spinnerDialog.Visible = true
		local spin = true
		--Make it spin
		delay(0, function()
		  local spinPos = 0
			while spin do
				local pos = 0

				while pos < 8 do
					if pos == spinPos or pos == ((spinPos+1)%8) then
						spinnerIcons[pos+1].Image = "rbxasset://textures/ui/Spinner2.png"
					else
						spinnerIcons[pos+1].Image = "rbxasset://textures/ui/Spinner.png"
					end
					
					pos = pos + 1
				end
				spinPos = (spinPos + 1) % 8
				wait(0.2)
			end
		end)

		--Do the save while the spinner is going, function will wait
		local result = game:SaveToRoblox()
		if not result then
			--Try once more
			result = game:SaveToRoblox()
		end

		--Hide the spinner dialog
		spinnerDialog.Visible = false
		--And cause the delay thread to stop
		spin = false	

		--Now process the result
		if result then
			--Success, close
			game:FinishShutdown(false)
			clearAndResetDialog()
		else
			--Failure, show the second dialog prompt
			errorDialogMessageBox.Visible = true
		end
	end

	saveLocal = function()
		errorDialogMessageBox.Visible = false
		game:FinishShutdown(true)
		clearAndResetDialog()
	end

	dontSave = function()
		saveDialogMessageBox.Visible = false
		errorDialogMessageBox.Visible = false
		game:FinishShutdown(false)
		clearAndResetDialog()
	end
	cancel = function()
		saveDialogMessageBox.Visible = false
		errorDialogMessageBox.Visible = false
		clearAndResetDialog()
	end

	clearAndResetDialog = function()
		saveDialogMessageBox.Visible = true
		errorDialogMessageBox.Visible = false
		spinnerDialog.Visible = false
		shield.Visible = false
	end

	robloxLock(shield)
	shield.Visible = false
	return shield
end

local createReportAbuseDialog = function()
	--Only show things if we are a NetworkClient
	waitForChild(game,"NetworkClient")

	waitForChild(game,"Players")
	waitForProperty(game.Players, "LocalPlayer")
	local localPlayer = game.Players.LocalPlayer
	
	waitForChild(game:GetService("CoreGui").RobloxGui, "BottomRightControl")
	waitForChild(game:GetService("CoreGui").RobloxGui.BottomRightControl, "ReportAbuse")
	local reportAbuseButton = game:GetService("CoreGui").RobloxGui.BottomRightControl.ReportAbuse

	local shield = Instance.new("TextButton")
	shield.Name = "ReportAbuseShield"
	shield.Text = ""
	shield.AutoButtonColor = false
	shield.Active = true
	shield.Visible = false
	shield.Size = UDim2.new(1,0,1,0)
	shield.BackgroundColor3 = Color3I(51,51,51)
	shield.BorderColor3 = Color3I(27,42,53)
	shield.BackgroundTransparency = 0.4
	shield.ZIndex = baseZIndex + 1

	local closeAndResetDialog

	local messageBoxButtons = {}
	messageBoxButtons[1] = {}
	messageBoxButtons[1].Text = "Ok"
	messageBoxButtons[1].Function = function() closeAndResetDialog() end 
	local calmingMessageBox = RbxGui.CreateMessageDialog("Thanks for your report!", "Our moderators will review the chat logs and determine what happened.  The other user is probably just trying to make you mad.\n\nIf anyone used swear words, inappropriate language, or threatened you in real life, please report them for Bad Words or Threats", messageBoxButtons)
	calmingMessageBox.Visible = false
	calmingMessageBox.Parent = shield

	local normalMessageBox = RbxGui.CreateMessageDialog("Thanks for your report!", "Our moderators will review the chat logs and determine what happened.", messageBoxButtons)
	normalMessageBox.Visible = false
	normalMessageBox.Parent = shield

	local frame = Instance.new("Frame")
	frame.Name = "Settings"
	frame.Position = UDim2.new(0.5, -250, 0.5, -200)
	frame.Size = UDim2.new(0.0, 500, 0.0, 400)
	frame.BackgroundTransparency = 1
	frame.Active = true
	frame.Parent = shield

	--[[local settingsBacker = Instance.new("Frame")
	settingsBacker.Size = UDim2.new(1, 0, 1, 0)
	settingsBacker.Style = Enum.FrameStyle.RobloxRound
	settingsBacker.Active = true
	settingsBacker.ZIndex = 1
	settinssBacker.Parent = frame]]--

	local settingsFrame = Instance.new("Frame")
	settingsFrame.Name = "ReportAbuseStyle"
	settingsFrame.Size = UDim2.new(1, 0, 1, 0)
	settingsFrame.Style = Enum.FrameStyle.RobloxRound
	settingsFrame.Active = true
	settingsFrame.ZIndex = baseZIndex
	settingsFrame.Parent = frame

	local title = Instance.new("TextLabel")
	title.Name = "Title"
	title.Text = "Report Abuse"
	title.TextColor3 = Color3I(221,221,221)
	title.Position = UDim2.new(0.5, 0, 0, 30)
	title.Font = Enum.Font.ArialBold
	title.FontSize = Enum.FontSize.Size36
	title.ZIndex = baseZIndex + 1
	title.Parent = settingsFrame

	local description = Instance.new("TextLabel")
	description.Name = "Description"
	description.Text = "This will send a complete report to a moderator.  The moderator will review the chat log and take appropriate action."
	description.TextColor3 = Color3I(221,221,221)
	description.Position = UDim2.new(0,20, 0, 55)
	description.Size = UDim2.new(1, -40, 0, 40)
	description.BackgroundTransparency = 1
	description.Font = Enum.Font.Arial
	description.FontSize = Enum.FontSize.Size18
	description.TextWrap = true
	description.ZIndex = baseZIndex + 1
	description.TextXAlignment = Enum.TextXAlignment.Left
	description.TextYAlignment = Enum.TextYAlignment.Top
	description.Parent = settingsFrame

	local playerLabel = Instance.new("TextLabel")
	playerLabel.Name = "PlayerLabel"
	playerLabel.Text = "Which player?"
	playerLabel.BackgroundTransparency = 1
	playerLabel.Font = Enum.Font.Arial
	playerLabel.FontSize = Enum.FontSize.Size18
	playerLabel.Position = UDim2.new(0.025,0,0,100)
	playerLabel.Size 	   = UDim2.new(0.4,0,0,36)
	playerLabel.TextColor3 = Color3I(255,255,255)
	playerLabel.TextXAlignment = Enum.TextXAlignment.Left
	playerLabel.ZIndex = baseZIndex + 1
	playerLabel.Parent = settingsFrame

	local abusingPlayer = nil
	local abuse = nil
	local submitReportButton = nil

	local updatePlayerSelection = nil
	local createPlayersDropDown = function()
		local players = game:GetService("Players")
		local playerNames = {}
		local nameToPlayer = {}
		local children = players:GetChildren()
		local pos = 1
		if children then
		   for i, player in ipairs(children) do
				if player:IsA("Player") and player ~= localPlayer then
					playerNames[pos] = player.Name
					nameToPlayer[player.Name] = player
					pos = pos + 1
				end
			end
		end
		local playerDropDown = nil
		playerDropDown, updatePlayerSelection = RbxGui.CreateDropDownMenu(playerNames, 
			function(playerName) 
				abusingPlayer = nameToPlayer[playerName] 
				if abuse and abusingPlayer then
					submitReportButton.Active = true
				end
			end)
		playerDropDown.Name = "PlayersComboBox"
		playerDropDown.ZIndex = baseZIndex + 1
		playerDropDown.Position = UDim2.new(.425, 0, 0, 102)
		playerDropDown.Size = UDim2.new(.55,0,0,32)
		
		return playerDropDown
	end
	
	local abuseLabel = Instance.new("TextLabel")
	abuseLabel.Name = "AbuseLabel"
	abuseLabel.Text = "What did they do?"
	abuseLabel.Font = Enum.Font.Arial
	abuseLabel.BackgroundTransparency = 1
	abuseLabel.FontSize = Enum.FontSize.Size18
	abuseLabel.Position = UDim2.new(0.025,0,0,140)
	abuseLabel.Size = UDim2.new(0.4,0,0,36)
	abuseLabel.TextColor3 = Color3I(255,255,255)
	abuseLabel.TextXAlignment = Enum.TextXAlignment.Left
	abuseLabel.ZIndex = baseZIndex + 1
	abuseLabel.Parent = settingsFrame

	local abuses = {"Bad Words or Threats","Bad Username","Talking about Dating","Account Trading or Sharing","Asking Personal Questions","Rude or Mean Behavior","False Reporting Me"}
	local abuseDropDown, updateAbuseSelection = RbxGui.CreateDropDownMenu(abuses, 
		function(abuseText) 
			abuse = abuseText 
			if abuse and abusingPlayer then
				submitReportButton.Active = true
			end
		end, true)
	abuseDropDown.Name = "AbuseComboBox"
	abuseDropDown.ZIndex = baseZIndex + 1
	abuseDropDown.Position = UDim2.new(0.425, 0, 0, 142)
	abuseDropDown.Size = UDim2.new(0.55,0,0,32)
	abuseDropDown.Parent = settingsFrame

	local shortDescriptionLabel = Instance.new("TextLabel")
	shortDescriptionLabel.Name = "ShortDescriptionLabel"
	shortDescriptionLabel.Text = "Short Description: (optional)"
	shortDescriptionLabel.Font = Enum.Font.Arial
	shortDescriptionLabel.FontSize = Enum.FontSize.Size18
	shortDescriptionLabel.Position = UDim2.new(0.025,0,0,180)
	shortDescriptionLabel.Size = UDim2.new(0.95,0,0,36)
	shortDescriptionLabel.TextColor3 = Color3I(255,255,255)
	shortDescriptionLabel.TextXAlignment = Enum.TextXAlignment.Left
	shortDescriptionLabel.BackgroundTransparency = 1
	shortDescriptionLabel.ZIndex = baseZIndex + 1
	shortDescriptionLabel.Parent = settingsFrame

	local shortDescriptionWrapper = Instance.new("Frame")
	shortDescriptionWrapper.Name = "ShortDescriptionWrapper"
	shortDescriptionWrapper.Position = UDim2.new(0.025,0,0,220)
	shortDescriptionWrapper.Size = UDim2.new(0.95,0,1,-310)
	shortDescriptionWrapper.BackgroundColor3 = Color3I(0,0,0)
	shortDescriptionWrapper.BorderSizePixel = 0
	shortDescriptionWrapper.ZIndex = baseZIndex + 1
	shortDescriptionWrapper.Parent = settingsFrame

	local shortDescriptionBox = Instance.new("TextBox")
	shortDescriptionBox.Name = "TextBox"
	shortDescriptionBox.Text = ""
	shortDescriptionBox.Font = Enum.Font.Arial
	shortDescriptionBox.FontSize = Enum.FontSize.Size18
	shortDescriptionBox.Position = UDim2.new(0,3,0,3)
	shortDescriptionBox.Size = UDim2.new(1,-6,1,-6)
	shortDescriptionBox.TextColor3 = Color3I(255,255,255)
	shortDescriptionBox.TextXAlignment = Enum.TextXAlignment.Left
	shortDescriptionBox.TextYAlignment = Enum.TextYAlignment.Top
	shortDescriptionBox.TextWrap = true
	shortDescriptionBox.BackgroundColor3 = Color3I(0,0,0)
	shortDescriptionBox.BorderSizePixel = 0
	shortDescriptionBox.ZIndex = baseZIndex + 1
	shortDescriptionBox.Parent = shortDescriptionWrapper

	submitReportButton = Instance.new("TextButton")
	submitReportButton.Name = "SubmitReportBtn"
	submitReportButton.Active = false
	submitReportButton.Font = Enum.Font.Arial
	submitReportButton.FontSize = Enum.FontSize.Size18
	submitReportButton.Position = UDim2.new(0.1, 0, 1, -80)
	submitReportButton.Size = UDim2.new(0.35,0,0,50)
	submitReportButton.AutoButtonColor = true
	submitReportButton.Style = Enum.ButtonStyle.RobloxButtonDefault 
	submitReportButton.Text = "Submit Report"
	submitReportButton.TextColor3 = Color3I(255,255,255)
	submitReportButton.ZIndex = baseZIndex + 1
	submitReportButton.Parent = settingsFrame

	submitReportButton.MouseButton1Click:connect(function()
		if submitReportButton.Active then
			if abuse and abusingPlayer then
				frame.Visible = false
				game.Players:ReportAbuse(abusingPlayer, abuse, shortDescriptionBox.Text)
				if abuse == "Rude or Mean Behavior" or abuse == "False Reporting Me" then
					calmingMessageBox.Visible = true
				else
					normalMessageBox.Visible = true
				end
			else
				closeAndResetDialog()
			end
		end
	end)

	local cancelButton = Instance.new("TextButton")
	cancelButton.Name = "CancelBtn"
	cancelButton.Font = Enum.Font.Arial
	cancelButton.FontSize = Enum.FontSize.Size18
	cancelButton.Position = UDim2.new(0.55, 0, 1, -80)
	cancelButton.Size = UDim2.new(0.35,0,0,50)
	cancelButton.AutoButtonColor = true
	cancelButton.Style = Enum.ButtonStyle.RobloxButtonDefault 
	cancelButton.Text = "Cancel"
	cancelButton.TextColor3 = Color3I(255,255,255)
	cancelButton.ZIndex = baseZIndex + 1
	cancelButton.Parent = settingsFrame

	closeAndResetDialog = function()
		--Delete old player combo box
		local oldComboBox = settingsFrame:FindFirstChild("PlayersComboBox")
		if oldComboBox then
			oldComboBox.Parent = nil
		end
		
		abusingPlayer = nil updatePlayerSelection(nil)
		abuse = nil updateAbuseSelection(nil)
		submitReportButton.Active = false
		shortDescriptionBox.Text = ""
		frame.Visible = true
		calmingMessageBox.Visible = false
		normalMessageBox.Visible = false
		shield.Visible = false		
		reportAbuseButton.Active = true
	end

	cancelButton.MouseButton1Click:connect(closeAndResetDialog)
	
	reportAbuseButton.MouseButton1Click:connect(
		function() 
			createPlayersDropDown().Parent = settingsFrame
			reportAbuseButton.Active = false
			shield.Visible = true
		end
	)

	robloxLock(shield)
	return shield
end

local createChatBar = function()
	--Only show a chat bar if we are a NetworkClient
	waitForChild(game, "NetworkClient")

	waitForChild(game, "Players")
	waitForProperty(game.Players, "LocalPlayer")
	
	local chatBar = Instance.new("Frame")
	chatBar.Name = "ChatBar"
	chatBar.Size = UDim2.new(1, 0, 0, 22)
	chatBar.Position = UDim2.new(0, 0, 1, 0)
	chatBar.BackgroundColor3 = Color3.new(0,0,0)
	chatBar.BorderSizePixel = 0

	local chatBox = Instance.new("TextBox")
	chatBox.Text = ""
	chatBox.Visible = false
	chatBox.Size = UDim2.new(1,-4,1,0)
	chatBox.Position = UDim2.new(0,2,0,0)
	chatBox.TextXAlignment = Enum.TextXAlignment.Left
	chatBox.Font = Enum.Font.Arial
	chatBox.ClearTextOnFocus = false
	chatBox.FontSize = Enum.FontSize.Size14
	chatBox.TextColor3 = Color3.new(1,1,1)
	chatBox.BackgroundTransparency = 1
	chatBox.Parent = chatBar

	local chatButton = Instance.new("TextButton")
	chatButton.Size = UDim2.new(1,-4,1,0)
	chatButton.Position = UDim2.new(0,2,0,0)
	chatButton.AutoButtonColor = false
	chatButton.Text = "To chat click here or press \"/\" key"
	chatButton.TextXAlignment = Enum.TextXAlignment.Left
	chatButton.Font = Enum.Font.Arial
	chatButton.FontSize = Enum.FontSize.Size14
	chatButton.TextColor3 = Color3.new(1,1,1)
	chatButton.BackgroundTransparency = 1
	chatButton.Parent = chatBar

	local activateChat = function()
		if chatBox.Visible then
			return
		end
		chatButton.Visible = false
		chatBox.Text = ""
		chatBox.Visible = true
		chatBox:CaptureFocus()
	end

	chatButton.MouseButton1Click:connect(activateChat)

	local guiService = game:GetService("GuiService")
	guiService:AddKey("/")
	guiService.KeyPressed:connect(function(key) if key == "/" then activateChat() end end)

	chatBox.FocusLost:connect(
		function(enterPressed)
			if enterPressed then
				if chatBox.Text ~= "" then
					local str = chatBox.Text
					if string.sub(str, 1, 1) == '%' then
						game.Players:TeamChat(str)
					else
						game.Players:Chat(str)
					end
				end
			end
			chatBox.Text = ""
			chatBox.Visible = false
			chatButton.Visible = true
		end)
	robloxLock(chatBar)
	return chatBar
end

--Spawn a thread for the help dialog
delay(0, 
	function()
		local helpDialog = createHelpDialog()
		helpDialog.Parent = game:GetService("CoreGui").RobloxGui
		
		game:GetService("CoreGui").RobloxGui.TopLeftControl.Help.MouseButton1Click:connect(function() helpDialog.Visible = true end)
		game:GetService("CoreGui").RobloxGui.TopLeftControl.Help.Active = true
	end)

--Spawn a thread for the Save dialogs
local isSaveDialogSupported = pcall(function() local var = game.LocalSaveEnabled end)
if isSaveDialogSupported then
	delay(0, 
		function()
			local saveDialogs = createSaveDialogs()
			saveDialogs.Parent = game:GetService("CoreGui").RobloxGui
		
			game.RequestShutdown = function()
				saveDialogs.Visible = true
				return true
			end
		end)
end

--Spawn a thread for the Report Abuse dialogs
delay(0, 
	function()
		createReportAbuseDialog().Parent = game:GetService("CoreGui").RobloxGui
		script.Parent.BottomRightControl.ReportAbuse.Active = true
	end)

--Spawn a thread for Chat Bar
local isChatBarSupported = pcall(function() return game.CoreGui.Version end)
local isMacChat, version = pcall(function() return game.GuiService.Version end)
if isChatBarSupported and isMacChat and version == 2 then
	delay(0,
		function()
			local chatBar = createChatBar()
			chatBar.Parent = game:GetService("CoreGui").RobloxGui
			game.GuiService:SetGlobalSizeOffsetPixel(0,-22)
		end)
end
end --LoadLibrary if

end)