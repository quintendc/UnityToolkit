using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

// objects that enter the KillZ will be destroyed
public class KillZ : ToolkitBehaviour
{

    [Tooltip("GameObjects with a tag from this list will not be destroyed.")]
    public List<string> tags = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        // if tags doesn't conatins the tag of the entering object -> destroy
        if (!tags.Contains(other.tag))
        {
            Destroy(other.gameObject);
        }

    }
}
