# GameBaseSetup
This is where I will be creating my capstone and running some of my testing for it.

## Contents
* [Installation and Setup Instructions](#installation-and-setup-instructions)
* [Important Information](#important-information)
* [Game Instance](#game-instance)
* [User Interface](#user-interface)
* [Player Character](#player-character)
* [Main Camera](#main-camera)
* [Health and Damage System](#health-and-damage-system)
* [Save System](#save-system)
* [Items](#items)
* [Inventory System](#inventory-system)

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
1.) Open the "Base" Scene from Packages -> Game Base -> Runtime -> Game Base -> Scenes -> "BaseScene" (double click)
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="179" height="227" alt="image" src="https://github.com/user-attachments/assets/594d2919-f5c6-40fa-ae23-c25c16ca3928" />
<br>
<br>
2.) With the "Base" scene open, go to File -> Build Profiles -> Scene List
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="211" height="227" alt="image" src="https://github.com/user-attachments/assets/b8237c91-4502-4286-9f85-0ac7068e8570" />
<img width="401" height="191" alt="image" src="https://github.com/user-attachments/assets/16343a1f-4e80-493c-8b82-989ee51e0d9d" />
<br>
<br>
3.) Click "Add Open Scenes"
<br>
The Base scene should be added to the project Scene List. Ensure that the box beside the scene is checked.
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="549" height="241" alt="image" src="https://github.com/user-attachments/assets/65989697-799e-4caf-aef8-44be6ebadb9b" />
<br>
<br>
4.) Repeat Steps 1, 2, and 3, for the Scenes: "UIDisplayScene", and for the scene that you intend for the main gameplay to take place in

#### Step Two: Game Instance Configuration
1.) In the "Base" Scene, click on the Game Instance Object in the Hierarchy
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="247" height="121" alt="image" src="https://github.com/user-attachments/assets/a754c0dd-61e4-4977-8617-230d1578689b" />
<br>
<br>
2.) Go to the "Game Instance" script in the Inspector, and change the "Game Scene Name" to the name of the scene that your game plays from
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="361" height="156" alt="image" src="https://github.com/user-attachments/assets/4c0722c2-6737-4ab0-8b21-270df8fe4228" />

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
2.) Add Avatar Masks to the Animator component:
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2a.) Locate Avatar Masks in: Project -> GameBase -> Runtime -> GameBase -> Default Assets -> Masks
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="245" height="267" alt="image" src="https://github.com/user-attachments/assets/07dac400-3e8b-4f96-a6f5-117d5d17aeae" />
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2b.) Drag "LeftArmMask" and "UpperBodyMask" (or a duplicated copy of each) into your "Assets". Dragging the Masks folder (or a duplicate &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;copy of it) will also work.
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="264" height="119" alt="image" src="https://github.com/user-attachments/assets/a4b5d4a1-da4d-4cf6-b885-e3612fce9a15" />
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2c.) Open up the "PlayerCharacter" Animator Controller in: Project -> GameBase -> Runtime -> GameBase -> Default Assets -> Animations &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-> PlayerCharacter (double click the AnimatorController to open the Animator) 
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="243" height="89" alt="image" src="https://github.com/user-attachments/assets/1bf09b45-6b2d-4e88-9001-4714f790c5b4" />
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2d.) Add "UpperBodyMask" to "OneHandedMeleeLayer": 1 - Click the gear icon next to "OneHandedMeleeLayer" in the Anamator. 2 - Click &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;the dot next to "Mask". 3 - In the pop-up, select the "UpperBodyMask". 4 - Exit the pop-up.
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="420" height="212" alt="image" src="https://github.com/user-attachments/assets/74448e58-a7b0-46a8-8f46-c4b4f808dee0" />
<img width="548" height="162" alt="image" src="https://github.com/user-attachments/assets/3bd31330-710d-4e26-acf9-4bb55f0260f5" />
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2e.) Repeat step 2d, adding "LeftArmMask" to "DefaultLeftArmLayer" and adding "UpperBodyMask" to "PistolLayer". Do not add an &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AvatarMask to "MovementLayer". 
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2f.) When you are finished, the layers should look like this (it is recommended to validate that other settings match as well)
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="440" height="164" alt="image" src="https://github.com/user-attachments/assets/afde96fd-2fe1-4e5e-b461-6f2211182404" />
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="437" height="126" alt="image" src="https://github.com/user-attachments/assets/4ffbcde5-6dfe-48a0-99fa-c9098675a5d6" />
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="437" height="131" alt="image" src="https://github.com/user-attachments/assets/8c3c6d2e-4b20-40de-aeb5-ce0287528139" />
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="439" height="115" alt="image" src="https://github.com/user-attachments/assets/c6f4dca8-e075-4e49-8912-436204796727" />







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
1.) Ensure that there is a SerializableDictionary in the GameData object with the correct value type for the data you are planning to save.
<br>
<img width="575" height="190" alt="image" src="https://github.com/user-attachments/assets/fd7a5abe-7adf-461e-ad9c-1dc81c0920cb" />
<br>
2.) To save the data in any class, ensure that the IDataInterface is added to the class.
<br>
<img width="404" height="28" alt="image" src="https://github.com/user-attachments/assets/189e12fb-65fa-4216-86c8-3b33b8111fc2" />
<br>

