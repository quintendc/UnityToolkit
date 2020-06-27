using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;

public class CameraShake : ToolkitBehaviour
{

    #region public properties

    [Header("Shake axis")]
    public bool ShakeX = true;
    public bool ShakeY = true;
    public bool ShakeZ = false;

    #endregion

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = Camera.main.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition =  new Vector3((ShakeX == true) ? x : originalPos.x,
                                                               (ShakeY == true) ? y : originalPos.y,
                                                               (ShakeZ == true) ? z : originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;

    }


}
