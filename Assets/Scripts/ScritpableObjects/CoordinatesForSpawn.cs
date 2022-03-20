using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoordinatesForSpawn", menuName = "ScriptableObjects/CoordinatesFowSpawn")]
public class CoordinatesForSpawn : ScriptableObject
{
    public List<Coordinates> Values;
}
