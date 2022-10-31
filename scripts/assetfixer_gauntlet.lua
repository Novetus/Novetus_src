-- link - single quote
'https://assetdelivery.roblox.com/v1/asset?id=1818'
-- link - double quote
"https://assetdelivery.roblox.com/v1/asset?id=1818"
-- xml
<lol=lol><blah>https://assetdelivery.roblox.com/v1/asset?id=1818</blah></lol>

-- link - single quote, w/ space
'https://assetdelivery.roblox.com/v1/asset?id=1818 '
-- link - double quote, w/ space
"https://assetdelivery.roblox.com/v1/asset?id=1818 "
-- xml, w/ space
<lol=lol><blah>https://assetdelivery.roblox.com/v1/asset?id=1818 </blah></lol>

--just the link. no spaces.
https://assetdelivery.roblox.com/v1/asset?id=1818
--a variation of a roblox link
http://www.roblox.com/asset/?version=1&amp;id=1818
--another variation of a roblox link
http://www.roblox.com/asset/?version=1&id=1818
--another possible variation
http://www.roblox.com/asset/?id=1818&amp;version=1
--another one
http://www.roblox.com/asset/?id=1818&version=1
--quotation link
"http://www.roblox.com/asset/?id=[ID]" 

-- script snippet
game:GetService("ContentProvider"):Preload("http://www.roblox.com/asset/?id="..images[m])
l.Image = "http://www.roblox.com/asset/?id="..images[m]

--spooking out snippet
{&quot;Boing&quot;, &quot;http://novetus.me/asset?id=12222124&quot;, 2.509},--2

--menderman snippet
&#9;&#9;   backq2.Name = &quot;Backq2&quot;
&#9;&#9;   backq2.formFactor = &quot;Plate&quot;
&#9;&#9;   backq2.Size = Vector3.new(1,0.4,1)
&#9;&#9;   local mesh7 = Instance.new(&quot;SpecialMesh&quot;)
         mesh7.MeshId = &quot;http://novetus.me/asset?id=17659272&quot;
         mesh7.TextureId = &quot;http://novetus.me/asset?id=17659268&quot;
&#9;&#9;   mesh7.Scale = Vector3.new(1,1.5,2.5) 
&#9;&#9;   mesh7.Parent = char.Backq2
&#9;&#9;   local w9 = Instance.new(&quot;Weld&quot;)
&#9;&#9;   w9.Parent = torso
&#9;&#9;   w9.Part0 = w9.Parent
&#9;&#9;   w9.Part1 = backq2
         w9.C1 = CFrame.fromEulerAnglesXYZ(0,0,-1.6) +Vector3.new(1.2,-1.2,-1.2)

--snippet from survive the spheres
Badges = {[0] = 30058718, [100] = 29890585, [200] = 29918752, [400] = 30057939}
Playing = {}
Tutorial = {&quot;http://www.roblox.com/asset/?id=30093873&quot;, &quot;http://www.roblox.com/asset/?id=30093900&quot;, &quot;http://www.roblox.com/asset/?id=30093954&quot;, &quot;http://www.roblox.com/asset/?id=30094010&quot;, &quot;http://www.roblox.com/asset/?id=30094028&quot;, &quot;http://www.roblox.com/asset/?id=30094049&quot;}
BallName = &quot;bawlz&quot;
Base = workspace.Base
CountDownTime = 15
Siz = 8
Size = Vector3.new(Siz, Siz, Siz)
Multiplier = 1
SuckSpeed = 1.5
SuckSoundId = &quot;http://www.roblox.com/asset/?id=10722059&quot;
TickSoundId = &quot;rbxasset://sounds\\clickfast.wav&quot;
PingSoundId = &quot;http://www.roblox.com/asset/?id=13114759&quot;
HorrorSoundId = &quot;http://www.roblox.com/asset/?id=2767085&quot;
LostSoundId = &quot;http://www.roblox.com/asset/?id=13378571&quot;
WonSoundId = &quot;http://www.roblox.com/asset/?id=15632562&quot;
EatTime = 2
BallMass = 15000
copyrate = 15
Powerups = {}
Lobby = script.Parent.Lobby
VipId = 28197859