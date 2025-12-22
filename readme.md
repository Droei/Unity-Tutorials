# FPS-character-controller
Files for my fps character controller


## Tutorial

### Intro
Hey everyone, welcome to my first tutorial on this channel!
For my first tutorial I want to a first person character using the new Unity Input system, when I was looking for this I genuinely could not find an up to date video so I decided to make one myself!

Just in short to Introduce myself, I'm Daan, I started game development around 2018 as a complete noob teaching myself, so if you are just starting out or struggling with code just know that I understand the feeling and I will do my best to make sure what we do is understandable yet written in a way that's actually usefull!

Anyway lets get right into it!

### Disclaimer
If you are just looking for a working basis for your project or got lost, you can find a working version of this tutorial and all other tutorials I make on my github!
The link is in the description!

### New Unity input system
Alright lets get started with setting up our inputs, unlike in previous version we don't do this in code anymore!
Instead lets create a new folder named Actions and in there create an Input Actions file on the bottom!
Lets call it Player Actions, we now double click it and the Input actions editor pops up.

Lets create our first and usually only Actio map, we'll call it Player!
Now we can add our actual actions, lets start with moving so we'll add an action named move!
You don't move using one button ofcorse so we change the action type to a value and then the control type to a vector2 so it takes up, down, left and right instead!

Now we just need to add the actual inputs, and honestly its just as simple!
We open the drop down and press <No binding> now we can select what input it needs to check for, you can manually look for inputs but I personally prefer to use Listen as it never fails me.
Now I can for example wiggle my controller joystick and the correct input appears!

Ofcorse keyboard inputs aren't joysticks so instead we select the + button and select "Up\Down\Left\Right Composite".
Now we can use the same concept of listening and pressing your buttons to assign different inputs!

Next lets repeat the same process a few more times for our other inputs.
First lets add a button for shooting, ofcorse this will stay as an action type of button.
Lets now add our shoot buttons! Listen does not work for mouse inputs so you will have to open up the mouse menu and click left button.
For controller listen just works the same!

And ofcorse the same for jump! You get the jist by now!

Now one more maybe slight curveball is looking around, lets make sure we get that right!
First we make a look Action and once again make it a value of vector2.
We then add our first binding, you find it under mouse as Delta, it basically checks how the mouse is moved.
Ehm that's all, the other input is just a jiggle with our controller again!

And that's all all of our inputs are ready to go, no messy code or anything just a few clicks and we are ready to get started on building our First person player!

### Building our Player
Now we make our player!
Lets start with creating test scene!
Now we spawn in a plane and make sure the transform is all on 0,0,0, just to be able to orient ourselves a little bit.

Alright now for our player, we create an empty game object that exists to hold and move all the parts of our player.
Inside our player we create a good ol' BEAN, a cube and also move in our camera.
Make sure to reset your camera's position!
Now we select all and move it up a bit over the plane, it doesn't have to be perfect!

Now we will use the cube to visualise the direction the player is looking in, which is ofcorse the direction the camera is pointed at!
For everything to be tidied up I will also add a few materials to give our bean and cube some more color!

Now to end it off I just want to add a shoot position, we will use this to spawn in our bullets later.
This is once again an empty and I will move it in right in front of our little cube.
Als I like to give this empty a little visual so I can make sure its in place!

### Setting up our coding environment
Before we start writing code and moving scripts etc lets be sure we are all in sync. 
For this project we will have 4 different scripts:
- PlayerMove: Which goes on the player and is used to move the whole player object around
- CameraLook: Which goes on the camera and is responsible for everything about looking around
- PlayerShooting: Which also goes on the player and is responsible for everything related to shooting
- And last Bullet: Which handles the actual bullet moving forward and colliding with stuff!

As you can see I like to split my scripts up because usually I see tutorials moving these 3 scripts in one and it really gets me heated up because its so damn unclear for beginners and good programmers would never do it that way, how are people supposed to learn programming when the people explaining it is just mashing everything together, it's really just so unclear its frustrating! 

