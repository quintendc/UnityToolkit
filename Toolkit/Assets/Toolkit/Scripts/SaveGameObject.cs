using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MySaveGameObject", menuName = "Toolkit/SaveGameObject", order = 1)]
public class SaveGameObject : ScriptableObject
{
    public int Id { get; set; }
    public string Name { get; set; }
}
