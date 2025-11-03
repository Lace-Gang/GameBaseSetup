# GameBaseSetup
This is where I will be creating my capstone and running some of my testing for it.

## Contents
* [Installation and Setup Instructions](#installation-and-setup-instructions)
* [Important Information](#important-information)
* [Game Instance](#game-instance)
* [User Interface](#user-interface)
* [Save System](#save-system)
* [Player Character](#player-character)
* [Main Camera](#main-camera)
* [Health and Damage System](#health-and-damage-system)

## Installation and Setup Instructions
* [Install From Disk](#install-from-disk)
* [Setup](#gamebase-setup)

### Install From Disk
1.) Click the "Code" dropdown, and then click "Download ZIP"
<br>
<img width="371" height="323" alt="image" src="https://github.com/user-attachments/assets/67a1f73c-a74f-45fc-92fe-d75c3dd8e494" />

2.) Find the downloaded zip file in File Explore. Right click the zip file and then click "Extract All"
<br>
<img width="388" height="202" alt="image" src="https://github.com/user-attachments/assets/c4266c10-99fb-4300-9618-0a13b4adcb2c" />

3.) Choose the location to save the files to and then click "Extract"
<br>
<img width="436" height="360" alt="image" src="https://github.com/user-attachments/assets/03712191-f04f-44d1-98a0-0822bf48734e" />

4.) In your Unity Project: Go to "Windows" and "Package Manager"
<br>
<img width="223" height="444" alt="image" src="https://github.com/user-attachments/assets/af9aeb52-dc90-4bd0-8a37-f7258bf97148" />

5.) Click the "+" Dropdown and then select "Install package from disk"
<br>
<img width="224" height="156" alt="image" src="https://github.com/user-attachments/assets/78f3deb4-3854-4b24-8cfd-5889bfbb616a" />

6.) Navigate to the extracted file, then navigate to: GameBaseSetup-main>Packages>com.lace.gamebase and select "package.json", then click "Open"
<br>
<img width="484" height="380" alt="image" src="https://github.com/user-attachments/assets/e614fcdc-c1bf-4880-a8ad-f258bec99e15" />

7.) Game Base should now be visible in your Package Manager
<br>
<img width="452" height="169" alt="image" src="https://github.com/user-attachments/assets/626de6c3-4f98-46ca-b4c5-71edf1cfee17" />

8.) A Game Base file will appear under "Packages" in your project window. Scripts and Prefab objects can be accessed from here in the "runtime" folder
<br>
<img width="323" height="320" alt="image" src="https://github.com/user-attachments/assets/1bf40035-0f76-4899-9237-275b81f77db3" />

<br>

### GameBase Setup
When setting up GameBase:
<br>
1.) [Set up the Game Instance](#game-instance-set-up) (This is ABSOLUTELY required)
<br>
2.) [Set up the Player Character](#player-set-up) (This is REQUIRED)
<br>
3.) [Adjust User Interface](#setting-up-the-user-interface) (Advised but not required)
<br>
4.) Change Player [Avatar and Animations](#player-avatar-and-animations) (Advised but not required)

## How To Use
[To Be Writen]


## Important Information
1.) The Game must be run from the "Base" Scene to ensure that the Game Instance and User Interface is always present. Alternatively, include a script in any other scenes that 
loads the "Base Scene" scene additively when the project opens.


# User Guide


## Game Instance
* [Set Up](#game-instance-set-up)
* [Additional Information](#game-instance-additional-information)

### Game Instance Set Up
#### Step One: Adding Required Scenes
In order to use the Game Manager, the "Base" scene should be added
<br>
1.) Open the "Base" Scene from Packages -> Game Base -> Runtime -> Game Base -> Scenes
<br>
<img width="179" height="227" alt="image" src="https://github.com/user-attachments/assets/594d2919-f5c6-40fa-ae23-c25c16ca3928" />
<br>
2.) With the "Base" scene open, go to File -> Build Profiles -> Scene List
<br>
<img width="211" height="227" alt="image" src="https://github.com/user-attachments/assets/b8237c91-4502-4286-9f85-0ac7068e8570" />
<img width="271" height="132" alt="image" src="https://github.com/user-attachments/assets/73659741-29ae-40fe-9f59-253da72cbfda" />
<br>
3.) Click "Add Open Scenes"
<br>
The Base scene should be added to the project Scene List. Ensure that the box beside the scene is checked.
<br>
<img width="368" height="159" alt="image" src="https://github.com/user-attachments/assets/40f30187-0272-42e7-b55f-2a41e6b7be88" />
<br>
4.) Repeat Steps 1, 2, and 3, for the Scenes: "UIDisplayScene", and for the scene that you intend for the main gameplay to take place in

