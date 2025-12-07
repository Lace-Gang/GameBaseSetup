# GameBaseSetup
This is where I will be creating my capstone and running some of my testing for it.

Game Base is a flexible framework designed to assist and expedite the Game Development process in Unity6. Game Base is designed to be fully extensible.

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
* [Sound](#sound)
* [Weapons](#weapons)
* [Miscellaneous](#miscellaneous)

## Installation and Setup Instructions
* [Install From Disk](#install-from-disk)
* [Setup](#gamebase-setup)

### Install From Disk
1.) Click the "Code" dropdown, and then click "Download ZIP"
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="371" height="323" alt="image" src="https://github.com/user-attachments/assets/67a1f73c-a74f-45fc-92fe-d75c3dd8e494" />

2.) Find the downloaded zip file in File Explore. Right click the zip file and then click "Extract All"
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="388" height="202" alt="image" src="https://github.com/user-attachments/assets/c4266c10-99fb-4300-9618-0a13b4adcb2c" />

3.) Choose the location to save the files to and then click "Extract"
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="436" height="360" alt="image" src="https://github.com/user-attachments/assets/03712191-f04f-44d1-98a0-0822bf48734e" />

4.) In your Unity Project: Go to "Windows" and "Package Manager"
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="223" height="444" alt="image" src="https://github.com/user-attachments/assets/af9aeb52-dc90-4bd0-8a37-f7258bf97148" />

5.) Click the "+" Dropdown and then select "Install package from disk"
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="224" height="156" alt="image" src="https://github.com/user-attachments/assets/78f3deb4-3854-4b24-8cfd-5889bfbb616a" />

6.) Navigate to the extracted file, then navigate to: GameBaseSetup-main>Packages>com.lace.gamebase and select "package.json", then click "Open"
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="484" height="380" alt="image" src="https://github.com/user-attachments/assets/e614fcdc-c1bf-4880-a8ad-f258bec99e15" />

7.) Game Base should now be visible in your Package Manager
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="452" height="169" alt="image" src="https://github.com/user-attachments/assets/626de6c3-4f98-46ca-b4c5-71edf1cfee17" />

8.) A Game Base file will appear under "Packages" in your project window. Scripts and Prefab objects can be accessed from here in the "runtime" folder
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="323" height="320" alt="image" src="https://github.com/user-attachments/assets/1bf40035-0f76-4899-9237-275b81f77db3" />

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

## Important Information
1.) The Game must be run from the "Base" Scene to ensure that the Game Instance and User Interface is always present. Alternatively, include a script in any other scenes that 
loads the "Base Scene" scene additively when the project opens.
<br> <br>
2.) The first time you run your project, you will be prompted to import TMP Essentials (if you have not already). This package is necessary for the User Interface to function properly. Please import TMP when you are prompted to. You may be prompted to import two TMP packages. If this happens, please import both.



<br>
<br>

# User Guide


## Game Instance
* [Set Up](#game-instance-set-up)
* [Additional Information](#game-instance-additional-information)

### Game Instance Set Up
#### Step One: Adding Required Scenes
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;In order to use the Game Manager, the "Base" scene should be added:
<br>
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Note: The Base scene should be added to the project Scene List. Ensure that the box beside the scene is checked.
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="549" height="241" alt="image" src="https://github.com/user-attachments/assets/65989697-799e-4caf-aef8-44be6ebadb9b" />
<br>
<br>
4.) Repeat Steps 1, 2, and 3, for the Scenes: "UIDisplayScene", and for the scene that you intend for the main 
<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;gameplay to take place in
<br>

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
<br>
3.) ANY type of ammunition you plan to use during the game MUST have an AmmunitionTracker added to the Ammunition List in the editor.
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="326" height="73" alt="image" src="https://github.com/user-attachments/assets/742ce188-4969-402a-ae70-d69db2c6f58c" />
<br>
<br>
4.) Please ensure that "Spawnable Sound" is set in the editor. It should be already, but if it is not, it will cause complications. Find more information on Spawnable Sound [here](#spawnable-sound).
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="611" height="132" alt="image" src="https://github.com/user-attachments/assets/bde8a234-7c54-4bb9-ae3d-bbe56115784c" />

<br>
<br>

### Game Instance Audio
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="329" height="290" alt="image" src="https://github.com/user-attachments/assets/77d71e77-b5e0-4078-bca8-e993728cf1a8" />
<br>
* Background music is currently played through the GameInstance.
* Background music will only play if "Plays Music" is checked to true in the editor
* "Music Player" must have an audio source set in the editor in order to play background music. (this should already be set prior to you downloading GameBase)
* This Audio Source does not need to have an Audio Resource set, but it should have "Loop" checked to true
* Different music can be set for different scenes.




<br>
<br>

### Game Instance Additional Information
1.) "Game State" referes to the state that the game will start in. 
<br>
<br>
2.) If the "Respawn Type" is set to "Respawn at Static Location", then there must be a Static Spawn Point present in the main gameplay scene that is configured for the 
player. Place the "Player Static Spawn Point prefab somewhere in the main gameplay scene. Alternatively, create an object in the scene, add the "Static Spawn Point" script
onto the object, and set "Player" as the static spawn tag.
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="301" height="117" alt="image" src="https://github.com/user-attachments/assets/b5a2801c-bc02-42d4-b24c-4f0cb5fe5ad8" />
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="321" height="233" alt="image" src="https://github.com/user-attachments/assets/a3df05c5-d244-4f05-ab9e-5438d7741136" />
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="309" height="65" alt="image" src="https://github.com/user-attachments/assets/ac6323b2-1c70-4157-b2b9-39ad7e2a8088" />
<br>
<br>
3.) The "Death Transition Timer" is the length of time after the Player Character dies that the GameInstance will wait before either respawning the player or transitioning to the LooseScreen. 
This is intended to allow the Player's death animation to play before transitioning scenes.


