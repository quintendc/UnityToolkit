using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public static class GameState
{

    public static bool Paused = false;
    public static List<PlayerInput> Players = new List<PlayerInput>();
    public static InputState InputState = InputState.Default;
    public static Scene LastLoadedAdditiveScene;
    public static float LoadingProgress;
    public static bool Saving;
}
