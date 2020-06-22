using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBinder : MonoBehaviour
{
    [Tooltip("Set Text")]
    public Text Target = null;

    [Tooltip("read property")]
    public Selectable Source = null;


    #region Type Methods

    /// <summary>
    /// method for slider
    /// </summary>
    /// <param name="value">value delivered by the Slider</param>
    public void SliderChanged(float value)
    {
        Target.text = value.ToString();
    }

    /// <summary>
    /// method for InputField
    /// </summary>
    /// <param name="text">text delivered by the InputField/param>
    public void OnTextChanged(string text)
    {
        Target.text = text;
    }

    #endregion



}