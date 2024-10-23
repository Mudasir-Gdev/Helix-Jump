using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    [Range(1, 11)]
    public int PartCount = 11;
    [Range(0, 11)]
    public int DeathPartCount = 1;
}
[CreateAssetMenu]
public class Stage : ScriptableObject
{
    public Color BackGroundColor = Color.white;
    public Color StageLevelPartColor = Color.white;
    public Color StageBallColor = Color.white;
    public List<Level> Levels = new List<Level>();

}
