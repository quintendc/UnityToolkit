using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SampleGameMode : AGameMode
{
    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instance.CreatePlayer(true);
        Toolkit.CreatePlayer(true); // shorthand method
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
