using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Coordinator : MonoBehaviour
{
    [SerializeField] private WorldSettings worldSettings;
    [SerializeField] private Transform worldGrid;
    [SerializeField] private GridPlacer gridPlacer;

    [SerializeField] GameObject pork;

    [SerializeField] Menu menu;

    private Dictionary<Coordinates, GameObject> coordinatesDict = new Dictionary<Coordinates, GameObject>();

    private Dictionary<Coordinates, Bomb> bombsCoordinates = new Dictionary<Coordinates, Bomb>();

    void Awake()
    {
        if (worldGrid.childCount != worldSettings.Rows || worldGrid.GetChild(0).childCount != worldSettings.CellsInRow)
            throw new System.Exception("Settings of the world size not match to the world grid");

        for (uint x = 0; x < worldSettings.CellsInRow; x++)
            for (uint y = 0; y < worldSettings.Rows; y++)
                coordinatesDict.Add(new Coordinates(x, y), null);
    }

    public bool CanMoveHere(Coordinates coordinates)
    {   
        if (coordinates.x != uint.MaxValue
            && coordinates.y != uint.MaxValue
            && coordinates.x < worldSettings.CellsInRow
            && coordinates.y < worldSettings.Rows)
            return coordinatesDict[coordinates] == null ? true : false;

        return false;
    }

    public bool IsTherePork(Coordinates coordinates)
    {
        if (coordinatesDict.ContainsKey(coordinates))
            return coordinatesDict[coordinates] == pork ? true : false;

        return false;
    }

    public bool IsThereBomb(Coordinates coordinates)
    {
        if (bombsCoordinates.ContainsKey(coordinates))
            return bombsCoordinates[coordinates] != null;

        return false;
    }

    public void SetCoordinates(GameObject obj, Coordinates coordinates)
    {
        coordinatesDict[coordinates] = obj;

        gridPlacer.Place(obj, coordinates);
    }

    public void SetCoordinates(Bomb bomb, Coordinates coordinates)
    {
        bombsCoordinates[coordinates] = bomb;

        gridPlacer.Place(bomb.gameObject, coordinates);
    }

    public bool Remove(Coordinates coordinates)
    {
        if (bombsCoordinates.ContainsKey(coordinates) && bombsCoordinates[coordinates] != null)
        {
            Destroy(bombsCoordinates[coordinates].gameObject);
        }
        if (coordinatesDict.ContainsKey(coordinates) && coordinatesDict[coordinates] != null)
        {
            if (coordinatesDict[coordinates].GetComponent<Obstacle>()
                && worldSettings.UnbreakableObstacles)
            {
                return false;
            }
            Destroy(coordinatesDict[coordinates]);
            return true;
        }
        return false;
    }

    public void Move(GameObject obj, Coordinates coordinates)
    {

        if (coordinatesDict.ContainsValue(obj) && CanMoveHere(coordinates))
        {
            var oldCoordinates = coordinatesDict.FirstOrDefault(x => x.Value == obj).Key;

            SetCoordinates(obj, coordinates);
            coordinatesDict[oldCoordinates] = null;
        }
        
    }

    public void GameOver(bool porkieDied = false)
    {
        menu.GameOver(porkieDied: porkieDied);
    }
}
