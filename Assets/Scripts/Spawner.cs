using UnityEngine;

public class Spawner : MonoBehaviour, IInitable
{
    [SerializeField] private CoordinatesForSpawn coordinates;

    [SerializeField] private Coordinator coordinator;

    [SerializeField] private GameObject prefabToSpawn;

    [SerializeField] private Transform parrentInHierarchy;

    [SerializeField] private TickSystem tickSystem;

    public void Init()
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