<br>
<br>






## User Interface
* [Set Up](#setting-up-the-user-interface)
* [Editing User Interface Frontend](#editing-user-interface-frontend)
* [Editing User Interface Backend or Adding UI Elements](#editing-user-interface-backend-or-adding-ui-elements)
* [User Interface Audio](#user-interface-audio)

### Setting Up The User Interface
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NOTE: Do NOT add a User Interface to any scene directly. There is already a User Interface present in the "Base Scene".
<br>
<br>
1.) For the setup of the User Interface, and for a User Interface that is more personalized to your game, it is advised to complete the steps found in [Editing User Interface Frontend](#editing-user-interface-frontend)

<br>

### Editing User Interface Frontend
1.) Go to the User Interface Object: Open "Base Scene", in the hierarchy, the "User Interface" object is located under the "Game Manager" object.
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="239" height="80" alt="image" src="https://github.com/user-attachments/assets/fc486c0c-1dbd-4ae1-9f21-c8f49490e974" />
<br>
<br>
2.) In order to view the current appearance of the User Interface:
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2a.) Ensure view is in "Game" mode and not in "Scene" mode or any other views (ie the animator)
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="256" height="100" alt="image" src="https://github.com/user-attachments/assets/0d79cb99-c5aa-4a66-9dc1-e7a5d4a7ed4d" />
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2b.) Set the "Test Camera" to active in the Inspector
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="455" height="143" alt="image" src="https://github.com/user-attachments/assets/156d7be6-1a42-4dbc-b119-7bb54f8e018c" />
<br>
<br>
3.) To see specific UI screens, those screens will have to be set to active in the inspector, and the "Fade Screen" will have to be set to inactive.
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="383" height="242" alt="image" src="https://github.com/user-attachments/assets/be0a4779-9135-4ef0-9c3e-dcbb20e28501" />
<br>
<br>
4.) It is advised to look through each screen in the UI to ensure that all text, images, arrangements, and other aspects of the display are to your liking.
<br>
<br>
IMPORTANT: It is highly inadvised to delete the following components: 
* "HealthBar" (HUD)
* "LivesText" (HUD)
* "ScoreText" (HUD)
* "EquippedBox" (HUD)
* "EquippedWeaponBox" (HUD)
* "SaveButton" (PauseScreen)
* "LoadButton" (MainMenu)
* "Final Score Text" (WinScreen & LooseScreen)
* "InteractionPromptBox" (InteractionPrompt)
* "InventoryBox" (InventoryScreen)
* "MenuBox" (InventoryMenuScreen)
* "UseButton" (InventoryMenuScreen)
* "EquipButton" (InventoryMenuScreen)
* "DiscardButton" (InventoryMenuScreen)

Deleting any of these components will cause Errors across one or more scripts, including the GameInstance script, UserInterface script, Inventory script, and PlayerCharacter script. If you do not need
or want these components, it is avised to either set them to inactive in the Inspector, or be prepared to make edits to the GameBase scripts. 
<br>
<br>
5.) When you have finished editing the appearance and arrangement of the User Interface, it is highly advised to set the "Fade Screen" to active, all other screens to inactive, and the "Test Camera" to inactive.
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="626" height="311" alt="image" src="https://github.com/user-attachments/assets/903aa565-9078-40a9-a852-fea44beefe36" />

<br>

### Editing User Interface Backend Or Adding UI Elements
Note: When editing User Interface backend (including adding new button functionality, or new screens), it is advised to:
<br>
<br>
  1.) Write Button Click event functions into the "UserInterface" script. From there, the User Interface can trigger functionality elsewhere. (this is advised for simplicity, so that 
the "User Interface" object can function without requiring attatchments and references to unrelated objects.
<br>
<br>
  2.) When editing values at runtime (ie healthbars, scores, etc), do so with functions written in the User Interface. This will allow other scripts to update the UI display as necessary without requiring a reference to anything as the UserInterface can be accessed from anywhere without a reference using: UserInterface.Instance


<br>
<br>



### User Interface Audio
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="324" height="224" alt="image" src="https://github.com/user-attachments/assets/a4255c4a-0618-4e74-aa3b-7f42d1462132" />
<br>
* When "Use Audio" is checked to true, "Button Click Sound" will be played when button is clicked



<br><br>










## Player Character