3.) In the SaveData method (required by IDataInterface), check the SerializableDictionary with the correct value type in the GameData object for your desired key for the
value. If the key already exists, update it's value, otherwise add and new key/value pair with your desired key and value.
<br>
This is the suggested way to structure this:
<br>
<img width="500" height="101" alt="image" src="https://github.com/user-attachments/assets/4a33d002-298e-4566-8bef-c984a14e7539" />
<br>

4.) In the LoadData method (required by IDataInterface), check the SerializableDictionary with the correct value type in the GameData object for the key of the data you want to load.
If the key exists, load data from the SerializableDictionary using the key.
<br>
This is the suggested way to structure this:
<br>
<img width="386" height="56" alt="image" src="https://github.com/user-attachments/assets/fbb061cc-3668-400c-8446-aa5924e09c34" />
<br>
<br>
Note: If you plan to have more than one instance of a savable object, it is highly advised to use an ID system, with the ID added into each key when saving and loading, as the examples above show.
<br>
<img width="275" height="47" alt="image" src="https://github.com/user-attachments/assets/83e61e6e-3b71-45b2-8d40-deb17c13a806" />

<br>

### File Data Handler
Modifications to the Data Serialization/Deserialization and Encryption/Decryption or to the File Read/Write system must be made in the FileDataHandler. It is advised not to do this if you are not familiar JSON

<br>
<br>


## Items
* [Included Prefabs](#included-item-prefabs)
* [Create New Item](#creating-a-new-item)

### Inluded Item Prefabs
* Basic Health Recovery Item
* Basic Score Increase Item
* Basic Health Upgrade
* Savable Health Recovery Item
* Savable Score Increase Item
* Savable Health Upgrade

<br>
  
### Creating A New Item
1.) Make your item script a child of one of the abstract item scripts (or a child of an item script that derives from an abstact item script). For items that should appear in the inventory, use the "InventoryItem" script (these items will be able to save as well). For items that should be saved and loaded, use the "SavableItem" script. Otherwise, use the "ItemBase" script.
<br>
  1a.) Non-Saving:
  <br>
  <img width="242" height="35" alt="image" src="https://github.com/user-attachments/assets/9942ab15-3811-4c2b-a1ed-f7cc381d4994" />
  <br>
  1b.) Saving:
  <br>
  <img width="240" height="24" alt="image" src="https://github.com/user-attachments/assets/b23cda58-30e9-4b78-9331-190c53b7e246" />
  <br>
  1c.) Inventory:
  <br>
  <img width="244" height="26" alt="image" src="https://github.com/user-attachments/assets/0a08e473-f43d-4189-91d5-5afb53595205" />
