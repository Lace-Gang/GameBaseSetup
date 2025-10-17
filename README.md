# GameBaseSetup
This is where I will be creating my capstone and running some of my testing for it.

## Contents
* Save System

## Instilation Instructions
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


## How To Use
[To Be Writen]


## User Guide

### Save System
#### Add To Scene
To set up save system, drag DataPersistenceManager from the Prefabs folder into the scene hierarchy

<br>
<img width="274" height="350" alt="image" src="https://github.com/user-attachments/assets/d7678828-4aed-40cd-a895-1c22a49ffb99" />
<img width="274" height="155" alt="image" src="https://github.com/user-attachments/assets/b128d6bf-b5e4-4f81-9902-5764b21c7873" />

Ensure that at least one save condition is selected, and at least one load condition is selected.
<br>
<img width="179" height="78" alt="image" src="https://github.com/user-attachments/assets/b523a8e3-d41f-44c1-a13d-ad70fd449cbd" />



### Player Character
#### Set Up
To set up save system, drag the Player from the Prefabs folder into the scene hierarchy
<br>
<img width="308" height="171" alt="image" src="https://github.com/user-attachments/assets/2835c58b-6b44-446d-a4de-a6e5435a001b" />

A player will be spawned in the scene
<img width="381" height="241" alt="image" src="https://github.com/user-attachments/assets/792cae22-0de0-46fc-b78a-c2bb4348328f" />



#### Player Controller
Player input, actions, states, and movements are defined here
<img width="226" height="329" alt="image" src="https://github.com/user-attachments/assets/bcd23709-3ee7-477c-91f2-f50e156798f0" />
Note: If changing the player inputs, please try to match the input types (ie, Composit (2D Vector) -> Composit (2D Vector) || Binding -> Binding), failure to do so could lead to runtime or compiletime errors.

To add new player inputs and actions:
Use an InputAction to track player inputs and trigger action methods
<br>
<img width="238" height="24" alt="image" src="https://github.com/user-attachments/assets/4a21dc76-7d56-4959-9904-b36cb3524d0a" />

Set up action methods with "InputAction.CallbackContext ctx" as the only parameter
<br>
<img width="313" height="25" alt="image" src="https://github.com/user-attachments/assets/12da8088-6add-41e5-b6c2-91e6780e57c6" />

Bind InputAction to Action Method in the Awake method, Enable InputAction in the OnEnable method, and Disable InputAction in the OnDisable method
<br>
<img width="233" height="472" alt="image" src="https://github.com/user-attachments/assets/88847128-e147-45c9-b3b1-35e0763536f1" />





### Main Camera
Features a First Person Camera and Third Person Camera that has a defined target. The Third Person Camera will auto-adjust its distance from the target to keep the target in view if another object would otherwise block it from view.

#### Set Up
To add a MainCamera to the scene add a player character from the prefab folder (a MainCamera is included in the Player prefab with the PlayerCharacter as the target)
Camera Settings can be edited on the Main Camera object.
<br>
<img width="308" height="171" alt="image" src="https://github.com/user-attachments/assets/2835c58b-6b44-446d-a4de-a6e5435a001b" />
<img width="466" height="258" alt="image" src="https://github.com/user-attachments/assets/9f988cc0-0ce6-4907-9070-98155fe3efbc" />

Alternatively, add the MainCamera script to a camera object, and add the Transform of the target in the Universal Camera Settings section of the script in the editor.
<br>
<img width="207" height="32" alt="image" src="https://github.com/user-attachments/assets/35876392-80d7-4230-8a55-8fc7e1ceba20" />


#### First Person / Third Person
Change from First Person Camera to Third Person Camera (or from Third Person Camera to First Person Camera) in the Universal Camera Settings section of the script in the editor.
<br>
<img width="214" height="22" alt="image" src="https://github.com/user-attachments/assets/c9c00fcb-67e9-411a-aad0-b63b58a925c8" />

* Camera adjustments to follow the target are defined in the MainCamera script
* Player movement based on camera direction is defined in the PlayerController script





#### Save Data From Objects
In the GameData Script, add a public variable for each peice of data in your game you want to save. It is recommended but not required to remove values from GameData if you
will not be saving those values (this will save space in the save file).
Note: if you want any data to be loaded from an unused save file, that will have to be defined here.
<br>
<img width="244" height="185" alt="image" src="https://github.com/user-attachments/assets/0d22bdf9-ebe9-487e-bfeb-909ca59555a7" />

To save the any data in any class, ensure that the IDataInterface is added to the class.
<br>
<img width="404" height="28" alt="image" src="https://github.com/user-attachments/assets/189e12fb-65fa-4216-86c8-3b33b8111fc2" />

In the SaveData method (required by IDataInterface), update any values in the GameData object that are associated with this class
<br>
<img width="410" height="95" alt="image" src="https://github.com/user-attachments/assets/4ead06a5-6d05-4576-8ec9-21cac77e359f" />

In the LoadData method (required by IDataInterface), load any values from the GameData object that are associated with this class
<br>
<img width="392" height="150" alt="image" src="https://github.com/user-attachments/assets/205695e4-b0a9-4356-9abd-7026957722b7" />
Note: "if(!data.isNewSave)" allows savable data to be adjusted in the editor rather than the GameData object
<br>
<img width="331" height="34" alt="image" src="https://github.com/user-attachments/assets/25f688e0-6ac6-4a06-a11d-7387bd45386e" />



#### File Data Handler
Modifications to the Data Serialization/Deserialization and Encryption/Decryption or to the File Read/Write system must be made in the FileDataHandler. It is advised not to do this if you are not familiar JSON



