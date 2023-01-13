local this = {}

function this:Name()
	return "URL Setup Addon"
end

function this:IsEnabled(Script, Client)
	if (Script ~= "Studio") then
		return true
	else
		return false
	end
end

function this:PreInit(Script, Client)
	pcall(function() game:GetService("BadgeService"):SetPlaceId(game.PlaceId) end)
	pcall(function() game:GetService("BadgeService"):SetAwardBadgeUrl("http://www.roblox.com/Game/Badge/AwardBadge.ashx?UserID=%d&BadgeID=%d&PlaceID=%d") end)
	pcall(function() game:GetService("BadgeService"):SetHasBadgeUrl("http://www.roblox.com/Game/Badge/HasBadge.ashx?UserID=%d&BadgeID=%d") end)
	pcall(function() game:GetService("BadgeService"):SetIsBadgeDisabledUrl("http://www.roblox.com/Game/Badge/IsBadgeDisabled.ashx?BadgeID=%d&PlaceID=%d") end)
	pcall(function() game:GetService("BadgeService"):SetIsBadgeLegalUrl("") end)
end

-- DO NOT REMOVE THIS. this is required to load this addon into the game.

function AddModule(t)
	print("AddonLoader: Adding " .. this:Name())
	table.insert(t, this)
end

_G.CSScript_AddModule=AddModule