<br>
2.) When setting up the new item, ensure that the object has a Collider component, and that the Collider is set to "Trigger
<br>
<img width="294" height="99" alt="image" src="https://github.com/user-attachments/assets/2de013bb-d9df-4099-99a8-7310a2250dc6" />
<br>
3.) If the item you are setting up is an Inventory Item, additional instructions may be found [here](#creating-inventory-items)

<br>
<br>


## Inventory System
* [Set Up](#inventory-set-up)
* [Adjusting Inventory Screen](#adjusting-inventory-screen)
* [Creating Inventory Items](#inventory-items)
* [Stacking Inventory Items](#stacking-inventory-items)
* [Saving Inventory](#saving-inventory-items)

### Inventory Set Up
NOTE: The Inventory is already located in the 'BaseScene' as a part of the 'GameInstance' object, and does not need to be added to any scene.
<br>
<img width="545" height="284" alt="image" src="https://github.com/user-attachments/assets/cb8c7a3a-66ce-4fd1-a9ae-ab303380f68a" />
<br>
#### To use the Inventory System:
1.) Ensure that "Use Inventory" is checked to "true"
<br>
<img width="212" height="97" alt="image" src="https://github.com/user-attachments/assets/79d72d29-2f52-48e8-9d1c-5e2e9eb6f215" />
<br>
2.) (Optional) If you want items to be automatically equipped when they are picked up and nothing else is eqiupped, ensure that "Send First Item To Equipped" is set to "true". Otherwise
set it to "false.
<br>
<img width="182" height="98" alt="image" src="https://github.com/user-attachments/assets/2011e86c-42f3-4f8d-a60d-d12be9a75341" />
<br>
3.) Ensure that "Equipped Item Box" is NOT empty.
<br>
<img width="299" height="40" alt="image" src="https://github.com/user-attachments/assets/1a9d4982-bfe2-4349-a390-1ba3933a8230" />
<br>
  3a.) If "Equipped Item Box" IS empty, then go to "User Interface" -> "HUD", and find the "Equipped Item Box" located in the HUD. The click and drag "Equipped Item Box" object from the hierarchy into "Equipped Item Box" in the "Inventory" script.
<br>
<img width="545" height="244" alt="image" src="https://github.com/user-attachments/assets/2da0e2bb-1e1a-40d9-b5df-20c82253e62d" />
<br>
4.) (Optional) If you want the items in the inventory to save and load, follow the steps outlined [here](#saving-inventory-items)
<br>
5.) (Optional) The "EquippedItemBox" will display the key that when pressed will use the equipped item, as shown below.
<br>
<img width="75" height="68" alt="image" src="https://github.com/user-attachments/assets/417ac1eb-869c-45db-88b7-dcc7c53b2a4c" />
  5a.) The default for this key is "E". To change this key, go to "UserInterface" -> "HUD" -> "EquippedBox" and in the "EquippedItemBox" script, change the "Use Key".
  <br>
  <img width="543" height="200" alt="image" src="https://github.com/user-attachments/assets/066cd9a7-72de-4dd6-b391-4148260848eb" />
<br>
6.) (Optional) This is the default layout and size of the Inventory when displayed in the Inventory Screen:
<br>
<img width="742" height="506" alt="image" src="https://github.com/user-attachments/assets/139a27fe-6402-4a5b-95da-3603d0b3ecdd" />
<br>
To adjust the size of the Inventory, or the dimensions or proportions of the layout, follow the steps outlined [here](#adjusting-inventory-system)

  
### Adjusting Inventory Screen
IMPORTANT TO NOTE: The Inventory Box is generated _at runtime!_ Adjusting the size of the "InventoryBox" will not persist at runtime, nor will any size adjustments made to the "InventoryItemBox". However, _location_ adjustments may be made to the "InventoryBox", and these adjustments will persist.
<br>
1.) Go to "User Interface", then find the "Inventory Screen" section of the "User Interface" script.
<br>
<img width="283" height="215" alt="image" src="https://github.com/user-attachments/assets/45acef7d-84b6-4e0e-bd93-854c2bf7d0b4" />
<br>
  1a.) "Image Box Width" and "Image Box Height" can be used to adjust the size of the "InventoryItemBox" that are generated.
  <br>
  1b.) "Rows" and "Columns" can be used to alter the number of "InventoryItemBox" that are generated, as well as their layout.
  <br>
  1c.) "Margin" is the distance in between each "InventoryItemBox"
  1d.) "Padding" is the distance from the edges of the "InventoryBox" to the nearest "InventoryItemBoxes"
