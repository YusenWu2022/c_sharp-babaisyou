using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementTypes
{
    Empty = 0,
    Baba,
    Wall,
    Flag,
    BabaString,
    WallString,
    Is,
    You,
    Stop,
    Win,
    FlagString,
    Push
}

// 贴图选择来存储布局数据，更方便编辑
[CreateAssetMenu()][System.Serializable]
public class LevelCreator : ScriptableObject
{
    [SerializeField]
    public List<ElementTypes> level = new List<ElementTypes>();
}
