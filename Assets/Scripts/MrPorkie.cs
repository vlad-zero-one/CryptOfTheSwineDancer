using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrPorkie : MonoBehaviour, ITickableObject
{
    [SerializeField] private TickSystem tickSystem;

    [SerializeField] private Coordinator coordinator;

    [SerializeField] private GameObject bombPrefab;

    [SerializeField] private GameObject bombsInHierarchy;

    private Direction currentDirection;
    private Coordinates coordinates;

    private bool bombPlaced = false;
    private GameObject bomb;

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
        SetDirection(Direction.ToTheMoon);
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
            case Direction.ToTheMoon:
                InstantiateBomb();
                break;
        }
        if (coordinator.CanMoveHere(newCoordinates))
        {
            coordinates = newCoordinates;
            coordinator.Move(gameObject, coordinates);
            ActivateBomb();
        }
        else if (currentDirection == Direction.ToTheMoon)
        {

        }
    }

    private void InstantiateBomb()
    {
        if (!bombPlaced)
        {
            bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity, bombsInHierarchy.transform);
            bomb.GetComponent<Bomb>().PlaceBomb(coordinates);
            bombPlaced = true;
        }
    }

    private void ActivateBomb()
    {
        if (bombPlaced)
        {
            bomb.GetComponent<Bomb>().LightTheFuse(tickSystem, coordinator);
            bombPlaced = false;
        }
    }

    private void SetDirection(Direction dir)
    {
        currentDirection = dir;
    }

    private void OnDestroy()
    {
        coordinator.GameOver(porkieDied: true);
    }
}

enum Direction
{
    ToTheMoon,
    Up,
    Down,
    Left,
    Right,
    
    Count
}