<br>

### Inventory Items
1.) To create an inventory item, first follow the steps located [here](#creating-a-new-item), then configure based on the following information.
<br>
2.) "InventoryItem" is a child class of "SavableItem", and as such shares all of the same properties in the editor. In addition to these properties, are the following:
<br>
<img width="281" height="122" alt="image" src="https://github.com/user-attachments/assets/2029879a-2d63-4712-a173-345347f6d98e" />
<br>
2a.) "Inventory ID" is a failsafe method to tell two distinct items appart. This MUST be unique if _both_: 
  * Two or more items have all of the same properties but have different sprites
  * You do NOT want these items to stack. (stacking in this scenario will only display the sprite of the most recent item that was picked up)
  <br>
  2b.) "Inventory Sprite" if the image that will be displayed in the "InventoryScreen" and in the "EquippedBox" for this item. This must be an actual sprite, as opposed to a .JPEG, a .PNG or a material.
  <br>
  2c.) "Use From Inventory" - If set to "true" this item can be used directly from the "InventoryScreen" without equipping it first. If set to false, item must first be equipped before being used.
<br>
  2d.) "Equippable" - If set to "true" this item can be equipped. If set to false, this item cannot be equipped from the menu, and will not auto-equip even if the "Send First Item To Equipped" option is set to "true"
  in the Inventory.
<br>
  2e.) "Removable" - If set to "true" this item can be removed from the inventory without using the item, effectively discarding the item.
<br>
  2f.) "Consume After Use" - If set to "true" then using this item will reduce the number of this item in the inventory by one. If set to "false" then this item can be used infinitely from the inventory.
<br>
  2g.) "Stack Instances In Inventory" - If set to "true" then multiple instances of this item can be stored together and will only take up one Inventory Slot. For full instructions on setting up Item Stacking in Inventory, see the instructions [here](#stacking-inventory-items)
<br>

#### Stacking Inventory Items
When an item stacks in the inventory, that means that multiple instances of this item can be stored together and will only take up one Inventory Slot. There are a few requirements that must be met in order for items to stack in the inventory:
<br>
1.) In the "Inventory Item Information" on that object's script, "Stack Instances In Inventory" must be set to true.
<br>
<img width="137" height="128" alt="image" src="https://github.com/user-attachments/assets/e6efdd3d-7c0c-4f57-a440-01c449b7e556" />
<br>
2.) All other settings under "Inventory Item Information" must be identical between instances of an item you want to stack (with the exception of the "Inventory Sprite"). These settings are: "Inventory ID", "Use From Inventory", "Equippable", "Removable" and "Consume After Use". Any differences will result in the item NOT stacking.
<br>
3.) The "Name" setting in the "Basic Item Information" section must be the same between instances of the item you want to stack.
<br>
<img width="222" height="32" alt="image" src="https://github.com/user-attachments/assets/b9d48d35-2e50-449d-8d42-6ec08b941d0c" />


### Saving Inventory Items
For an item to be saved once it has been added to the inventory certain steps MUST be taken:
<br>
1.) The item must be made into a prefab.
<br>
2.) Go to "BaseScene" -> "Game Instance" -> "Inventory". In the Inventory script, find the "Savable Inventory Items" property.
<br>
<img width="181" height="151" alt="image" src="https://github.com/user-attachments/assets/201027e7-129c-466f-9973-9903f3ee1c9d" />
<br>
3.) Add the prefab of the item you want to be saved to this list. This can be done by clicking and dragging the prefab onto the list from the Project Folder.
<br>
<img width="544" height="163" alt="image" src="https://github.com/user-attachments/assets/9d2f2a65-d083-474c-8496-779557745323" />
<br>


<br>
<br>

# Art Credits:
* Credit to Mixamo for the default player model and default player animations!