* [Player Set Up](#player-set-up)
* [Player Controller](#player-controller)
* [Player Health](#player-health)
* [Player Avatar And Animations](#player-avatar-and-animations)
* [Player Audio](#player-audio)

### Player Set Up
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NOTE: Do NOT add a "Player" prefab directly to the scene.
<br>
<br>
1.) Ensure that a "Player Spawn Point" is present in the main gameplay scene. This is where the player will spawn (if not loading a previous save file). This will also allow the player 
to spawn in the correct location when loading a previous save file. The "Game Instance" object will be responsible for spawning the Player Character
<br>
<img width="347" height="290" alt="image" src="https://github.com/user-attachments/assets/96f7830e-4f48-45f6-ae0c-a1cdfcd8eb1d" />
<img width="260" height="224" alt="image" src="https://github.com/user-attachments/assets/732997f9-78ca-44bb-bf66-8c0fb37a5c79" />
<br>
<br>
2.) If you ever plan to have the player holding weapons or items, please see the details listed in [Sockets](#sockets).
<br>
<br>
3.) Add Avatar Masks to the Animator component:
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3a.) Locate Avatar Masks in: Project -> GameBase -> Runtime -> GameBase -> Default Assets -> Masks
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="245" height="267" alt="image" src="https://github.com/user-attachments/assets/07dac400-3e8b-4f96-a6f5-117d5d17aeae" />
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3b.) Drag "LeftArmMask" and "UpperBodyMask" (or a duplicated copy of each) into your "Assets". Dragging the Masks folder (or a duplicate copy of it) will also work.
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="264" height="119" alt="image" src="https://github.com/user-attachments/assets/a4b5d4a1-da4d-4cf6-b885-e3612fce9a15" />
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3c.) Open up the "PlayerCharacter" Animator Controller in: Project -> GameBase -> Runtime -> GameBase -> Default Assets -> Animations -> PlayerCharacter (double click the AnimatorController to open the Animator) 
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="243" height="89" alt="image" src="https://github.com/user-attachments/assets/1bf09b45-6b2d-4e88-9001-4714f790c5b4" />
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3d.) Add "UpperBodyMask" to "OneHandedMeleeLayer": 1 - Click the gear icon next to "OneHandedMeleeLayer" in the Anamator. 2 - Click the dot next to "Mask". 3 - In the pop-up, select the "UpperBodyMask". 4 - Exit the pop-up.
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="420" height="212" alt="image" src="https://github.com/user-attachments/assets/74448e58-a7b0-46a8-8f46-c4b4f808dee0" />
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="548" height="162" alt="image" src="https://github.com/user-attachments/assets/3bd31330-710d-4e26-acf9-4bb55f0260f5" />
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3e.) Repeat step 2d, adding "LeftArmMask" to "DefaultLeftArmLayer" and adding "UpperBodyMask" to "PistolLayer". Do not add an AvatarMask to "MovementLayer". 
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3f.) When you are finished, the layers should look like this (it is recommended to validate that other settings match as well)
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="440" height="164" alt="image" src="https://github.com/user-attachments/assets/afde96fd-2fe1-4e5e-b461-6f2211182404" />
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="437" height="126" alt="image" src="https://github.com/user-attachments/assets/4ffbcde5-6dfe-48a0-99fa-c9098675a5d6" />
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="437" height="131" alt="image" src="https://github.com/user-attachments/assets/8c3c6d2e-4b20-40de-aeb5-ce0287528139" />
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="439" height="115" alt="image" src="https://github.com/user-attachments/assets/c6f4dca8-e075-4e49-8912-436204796727" />







<br>

### Player Controller
Player input, actions, states, and movements are defined here
<br>
<br>
<img width="226" height="329" alt="image" src="https://github.com/user-attachments/assets/bcd23709-3ee7-477c-91f2-f50e156798f0" />
<br>
<br>
Note: If changing the player inputs, please try to match the input types (ie, Composit (2D Vector) -> Composit (2D Vector) || Binding -> Binding), failure to do so could lead to runtime or compiletime errors.
<br>
<br>

To add new player inputs and actions:
<br>
<br>
Use an InputAction to track player inputs and trigger action methods
<br>
<br>
<img width="238" height="24" alt="image" src="https://github.com/user-attachments/assets/4a21dc76-7d56-4959-9904-b36cb3524d0a" />
<br>
<br>

Set up action methods with "InputAction.CallbackContext ctx" as the only parameter
<br>
<br>
<img width="313" height="25" alt="image" src="https://github.com/user-attachments/assets/12da8088-6add-41e5-b6c2-91e6780e57c6" />
<br>
<br>

Bind InputAction to Action Method in the Awake method, Enable InputAction in the OnEnable method, and Disable InputAction in the OnDisable method
<br>
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NOTE: If you intend to use an avatar that is NOT humanoid, you will need to rework the animations
#### To Change Player Avatar:
1.) Configure Avatar's Animation Type to be Humanoid in "Rig"
<br>
<img width="443" height="290" alt="image" src="https://github.com/user-attachments/assets/3e7bb1ec-1f08-475b-9971-53553b50c0f1" />
<br>
<br>
2.) Drag new Model into "Player Character" in the "Player" prefab. (You may need to adjust the Model's position to ensure the model's feet touch the ground)
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="212" height="65" alt="image" src="https://github.com/user-attachments/assets/2bf58f21-6f93-47d3-911e-ccdcf2174f8f" />
<br>
<br>
3.) For each Animation, set "source" to be the avatar of the new model.
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="288" height="199" alt="image" src="https://github.com/user-attachments/assets/3505d3cf-5e7f-43c3-a40a-c1c08cc8b26b" />
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IMPORTANT: You MUST set the new avatar in: "Y Bot@Death", "Y Bot@Idle", "Y Bot@JumpForward", "Y Bot@JumpUp", "Y Bot@PistolAim", "Y Bot@PistolWalk", "Y Bot@Stable Sword Outward Slash", "Y Bot@StandardRun", "Y Bot@Sword And Shield Walk", and "Y Bot@Walking"
<br>
<br>
4.) If you plan to reconfigure or use the Sword or Pistol prefabs that are included with Game Base, you must add a socket to the new character model with ID: PlayerRightHandWeaponSocket
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4a.) Create an empty game object as a child of the the "mixamorig:RightHand"
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="362" height="361" alt="image" src="https://github.com/user-attachments/assets/57c52bb8-29b8-4912-8a1b-dbb41d74403e" />
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4b.) Position the game object in roughly the same position as shown in the following two pictures:
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="469" height="279" alt="image" src="https://github.com/user-attachments/assets/f342a802-c102-43f2-9cbb-3215dd2b8d67" />
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="518" height="239" alt="image" src="https://github.com/user-attachments/assets/c2cc93c0-07ef-459e-9948-10be9f5820f1" />
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4c.) Add a Socket Script, located in Project under: Packages -> Game Base -> Runtime -> Game Base -> Scripts -> Items -> Weapons
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="373" height="335" alt="image" src="https://github.com/user-attachments/assets/eab69efb-c6a4-46a5-8f22-759fcbbb3bef" />
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4d.) In the Inspector, set Socket ID to: PlayerRightHandWeaponSocket
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="335" height="253" alt="image" src="https://github.com/user-attachments/assets/6b68b579-104d-4a1c-b8aa-42113c966bd8" />

