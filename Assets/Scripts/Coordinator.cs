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

    bool once = true;

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

    public void SetCoordinates(GameObject obj, Coordinates coordinates)
    {
        coordinatesDict[coordinates] = obj;

        gridPlacer.Place(obj, coordinates);
    }

    public void Move(GameObject obj, Coordinates coordinates)
    {

        if (coordinatesDict.ContainsValue(obj) && CanMoveHere(coordinates))
        {
            var oldCoordinates = coordinatesDict.FirstOrDefault(x => x.Value == obj).Key;

            //Debug.Log(oldCoordinates.x + " " + oldCoordinates.y + " " + coordinatesDict[oldCoordinates]);

            SetCoordinates(obj, coordinates);
            coordinatesDict[oldCoordinates] = null;

            //Debug.Log(oldCoordinates.x + " " + oldCoordinates.y + " " + coordinatesDict[oldCoordinates]);
        }
        
    }

    public void GameOver()
    {
        menu.GameOver();
    }

    private void Update()
    {
        if (once)
        {
            foreach(var cell in coordinatesDict)
            {
                Debug.Log(cell.Key.x + " " + cell.Key.y + " " + cell.Value);
            }
            once = false;
        }
    }
}
