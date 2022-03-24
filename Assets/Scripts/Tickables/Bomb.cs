using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, ITickableObject
{
    private const uint TICKS_TO_EXPLODE = 2;

    private uint timer;

    private TickSystem tickSystem;
    private Coordinator coordinator;
    private Coordinates coordinates;

    public event BombExplodedDelegate BombExplodedEvent;

    public void LightTheFuse(TickSystem tickSystem, Coordinator coordinator, Coordinates coordinates)
    {
        this.coordinates = coordinates;
        this.tickSystem = tickSystem;
        this.coordinator = coordinator;

        timer = TICKS_TO_EXPLODE;

        coordinator.SetCoordinates(this, coordinates);

        SubscribeActionOnTick();
    }

    public void SubscribeActionOnTick()
    {
        tickSystem.LateTickEvent += StartTimer;
    }

    private void StartTimer()
    {
        if (timer <= 0)
        {
            ExplodeBomb();
        }
        timer--;
    }

    private void ExplodeBomb()
    {
        BombExplodedEvent.Invoke(this.coordinates);

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                var cord = new Coordinates((uint)((int)coordinates.x + x), (uint)((int)coordinates.y + y));
                coordinator.Remove(cord);
            }
        }
    }

    private void OnDestroy()
    {
        tickSystem.LateTickEvent -= StartTimer;
    }
}
