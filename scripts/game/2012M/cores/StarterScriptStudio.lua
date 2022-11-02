
-- Creates all neccessary scripts for the gui on initial load, everything except build tools
-- Created by Ben T. 10/29/10
-- Please note that these are loaded in a specific order to diminish errors/perceived load time by user

delay(0, function()

print("Accurate CS by Matt Brown.")

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


local backpackTestPlaces = {41324860,87241143,1818,65033,25415,14403,33913,21783593,17467963,3271,16184658}

waitForChild(game:GetService("CoreGui"),"RobloxGui")
local screenGui = game:GetService("CoreGui"):FindFirstChild("RobloxGui")

local scriptContext = game:GetService("ScriptContext")

-- Resizer (dynamically resizes gui)
dofile("rbxasset://scripts\\cores\\Resizer.lua")

-- SubMenuBuilder (builds out the material,surface and color panels)
dofile("rbxasset://scripts\\cores\\SubMenuBuilder.lua")

-- ToolTipper  (creates tool tips for gui)
dofile("rbxasset://scripts\\cores\\ToolTipper.lua")

--[[if game.CoreGui.Version < 2 then
	-- (controls the movement and selection of sub panels)
	-- PaintMenuMover
	scriptContext:AddCoreScript(36040464,screenGui.BuildTools.Frame.PropertyTools.PaintTool,"PaintMenuMover")
	-- MaterialMenuMover
	scriptContext:AddCoreScript(36040495,screenGui.BuildTools.Frame.PropertyTools.MaterialSelector,"MaterialMenuMover")
	-- InputMenuMover
	scriptContext:AddCoreScript(36040483,screenGui.BuildTools.Frame.PropertyTools.InputSelector,"InputMenuMover")
end]]

-- SettingsScript 
dofile("rbxasset://scripts\\cores\\SettingsScript.lua")

-- MainBotChatScript
dofile("rbxasset://scripts\\cores\\MainBotChatScriptStudio.lua")

if game.CoreGui.Version >= 2 then
	-- New Player List
	dofile("rbxasset://scripts\\cores\\PlayerlistScript.lua")
	-- Popup Script
	dofile("rbxasset://scripts\\cores\\PopupScript.lua")
	-- Friend Notification Script (probably can use this script to expand out to other notifications)
	dofile("rbxasset://scripts\\cores\\NotificationScript.lua")
end

if game.CoreGui.Version >= 3 then
	waitForProperty(game,"PlaceId")
	local inRightPlace = false
	for i,v in ipairs(backpackTestPlaces) do
		if v == game.PlaceId then
			inRightPlace = true
			break
		end
	end
	
	waitForChild(game,"Players")
	waitForProperty(game.Players,"LocalPlayer")
	if game.Players.LocalPlayer.userId == 7210880 or game.Players.LocalPlayer.userId == 0 then inRightPlace = true end
	
	--if not inRightPlace then return end -- restricting availability of backpack

	-- Backpack Builder
	dofile("rbxasset://scripts\\cores\\BackpackBuilder.lua")
	waitForChild(screenGui,"CurrentLoadout")
	waitForChild(screenGui.CurrentLoadout,"TempSlot")
	waitForChild(screenGui.CurrentLoadout.TempSlot,"SlotNumber")
	-- Backpack Script
	dofile("rbxasset://scripts\\cores\\BackpackScript.lua")
end

end)