<br>
<br>

#### To Change Existing Player Animation(s)
(to add an animation that there is no default for, follow steps 1 and 2 and then add the new animation wherever necessary)
<br>
1.) In the new animation -> Rig, set "Animation Type" to "Humanoid", "Avatar Definition" to "Copy From Other Avatar", and set the Source to your chosen avatar. If using the default avatar, set to the YBotAvatar
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="288" height="199" alt="image" src="https://github.com/user-attachments/assets/df7e6837-b3ec-4ebd-807d-248f2a75a139" />
<br>
<br>
2.) Open the Animator (which can be accessed by double clicking the "Player Character" Animator component
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="194" height="90" alt="image" src="https://github.com/user-attachments/assets/3355f4a3-c252-45e1-9b3a-6a53b7ed059b" />
<br>
<br>
3.) Locate and click the state you want to change. Then, drag new animation into the "Motion" feild of the state. (idle, walk, and run animations are located in the "Movement" Blend Tree)
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="296" height="77" alt="image" src="https://github.com/user-attachments/assets/712f3bec-bf02-4d2f-929b-f14ec83435bc" />

<br>
<br>

#### When Adding Animation Layers
IMPORTANT: If you add one or more layers to the "PlayerCharacter" Animator Controller, it is advised to then open the "PlayerController" script, and find the methods: "SetWeapon", "OnDeath", and "OnRespawn". 
These methods each set layer weights for the animator. It is advised to make sure that layer weights are still being assigned properly. Consider whether adding your own layers to these methods or altering the methods to prevent conflict would be benefiscial to your game. Prior to modifications:
* LayerIndex 0 corresponds to the "MovementLayer"
* LayerIndex 1 corresponds to the "OneHandedMeleeLayer"
* LayerIndex 2 corresponds to the "DefaultLeftArmLayer"
* LayerIndex 3 corresponds to the "PistolLayer"
  <br>
And the scripts look like this:
<br>
<img width="221" height="332" alt="image" src="https://github.com/user-attachments/assets/e8913639-a026-4090-8302-5ce94dfe3742" />
<img width="188" height="148" alt="image" src="https://github.com/user-attachments/assets/61bcd539-71ca-48d1-8394-cb1cea5da9d6" />
<img width="185" height="96" alt="image" src="https://github.com/user-attachments/assets/627f191f-e135-4326-8723-f26dd45eb8ed" />

<br>
<br>



### Player Audio
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="316" height="83" alt="image" src="https://github.com/user-attachments/assets/fe02c5fc-6e4f-4d4b-9fec-629a1a479b18" />
<br>
* Sound effects for the player being damaged and for the player dying can both be set here.
* Here you can also toggle whether the audio will play for both





<br>
<br>







## Main Camera
Features a First Person Camera and Third Person Camera that has a defined target. The Third Person Camera will auto-adjust its distance from the target to keep the target in view if another object would otherwise block it from view.

