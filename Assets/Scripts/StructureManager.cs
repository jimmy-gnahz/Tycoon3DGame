using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public StructurePrefabWeighted[] housePrefabs, specialPrefabs, grassPrefabs, bigStructuresPrefabs;
    public PlacementManager placementManager;
    public MoneyManager moneyManager;

    private float[] houseWeights, specialWeights, grassWeights, bigStructuresWeights;

    private void Start()
    {
        houseWeights = housePrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        specialWeights = specialPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        grassWeights = grassPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        bigStructuresWeights = bigStructuresPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
    }

    public void PlaceBigStructure(Vector3Int position)
    {
        int width = 2;
        int height = 2;

        bool nearRoad = true;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);
                nearRoad = nearRoad && CheckPositionBeforePlacement(newPosition, CellType.Structure);
            }
        }
        if (!nearRoad)
        {
            Debug.Log("Failed here");
            return;
        }

        if (CheckPositionBeforePlacement(position, CellType.Structure))
        {
            int randomIndex = GetRandomWeightedIndex(bigStructuresWeights);
            placementManager.PlaceObjectOnTheMap(position, bigStructuresPrefabs[randomIndex].prefab, CellType.Structure, width, height);
            AudioPlayer.instance.PlayPlacementSound();
            moneyManager.Spend(CellType.SpecialStructure);
        }
    }

    private bool CheckBigStructure(Vector3Int position, int width, int height)
    {
        throw new NotImplementedException();
    }

    public void PlaceHouse(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position, CellType.Structure))
        {
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, housePrefabs[randomIndex].prefab, CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
            moneyManager.Spend(CellType.Structure);
        }
    }

    public void PlaceSpecial(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position, CellType.SpecialStructure))
        {
            int randomIndex = GetRandomWeightedIndex(specialWeights);
            placementManager.PlaceObjectOnTheMap(position, specialPrefabs[randomIndex].prefab, CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
            moneyManager.Spend(CellType.Structure);
        }
    }

    public void PlaceGrass(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position, CellType.Grass))
        {
            int randomIndex = GetRandomWeightedIndex(grassWeights);
            placementManager.PlaceObjectOnTheMap(position, grassPrefabs[randomIndex].prefab, CellType.Grass);
            AudioPlayer.instance.PlayPlacementSound();
            moneyManager.Spend(CellType.Grass);
            
        }
    }

    private int GetRandomWeightedIndex(float[] weights)
    {
        float sum = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i];
        }

        float randomValue = UnityEngine.Random.Range(0, sum);
        float tempSum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            if (randomValue >= tempSum && randomValue < tempSum + weights[i])
            {
                return i;
            }
            tempSum += weights[i];
        }
        return 0;
    }

    private bool CheckPositionBeforePlacement(Vector3Int position, CellType type)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
        {
            Debug.Log("This position is out of bounds");
            return false;
        }


        if (type != CellType.Grass) // Not Grass
        {
            if (!placementManager.CheckIfPositionIsGrass(position))
            {
                Debug.Log("This position is not GRASS：" + position.x + " " + position.z);
                return false;
            }
        }
        else // If grass
        {
            if (!placementManager.CheckIfPositionIsOfType(position, CellType.Empty))
            {
                Debug.Log("This position is not EMPTY");
                return false;
            }
        }

        if (placementManager.GetNeighboursOfTypeFor(position, CellType.Road).Count <= 0)
        {
            Debug.Log("Must be placed near a road");
            return false;
        }

        return true;
    }
}

[Serializable]
public struct StructurePrefabWeighted
{
    public GameObject prefab;
    [Range(0, 1)]
    public float weight;
}