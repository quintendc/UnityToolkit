using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolkitCamera : MonoBehaviour
{

    public CameraShake cameraShake;
    public Camera Camera;

    /// <summary>
    /// shake the Toolkit camera
    /// </summary>
    /// <param name="overrideDefaultDuration">duration of the shake in seconds</param>
    /// <param name="overrideDefaultMagnitude">the magnitude of the shake</param>
    /// <param name="overrideShakeX">override default camers X Axis setting</param>
    /// <param name="overrideShakeY">override default camers Y Axis setting</param>
    /// <param name="overrideShakeZ">override default camers Z Axis setting</param>
    public void ShakeToolkitCamera(float? overrideDefaultDuration = null, float? overrideDefaultMagnitude = null, bool overrideShakeX = true, bool overrideShakeY = true, bool overrideShakeZ = false)
    {
        StartCoroutine(cameraShake.Shake(overrideDefaultDuration, overrideDefaultMagnitude, overrideShakeX, overrideShakeY, overrideShakeZ));
    }

}