#### Step Two: Game Instance Configuration
1.) In the "Base" Scene, click on the Game Instance Object in the Hierarchy
<br>
<img width="247" height="121" alt="image" src="https://github.com/user-attachments/assets/a754c0dd-61e4-4977-8617-230d1578689b" />
<br>
2.) Go to the "Game Instance" script in the Inspector, and change the "Game Scene Name" to the name of the scene that your game plays from
<br>
<img width="361" height="156" alt="image" src="https://github.com/user-attachments/assets/4c0722c2-6737-4ab0-8b21-270df8fe4228" />

<br>

### Game Instance Additional Information
1.) "Game State" referes to the state that the game will start in. It is highly advised to only change this to: "Load Title", "Load Main Menu", or "Start Game"
<br>
2.) If the "Respawn Type" is set to "Respawn at Static Location", then there must be a Static Spawn Point present in the main gameplay scene that is configured for the 
player. Place the "Player Static Spawn Point prefab somewhere in the main gameplay scene. Alternatively, create an object in the scene, add the "Static Spawn Point" script
onto the object, and set "Player" as the static spawn tag.
<br>
<img width="301" height="117" alt="image" src="https://github.com/user-attachments/assets/b5a2801c-bc02-42d4-b24c-4f0cb5fe5ad8" />
<br>
<img width="321" height="233" alt="image" src="https://github.com/user-attachments/assets/a3df05c5-d244-4f05-ab9e-5438d7741136" />
<br>
<img width="309" height="65" alt="image" src="https://github.com/user-attachments/assets/ac6323b2-1c70-4157-b2b9-39ad7e2a8088" />

<br>
<br>

## User Interface
* [Set Up](#setting-up-the-user-interface)
* [Editing User Interface Frontend](#editing-user-interface-frontend)
* [Editing User Interface Backend or Adding UI Elements](#editing-user-interface-backend-or-adding-ui-elements)

### Setting Up The User Interface
NOTE: Do NOT add a User Interface to any scene directly. There is already a User Interface present in the "Base Scene".
<br>
<br>
1.) For the setup of the User Interface, and for a User Interface that is more personalized to your game,  it is advised to complete the steps found in [Editing User Interface Frontend](#editing-user-interface-frontend)

<br>

### Editing User Interface Frontend
1.) Go to the User Interface Object: Open "Base Scene", in the hierarchy, the "User Interface" object is located under the "Game Manager" object.
<br>
<img width="239" height="80" alt="image" src="https://github.com/user-attachments/assets/fc486c0c-1dbd-4ae1-9f21-c8f49490e974" />
<br>
2.) In order to view the current appearance of the User Interface:
<br>
  2a.) Ensure view is in "Game" mode and not in "Scene" mode or any other views (ie the animator)
<br>
<img width="256" height="100" alt="image" src="https://github.com/user-attachments/assets/0d79cb99-c5aa-4a66-9dc1-e7a5d4a7ed4d" />
<br>
  2b.) Set the "Test Camera" to active in the Inspector
