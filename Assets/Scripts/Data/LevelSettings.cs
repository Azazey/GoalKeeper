using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType{
    StartOfTier,
    Between,
    EndOfTier
}

[CreateAssetMenu(fileName = "New Level", menuName = "My Custom Stuff/Level Creator/Create new Level Settings")]
public class LevelSettings : ScriptableObject
{
    public LevelType LevelType;
    [Header("Maximal Progress is not the amount of score that need to pass level!")]
    public int MaxProgress;
    public int RoundTimeInSeconds;
    public int ItemsFireRate;
    public Sprite Sprite;
}
