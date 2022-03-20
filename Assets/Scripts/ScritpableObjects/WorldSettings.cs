using UnityEngine;

[CreateAssetMenu(fileName = "WorldSettings", menuName = "ScriptableObjects/WorldSettings")]
public class WorldSettings : ScriptableObject
{
    [Header("Map Size")]
    public uint Rows;
    public uint CellsInRow;
}
