-- Creates all neccessary scripts for the gui on initial load, everything except build tools
-- Created by Ben T. 10/29/10
-- Please note that these are loaded in a specific order to diminish errors/perceived load time by user

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

waitForChild(game:GetService("CoreGui"),"RobloxGui")
local screenGui = game:GetService("CoreGui"):FindFirstChild("RobloxGui")

local scriptContext = game:GetService("ScriptContext")

-- Resizer (dynamically resizes gui)
dofile("rbxasset://scripts\\cores\\Resizer.lua")

-- SubMenuBuilder (builds out the material,surface and color panels)
dofile("rbxasset://scripts\\cores\\SubMenuBuilder.lua")

-- ToolTipper  (creates tool tips for gui)
dofile("rbxasset://scripts\\cores\\ToolTipper.lua")

--[[-- (controls the movement and selection of sub panels)
-- PaintMenuMover
scriptContext:AddCoreScript(36040464,screenGui.BuildTools.Frame.PropertyTools.PaintTool,"PaintMenuMover")
-- MaterialMenuMover
scriptContext:AddCoreScript(36040495,screenGui.BuildTools.Frame.PropertyTools.MaterialSelector,"MaterialMenuMover")
-- InputMenuMover
scriptContext:AddCoreScript(36040483,screenGui.BuildTools.Frame.PropertyTools.InputSelector,"InputMenuMover")]]--

-- SettingsScript 
dofile("rbxasset://scripts\\cores\\SettingsScript.lua")

-- MainChatScript
dofile("rbxasset://scripts\\cores\\MainBotChatScript.lua")

end)