* [Main Camera Set Up](#main-camera-set-up)
* [First Person Vs Third Person](#first-person-vs-third-person)
* [Telling Camera To Ignore Objects](#telling-camera-to-ignore-objects)

### Main Camera Set Up
To add a MainCamera to the scene, add the MainCamera script to a camera object, and add the Transform of the target in the Universal Camera Settings section of the script in the editor.
<br>
<br>
<img width="207" height="32" alt="image" src="https://github.com/user-attachments/assets/35876392-80d7-4230-8a55-8fc7e1ceba20" />

<br>

### First Person Vs Third Person
Change from First Person Camera to Third Person Camera (or from Third Person Camera to First Person Camera) in the Universal Camera Settings section of the script in the editor.
<br>
<br>
<img width="214" height="22" alt="image" src="https://github.com/user-attachments/assets/c9c00fcb-67e9-411a-aad0-b63b58a925c8" />
<br>

* Camera adjustments to follow the target are defined in the MainCamera script
* Player movement based on camera direction is defined in the PlayerController script

<br>

### Telling Camera To Ignore Objects
The third person camera is able to adjust its distance from the target to keep the target in view when an object passes in between the camera and the target. 
However, at times you may want to tell the camera to ignore certain objects, such as small items, and items that aren't visible. To do this:
<br>
<br>
1.) Add a "TagManager" component to the object you want the camera to ignore. The "TagManager" script can be found in: GameBase -> Scripts -> Game
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="289" height="350" alt="image" src="https://github.com/user-attachments/assets/c5ee7ebd-e16b-4e48-a6f9-ff560f308ff8" />
<br>
<br>
2.) Add an&nbsp;&nbsp; IgnoredByOrbitalCamera &nbsp;&nbsp;tag to the list of tags (without any white-space). This will tell the camera to disregard this object while running its distance adjustment calculations.
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="332" height="119" alt="image" src="https://github.com/user-attachments/assets/44cb9e0f-8f00-4529-8178-8e281706c781" />
<br>
<br>
[Additional information about the TagManager](#tag-manager)



<br>
<br>


## Health and Damage System
* [Health And Damage System Set Up](#health-and-damage-system-set-up)

### Health And Damage System Set Up
To create a source of damage, add a Damage Source script to an object with a collider set to trigger.
<br>
<br>
<img width="296" height="310" alt="image" src="https://github.com/user-attachments/assets/6b52ef9e-8016-4365-a592-a360040b8a7e" />
<br>

If you want an object to be damageable through the Health and Damage System, that object must have a component that includes the IDamagableInterface. This
indicates to Damage Sources that an object can receive damage.
<br>
<br>
<img width="371" height="41" alt="image" src="https://github.com/user-attachments/assets/95268ee9-2125-4dcb-8d95-14b474737e36" />
<br>

NOTE: There is a Health component as well. This component is optional, but can quickly privode basic health functionality. The Health script, DamageSource script, and IDamagableInterface script can all be found in: GameBase -> Scripts -> Damage and Health
<br>
<br>
<img width="288" height="335" alt="image" src="https://github.com/user-attachments/assets/a4611672-1185-4769-b0c3-21bc6e962d52" />



<br>
<br>




## Save System

* [Add To Scene](#add-to-scene)
* [Save Data From Objects](#save-data-from-objects)
* [File Data Handler](#file-data-handler)
* [Encryption and Decryption](#encryption-and-decryption)

### Add To Scene
1.) Do NOT add the Data Persistence Manager to a scene directly. There is already a Data Persistence Manager present in the "Base Scene".
<br>
<br>
2.) Open the "Base Scene". Ensure that at least one save condition is selected, and at least one load condition is selected on either the "Data Persistence Manager" script located in the Data Persistence Manager (prefab or the object that is a child to the Game Instance object) and/or the "Game Instance" script on the "Game Instance" object.
<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="602" height="320" alt="image" src="https://github.com/user-attachments/assets/1974126d-0d44-4249-8955-13a0b977b276" />
<br>
<br>
These are the alternative save and load conditions present in the "Game Instance" script.
<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="293" height="114" alt="image" src="https://github.com/user-attachments/assets/05946424-7be5-4e83-811f-c203aeedb787" />

<br>

### Save Data From Objects
1.) Ensure that there is a SerializableDictionary in the GameData object with the correct value type for the data you are planning to save.
<br>
<br>
<img width="575" height="190" alt="image" src="https://github.com/user-attachments/assets/fd7a5abe-7adf-461e-ad9c-1dc81c0920cb" />
<br>
<br>
2.) To save the data in any class, ensure that the IDataPersistence interface is added to the class, or that save and load functions can be called through another script with the IDataPersistence interface.
<br>
<br>
<img width="404" height="28" alt="image" src="https://github.com/user-attachments/assets/189e12fb-65fa-4216-86c8-3b33b8111fc2" />
<br>

3.) In the SaveData method (required by IDataInterface), check the SerializableDictionary with the correct value type in the GameData object for your desired key for the
value. If the key already exists, update it's value, otherwise add and new key/value pair with your desired key and value.
<br>
<br>
This is the suggested way to structure this:
<br>
<br>
<img width="500" height="101" alt="image" src="https://github.com/user-attachments/assets/4a33d002-298e-4566-8bef-c984a14e7539" />
<br>

4.) In the LoadData method (required by IDataInterface), check the SerializableDictionary with the correct value type in the GameData object for the key of the data you want to load.
If the key exists, load data from the SerializableDictionary using the key.
<br>
<br>
This is the suggested way to structure this:
<br>
<br>
<img width="386" height="56" alt="image" src="https://github.com/user-attachments/assets/fbb061cc-3668-400c-8446-aa5924e09c34" />
<br>
<br>
Note: If you plan to have more than one instance of a savable object, it is highly advised to use an ID system, with the ID added into each key when saving and loading, as the examples above and the example below show.
<br>
<br>
<img width="275" height="47" alt="image" src="https://github.com/user-attachments/assets/83e61e6e-3b71-45b2-8d40-deb17c13a806" />

<br>

### Encryption and Decryption
Note: If you check or uncheck "Use Encryption" while you have an existing save file, and then run the game, and error will be thrown the first time the game tries to load the file, however a new save file will be made, and the game will function as normal.
The same thing happens if you change the Encryption Code Word when you have an existing save file.
<br>
<br>
Encryption and decryption is handled by the File Data Handler. If you want additional security, you are encouraged to edit the method of encryption, however, you are advised to only do this if you are familiar with the mechanics of data encryption AND decryption.

<br>

### File Data Handler
Modifications to the Data Serialization/Deserialization and Encryption/Decryption or to the File Read/Write system must be made in the FileDataHandler. It is advised not to do this if you are not familiar JSON

<br>

### Save Files
If you want to change the location that save files are saved to, the file path can be specified in the DataPersistenceManager script in the Start method here:
<br>
<br>
<img width="659" height="157" alt="image" src="https://github.com/user-attachments/assets/32617888-8a8f-492b-8f6a-89f8b751b089" />
<br>
<br>
If you have not changed the file path, and you need to delete to open the save file directly at any time, you may be able to find it in your File Explorer in: This PC -> Windows(C:) -> Users -> [You] -> AppData -> LocalLow -> DefaultCompany -> [YourProjectName]
<br>
<br>
If the file is not here, you are not a Windows user, or you have a different file structure, then I apologize that I do not know where your save file will be located.

<br>
<br>


## Items
* [Included Prefabs](#included-item-prefabs)
* [Create New Item](#creating-a-new-item)

### Included Item Prefabs
* Basic Health Recovery Item
* Basic Score Increase Item
* Basic Health Upgrade
* Savable Health Recovery Item
* Savable Score Increase Item
* Savable Health Upgrade
* Inventory Health Recovery Item

<br>
  
### Creating A New Item
1.) Make your item script a child of one of the abstract item scripts (or a child of an item script that derives from an abstact item script). For items that should appear in the inventory, use the "InventoryItem" script (these items will be able to save as well). For items that should be saved and loaded, use the "SavableItem" script. Otherwise, use the "ItemBase" script.
<br><br>
  1a.) Non-Saving:
  <br><br>
  <img width="242" height="35" alt="image" src="https://github.com/user-attachments/assets/9942ab15-3811-4c2b-a1ed-f7cc381d4994" />
  <br><br>
  1b.) Saving:
  <br><br>
  <img width="240" height="24" alt="image" src="https://github.com/user-attachments/assets/b23cda58-30e9-4b78-9331-190c53b7e246" />
  <br><br>
  1c.) Inventory:
  <br><br>
  <img width="244" height="26" alt="image" src="https://github.com/user-attachments/assets/0a08e473-f43d-4189-91d5-5afb53595205" />
