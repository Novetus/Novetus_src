-- executes before the game starts (server, solo, studio)
-- arguments: Script Type - returns the script type name (Server, Solo, Studio)
function PreInit(Script)
end

-- executes after the game starts (server, solo, studio)
-- arguments: none
function PostInit()
end

-- executes after a character loads (server, solo, studio)
-- arguments: Player - Player getting a character loaded, Appearance - The object containing the appearance values 
-- notes: in play solo, you may have to respawn once to see any print outputs.
function OnLoadCharacter(Player, Appearance)
end

-- executes after a player joins (server)
-- arguments: Player - the Player joining
function OnPlayerAdded(Player)
end

-- executes after a player leaves (server)
-- arguments: Player - the Player leaving
function OnPlayerRemoved(Player)
end

-- executes before a player gets kicked (server)
-- arguments: Player - the Player getting kicked, Reason - the reason the player got kicked
function OnPlayerKicked(Player, Reason)
end

_G.CSScript_PreInit=PreInit
_G.CSScript_PostInit=PostInit
_G.CSScript_OnLoadCharacter=OnLoadCharacter
_G.CSScript_OnPlayerAdded=OnPlayerAdded
_G.CSScript_OnPlayerRemoved=OnPlayerRemoved
_G.CSScript_OnPlayerKicked=OnPlayerKicked