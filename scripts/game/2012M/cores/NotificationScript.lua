
delay(0, function()

function waitForProperty(instance, property)
	while not instance[property] do
		instance.Changed:wait()
	end
end
function waitForChild(instance, name)
	while not instance:FindFirstChild(name) do
		instance.ChildAdded:wait()
	end
end

waitForProperty(game.Players,"LocalPlayer")
waitForChild(game:GetService("CoreGui").RobloxGui,"Popup")
waitForChild(game:GetService("CoreGui").RobloxGui.Popup,"AcceptButton")
game:GetService("CoreGui").RobloxGui.Popup.AcceptButton.Modal = true

local localPlayer = game.Players.LocalPlayer
local acceptedTeleport = Instance.new("IntValue")

local friendRequestBlacklist = {}

local teleportEnabled = true

local makePopupInvisible = function()
	if game:GetService("CoreGui").RobloxGui.Popup then game:GetService("CoreGui").RobloxGui.Popup.Visible = false end
end

function makeFriend(fromPlayer,toPlayer)
	
	local popup = game:GetService("CoreGui").RobloxGui:FindFirstChild("Popup")
	if popup == nil then return end -- there is no popup!
	if popup.Visible then return end -- currently popping something, abort!
	if friendRequestBlacklist[fromPlayer] then return end -- previously cancelled friend request, we don't want it!

	popup.PopupText.Text = "Accept Friend Request from " .. tostring(fromPlayer.Name) .. "?"
	popup.PopupImage.Image = "http://www.roblox.com/thumbs/avatar.ashx?userId="..tostring(fromPlayer.userId).."&x=352&y=352"
	
	showTwoButtons()
	popup.Visible = true
	popup.AcceptButton.Text = "Accept"
	popup.DeclineButton.Text = "Decline"
	popup:TweenSize(UDim2.new(0,330,0,350),Enum.EasingDirection.Out,Enum.EasingStyle.Quart,1,true)
	
	local yesCon, noCon

	yesCon = popup.AcceptButton.MouseButton1Click:connect(function()
		popup.Visible = false
		toPlayer:RequestFriendship(fromPlayer)
		if yesCon then yesCon:disconnect() end
		if noCon then noCon:disconnect() end
		popup:TweenSize(UDim2.new(0,0,0,0),Enum.EasingDirection.Out,Enum.EasingStyle.Quart,1,true,makePopupInvisible())
	end)

	noCon = popup.DeclineButton.MouseButton1Click:connect(function()
		popup.Visible = false
		toPlayer:RevokeFriendship(fromPlayer)
		friendRequestBlacklist[fromPlayer] = true 
		print("pop up blacklist")
		if yesCon then yesCon:disconnect() end
		if noCon then noCon:disconnect() end
		popup:TweenSize(UDim2.new(0,0,0,0),Enum.EasingDirection.Out,Enum.EasingStyle.Quart,1,true,makePopupInvisible())
	end)
end


game.Players.FriendRequestEvent:connect(function(fromPlayer,toPlayer,event)

	-- if this doesn't involve me, then do nothing
	if fromPlayer ~= localPlayer and toPlayer ~= localPlayer then return end

	if fromPlayer == localPlayer then
		if event == Enum.FriendRequestEvent.Accept then
			game:GetService("GuiService"):SendNotification("You are Friends",
			"With " .. toPlayer.Name .. "!",
			"http://www.roblox.com/thumbs/avatar.ashx?userId="..tostring(toPlayer.userId).."&x=48&y=48",
			5,
			function()
			
			end)
		end
	elseif toPlayer == localPlayer then
		if event == Enum.FriendRequestEvent.Issue then
			if friendRequestBlacklist[fromPlayer] then return end -- previously cancelled friend request, we don't want it!
			game:GetService("GuiService"):SendNotification("Friend Request",
				"From " .. fromPlayer.Name,
				"http://www.roblox.com/thumbs/avatar.ashx?userId="..tostring(fromPlayer.userId).."&x=48&y=48",
				8,
				function()
					makeFriend(fromPlayer,toPlayer)
				end)
		elseif event == Enum.FriendRequestEvent.Accept then
			game:GetService("GuiService"):SendNotification("You are Friends",
			"With " .. fromPlayer.Name .. "!",
			"http://www.roblox.com/thumbs/avatar.ashx?userId="..tostring(fromPlayer.userId).."&x=48&y=48",
			5,
			function()
			
			end)
		end
	end
end)

function showOneButton()
	local popup = script.Parent:FindFirstChild("Popup")
	if popup then
		popup.OKButton.Visible = true
		popup.DeclineButton.Visible = false
		popup.AcceptButton.Visible = false
	end
end

function showTwoButtons()
	local popup = script.Parent:FindFirstChild("Popup")
	if popup then
		popup.OKButton.Visible = false
		popup.DeclineButton.Visible = true
		popup.AcceptButton.Visible = true
	end	
end

if teleportEnabled then
	game:GetService("TeleportService").ErrorCallback = function(message)
		local popup = game:GetService("CoreGui").RobloxGui:FindFirstChild("Popup")
		showOneButton()
		popup.PopupText.Text = message
		local clickCon
		clickCon = popup.OKButton.MouseButton1Click:connect(function()
			if clickCon then clickCon:disconnect() end
			game.GuiService:RemoveCenterDialog(game:GetService("CoreGui").RobloxGui:FindFirstChild("Popup"))
			popup:TweenSize(UDim2.new(0,0,0,0),Enum.EasingDirection.Out,Enum.EasingStyle.Quart,1,true,makePopupInvisible())
		end)
		game.GuiService:AddCenterDialog(game:GetService("CoreGui").RobloxGui:FindFirstChild("Popup"), Enum.CenterDialogType.QuitDialog,
			--ShowFunction
			function()
				showOneButton()
				game:GetService("CoreGui").RobloxGui:FindFirstChild("Popup").Visible = true 
				popup:TweenSize(UDim2.new(0,330,0,350),Enum.EasingDirection.Out,Enum.EasingStyle.Quart,1,true)
			end,
			--HideFunction
			function()
				popup:TweenSize(UDim2.new(0,0,0,0),Enum.EasingDirection.Out,Enum.EasingStyle.Quart,1,true,makePopupInvisible())
			end)

	end
	game:GetService("TeleportService").ConfirmationCallback = function(message, placeId, spawnName)
		local popup = game:GetService("CoreGui").RobloxGui:FindFirstChild("Popup")
		popup.PopupText.Text = message
		popup.PopupImage.Image = ""
		
		local yesCon, noCon
		
		local function killCons()
			if yesCon then yesCon:disconnect() end
			if noCon then noCon:disconnect() end
			game.GuiService:RemoveCenterDialog(game:GetService("CoreGui").RobloxGui:FindFirstChild("Popup"))
			popup:TweenSize(UDim2.new(0,0,0,0),Enum.EasingDirection.Out,Enum.EasingStyle.Quart,1,true,makePopupInvisible())
		end

		yesCon = popup.AcceptButton.MouseButton1Click:connect(function()
			killCons()
			local success, err = pcall(function() game:GetService("TeleportService"):TeleportImpl(placeId,spawnName) end)
			if not success then
				showOneButton()
				popup.PopupText.Text = err
				local clickCon
				clickCon = popup.OKButton.MouseButton1Click:connect(function()
					if clickCon then clickCon:disconnect() end
					game.GuiService:RemoveCenterDialog(game:GetService("CoreGui").RobloxGui:FindFirstChild("Popup"))
					popup:TweenSize(UDim2.new(0,0,0,0),Enum.EasingDirection.Out,Enum.EasingStyle.Quart,1,true,makePopupInvisible())
				end)
				game.GuiService:AddCenterDialog(game:GetService("CoreGui").RobloxGui:FindFirstChild("Popup"), Enum.CenterDialogType.QuitDialog,
						--ShowFunction
						function()
							showOneButton()
							game:GetService("CoreGui").RobloxGui:FindFirstChild("Popup").Visible = true 
							popup:TweenSize(UDim2.new(0,330,0,350),Enum.EasingDirection.Out,Enum.EasingStyle.Quart,1,true)
						end,
						--HideFunction
						function()
							popup:TweenSize(UDim2.new(0,0,0,0),Enum.EasingDirection.Out,Enum.EasingStyle.Quart,1,true,makePopupInvisible())
						end)
			end
		end)

		noCon = popup.DeclineButton.MouseButton1Click:connect(function()
			killCons()
			local success = pcall(function() game:GetService("TeleportService"):TeleportCancel() end)
		end)

		local centerDialogSuccess = pcall(function() game.GuiService:AddCenterDialog(game:GetService("CoreGui").RobloxGui:FindFirstChild("Popup"), Enum.CenterDialogType.QuitDialog,
						--ShowFunction
						function()
							showTwoButtons()
							popup.AcceptButton.Text = "Leave"
							popup.DeclineButton.Text = "Stay"
							game:GetService("CoreGui").RobloxGui:FindFirstChild("Popup").Visible = true 
							popup:TweenSize(UDim2.new(0,330,0,350),Enum.EasingDirection.Out,Enum.EasingStyle.Quart,1,true)
						end,
						--HideFunction
						function()
							popup:TweenSize(UDim2.new(0,0,0,0),Enum.EasingDirection.Out,Enum.EasingStyle.Quart,1,true,makePopupInvisible())
						end)
					end)
					
		if centerDialogSuccess == false then
			game:GetService("CoreGui").RobloxGui:FindFirstChild("Popup").Visible = true 
			popup.AcceptButton.Text = "Leave"
			popup.DeclineButton.Text = "Stay"
			popup:TweenSize(UDim2.new(0,330,0,350),Enum.EasingDirection.Out,Enum.EasingStyle.Quart,1,true)
		end
		return true
					
	end
end

end)