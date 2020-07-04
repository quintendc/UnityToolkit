using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum WidgetTypes
{
    Default,    // fall-back never use this state
    MainMenu,   // main menu
    Loading,    // loading screen
    Pause,      // pause screen
    Settings,   // settings menu
    HUD,        // HUD
    GameOver,   // GameOver / end of round
}

public enum InputState
{
    Default,    // fall-back never use this state
    Pawn,       // send input from playerController to pawn
    Widget      // send input from playerController to Widgets
}