<br>
<img width="455" height="143" alt="image" src="https://github.com/user-attachments/assets/156d7be6-1a42-4dbc-b119-7bb54f8e018c" />
<br>
3.) To see specific UI screens, those screens will have to be set to active in the inspector, and the "Fade Screen" will have to be set to inactive.
<br>
<img width="383" height="242" alt="image" src="https://github.com/user-attachments/assets/be0a4779-9135-4ef0-9c3e-dcbb20e28501" />
<br>
4.) It is advised to look through each screen in the UI to ensure that all text, images, arrangements, and other aspects of the display are to your liking.
<br>
IMPORTANT: It is highly inadvised to delete the components: 
* "HealthBar" (HUD)
* "LivesText" (HUD)
* "SaveButton" (PauseScreen)
* "LoadButton" (MainMenu)

Deleting any of these components will cause Errors across multiple scripts, including the GameInstance script, UserInterface script, and PlayerCharacter script. If you do not need
or want these components, it is avised to either set them to inactive in the Inspector, or be prepared to make edits to the GameBase scripts. 
<br>
<br>
5.) When you have finished editing the appearance and arrangement of the User Interface, it is highly advised to set the "Fade Screen" to active, all other screens to inactive, and the "Test Camera" to inactive.
<br>
<img width="626" height="311" alt="image" src="https://github.com/user-attachments/assets/903aa565-9078-40a9-a852-fea44beefe36" />

<br>

### Editing User Interface Backend Or Adding UI Elements
When editing User Interface backend (including adding new button functionality, or new screens), it is advised to:
<br>
  1.) Write Button Click event functions into the "UserInterface" script. From there, the User Interface can trigger functionality elsewhere. (this is advised for simplicity, so that 
the "User Interface" object can function without requiring attatchments and references to unrelated objects.
<br>
<br>
  2.) When editing values at runtime (ie healthbars, scores, etc), do so with functions written in the User Interface that can be triggered by the GameInstance. (This will allow other scripts to update the UI display as necessary without requiring a reference to anything as the GameInstance can be accessed from anywhere without a reference.)
<br>
<br>
  3.) Set visibility of User Interface elements through the Game Instance. (this is advised because the GameInstance already handles all other UI visibility.)

<br>
<br>











## Player Character

