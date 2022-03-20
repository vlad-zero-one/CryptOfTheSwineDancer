using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private ObstacleCoordinates coordinates;

    [SerializeField] private Coordinator coordinator;

    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GridPlacer gridPlacer;
    [SerializeField] private Transform parrentInHierarchy;

    void Start()
    {
        foreach (var coord in coordinates.Values)
        {
            if (coordinator.CanMoveHere(coord))
            {
                var obstacle = Instantiate(obstaclePrefab, parrentInHierarchy);
                coordinator.SetCoordinates(obstacle, coord);
            }

        }
    }
}
