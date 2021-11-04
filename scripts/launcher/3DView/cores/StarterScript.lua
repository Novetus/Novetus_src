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

waitForChild(game:GetService("CoreGui"),"RobloxGui")

dofile("rbxasset://scripts\\cores\\PlayerlistScript.lua")
end)