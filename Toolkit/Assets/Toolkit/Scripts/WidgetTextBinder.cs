using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

[Serializable]
public class WidgetTextBinder
{
    [Tooltip("Drag & Drop a Text Object into this field")]
    public Text Text = null;
    [Tooltip("Enter a Variable name from the Widget Script you want to bind to")]
    public string VariableName;    
}
