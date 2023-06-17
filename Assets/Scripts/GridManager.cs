using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class GridManager : MonoBehaviour
{
    [SerializeField] private int rows = default;
    [SerializeField] private int columns = default;

    [SerializeField] private List<ObjectTyp> objectTyps = new List<ObjectTyp>();

    private GemType[,] grid;
    private GameObject parentObject;
    
    // Start is called before the first frame update
    void Start()
    {
        //GenerateLevel();
    }

    public void GenerateLevel()
    {
        CreateNewParentObject();
        
        do
        {
            GenerateGrid();
        }
        while (!HasValidMatch());
    }

    private void CreateNewParentObject()
    {
        if (parentObject != null)
            Destroy(parentObject);

        parentObject = new GameObject("Parent");
        parentObject.transform.position = Vector3.zero;
    }

    private void GenerateGrid()
    {
        grid = new GemType[rows, columns];

        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                GemType randomGemType = GetRandomGemType();
                InstantiateRandomGemType(i, j, randomGemType);
            }
        }
    }

    private void InstantiateRandomGemType(int i, int j , GemType randomGem)
    {
        for(int gem = 0; gem < objectTyps.Count; gem++)
        {
            if(objectTyps[gem].GemType == randomGem)
            {
                GameObject newGameobject = (GameObject)Instantiate(objectTyps[gem].gameObject);
                newGameobject.transform.position = new Vector2(i, j);
                grid[i, j] = randomGem;

                break;
            }
        }
    }

    private GemType GetRandomGemType()
    {
        return (GemType)typeof(GemType).RandomValue();

    }

    private bool HasValidMatch()
    {
        // for horizontal matches
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns - 2; j++)
            {
                GemType gemType = grid[i, j];

                if (gemType == grid[i, j + 1] && gemType == grid[i, j + 2])
                    return true;
            }
        }

        // for vertical matches
        for( int i = 0; i < columns; i++)
        {
            for(int j = 0; j < rows - 2; j++)
            {
                GemType gemType = grid[j, i];

                if (gemType == grid[j + 1, i] && gemType == grid[j + 2, i])
                    return true;
            }
        }

        return false;
    }
}
