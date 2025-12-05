using UnityEngine;

namespace GameBase
{
    //Defintes which part of the Game Cycle the game is currently in
    public enum GameState
    {
        LOADTITLE,
        TITLESCREEN,
        LOADMAINMENU,
        MAINMENUSCREEN,
        STARTGAME,
        LOADGAME,
        LOADSAVE,
        PLAYGAME,
        WINGAME,
        LOSEGAME,
        WINSCREEN,
        LOSESCREEN
    }

    //Defines how the player respawns
    public enum RespawnType
    {
        RESPAWNINPLACE,                 //respawns player in place
        LOADLASTSAVE,                   //reloads save file, and respawns player in the process
        RESPAWNATSAVELOCATION,          //respawns player at location of last loaded save without reloading game or save
        RESPAWNATSTATICLOCATION         //respawns at a set location without loading any saved data
    }

    //Defines how the player restarts
    public enum RestartMode
    {
        RESTARTFROMLASTSAVE,    //after loosing, player will restart at last save point
        RESTARTFROMBEGINNING      //after loosing, player must restart from the beginning
    }

}
