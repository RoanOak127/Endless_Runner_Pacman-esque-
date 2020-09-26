# Endless_Runner_Pacman-esque-
-----------------------------------------------------------------------------------
Assignment 1: Endless Runner - Temple Run-Like Game README
-----------------------------------------------------------------------------------
Student Name: Roan Dominguez
Student abc123: ppb219


-----------------------------------------------------------------------------------
Links to Project (if you used Google Drive, OneDrive, or Dropbox for submission)
-----------------------------------------------------------------------------------
Please provide your Google Drive, OneDrive, or Dropbox link here if you used it for 
your submission, otherwise you may skip this section if you just submitted your
assignment on Blackboard (zipped up). (Make sure the link does not expire and the 
link permissions allow us to download, otherwise you may receive a grade of 0 on 
your assignment submission if we cannot download it.)

Place this Link to Your Unity Project Here:



-----------------------------------------------------------------------------------
Game Controls 
-----------------------------------------------------------------------------------
Please explain what your buttons do. (You do not have to use all of these buttons.
If you use buttons other than these, please mention them to let us know.)

Mouse Movement:
Mouse Click: When in the pause menu, the resume, restart, and quit buttons are clickable
↑: Player Jumps
A or ←: Player moves left
D or →: Player moves right
S or ↓: Player falls down faster
Spacebar: Player Jumps
Esc: 


-----------------------------------------------------------------------------------
Assets Downloaded for Game
-----------------------------------------------------------------------------------
Any assets that you downloaded and used from the Asset Store needs to be documented.
Please provide any links to these assets from the Asset Store you downloaded here:

(ProBuilder is built in now)

Skybox Series Free: https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633

Standard Assets: https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2018-4-32351

-----------------------------------------------------------------------------------
Instructions for Students
-----------------------------------------------------------------------------------
Please make sure you place all of your written scripts within a folder called 
"Scripts" to make it easier for the professor and/or TA/grader to find your 
written code for grading.

The following sections are divided into each grading criteria for your assignment
submission. For each section where you must briefly describe something, a sentence
or two is all that is needed just to get your point across. If you did not
implement a particular feature (grading criteria), such as the extra credits, then 
simply mention "Did not do".


-----------------------------------------------------------------------------------
1. Character Automatically Moves Forward (10 pts)
-----------------------------------------------------------------------------------
Briefly describe how you implemented getting your character to automatically move
forward. e.g. Which script did you write that implements this? (Mention whether 
this script was attached to any game object and which ones.)

***This is in the Character Controller script attached to the player object, on each update 
the player is automatically moved forward by adding 1 to the z axis of its position, and 
applying time.deltatime and a speed to that so that the movement is smooth and swift.

-----------------------------------------------------------------------------------
2. Character Can Move Side-to-Side with A/D or Left/Right Arrow Keys (10 pts)
-----------------------------------------------------------------------------------
Briefly describe how you implemented giving your character the ability to move
side-to-side. e.g. Which script did you write that implements this? (Mention 
whether this script was attached to any game object and which ones.)

***This is in the Character Controller script attached to the player object and on 
the same line as the one for moving forward. On the x axis of the added vector 
position, it has Input.getAxis("Horizontal") which is already key bound to the 
arrow and A/D keys.

-----------------------------------------------------------------------------------
3. Character Can Jump with Space (10 pts)
-----------------------------------------------------------------------------------
Briefly describe how you implemented giving your character the ability to jump.
e.g. Which script did you write that implements this? (Mention whether this script
was attached to any game object and which ones.)

***This is in the Character Controller script attached to the player object. In the 
update function it check is player is pushing the Space key or the up arrow. If the 
character is not already in the air (ie isGrounded) then an impulse force is applied 
in an upwards direction.

***If player is already in the air, jump will not work

-----------------------------------------------------------------------------------
4. Character Dies When Falling in a Gap (20 pts)
-----------------------------------------------------------------------------------
Briefly describe how you implemented giving your character the ability to die when
falling in a gap. e.g. Which script did you write that implements this? (Mention 
whether this script was attached to any game object and which ones.)

***I made what I lovingly call a death platform. It's a very large platform that 
is at the same x,y location as a newly instantiated platform, but is 5 units below 
it. It does not have a mesh, just a trigger collider to indicate when the player or 
other objects with gravity fall on it. I tested it by running and jumping off the 
edges of the regular platform to make sure it was large enough. 5 units is more than 
double the random 2 units a new platform can generate up or down from the one before 
it this way it doesn't collide with anything it's not supposed to. This is made in 
the CreatePlatforms scrip attached to the GameManager.

What happens when your character "dies"? e.g. Do you get Game Over, restart the
level, or lose some life?

***It invokes the game over function. Right now that means it automatically stops 
the application and exits the window. I will make it better in the future. This is 
in the GameControllerScript attached to the GameManager.


-----------------------------------------------------------------------------------
5. Procedurally Generated Levels (20 pts)
-----------------------------------------------------------------------------------
Briefly describe how you implemented procedurally generated levels, especially 
how you randomized the objects and placement of these objects or gaps. e.g. Which 
script did you write that implements this? (Mention whether this script was 
attached to any game object and which ones.)

***There is only one level that gets reloaded each time the timer goes down to 0. 
I make it different by making a switch statement where the level is % by 5. There 
are 1-5 cases where each level gets a different amount of enemies and walls added 
in, and then everytime it reaches the 1 it increases the speed and reduces the enemies 
back to something managable again so that things don't get clustered. This is all done 
in the GameControlScript attached to the GameManager.

