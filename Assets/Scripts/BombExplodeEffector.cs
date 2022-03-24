using UnityEngine;

public delegate void BombExplodedDelegate(Coordinates coordinates);

public class BombExplodeEffector : MonoBehaviour
{
    [SerializeField] private GridPlacer gridPlacer;
    [SerializeField] private GameObject effectCircle;

    public void Animate(Coordinates coordinates)
    {
        gridPlacer.Place(effectCircle, coordinates);
        effectCircle.GetComponent<Animation>().Play();
    }
}
