using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, ITickableObject
{
    [SerializeField] private TickSystem tickSystem;

    [SerializeField] private Coordinator coordinator;

    private Direction currentDirection;
    private Coordinates coordinates;

    private void Start()
    {
        coordinates = new Coordinates(5, 5);
        coordinator.SetCoordinates(gameObject, coordinates);

        SubscribeActionOnTick();
    }

    public void SubscribeActionOnTick()
    {
        tickSystem.TickEvent += RandomMove;

        tickSystem.LateTickEvent += LookUp;
    }

    private void RandomMove()
    {
        var placeToMove = FreePlace();

        coordinator.Move(gameObject, placeToMove);
    }

    private void SetRandomDirection()
    {
        var dir = UnityEngine.Random.Range(0, (int)Direction.Count);
        currentDirection = (Direction)dir;
    }

    private void LookUp()
    {
        var x = coordinates.x;
        var y = coordinates.y;

        if (coordinator.IsTherePork(new Coordinates(x - 1, y))
            || coordinator.IsTherePork(new Coordinates(x + 1, y))
            || coordinator.IsTherePork(new Coordinates(x, y - 1))
            || coordinator.IsTherePork(new Coordinates(x, y + 1)))
        {
            ChangeSpriteToAgressive();
            coordinator.GameOver();
        }
    }

    private Coordinates FreePlace()
    {
        SetRandomDirection();

        var newCoordinates = coordinates;

        switch (currentDirection)
        {
            case Direction.Up:
                newCoordinates = new Coordinates(coordinates.x, coordinates.y + 1);
                break;
            case Direction.Down:
                newCoordinates = new Coordinates(coordinates.x, coordinates.y - 1);
                break;
            case Direction.Left:
                newCoordinates = new Coordinates(coordinates.x - 1, coordinates.y);
                break;
            case Direction.Right:
                newCoordinates = new Coordinates(coordinates.x + 1, coordinates.y);
                break;
        }

        if (coordinator.CanMoveHere(newCoordinates))
        {
            coordinates = newCoordinates;
            return coordinates;
        }
        else
        {
            return FreePlace();
        }
    }

    private void ChangeSpriteToAgressive()
    {
        return;
    }
}
