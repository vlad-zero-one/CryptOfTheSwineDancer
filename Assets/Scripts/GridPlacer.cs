using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacer : MonoBehaviour
{
    public bool Place(GameObject obj, Coordinates coord)
    {
        if (transform.childCount > coord.y && transform.GetChild(0).childCount > coord.x)
        {
            if (transform.GetChild((int)coord.y) is Transform row)
            {
                if (row.GetChild((int)coord.x) is Transform cell)
                {
                    obj.transform.position = new Vector2(cell.position.x, cell.position.y);

                    return true;
                }
            }
        }

        Debug.LogError($"GridPlacer tryed to place {obj.name} over the coordinates system. Point {coord.x} {coord.y}");
        return false;
    }
}
