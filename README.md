# Consyl-Engine

This is an ASCII Console Game Engine! Go to [Releases](https://github.com/AAli107/Consyl-Engine/releases) and select the latest release to download. Once the download finishes, extract the zip file then read the documentation below if you're new to the game engine.





# Documentation



## Requirements

### Recommended Hardware Requirements

1. CPU: Processor with 2 GHz dual-core

2. Memory: 2 GB of DDR3 RAM

3. GPU: Not needed (PC at least needs graphics display on screen)

4. Storage: Consyl takes up around 5 MB of storage, so you don’t need to worry about it

   - As long as your computer can run command prompt, you should be fine.

   

### Recommended Software Requirements

1. Windows 10 Operating System or above.

2. Microsoft Visual Studio that can run C# .NET Core 3.1 [(You can download it here)](https://visualstudio.microsoft.com/vs/community/)

3. Microsoft's C# .NET Core 3.1 [(Download it here)](https://dotnet.microsoft.com/download)

   

### Recommended User Knowledge

1. Basic C# code knowledge [(You can watch Brackeys' C# Basics Tutorial Series to get you caught up)](https://www.youtube.com/playlist?list=PLPV2KyIb3jR4CtEelGPsmPzlvP7ISPYzR)
2. Basic Math Knowledge
3. Experience with playing Games (not necessary but recommended)



**Note:** You shouldn’t worry too much about the requirements since most of the requirements like hardware and software you probably have already met.






## Game Code Basics

### Navigating to the Main Scripting File

To code your first game, you need to open “Consyl Engine.sln” and from the Solution Explorer, choose “GameCode.cs” which is the part where you put all of your code in for your game. (“Image A” shows how the code is supposed to look)

<img src="images/picture_001.png">

**Image A:** This image shows the place where you code your game.



### A Look Around Inside the File

“GameCode.cs” contains the basics of what you need in making your games. It has two important sections, one is where you put your variables and data, and the other is where your code gets executed.

The Section where your code is executed has three main parts, “OnGameStart”, “OnGameUpdate”, and “OnGameEnd”.

#### OnGameStart

Is a place where the code you inserted gets executed when the game starts.

#### OnGameUpdate

Is a place where the code you inserted gets executed repeatedly only when the game is running.

#### OnGraphicsUpdate

It’s the same as “OnGameUpdate” but it’s for Graphics.

#### OnGameEnd

Is a place where the code you inserted there gets executed when the game closes through code.





### **What are Variables?**

Variables are pieces of information that get created when the program runs, they are stored inside your computer until you decide to close your program/game. Variables can be manipulated and read by the code to do many fancy instructions.

#### The Main Relevant Types of Variables

**Boolean:** A variable coded in as `bool` that can store either a `true` or `false` value.

**Integer:** A variable coded in as `int` that can store non-decimal numbers.

**Floating-point number:** A variable coded in as `float` that stores numbers with decimals.

**String:** A variable coded as a `string` that can store text.

**Character:** A variable coded as `char` which stores a single letter/symbol

#### How to Code in Variables?

To add in your variable, you need to type in “static” then you type in the variable type, then the variable name and equal sign then the value/data. Here are some examples of variables in the image shown below:

<img align="left" src="images/picture_002.png">

**Image B:** Shows examples of different types of variables.



#### Important notes about variables

It is important to make your variable names easy to read and understand, so you format them like by making the first letter lowercase and if the name has multiple words, the beginning of every word other than the first one will be uppercase. Do not add space between the variable name as it would cause errors. Also, you don’t need to add a value/data inside the variable when you make it, you can just make it without values.





### **An ASCII Pixel**

When you press the “F5” key you’ll start to load the game, and it’ll open a window on the screen with nothing but a black window. We’ll start from the very basic level of drawing an image on the screen by drawing a single “ASCII” pixel. “ASCII” is a format of displaying letters/symbols on a computer, but in Consyl Engine we don’t have true pixels, we do have something named an “ASCII Pixel” which is a pixel made of an ASCII Symbol, which is not a real pixel but will resemble one. 

#### **Drawing an ASCII Pixel on screen**

To Draw an ASCII Pixel, you’ll need to write down in between the curly brackets below “OnGraphicsUpdate” function “gfx.DrawPixel();”. Right now, it will not draw anything, and it will display an error because you need to tell it where on the screen you want the pixel to appear and what it looks like. For example, you want your pixel to be placed at a coordinate of x = 1 and y = 5, and the ASCII pixel you want to draw looks like this “O”. So, you want instead to type `gfx.DrawPixel(1, 5, ‘O’);`, and when you run your code, you’ll see the output as shown in Image C. You can add more pixels by creating a new line with the same code with different coordinates and ASCII symbols. (Make sure when running your game, you maximize your window to show your result properly)

<img src="images/picture_003.png" style="zoom:300%;" >

**Image C:** This Image shows what happens if you code in `gfx.DrawPixel(1, 5, ‘O’);`

<br>

<br>





## Player input

**Before adding player Input**

All the way at the top of the “GameCode.cs” script, you’ll see `using` codes which gives access to more code for the developer to use. “Image D” shows what the section displaying `using EZInput;` looks like. If you don’t have that for whatever reason, type it down since you will need this to allow for user input.

<img align="left" src="images/picture_004.png">

**Image D:** Shows EZInput

<br>



#### How to Add Player Input?

You need to type in, inside the OnGameUpdate function an if statement, which is used to check for bools and functions. To make an if statement, you need to type `if()` and open and close curly brackets like this:

```c#
If ()
{
    // Code here
}
```

Right now, there is no logic. So, you need to add between the parenthesis next to `if` your logic, if the logic is true, then execute the code between the curly brackets. So, we do this:

```c#
if (Keyboard.IsKeyPressed(Key.A))
{
    // Code here
}
```

This will check if you have pressed the A key. if you did the code will execute. If you add the `DrawPixel()` code from before inside the curly brackets, this will result when you run it to make the pixel only appear when you press A. Note that you can change `Key.A` to any key on the keyboard, like `Key.B`, `Key.Shift`, etc.





#### **Creating a Controllable Player**

Before, we drew a pixel on the screen and made it only show when you press the “A” key. Now remove all of that and let’s make a way to make a controllable player as a pixel.

First, you need to create 3 variables, one that stores the x coordinate, one that stores the y coordinate, and one that stores the speed of the player. Image E will show you how this is done.

<img align="left" src="images\picture_005.png" alt="picture_005" style="zoom: 150%;" />

**Image E:** Shows the required variables for controlling the player.

Then in OnGraphicsUpdate() Function you type this between the curly brackets:

`gfx.DrawPixel((int)playerX, (int)playerY, ‘O’);`

Note that `(int)` is a way to convert from float to integer. This piece of code will draw the image at whatever the playerX and playerY was at. You can set the values to whatever you like, which would change where the player is on the screen.

<img src="images\picture_006.png" alt="picture_006" style="zoom:150%;" /> <br>

**Image F:** Shows what the code looks like. (`gfx.DrawPixel()` function is in OnGameUpdate() so that the image would fit)







## Graphics.cs

As you saw previously, you can draw images using the DrawPixel() code. But there are shortcuts to draw many shapes like circles, rectangles, lines, and more.

Drawing Lines can be done by using gfx.DrawLine() function. You add the first point’s x and y coordinates and the second point’s x and y coordinates, then choose the character you want the line’s pixels to look like. For example, we type in this code inside the OnGraphicsUpdate() Function:            `gfx.DrawLine(10, 24, 80, 30, 'X');`

This will draw a line from point (10, 24) and point (80, 30) as exactly shown in Image G.

<img  src="images\picture_007.png" style="zoom: 100%;" />

**Image G:** Shows a single line of ASCII Pixels being drawn on screen.



Every other shape requires coordinates and only this one which requires more than one coordinate. So, just give the proper values that the drawing function and it will draw what it’s supposed to draw on the screen based on the values given.



#### Drawing a Circle

The function for drawing a circle requires the x and y coordinates, the radius, and how the ASCII pixels should look like. It should be easy to understand this once you messed around with the functions, also you can choose whether you want the circle to be an outline or a solid circle.







## Textures.cs

Textures are essential to make better-looking games and it saves the time of drawing every ASCII pixel one by one by placing `gfx.DrawPixel()`. But that’s not what you’re going to do. So, textures are basically the images that get loaded and placed in your game’s window.

 

#### How to draw a texture?

First, you need to create a special class variable, this variable will need the image file name with its format for example “guy.png”. To import an image, go to the folder where the exe of your game is stored and paste your image there. Image H shows what the variable looks like.

<img align="left" src="images\picture_008.png" alt="picture_008" style="zoom:150%;" />

**Image H:** Shows how the texture class variable looks like.



Since my texture is 64x64 in resolution I will place the texture at coordinates of (0, 0). To Draw the image, you need to code inside OnGraphicsUpdate() this code: `guyImage.DrawImage(0, 0);`

When you play the game, it will show up something like in Image I or whatever texture you used.

<img align="left" src="images\picture_009.png" alt="picture_009"  />

**Image I:** Shows the “guy.png” being drawn on the game’s window. (If you see colors on this image, it's just that the brackets on that image cause an artifact when looking at it through a zoomed out image)

 

Keep note that textures currently don’t fully support transparency, but there is a parameter in drawing images that controls whether you want the blank ASCII character to count as black color or transparent.

Consyl Engine currently supports the following file formats: **BMP**, **GIF**, **EXIF**, **JPG**, **PNG**, and **TIFF**.







## Audio.cs

Games would seem silent and filled with a void without Audio. So, sound is essential to give players feedback. You’re not going to use `Console.Beep()` which comes with the .NET Core 3.1. There is an object like the texture object which would load the audio file and play it whenever the play function is called.

 

#### How to play an audio file?

First, we need to create an audio class, which contains the sound effect file name/path. (Class variable shown in Image J)

<img align="left" src="images\picture_010.png" alt="picture_010" style="zoom:125%;" />

**Image J:** Shows how the Audio class variable looks like.



Then, create an if statement that contains the play where you press any letter you like, for example, “S”. The game would crash immediately since it was playing the sound many times each second. So, you need to create a bool variable that will make it play only once. Add the variable to the if statement and set it to false once the audio plays. Image K will show what the code will look like.

<img align="left" src="images\picture_011.png" alt="picture_011" style="zoom:80%;" />

**Image K:** Shows how the code was written for playing a sound file when pressing “S” once.



Note that the Audio system only supports **MP3** and **WAV** file types only. The Audio System also supports Stopping, Pausing, and unpausing of the audio. All are found in the Audio Class Variable that you created as functions.







## Engine.cs

So far, you’ve been programming all in a single file, the “GameCode.cs” file. But now, you’ll be modifying some values inside the Engine’s code, and learn more on how to make your games better.

 

#### Inside the Engine’s code

Open “Engine.cs”, then you’ll see many lines of code, most of which you would ignore and not mess with unless you know what you’re doing. There is a section inside the Engine script file that stores a lot of important variables. The variables are organized into the 3 sections as seen in Image L.

<img align="left" src="images\picture_012.png" alt="picture_012"  />

**Image L:** Shows all the user-modifiable variables inside “Engine.cs”.



Here is a list of all the variables and what they do:

1. **gameTitle:** Allows the modification of the Game’s title.
2. **gameRunning:** Can be set to false in “GameCode.cs” to close the game.
3. **gamePaused:** If true, it will pause the execution of `OnGameUpdate()`.
4. **deltaTime:** Read-only variable that stores the amount of time it took to render the last frame.
5. **currentFPS:** Read-only variable that stores the Frames per Second the game is running at.
6. **drawASCIIRender:** Enables and Disables the ASCII graphics. (If you don’t want to use it)
7. **resolution:** Drawing Resolution for the ASCII Graphics.
8. **framerate:** The maximum framerate the game can run at.
9. **BgColor:** The Background Color of the game.
10. **FgColor:** The Color of the texts and characters.



These variables can be used to make your games better and most can be controlled in “GameCode.cs” by typing for example,  

`Engine.gameRunning = false;` This will cause the game to quit.

`Engine.gamePaused = true;` This will cause the game to pause by blocking the execution of the OnGameUpdate() function.







## Utilities.cs

Utilities.cs contains many useful functions that you would probably use for your Consyl-based Video Games.

 

#### Rand

Rand is a class inside the Utilities class that contains many functions that help with generating random numbers and values, it contains functions that can randomize floats in between a range of inputted values, same goes with integers. Also, it has a function that randomizes between true or false for code that requires Boolean statements like if statements.

 

#### Vec2D

Vec2D is another class inside the Utilities class that contains some useful functions for 2D vectors.

So far there are three functions, one calculates the distance between two vector2s.

The 2nd function will get two 2D vector points and calculate the midpoint of those two points.

The last function will rotate your Vector2 around a specified place on the screen with the specified angles in degrees.



#### Vec3D

Vec3D is a class inside the Utilities class that contains some useful functions for 3D vectors.

So far there are three functions, one calculates the distance between two vector3s.

The 2nd function will get two 3D vector points and calculate the midpoint of those two points.

 The 3rd function will convert 3D Vectors into 2D vectors, which is useful for rendering 3D graphics.



#### Numbers

Numbers is a class under the Utilities class that contains some useful functions for anything related to numbers like floats and integers.

Right now, there are four functions, one that calculates the average number from a float array you input in.

The 2nd function calculates the distance between two numbers in 1D space. You can use it to calculate how far an object is from another object or ground.

The 3rd function converts Degrees to Radian values.

The 4th function converts backward of the 3rd function which converts Radian into Degrees.







## GameObject.cs

GameObject is an advanced tool that will help you make games quicker and easier. It will allow you to create objects like walls, players, enemies, props, and more with ease. In order to make an Object, you need to type in OnGameStart() the following code:

```csharp
public static void OnGameStart() // Gets Executed when game starts running/when the game begins
{
    Engine.CreateGameObject(); // It will ask you to input parameters, add whatever values you see fit.
}
```

There is also `Engine.CreateGameObjectNoTex()` which is the same thing, but is added so that if you don't want to add any texture into your object.

Here's an important method under the Engine class, which is `Engine.DestroyGameObject()`, which destroys a gameObject by inputting its unique ID.

Creating GameObjects with the methods used above will return it's unique ID which is selected at random. You need to store it in an int variable so you can reference the GameObject later.



You need to add parameters first when you create a GameObject, here is the list of parameters you need to include when creating a GameObject:

1. **x** - It's the x coordinate of the object.
2. **y** - it's the y coordinate of the object.
3. **collisionEnabled** - Set to true, if you want your object to collide with other objects.
4. **collisionWidth** - It's the Width of the collision box.
5. **collisionHeight** - It's the height of the collision box.
6. **detectOverlap** - Set to true, if you want your object to detect overlapping.
7. **image** - The texture/sprite of the GameObject.
8. **isPushable** - If true and collision is enabled, the object will be pushed back if it collides. (can be used for creating players)
9. **colOffsetX [OPTIONAL]** - It offsets the collision box in the x-axis relative to the location of the object.
10. **colOffsetY [OPTIONAL]** - It offsets the collision box in the y-axis relative to the location of the object.
11. **collideWithBounds [OPTIONAL]** - If true, the object cannot leave the screen bounds.
12. **drawDebugCollision [OPTIONAL]** - If true, it will show the outline of the collision box. (useful for debugging your game)



There are more variables you can change, like enabling gravity by doing this: `Engine.GetGameObjectByID(2314145).hasGravity = true`



Note that the ID 2314145 in the examples is different based on which GameObject you want to change. Meaning that if you want to do something like changing friction strength with another GameObject you created, you need to get it's ID, when creating it, let's assume its ID is 1340487, you need to do this: `Engine.GetGameObjectByID(1340487).friction = 0.5f;` or you could do this when creating your object, so that things are easier:

```c#
 // Insert static Variables here \\
// \/\/\/\/\/\/\/\/\/\/\/\/\/\/\/ \\
static int objID;
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ \\

public static void OnGameStart() // Gets Executed when game starts running/when the game begins
{
	objID = Engine.CreateGameObject(); // Be sure to fill in the parameters
	Engine.GetGameObjectByID(objID).friction = 0.5f; 
}
```



Here is the list of variables that are not placed when creating your GameObject:

1. **friction** - It will slow down the Object from its velocity. The higher the number, the faster the GameObject would decelerate.
2. **gravityStrength** - It will determine how strong the gravity of the GameObject.
3. **speed** - This is the velocity of the GameObject which is stored as a Vector2 variable.
4. **isOverlapping** - A variable you shouldn't change or set to anything, because that will become true if the GameObject overlaps with another GameObject's collision box. It will only work if the detectOverlap variable is true.
5. **isVisible** - This variable allows you to change whether the player can see your Game Object texture or not.



There are some methods inside GameObjects you use to control them which are listed below:

1. **AddVelocity(Vector2 addedVelocity)** - This will add more speed to the GameObject's velocity.
2. **AddLocation(Vector2 addedLocation)** - Will add values to the coordinates of the GameObject.
3. **Teleport(Vector2 newLoc, bool resetSpeed)** - Will move the GameObject to a desired location, while asking you if you want to reset the velocity or not.
4. **IsObjectOverlapping()** - It will return the `isOverlapping` variable.







## Raycast2D.cs

It's a small but useful tool that is used in a lot of games which fires an invisible line from a location and returns true if it hits a Game Object.

When creating a Raycast2D object, you need to type the parameters for its constructor (it has two constructors):

1. **start** - The start location of the Raycast.
2. **end** - The end location of the Raycast.
3. **ignoredObjects [Only in one of the constructors]** - It's the Array of Game Objects that the Raycast would ignore and not hit.
4. **drawDebug [OPTIONAL]** - Whether you want the Raycast to be visible or not.



When it hits an object it stores information in these variables stored inside:

1. **hitLoc** - The impact location of the Raycast.
2. **hitObject** - It's the GameObject that got hit.
3. **hit** - A boolean variable that tells whether the raycast hits a GameObject or not.
4. **distance** - it's the distance between the starting and hit points.



In order to create 2D Raycasts, you must first create a Raycast2D variable like this, `Raycast2D ray = new Raycast2D();`  make sure to fill in all the parameters. Then if you want to cast the ray you must type this, `ray.CastLine();` which would cast the ray and the method will return the "hit" variable. If you set the drawDebug variable to true, you can visualize the Line on the screen.

