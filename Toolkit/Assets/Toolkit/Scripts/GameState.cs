using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class GameState
{

    public static bool Paused = false;
    public static List<Player> Players = new List<Player>();
    public static InputState InputState = InputState.Default;

}
