using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum WidgetTypes
{
    Default,    // fall-back never use this state
    MainMenu,
    Loading,
    Pause,
    Settings,
}

public enum InputState
{
    Default,    // fall-back never use this state
    Pawn,       // send input from playerController to pawn
    Widget      // send input from playerController to Widgets
}
