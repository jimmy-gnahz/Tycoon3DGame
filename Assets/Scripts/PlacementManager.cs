using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width, height;
    Grid placementGrid;

    private void Start()
    {
        placementGrid = new Grid(width, height);
    }

    internal bool checkIfPositionInBound(Vector3Int position)
    {
        if (position.x >= 0 && position.x < width && position.z >= 0 && position.z < height)
        {
            return true;
        }
        return false;
    }

    internal bool checkIfPositionIsFree(Vector3Int position)
    {
        return checkIfPositionIsOfType(position, CellType.Empty);
    }

    private bool checkIfPositionIsOfType(Vector3Int position, CellType type)
    {
        return placementGrid[position.x, position.z] == type;
    }

    internal void placeTemporaryStructure(Vector3Int position, GameObject roadStraight, CellType type)
    {
        placementGrid[position.x, position.z] = type;
        GameObject newStructure = Instantiate(roadStraight, position, Quaternion.identity); 
    }
}
