using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrPorkie : MonoBehaviour, ITickableObject
{
    [SerializeField] private TickSystem tickSystem;

    [SerializeField] private Coordinator coordinator;

    private Direction currentDirection;
    private Coordinates coordinates;

    private void Start()
    {
        coordinates = new Coordinates(0, 0);
        coordinator.SetCoordinates(gameObject, coordinates);

        SubscribeActionOnTick();
    }

    public void SubscribeActionOnTick()
    {
        tickSystem.TickEvent += MoveOrPlaceBomb;
    }

    public void MoveUp()
    {
        SetDirection(Direction.Up);
    }

    public void MoveDown()
    {
        SetDirection(Direction.Down);
    }

    public void MoveLeft()
    {
        SetDirection(Direction.Left);
    }

    public void MoveRight()
    {
        SetDirection(Direction.Right);
    }

    public void PlaceBomb()
    {
        SetDirection(Direction.Stay);
    }

    private void MoveOrPlaceBomb()
    {
        Coordinates newCoordinates = coordinates;
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
            coordinator.Move(gameObject, coordinates);
        }
        else
            currentDirection = Direction.Stay;

    }

    private void SetDirection(Direction dir)
    {
        currentDirection = dir;
    }
}

enum Direction
{
    Stay,
    Up,
    Down,
    Left,
    Right,
    
    Count
}
