using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;

public class CameraShake : ToolkitBehaviour
{

    #region public properties

    [Header("Default settings")]
    public float DefaultDuration = 0.15f;
    public float DefaultMagnitude = 4f;

    [Header("Default shake axis")]
    public bool ShakeX = true;
    public bool ShakeY = true;
    public bool ShakeZ = false;


    #endregion

    /// <summary>
    /// shake the Toolkit camera
    /// </summary>
    /// <param name="overrideDefaultDuration">duration of the shake in seconds</param>
    /// <param name="overrideDefaultMagnitude">the magnitude of the shake</param>
    /// <param name="overrideShakeX">override default camers X Axis setting</param>
    /// <param name="overrideShakeY">override default camers Y Axis setting</param>
    /// <param name="overrideShakeZ">override default camers Z Axis setting</param>
    public IEnumerator Shake(float? overrideDefaultDuration = null, float? overrideDefaultMagnitude = null, bool overrideShakeX = true, bool overrideShakeY = true, bool overrideShakeZ = false)
    {

        float duration = (overrideDefaultDuration > DefaultDuration) ? (float)overrideDefaultDuration : DefaultDuration;
        float magnitude = (overrideDefaultMagnitude > DefaultMagnitude) ? (float)overrideDefaultMagnitude : DefaultMagnitude;

        bool shakeX = (overrideShakeX == true) ? overrideShakeX : ShakeX;
        bool shakeY = (overrideShakeY == true) ? overrideShakeX : ShakeY;
        bool shakeZ = (overrideShakeZ == true) ? overrideShakeX : ShakeZ;

        Vector3 originalPos = Camera.main.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition =  new Vector3((shakeX == true) ? x : originalPos.x,
                                                               (shakeY == true) ? y : originalPos.y,
                                                               (shakeZ == true) ? z : originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;

    }


}