<br><br>
2.) When setting up the new item, ensure that the object has a Collider component, and that the Collider is set to "Trigger
<br><br>
<img width="294" height="99" alt="image" src="https://github.com/user-attachments/assets/2de013bb-d9df-4099-99a8-7310a2250dc6" />
<br><br>
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NOTE: The Inventory is already located in the 'BaseScene' as a part of the 'GameInstance' object, and does not need to be added to any scene.
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="545" height="284" alt="image" src="https://github.com/user-attachments/assets/cb8c7a3a-66ce-4fd1-a9ae-ab303380f68a" />
<br><br>
#### To use the Inventory System:
1.) Ensure that "Use Inventory" is checked to "true"
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="212" height="97" alt="image" src="https://github.com/user-attachments/assets/79d72d29-2f52-48e8-9d1c-5e2e9eb6f215" />
<br><br>
2.) (Optional) If you want items to be automatically equipped when they are picked up and nothing else is eqiupped, ensure that "Send First Item To Equipped" is set to "true". Otherwise
set it to "false.
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="182" height="98" alt="image" src="https://github.com/user-attachments/assets/2011e86c-42f3-4f8d-a60d-d12be9a75341" />
<br><br>
3.) Ensure that "Equipped Item Box" and "Equipped Weapon Box" are NOT empty.
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="299" height="40" alt="image" src="https://github.com/user-attachments/assets/1a9d4982-bfe2-4349-a390-1ba3933a8230" />
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3a.) If either ARE empty, then go to "User Interface" -> "HUD", and find the "Equipped Item Box" and/or "Equipped Weapon Box" located in the HUD. The click and drag "Equipped Item Box" object and/or "Equipped Weapon Box" object from the hierarchy into "Equipped Item Box" and/or "Equipped Weapon Box" in the "Inventory" script.
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="623" height="215" alt="image" src="https://github.com/user-attachments/assets/a66d065b-d979-476a-9bf5-cade062270e4" />
<br><br>
4.) (Optional) If you want the items in the inventory to save and load, follow the steps outlined [here](#saving-inventory-items)
<br><br>
5.) (Optional) The "EquippedItemBox" will display the key that when pressed will use the equipped item, as shown below.
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="75" height="68" alt="image" src="https://github.com/user-attachments/assets/417ac1eb-869c-45db-88b7-dcc7c53b2a4c" />
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5a.) The default for this key is "E". To change this key, go to "UserInterface" -> "HUD" -> "EquippedBox" and in the "EquippedItemBox" script, change the "Use Key".
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="543" height="200" alt="image" src="https://github.com/user-attachments/assets/066cd9a7-72de-4dd6-b391-4148260848eb" />
<br><br>
6.) (Optional) This is the default layout and size of the Inventory when displayed in the Inventory Screen:
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="742" height="506" alt="image" src="https://github.com/user-attachments/assets/139a27fe-6402-4a5b-95da-3603d0b3ecdd" />
<br><br>
To adjust the size of the Inventory, or the dimensions or proportions of the layout, follow the steps outlined [here](#adjusting-inventory-system)

<br>
  
### Adjusting Inventory Screen
IMPORTANT TO NOTE: The Inventory Box is generated _at runtime!_ Adjusting the size of the "InventoryBox" will not persist at runtime, nor will any size adjustments made to the "InventoryItemBox". However, _location_ adjustments may be made to the "InventoryBox", and these adjustments will persist.
<br><br>
1.) Go to "User Interface", then find the "Inventory Screen" section of the "User Interface" script.
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="283" height="215" alt="image" src="https://github.com/user-attachments/assets/45acef7d-84b6-4e0e-bd93-854c2bf7d0b4" />
<br><br>
  1a.) "Image Box Width" and "Image Box Height" can be used to adjust the size of the "InventoryItemBox" that are generated.
  <br><br>
  1b.) "Rows" and "Columns" can be used to alter the number of "InventoryItemBox" that are generated, as well as their layout.
  <br><br>
  1c.) "Margin" is the distance in between each "InventoryItemBox"
  <br><br>
  1d.) "Padding" is the distance from the edges of the "InventoryBox" to the nearest "InventoryItemBoxes"