* [Player Set Up](#player-set-up)
* [Player Controller](#player-controller)
* [Player Health](#player-health)
* [Player Avatar And Animations](#player-avatar-and-animations)

### Player Set Up
NOTE: Do NOT add a "Player" prefab directly to the scene.
<br>
<br>
1.) Ensure that a "Player Spawn Point" is present in the main gameplay scene. This is where the player will spawn (if not loading a previous save file). This will also allow the player 
to spawn in the correct location when loading a previous save file. The "Game Instance" object will be responsible for spawning the Player Character
<br>
<img width="347" height="290" alt="image" src="https://github.com/user-attachments/assets/96f7830e-4f48-45f6-ae0c-a1cdfcd8eb1d" />
<img width="260" height="224" alt="image" src="https://github.com/user-attachments/assets/732997f9-78ca-44bb-bf66-8c0fb37a5c79" />

<br>

### Player Controller
Player input, actions, states, and movements are defined here
<br>
<img width="226" height="329" alt="image" src="https://github.com/user-attachments/assets/bcd23709-3ee7-477c-91f2-f50e156798f0" />
<br>
Note: If changing the player inputs, please try to match the input types (ie, Composit (2D Vector) -> Composit (2D Vector) || Binding -> Binding), failure to do so could lead to runtime or compiletime errors.
<br>

To add new player inputs and actions:
<br>
Use an InputAction to track player inputs and trigger action methods
<br>
<img width="238" height="24" alt="image" src="https://github.com/user-attachments/assets/4a21dc76-7d56-4959-9904-b36cb3524d0a" />
<br>

Set up action methods with "InputAction.CallbackContext ctx" as the only parameter
<br>
<img width="313" height="25" alt="image" src="https://github.com/user-attachments/assets/12da8088-6add-41e5-b6c2-91e6780e57c6" />
<br>

Bind InputAction to Action Method in the Awake method, Enable InputAction in the OnEnable method, and Disable InputAction in the OnDisable method
<br>
<img width="233" height="472" alt="image" src="https://github.com/user-attachments/assets/88847128-e147-45c9-b3b1-35e0763536f1" />

<br>

### Player Health
The PlayerCharacter component includes the IDamagableInterface to enable damage to the player through the Game Base Health and Damage system. To prevent Player Character from taking damage through 
Game Base's damage system, uncheck "Is Damagable" in the Player Character Script.
<br>
<img width="288" height="94" alt="image" src="https://github.com/user-attachments/assets/f4cc78fb-73e6-4473-8acd-e0055d9ab4d2" />

<br>

### Player Avatar And Animations
NOTE: If you intend to use an avatar that is NOT humanoid, you will need to rework the animations
#### To Change Player Avatar:
1.) Configure Avatar to be Humanoid in "Rig"
<br>
<img width="298" height="198" alt="image" src="https://github.com/user-attachments/assets/1dca4904-9807-44cd-88ab-e7eccd8fd8a7" />
<br>
2.) Drag new Model into "Player Character" in the "Player" prefab.
<br>
<img width="212" height="65" alt="image" src="https://github.com/user-attachments/assets/2bf58f21-6f93-47d3-911e-ccdcf2174f8f" />
<br>
3.) For each Animation, set "source" to be the avatar of the new model.
<br>
<img width="288" height="199" alt="image" src="https://github.com/user-attachments/assets/3505d3cf-5e7f-43c3-a40a-c1c08cc8b26b" />
<br>
You MUST set the new avatar in: "Y Bot@Death", "Y Bot@Idle", "Y Bot@JumpForward", "Y Bot@JumpUp", "Y Bot@StandardRun", and "Y Bot@Walking"

#### To Change Existing Player Animation(s)
(to add an animation that there is no default for, follow steps 1 and 2 and then add the new animation wherever necessary)
<br>
1.) In the new animation -> Rig, set "Animation Type" to "Humanoid", "Avatar Definition" to "Copy From Other Avatar", and set the Source to your chosen avatar. If using the default avatar, set to the YBotAvatar
<br>
<img width="288" height="199" alt="image" src="https://github.com/user-attachments/assets/df7e6837-b3ec-4ebd-807d-248f2a75a139" />
<br>
2.) Open the Animator (which can be accessed by double clicking the "Player Character" Animator component
<br>
<img width="194" height="90" alt="image" src="https://github.com/user-attachments/assets/3355f4a3-c252-45e1-9b3a-6a53b7ed059b" />
<br>
3.) Locate and click the state you want to change. Then, drag new animation into the "Motion" feild of the state. (idle, walk, and run animations are located in the "Movement" Blend Tree)
<br>
<img width="296" height="77" alt="image" src="https://github.com/user-attachments/assets/712f3bec-bf02-4d2f-929b-f14ec83435bc" />




<br>
<br>







## Main Camera
Features a First Person Camera and Third Person Camera that has a defined target. The Third Person Camera will auto-adjust its distance from the target to keep the target in view if another object would otherwise block it from view.

