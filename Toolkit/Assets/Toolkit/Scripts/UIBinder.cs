using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBinder : MonoBehaviour
{
    [Tooltip("Set Text")]
    public Text Target = null;

    [Header("Source")]
    [Tooltip("select the type of source")]
    public SourceType SourceType;

    [Tooltip("read property")]
    public Selectable Source = null;

    private void Update()
    {
        //switch (SourceType)
        //{
        //    case SourceType.Slider:
        //        Target.text = (Slider)Source.Value
        //        break;
        //    case SourceType.CheckBox:
        //        break;
        //    case SourceType.InputField:
        //        break;
        //    default:
        //        break;
        //}
    }


    public void SliderChanged(float value)
    {
        Target.text = value.ToString();
    }


}

public enum SourceType
{
    Slider,
    CheckBox,
    InputField
}