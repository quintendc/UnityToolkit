using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Converters
{

    /// <summary>
    /// convert vetcor3 to an array of floats
    /// </summary>
    /// <param name="vector3">vector3 variable</param>
    /// <returns>array of float with a length of 3</returns>
    public static float[] ConvertVector3ToFloatArray(Vector3 vector3)
    {

        float[] array = new float[3];

        array[0] = vector3.x;
        array[1] = vector3.y;
        array[2] = vector3.z;


        return array;
    }


    public static Vector3 ConvertFloatArrayToVector3(float[] array)
    {
        Vector3 vector3 = new Vector3();

        vector3.x = array[0];
        vector3.y = array[1];
        vector3.z = array[2];

        return vector3;
    }


}