* [Main Camera Set Up](#main-camera-set-up)
* [First Person Vs Third Person](#first-person-vs-third-person)

### Main Camera Set Up
To add a MainCamera to the scene, add the MainCamera script to a camera object, and add the Transform of the target in the Universal Camera Settings section of the script in the editor.
<br>
<img width="207" height="32" alt="image" src="https://github.com/user-attachments/assets/35876392-80d7-4230-8a55-8fc7e1ceba20" />

<br>

### First Person Vs Third Person
Change from First Person Camera to Third Person Camera (or from Third Person Camera to First Person Camera) in the Universal Camera Settings section of the script in the editor.
<br>
<img width="214" height="22" alt="image" src="https://github.com/user-attachments/assets/c9c00fcb-67e9-411a-aad0-b63b58a925c8" />
<br>

* Camera adjustments to follow the target are defined in the MainCamera script
* Player movement based on camera direction is defined in the PlayerController script

<br>
<br>


## Health and Damage System
* [Health And Damage System Set Up](#health-and-damage-system-set-up)

### Health And Damage System Set Up
To create a source of damage, add a Damage Source script to an object with a collider set to trigger.
<br>
<img width="296" height="310" alt="image" src="https://github.com/user-attachments/assets/6b52ef9e-8016-4365-a592-a360040b8a7e" />
<br>

If you want an object to be damageable through the Health and Damage System, that object must have a component that includes the IDamagableInterface. This
indicates to Damage Sources that an object can receive damage.
<br>
<img width="371" height="41" alt="image" src="https://github.com/user-attachments/assets/95268ee9-2125-4dcb-8d95-14b474737e36" />
<br>

NOTE: There is a Health component as well. This component is optional, but can quickly privode basic health functionality.

<br>
<br>




## Save System

* [Add To Scene](#add-to-scene)
* [Save Data From Objects](#save-data-from-objects)
* [File Data Handler](#file-data-handler)

### Add To Scene
1.) Do NOT add the Data Persistence Manager to a scene directly. There is already a Data Persistence Manager present in the "Base Scene".

2.) Open the "Base Scene". Ensure that at least one save condition is selected, and at least one load condition is selected on either the "Data Persistence Manager" script located in the Data Persistence Manager (prefab or the object that is a child to the Game Instance object) and/or the "Game Instance" script on the "Game Instance" object.
<br>
<img width="623" height="362" alt="image" src="https://github.com/user-attachments/assets/d990d7f1-8715-4449-84b6-88fa7e7865d1" />
<br>
There are alternative save and load conditions present in the "Game Instance" script.
<br>
<img width="293" height="114" alt="image" src="https://github.com/user-attachments/assets/05946424-7be5-4e83-811f-c203aeedb787" />

<br>

### Save Data From Objects
In the GameData Script, add a public variable for each peice of data in your game you want to save. It is recommended but not required to remove values from GameData if you
will not be saving those values (this will save space in the save file).
Note: if you want any data to be loaded from an unused save file, that will have to be defined here.
<br>
<img width="244" height="185" alt="image" src="https://github.com/user-attachments/assets/0d22bdf9-ebe9-487e-bfeb-909ca59555a7" />
<br>

To save the any data in any class, ensure that the IDataInterface is added to the class.
<br>
<img width="404" height="28" alt="image" src="https://github.com/user-attachments/assets/189e12fb-65fa-4216-86c8-3b33b8111fc2" />
<br>

In the SaveData method (required by IDataInterface), update any values in the GameData object that are associated with this class
<br>
<img width="410" height="95" alt="image" src="https://github.com/user-attachments/assets/4ead06a5-6d05-4576-8ec9-21cac77e359f" />
<br>

In the LoadData method (required by IDataInterface), load any values from the GameData object that are associated with this class
<br>
<img width="392" height="150" alt="image" src="https://github.com/user-attachments/assets/205695e4-b0a9-4356-9abd-7026957722b7" />
<br>
Note: "if(!data.isNewSave)" allows savable data to be adjusted in the editor rather than the GameData object
<br>
<img width="331" height="34" alt="image" src="https://github.com/user-attachments/assets/25f688e0-6ac6-4a06-a11d-7387bd45386e" />

<br>

### File Data Handler
Modifications to the Data Serialization/Deserialization and Encryption/Decryption or to the File Read/Write system must be made in the FileDataHandler. It is advised not to do this if you are not familiar JSON

<br>
<br>



# Art Credits:
* Credit to Mixamo for the default player model and default player animations!