<br><br>

### Inventory Items
1.) To create an inventory item, first follow the steps located [here](#creating-a-new-item), then configure based on the following information.
<br><br>
2.) "InventoryItem" is a child class of "SavableItem", and as such shares all of the same properties in the editor. In addition to these properties, are the following:
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="281" height="122" alt="image" src="https://github.com/user-attachments/assets/2029879a-2d63-4712-a173-345347f6d98e" />
<br><br>
2a.) "Inventory ID" is a failsafe method to tell two distinct items appart. This MUST be unique if _both_: 
  * Two or more items have all of the same properties but have different sprites
  * You do NOT want these items to stack. (stacking in this scenario will only display the sprite of the most recent item that was picked up)
  <br><br>
  2b.) "Inventory Sprite" if the image that will be displayed in the "InventoryScreen" and in the "EquippedItemBox" for this item. This must be an actual sprite, as opposed to a .JPEG, a .PNG or a material.
  <br><br>
  2c.) "Use From Inventory" - If set to "true" this item can be used directly from the "InventoryScreen" without equipping it first. If set to false, item must first be equipped before being used.
<br><br>
  2d.) "Equippable" - If set to "true" this item can be equipped. If set to false, this item cannot be equipped from the menu, and will not auto-equip even if the "Send First Item To Equipped" option is set to "true"
  in the Inventory.
<br><br>
  2e.) "Removable" - If set to "true" this item can be removed from the inventory without using the item, effectively discarding the item.
<br><br>
  2f.) "Consume After Use" - If set to "true" then using this item will reduce the number of this item in the inventory by one. If set to "false" then this item can be used infinitely from the inventory.
<br><br>
  2g.) "Stack Instances In Inventory" - If set to "true" then multiple instances of this item can be stored together and will only take up one Inventory Slot. For full instructions on setting up Item Stacking in Inventory, see the instructions [here](#stacking-inventory-items)
<br><br>

#### Stacking Inventory Items
When an item stacks in the inventory, that means that multiple instances of this item can be stored together and will only take up one Inventory Slot. There are a few requirements that must be met in order for items to stack in the inventory:
<br><br>
1.) In the "Inventory Item Information" on that object's script, "Stack Instances In Inventory" must be set to true.
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="137" height="128" alt="image" src="https://github.com/user-attachments/assets/e6efdd3d-7c0c-4f57-a440-01c449b7e556" />
<br><br>
2.) All other settings under "Inventory Item Information" must be identical between instances of an item you want to stack (with the exception of the "Inventory Sprite"). These settings are: "Inventory ID", "Use From Inventory", "Equippable", "Removable" and "Consume After Use". Any differences will result in the item NOT stacking.
<br><br>
3.) The "Name" setting in the "Basic Item Information" section must be the same between instances of the item you want to stack.
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="222" height="32" alt="image" src="https://github.com/user-attachments/assets/b9d48d35-2e50-449d-8d42-6ec08b941d0c" />
<br><br>

### Saving Inventory Items
For an item to be saved once it has been added to the inventory certain steps MUST be taken:
<br><br>
1.) The item must be made into a prefab.
<br><br>
2.) Go to "BaseScene" -> "Game Instance" -> "Inventory". In the Inventory script, find the "Savable Inventory Items" property.
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="181" height="151" alt="image" src="https://github.com/user-attachments/assets/201027e7-129c-466f-9973-9903f3ee1c9d" />
<br><br>
3.) Add the prefab of the item you want to be saved to this list. This can be done by clicking and dragging the prefab onto the list from the Project Folder.
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="544" height="163" alt="image" src="https://github.com/user-attachments/assets/9d2f2a65-d083-474c-8496-779557745323" />



<br>
<br>



