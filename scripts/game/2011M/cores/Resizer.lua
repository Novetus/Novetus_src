-- this script is responsible for keeping the gui proportions under control

delay(0, function()

local screen = game:GetService("CoreGui").RobloxGui

local BottomLeftControl
local BottomRightControl
local TopLeftControl
local BuildTools

local controlFrame = screen:FindFirstChild("ControlFrame")
local loadoutPadding = 43
local currentLoadout

BottomLeftControl = controlFrame:FindFirstChild("BottomLeftControl")
BottomRightControl = controlFrame:FindFirstChild("BottomRightControl")
TopLeftControl = controlFrame:FindFirstChild("TopLeftControl")
currentLoadout = screen:FindFirstChild("CurrentLoadout")
BuildTools = controlFrame:FindFirstChild("BuildTools")

function makeYRelative()

	BottomLeftControl.SizeConstraint = 2
	BottomRightControl.SizeConstraint = 2
	if TopLeftControl then TopLeftControl.SizeConstraint = 2 end
	if currentLoadout then currentLoadout.SizeConstraint = 2 end
	if BuildTools then BuildTools.Frame.SizeConstraint = 2 end
	
	BottomLeftControl.Position = UDim2.new(0,0,1,-BottomLeftControl.AbsoluteSize.Y)
	BottomRightControl.Position = UDim2.new(1,-BottomRightControl.AbsoluteSize.X,1,-BottomRightControl.AbsoluteSize.Y)
	
end



function makeXRelative()

	BottomLeftControl.SizeConstraint = 1
	BottomRightControl.SizeConstraint = 1
	if TopLeftControl then TopLeftControl.SizeConstraint = 1 end
	if currentLoadout then currentLoadout.SizeConstraint = 1 end
	if BuildTools then BuildTools.Frame.SizeConstraint = 1 end

	BottomLeftControl.Position = UDim2.new(0,0,1,-BottomLeftControl.AbsoluteSize.Y)
	BottomRightControl.Position = UDim2.new(1,-BottomRightControl.AbsoluteSize.X,1,-BottomRightControl.AbsoluteSize.Y)

end

local function resize()
	if screen.AbsoluteSize.x > screen.AbsoluteSize.y then
		makeYRelative()
	else
		makeXRelative()
	end
	if currentLoadout then
		currentLoadout.Position =
			UDim2.new(0,screen.AbsoluteSize.X/2 -currentLoadout.AbsoluteSize.X/2,currentLoadout.Position.Y.Scale,-currentLoadout.AbsoluteSize.Y - loadoutPadding)
	end
end
screen.Changed:connect(function(property)

	if property == "AbsoluteSize" then
		wait()
		resize()
	end

end)

wait()
resize()

end)