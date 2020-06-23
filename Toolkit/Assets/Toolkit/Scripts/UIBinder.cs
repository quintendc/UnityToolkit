using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBinder : MonoBehaviour
{
    [Tooltip("Set Text")]
    public Text Target = null;

    [Tooltip("read property")]
    public Selectable Source = null;

    public UnityEvent GetTextEvent;

    #region Type Methods

    private void Start()
    {
        //GetTextEvent.AddListener(GetText);
    }


    /// <summary>
    /// method for slider
    /// </summary>
    /// <param name="value">value delivered by the Slider</param>
    public void OnSliderChanged(float value)
    {
        Target.text = value.ToString();
    }

    /// <summary>
    /// method for InputField
    /// </summary>
    /// <param name="text">text delivered by the InputField/param>
    public void OnInputFieldChanged(string text)
    {
        Target.text = text;
    }


    public void OnCheckboxChanged(bool check)
    {
        if (check == true)
        {
            Target.text = "checked";
        }
        else
        {
            Target.text = "unchecked";
        }
    }


    //public void GetText()
    //{
    //    Target.text = Random.Range(0, 1000).ToString();
    //}

    //private void Update()
    //{
    //    GetText();
    //}

    #endregion



}