***Platforms and the other objects are randomized in the Platform Generator Script 
attached to the GameManager. The first platform is always at 0,0,0 but the next has 
the change or going +/- 7, +/-2, and will always have a gap of 5 units between platforms.

***As a platform is generated, it creates the death platform as described, and then it 
calls a function to fill the new platform with balls, walls, enemies, and powerups. The 
position of each object had to be tested to see where it's limits were since they were 
all of different sizes. They are randomly generated within their x and z limits. Their 
y's are all hard coded. As described the amounts of walls and enemies differs with each 
level. Balls have a variable in a private global but won't be changed unless manually.
Powerups/Downs have a 25% chance of showing up on a platform. 


-----------------------------------------------------------------------------------
6. Advancing Levels After Not Dying for 30 Seconds (20 pts)
-----------------------------------------------------------------------------------
Briefly describe how you implemented your character advancing to the next level
after 30 seconds of not dying. e.g. Which script did you write that implements 
this? (Mention whether this script was attached to any game object and which ones.)

***In the GameControllerScript attached to the GameManager when the timer runs out the 
game looks for every enemy that's still alive, disables its enemy control script, 
disables the players gravity and control script, waits two seconds, and then loads 
the next level

Briefly describe what pops up when you advance to the next level. If there is a 
script associated with this, what is it and what game object is it attached to?

***I did not! I meant to for prettyness's sake but didn't have time and I didn't see
this second part to this section so my bad ^_^'

-----------------------------------------------------------------------------------
7. Current Level and Time Counter from 0 to 30 Seconds (20 pts)
-----------------------------------------------------------------------------------
Briefly describe how you implemented this being displayed somewhere on the screen. 
e.g. Which script did you write that implements this and how is the current level
updated? (Mention whether this script was attached to any game object and which 
ones.)

***In the GameControlScript attached to GameManager, I implemented Text objects from 
the UI that I placed in game via the editor. I liked the look of keeping these on 
screen even when paused so that the user can see their level, time left, points, 
and health. The level text gets updated exactly once per level, and that's at the 
start of a scene load. The level only gets updated and has it's player pref set once 
the timer reaches zero and it goes into the change level function. This is so if the
player hits the restart in the pause menu it's a simple reloading of the same scene 
since nothing gets saved until the official change level function.

***Timer gets updated by the second and so does the timer text associated with it. 
It updates the next line after it counts down by 1.


-----------------------------------------------------------------------------------
8. Randomly Spawning Power-Ups (20 pts)
-----------------------------------------------------------------------------------
List the power-ups/downs that you implemented in your game (minimum of 3) and the
object associated with that power-up (such as a yellow coin, blue star, etc.):

     Power-Up/Down Object       |       Power-Up/Down Effect
1)   Balls (power up)           |  These give you points, ever 5 gives you +10 health
2)   Strawberry (power up)      |  Disables the enemy movement for 1 level
3)   Mushroom (power down)      |  Lowers your speed for one level

Briefly describe how you implemented this being displayed somewhere on the screen,
especially how you randomized the objects and placement of these objects. e.g. 
Which script did you write that implements this? (Mention whether this script was 
attached to any game object and which ones.)

***This was talked about in detail in 5. Procedurally Generated Levels paragraph 3.
Each object has a variable to control it's x and z and a random.range function that
chooses it's position from that range. The y stays constant. Balls are set to 5, the
other two have a 25% chance of being generated on any given platform. You can't lower
your speed more than once per level because then you might not be able to jump high
enough to reach the next platform. All of this is in the CreatePlatforms script
attached to the GameManager.

-----------------------------------------------------------------------------------
9. Two Other Things Besides Falls That Can Kill You When You Hit Them (20 pts)
-----------------------------------------------------------------------------------
List the 2 other things besides falls that can kill you when you hit them that you 
implemented in your game (minimum of 2). Please mention what these things are:

     Thing that Can Kill You When You Hit It
1)    Enemies (Ghosts) will instant end game                          
2)    Walls will lower health and if health is <= 0 game over                          

Briefly describe how you implemented this being displayed somewhere on the screen,
especially how you randomized these things and the random placement of these
things. e.g. Which script did you write that implements this? (Mention whether 
this script was attached to any game object and which ones.)

***This was talked about in detail in 5. Procedurally Generated Levels paragraph 3.
These have x and z variables that I found their upper and lower bounds to so that
when generated they stay on the platform but are in a random location. Y is constant.
The number will depend on what level you're on. These are created by the CreatePlatform
Script in the GameManager. The amounts of enemies and walls is controlled by the 
GameControllerScript in the GameManager.


-----------------------------------------------------------------------------------
Any Notes or Things the Professor or TA/Grader Should Be Aware Of
-----------------------------------------------------------------------------------
If there are any other concerns that you have about your submission or any known
bugs/glitches in your game that could potentially come up, please explain them here:

***If you change levels while in the middle of a jump, the character will float into 
space. It doesn't affect anything because when the next level loads it's set back to 
the starter position, but it's kinda weird to watch.

***Sometimes things generate ontop of each other. I haven't had a problem where that 
spot becomes unplayable, but sometimes it's like watching a car wreck and then a ghost 
snipes you.

***Sometimes an enemy appears too close to the starting player position when you load 
a new scene and you can't avoid it and you'll die. I'm sorry that I didn't make a replay 
and you have to relaunch the game. 

