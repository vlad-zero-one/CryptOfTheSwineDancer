using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DirectionDelegate(Direction direction);

public class MrPorkie : MonoBehaviour, ITickableObject, IInitable
{
    [SerializeField] private TickSystem tickSystem;

    [SerializeField] private Coordinator coordinator;

    [SerializeField] private GameObject bombPrefab;

    [SerializeField] private GameObject bombsInHierarchy;

    [SerializeField] private Coordinates startCoordinates;

    [SerializeField] private BombExplodeEffector bombExplodeEffector;

    private Direction currentDirection;
    private Coordinates coordinates;

    public event DirectionDelegate DirectionChangeEvent;

    public void Init()
    {
        coordinator.SetCoordinates(gameObject, startCoordinates);

        SubscribeActionOnTick();

        GetComponent<SpriteChanger>().Init(GetComponent<SpriteRenderer>());

        DirectionChangeEvent += GetComponent<SpriteChanger>().ChangeSprite;
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
        }
        else if (currentDirection == Direction.ToTheMoon)
        {

        }
    }

    private void InstantiateBomb()
    {
        if (coordinator.IsThereBomb(coordinates)) return;

        var bombGO = Instantiate(bombPrefab, transform.position, Quaternion.identity, bombsInHierarchy.transform);
        var bomb = bombGO.GetComponent<Bomb>();
        bomb.LightTheFuse(tickSystem, coordinator, coordinates);
        bomb.BombExplodedEvent += bombExplodeEffector.Animate;
    }

    private void SetDirection(Direction dir)
    {
        currentDirection = dir;
        DirectionChangeEvent.Invoke(dir);
    }

    private void OnDestroy()
    {
        DirectionChangeEvent -= GetComponent<SpriteChanger>().ChangeSprite;

        tickSystem.TickEvent -= MoveOrPlaceBomb;
        coordinator.GameOver(porkieDied: true);
    }
}
