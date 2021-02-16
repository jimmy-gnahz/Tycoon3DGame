using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlacementManager placementManager;
    public List<Vector3Int> temporaryPlacementPositions = new List<Vector3Int>();

    public GameObject roadStraight;

    public void PlaceRoad(Vector3Int position)
    {
        if (placementManager.checkIfPositionInBound(position) == false)
            return;
        if (placementManager.checkIfPositionIsFree(position) == false)
            return;
        placementManager.placeTemporaryStructure(position, roadStraight, CellType.Road);

    }
}
