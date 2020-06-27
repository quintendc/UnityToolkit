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

    public IEnumerator Shake(float? overrideDefaultDuration = null, float? overrideDefaultMagnitude = null, bool? overrideShakeX = null, bool? overrideShakeY = null, bool? overrideShakeZ = null)
    {

        float duration = (overrideDefaultDuration != null) ? (float)overrideDefaultDuration : DefaultDuration;
        float magnitude = (overrideDefaultMagnitude != null) ? (float)overrideDefaultMagnitude : DefaultMagnitude;

        bool shakeX = (overrideShakeX != null) ? (bool)overrideShakeX : ShakeX;
        bool shakeY = (overrideShakeY != null) ? (bool)overrideShakeX : ShakeY;
        bool shakeZ = (overrideShakeZ != null) ? (bool)overrideShakeX : ShakeZ;

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
