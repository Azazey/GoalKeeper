using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "My Custom Stuff/Level Creator/Create new Level Settings")]
public class LevelSettings : ScriptableObject
{
    [Header("Maximal Progress is not the amount of score that need to pass level!")]
    public int MaxProgress;
    public int RoundTimeInSeconds;
    public int ItemsFireRate;
    public int MaxItemsPerFire;
    public Sprite Sprite;
    public bool Bought;
    public int Cost;
}
