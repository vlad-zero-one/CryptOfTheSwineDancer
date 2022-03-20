using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CoordinatesForSpawn coordinates;

    [SerializeField] private Coordinator coordinator;

    [SerializeField] private GameObject prefabToSpawn;

    [SerializeField] private Transform parrentInHierarchy;

    [SerializeField] private TickSystem tickSystem;

    void Start()
    {
        foreach (var coord in coordinates.Values)
        {
            if (coordinator.CanMoveHere(coord))
            {
                var spawned = Instantiate(prefabToSpawn, parrentInHierarchy);

                spawned.GetComponent<BaseEnemy>()?.Init(coordinator, tickSystem, coord);

                coordinator.SetCoordinates(spawned, coord);
            }

        }
    }
}
