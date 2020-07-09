using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class ToolkitSettings
{

    [Tooltip("Ässign Widgets to this list where you can call the PauseGame method, always include your PauseScreen")]
    public List<AWidget> PauseGameEnabledWidgets = new List<AWidget>();

}