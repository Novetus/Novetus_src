-- Creates the tool tips for the new gui!

delay(0, function()

local controlFrame = game:GetService("CoreGui").RobloxGui:FindFirstChild("ControlFrame")

local topLeftControl
local bottomLeftControl
local bottomRightControl

if controlFrame then
	topLeftControl = controlFrame:FindFirstChild("TopLeftControl")
	bottomLeftControl = controlFrame:FindFirstChild("BottomLeftControl")
	bottomRightControl = controlFrame:FindFirstChild("BottomRightControl")
else
	topLeftControl = script.Parent:FindFirstChild("TopLeftControl")
	bottomLeftControl = script.Parent:FindFirstChild("BottomLeftControl")
	bottomRightControl = script.Parent:FindFirstChild("BottomRightControl")
end

local frame = Instance.new("TextLabel")
frame.RobloxLocked = true
frame.Name = "ToolTip"
frame.Text = "Hi! I'm a ToolTip!"
frame.Font = Enum.Font.ArialBold
frame.FontSize = Enum.FontSize.Size12
frame.TextColor3 = Color3.new(1,1,1)
frame.BorderSizePixel = 0
frame.ZIndex = 10
frame.Size = UDim2.new(2,0,1,0)
frame.Position = UDim2.new(1,0,0,0)
frame.BackgroundColor3 = Color3.new(0,0,0)
frame.BackgroundTransparency = 1
frame.TextTransparency = 1
frame.TextWrap = true

local inside = Instance.new("BoolValue")
inside.RobloxLocked = true
inside.Name = "inside"
inside.Value = false
inside.Parent = frame

function setUpListeners(frame)
	local fadeSpeed = 0.1
	frame.Parent.MouseEnter:connect(function()
		frame.inside.Value = true
		wait(1.2)
		if frame.inside.Value then
			while frame.inside.Value and frame.BackgroundTransparency > 0 do
				frame.BackgroundTransparency = frame.BackgroundTransparency - fadeSpeed
				frame.TextTransparency = frame.TextTransparency - fadeSpeed
				wait()
			end
		end
	end)
	frame.Parent.MouseLeave:connect(function()
		frame.inside.Value = false
		frame.BackgroundTransparency = 1
		frame.TextTransparency = 1
	end)
	frame.Parent.MouseButton1Click:connect(function()
		frame.inside.Value = false
		frame.BackgroundTransparency = 1
		frame.TextTransparency = 1
	end)
end

----------------- set up Top Left Tool Tips --------------------------

if topLeftControl then 
	local topLeftChildren = topLeftControl:GetChildren()

	for i = 1, #topLeftChildren do

		if topLeftChildren[i].Name == "Help" then
			local helpTip = frame:clone()
			helpTip.RobloxLocked = true
			helpTip.Text = "Help"
			helpTip.Parent = topLeftChildren[i]
			setUpListeners(helpTip)
		end

	end
end

---------------- set up Bottom Left Tool Tips -------------------------

local bottomLeftChildren = bottomLeftControl:GetChildren()

for i = 1, #bottomLeftChildren do

	if bottomLeftChildren[i].Name == "Exit" then
	    local exitTip = frame:clone()
	    exitTip.RobloxLocked = true
	    exitTip.Text = "Leave Place"
	    exitTip.Position = UDim2.new(0,0,-1,0)
	    exitTip.Size = UDim2.new(1,0,1,0)
	    exitTip.Parent = bottomLeftChildren[i]
	    setUpListeners(exitTip)
	elseif bottomLeftChildren[i].Name == "TogglePlayMode" then
	    local playTip = frame:clone()
	    playTip.RobloxLocked = true
     	playTip.Text = "Roblox Studio"
	    playTip.Position = UDim2.new(0,0,-1,0)
	    playTip.Parent = bottomLeftChildren[i]
	    setUpListeners(playTip)
	elseif bottomLeftChildren[i].Name == "ToolButton" then
	    local toolTip = frame:clone()
	    toolTip.RobloxLocked = true
	    toolTip.Text = "Build Tools"
	    toolTip.Position = UDim2.new(0,0,-1,0)
	    toolTip.Parent = bottomLeftChildren[i]
	    setUpListeners(toolTip)
	elseif bottomLeftChildren[i].Name == "SettingsButton" then
	    local toolTip = frame:clone()
	    toolTip.RobloxLocked = true
	    toolTip.Text = "Settings"
	    toolTip.Position = UDim2.new(0,0,-1,0)
	    toolTip.Parent = bottomLeftChildren[i]
	    setUpListeners(toolTip)
	end
end

---------------- set up Bottom Right Tool Tips -------------------------

local bottomRightChildren = bottomRightControl:GetChildren()

for i = 1, #bottomRightChildren do

	if bottomRightChildren[i].Name == "ToggleFullScreen" then
	    local fullScreen = frame:clone()
	    fullScreen.RobloxLocked = true
	    fullScreen.Text = "Fullscreen"
	    fullScreen.Position = UDim2.new(-1,0,-1,0)
	    fullScreen.Size = UDim2.new(2.4,0,1,0)
	    fullScreen.Parent = bottomRightChildren[i]
	    setUpListeners(fullScreen)
	elseif bottomRightChildren[i].Name == "ReportAbuse" then
	    local abuseTip = frame:clone()
	    abuseTip.RobloxLocked = true
		abuseTip.Text = "Report Abuse"
	    abuseTip.Position = UDim2.new(0,0,-1,0)
	    abuseTip.Parent = bottomRightChildren[i]
	    setUpListeners(abuseTip)
	elseif bottomRightChildren[i].Name == "Screenshot" then
	    local shotTip = frame:clone()
	    shotTip.RobloxLocked = true
	    shotTip.Text = "Screenshot"
	    shotTip.Position = UDim2.new(0,0,-1,0)
	    shotTip.Size = UDim2.new(2.1,0,1,0)
		shotTip.Parent = bottomRightChildren[i]
		setUpListeners(shotTip)
	elseif bottomRightChildren[i].Name:find("Camera") ~= nil then
		local cameraTip = frame:clone()
		cameraTip.RobloxLocked = true
		cameraTip.Text = "Camera View"
		if bottomRightChildren[i].Name:find("Zoom") then
			cameraTip.Position = UDim2.new(-1,0,-1.5)
		else
			cameraTip.Position = UDim2.new(0,0,-1.5,0)
		end
		cameraTip.Size = UDim2.new(2,0,1.25,0)
		cameraTip.Parent = bottomRightChildren[i]
		setUpListeners(cameraTip)
	elseif bottomRightChildren[i].Name == "RecordToggle" then
		local recordTip = frame:clone()
		recordTip.RobloxLocked = true
		recordTip.Text = "Take Video"
		recordTip.Position = UDim2.new(0,0,-1.1,0)
		recordTip.Size = UDim2.new(1,0,1,0)
		recordTip.Parent = bottomRightChildren[i]
		setUpListeners(recordTip)
	elseif bottomRightChildren[i].Name == "Help" then
		print("found help in bottom right")
		local helpTip = frame:clone()
		helpTip.RobloxLocked = true
		helpTip.Text = "Help"
		helpTip.Position = UDim2.new(-0.5,0,-1,0)
		helpTip.Size = UDim2.new(1.5,0,1,0)
		helpTip.Parent = bottomRightChildren[i]
		setUpListeners(helpTip)
	end
end

end)