## Sound
* [Background Music](#background-music)
* [Player Audio](#player-audio)
* [User Interface Audio](#ui-audio)

<br>

### Background Music
[Information on Background Music can be found here](#game-instance-audio)

### Player Audio
[Information about Player Related Audio can be found here](#player-audio)

### UI Audio
[Information about User Interface Audio can be found here](#user-interface-audio)

<br>
<br>



## Weapons
* [Included Weapon Prefabs](#included-weapon-prefabs)
* [Create New Weapon](#create-new-weapon)
* [Melee Weapons](#melee-weapons)
* [Ranged Weapons](#ranged-weapons)



### Included Weapon Prefabs
* Pistol
* PistolWeaponPickup
* Sword
* SwordWeaponPickup

<br>

### Create New Weapon
1.) First create a weapon. Game Base includes a base for [melee weapons](#melee-weapons) as well as [ranged weapons](#ranged-weapons)
<br>
<br>
2.) Create a "Weapon Item". This will be used to track the weapon in the inventory. This item will use the "WeaponItem" script (or a child script).
<br><br>
3.) The weapon should be a child object to the Weapon Item, and it's mesh should be set as the Weapon Item's mesh in the editor.
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<<img width="611" height="225" alt="image" src="https://github.com/user-attachments/assets/cd23d23f-c707-4515-b4ba-3b05744e0c9a" />
<br><br>
4.) The weapon must be added to the Weapon Item script in the editor as well
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<<img width="608" height="437" alt="image" src="https://github.com/user-attachments/assets/09e4a8da-26b9-4797-80cf-f28d89275505" />
<br><br>
5.) It is highly recommended for weapons to implement the [steps outlined here](#telling-camera-to-ignore-objects)
<br><br>
6.) The AnchorPointIndicator is by no means required, but it can be very helpful for seeing where the weapon will anchor to the socket.
<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img width="611" height="258" alt="image" src="https://github.com/user-attachments/assets/57f3cbb3-35fa-4051-93f1-53607dde3c13" />


<br>



### Melee Weapons
1.) To create a new melee weapon, a script must be made for that weapon that uses "MeleeWeapon" as a parent class. Alternatively, the sword script may be used and/or modified.
<br><br>
2.) A hitbox with a "DamageSource" component and with a hitbox set to "Trigger" must be part of the weapon
<br><br>
<img width="611" height="467" alt="image" src="https://github.com/user-attachments/assets/712e360b-09f1-41a8-a774-5c299e82d2d6" />
<br><br>
3.) Both the collider and the DamageSource must be added to the Melee Weapon's script in the editor
<br><br>
<img width="617" height="380" alt="image" src="https://github.com/user-attachments/assets/f8aec798-d18d-4520-be2f-dcff77742c77" />
<br><br>
4.) The "Weapon Name" is the name that will be displayed for the weapon in the UI both in the Inventory, and in the Equipped Item Box
<br><br>
<img width="317" height="55" alt="image" src="https://github.com/user-attachments/assets/e0a0afd1-50b9-464b-9b06-6f7b9e4e1661" />
<br><br>
5.) A "Socket Name" MUST be added, otherwise the weapon will not be able to be equipped. It must correspond to a weapon socket present somewhere on the Player Character. For more information, see [socket details here](#socket). If you do not want to configure a new socket, use the socket name: PlayerRightHandWeaponSocket
<br><br>
<img width="310" height="64" alt="image" src="https://github.com/user-attachments/assets/2f26aebc-626d-4314-9430-0c20dc5f6cfa" />
<br><br>
6.) The "Attack Duration" is the length of time in seconds in which the hitbox will be active upon attacking.
<br><br>
<img width="323" height="98" alt="image" src="https://github.com/user-attachments/assets/30c1801f-055c-441a-8d07-4181f22b2c7d" />
<br><br>

### Ranged Weapons





<br>
<br>


## Ammunition
* [Included Ammunition Prefabs](#included-ammunition-prefabs)
* [Creating Ammunition](#creating-ammunition)
* [Ammunition Types](#ammunition-types)
* [Ammunition Trackers](#ammunition-trackers)
* [Ammunition Refills](#ammunition-refills)


### Included Ammunition Prefabs
* Bullet
* BulletAmmunitionType
* BulletAmmunitionTracker
* BulletAmmunitionRefill

### Creating Ammunition
Ammunition should likely have a DamageSource component, and a DamagingProjectile component. However, if this is an ammunition is NOT intended for the included RangedWeapon script, these two things are not strictly required. (they ARE required if you are using them with a weapon made with the RangedWeaponScript). It is also advised to follow the [steps outlined here](#telling-camera-to-ignore-objects). 
<br><br>
Here is an example of a configured ammunition:
<br>
<img width="335" height="560" alt="image" src="https://github.com/user-attachments/assets/ab04f95a-becd-4354-8602-79d6cb07e1a5" />
<br><br>

### Ammunition Types
An Ammunition Type object only requires an AmmunitionType component, with the name of the ammuntion and a reference to the Ammunition Prefab that this AmmunitionType will represent. This IS required for full ammunition functionality. Here is an example:
<br><br>
<img width="614" height="182" alt="image" src="https://github.com/user-attachments/assets/7644fd21-ffcd-46bb-9275-fdbb5add7ecd" />
<br><br>


### Ammunition Trackers


### Ammunition Refills









<br>
<br>

## Miscellaneous
* [Sockets](#sockets)
* [Spawnable Sound](#spawnable-sound)
* [Tag Manager](#tag-manager)
### Sockets
A socket only includes an ID, and was developed with the intention of being used as a way for other objects to know where to "be". 
<br>
By setting a socket as a parent transform of an object, that object will now follow the socket whenever its transform changes. 
<br>
And by setting another object (ie a bone in a character rig) as the parent of the socket, both the socket and first object will follow the second object. 
<br>
As an example, this could be very useful for having a player or npc "hold" things such as weapons or tools.
<br>
Objects can be assigned to sockets at runtime, and the ID can allow objects to ensure they are being assigned to the correct socket.
<br>
Currently, sockets are only being used to enable equipping of weapons, but please feel free to be as creative as you'd like!

<br>
<br>

### Spawnable Sound
A spawnable sound is an object intended to be spawned in a specific location in order to play a specific sound.
<br>
This can be helpful when, for example, you want an object to play a sound when it is destroyed. 
<br>
If the object itself were to play the sound, the sound would never actually play because the object playing it would be destroyed at the same time.
<br>
Spawning the sound as a seperate object circumvents this issue.
<br>
A sound can most easily be spawned using the "SpawnSoundAtLocation" method built into the GameInstance, using the syntax:
<br>
<br>
GameInstance.Instance.SpawnSoundAtLocation(audio clip, location);
<br>
<br>
This function will also return the spawnable sound object if you need a reference to it for any reason.

<br>
<br>

### Tag Manager
The tag manager allows an object to have multiple tags.
<br>
This can have a number of applications and offer additional flexibility when trying to communicate between scripts.
<br>
One example is that the TagManager componenet is used to tell the camera when an object is allowed to pass between the camera and the player.
<br>
The TagManager script can search its list of tags for a specific tag (string), or other scripts can access the list directly.
<br>
The TagManager can also Add or Remove tags at runtime, and can save and load its list of tags in the even that runtime changes need to be persistent.


<br>
<br>



# Art Credits:
* Credit to Mixamo for the default player model and default player animations!


