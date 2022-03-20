using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleCoordinates", menuName = "ScriptableObjects/ObstacleCoordinates")]
public class ObstacleCoordinates : ScriptableObject
{
    public List<Coordinates> Values;
}
