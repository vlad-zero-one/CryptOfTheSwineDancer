using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, ITickableObject
{


    private const uint TICKS_TO_EXPLODE = 1;

    private uint timer;

    private TickSystem tickSystem;
    private Coordinator coordinator;
    private Coordinates coordinates;

    public void PlaceBomb(Coordinates coordinates)
    {
        this.coordinates = coordinates;
    }

    public void LightTheFuse(TickSystem tickSystem, Coordinator coordinator)
    {
        this.tickSystem = tickSystem;
        this.coordinator = coordinator;

        timer = TICKS_TO_EXPLODE;

        SubscribeActionOnTick();
    }

    public void SubscribeActionOnTick()
    {
        tickSystem.LateTickEvent += Kaboom;
    }

    private void Kaboom()
    {
        // �� ��������� ��� ����� ������
        if (timer + 1 == TICKS_TO_EXPLODE)
        {
            coordinator.SetCoordinates(gameObject, coordinates);
        }
        if (timer <= 0)
        {
            coordinator.BombExplode(coordinates);

            tickSystem.LateTickEvent -= Kaboom;
            Destroy(gameObject);
        }
        timer--;
    }
}