But before I get to heated up lets add one more thing!
There's 2 ways of using the Unity input system, through unity events or through raw code, I will show you both methods but in order to make the code way available you need to go back to your "Player actions" file and enable "Generate C# Class" and click apply!

### Player movement
Alright lets implement the first part of our player, lets start with movement!
First off we select our player object and move in our Player move.

I first show you the UnityEvent way of using the input system so I will add a PlayerInput component.
In order to move our player around in a clean way I will use the Unity Character controller component, there's many ways of doing this but I find this method the least difficult!

Now to set up our inputs, we will go to "Player input" under Behavior and set it to Invoke Unity Events. Now you can open the events tab and you should be able to see another Player tab.
For me this never appears on first try so I always click away and then go back and then it appears for me.

Now that we open it we can see all the different inputs we created earlier! 
But ofcorse we have nothing to actually interpret them so lets make a function that interprets the move input!

Alright so we are in our PlayerMove script, we don't need all that so lets remove it for now!
First of all we need a variable that will hold the input like left right up down, which is ofcorse a Vector2 as we defined earlier, we'll call it moveInput!
Now I create a function named OnMove(), in here I add a CallbackContext parameter that basically gives me the input of the user.
Then I basically just set the moveInput variable equal to the player input so now moveInput contains the direction the player is trying to move in!

We can now go back to our PlayerInput component and add our PlayerMoveScript to the Move event!
Now we can select PlayerMove OnMove in the dropdown menu and now every time the player triest to move our function named OnMove will be triggered and will collect the direction the player is trying to move in!

Now lets actually use this info and make the player move!
Lets first add requirecomponent above our class, this ensures that the object that has this script has the following components on them, because ofcorse we rely on them to make our move work!

Now that that's done we define our CharacterController when the game is started, we need this controller to later tell it to move our player around. 
Also a point of personal preference, when a function only has one line you can like collapse it in one long line, I think its more clean this way but its completely up to you if you want to follow it!

To move our character around we don't really need much, we first pull back our Update() function. 
Then we define our move direction, in a vector3. We multiply the x axis by right, meaning if x is negative (aka player inputs left) it will move the opposite of right wich is left, and we do the same for forward.

Now we just use the controller we defined earlier to move our player using controller.Move(move) and our player can move!
Before we test lets add one more variable named speed, we can use this to multiply our move in order to change the move speed of our character.
We then also multiply the result of move times speed by Time.Deltatime to keep your move speed independent from your computers framerate!

Alright there we go! lets try it out!
Ooh first looks like my character controller isn't centered so let's quickly move the center up.
And there we go it works as intended!

### Player jump
Adding jump isn't much harder really, lets first add a booleon that checks if the user pressed jump.
Now we do the same with OnMove but this time with OnJump, only difference is that this time we don't take the value from context but instead check if its performed which will only trigger once per click.
We then set pressedjump to true because the player just pressed jump.

Now we will basically add the jump movement to the movement controller. 
Honestly I don't know how complex I have to go in explaining this considering I just want to get you the understand the basics so I won't move in too much detail here.
First we declare 3 parameters:
- Gravity: how hard you get pulled back down
- jumpHeight: how high you want the player to jump
- and velocity: to track the user vertically

First we check if the player controller is currently standing on the ground.
Then we add a small if to make sure the player stays down so the player isn't accidentally is airborne.
Now if jump is pressed we will apply a force just strong enough to reach the jump height when considering the gravity and we say that pressed jump is false again, so once we are grounded we are grounded and press jump we can jump again.

We now add gravity to our velocity to make sure the players upward trajectory is updated and add in our move function.

Now lets test our jump!
Aaand it doesn't work, why? Well because I didn't set the OnJump input yet in the inspector.
And there we go it works just as expected now!

### Camera look
Alright now we'll make it possible for the player to look around.
lets use the code method of the new input system for this one!

We first create 2 variables: 
- inputActions: which holds the code version of the input system
- lookAction: which is the actual action we are following up on!

