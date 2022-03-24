using UnityEngine;

public class BaseEnemy : MonoBehaviour, ITickableObject
{
    [SerializeField] private TickSystem tickSystem;

    [SerializeField] private Coordinator coordinator;

    private Direction currentDirection;
    private Coordinates coordinates;

    public event DirectionDelegate DirectionChangeEvent;

    public void Init(Coordinator coordinator, TickSystem tickSystem, Coordinates coordinates)
    {
        this.coordinator = coordinator;
        this.tickSystem = tickSystem;
        this.coordinates = coordinates;

        SubscribeActionOnTick();

        GetComponent<SpriteChanger>().Init(GetComponent<SpriteRenderer>());

        DirectionChangeEvent += GetComponent<SpriteChanger>().ChangeSprite;
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
            DirectionChangeEvent.Invoke(currentDirection);
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

    private void OnDestroy()
    {
        transform?.GetComponentInParent<WinChecker>()?.CheckWin();

        tickSystem.TickEvent -= RandomMove;
        tickSystem.LateTickEvent -= LookUp;
    }
}