First we setup our look action, we create an Awake function that basically runs before anything else.
We now declare our inputActions variable, and then we declare our lookaction variable by taking the Look value from inputActions and putting it in lookAction.

Now we just need to enable lookAction because otherwise it won't do anything, and also disable for good practice.
And ofcorse we don't want our mouse to move all over our screen so when the object starts we set the cursor locked to the center of the screen and make it invisible.
Btw in play mode you can press ESCAPE to get out of the lockstate

With that done lets get to moving our camera first we set those 2 variables up
- sensitivity: to set your mouse sensitivity
- xRotation: tracks how far up or down the camera looks

In our Update function we take our look input and put it into a vector2 as we did in our movement class earlier.
Then we split its values up in mouseX and mouseY as both directions are handled differently.

We subtract mouseY from xRotation to sets it up & down rotation.
Then we use MathF.Clamp to set a minimum and maximum up and down rotation to make sure the camera doesn't start spinning.

Now we apply our values:
We set the local rotation of our camera, for this we use Quaternion.Euler, also make sure you use a big Q for Quaternion!
After that we rotate our parent player object left and right using vector3.up times mouse X to make sure everything rotates with it!

Now lets test! And it looks like the camera now works as intended!

### Being cute

We are getting so close to the end so lets get this done, congratz for making it this far tho!
Like you know this tutorial stuff is something I actually wanted to do for a while but I really didn't expect feeling so excited to teach people something I'm passionate about.
Anyway, I just want to say I'm proud of you keep up the grind and you got this!

### Shooting

My priority was mostly with moving so setting up shooting is more of a nice extra so I might go over it a bit quicker!
Lets first write our bullet!

We first set our speed variable.
Now in Update we get the current position of our bullet and the direction in front of it.
We then calculate how far we move our bullet forward using speed times time.deltatime to once again keep it framerate independent!

Now we need to check if the bullet is hitting something in front of it, for that I will use a raycast because its more accurate.
So we generate a raycast from our bullet using Physics.raycast, we generate in from our current position in the direction we are moving in, we can also check what we hit using RaycastHit but that's not for this tutorial and the length of the raycast is the distance we'll move this frame.
If the raycast touches something it'll be destroyed!

If it doesn't hit anything we move our bullet forward using transform.translate

Alright our bullet is set up so now lets get to shooting!
First we create 4 serializable variables:
- bullet: our bullet object we'll spawn
- shootPos: the position we'll spawn our bullet from
- camera: we'll take the look direction of the camera to determine the look direction of the bullet so our forward is always the bullet's forward
- firerate: and how fast the fire rate is when we just hold down our button

Then I will also create a coroutine variable which is a variable used for different type of timing stuff, well I'll show you how it works!

So a coroutine basically works with a function names IEnumerator, in this case I'll call it FireLoop(), then we add a While(true) loop so the loop will keep going.
Then we will instantiate our bullet at the position of our shootpos and we set the looking rotation of our bullet to the lookrotation of our camera.
Then here's where the magic happens, we do yield return new waitForSeconds(fireRate) which says that before we can move forward we first wait for .5 seconds and then continue.

So now we just have to link this Fireloop to our fire input.
Maybe you've guessed it by now but we can just do OnShoot again with our parameters!
Then we check if our button click has started so we start iterating our FireLoop using StartCoroutine and we will place that Coroutine in fireRoutine.
And we do this because once the player releases their click and the fireroutine is active we can call StopCorouting to stop iterating FireRoutine.

Alright lets have one more test.
First we create our little Bullet, just a little ball and we drag & drop our bullet script on there and our bullet is done.
Now for our player we drag & drop our shooting script on there. 
We then once again link our OnShootFunction to Shoot.
Then we drag & drop our objects in the right place and we can give it a little test!

And ooh boy it works! Ofcorse you can still spamclick but I felt like handling a good shooting implementation is for another time!


### Outro
Anyway this tutororial is done, I hope you enjoyed it and I'm looking forward to future tutorials.
Let me know in the comments if there's anything you'd like to see in future videos and with that I hope you have an amazing day, bye bye!
Keep up the grind!