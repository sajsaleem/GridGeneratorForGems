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

    private float initialHorizontalPosition = -10;

    private float horizontalPosition = default;
    private float verticalPosition = 15;

    private GemType[,] grid;

    private GameObject parentObject;

    // Start is called before the first frame update
    void Start()
    {
        //hello;
    }

    public void GenerateLevel()
    {
        CreateNewParentObject();

        grid = new GemType[rows, columns];

        horizontalPosition = initialHorizontalPosition;
        verticalPosition = 5;

        GenerateGrid();
        PopulateObjects();

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
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                if (row == 0 && col == 0)
                    grid[row, col] = GetRandomGemType();
                else
                {
                    grid[row, col] = GetUniqueGemType(row, col);
                }
            }
        }
    }

    private void PopulateObjects()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                InstantiateRandomGemType(row, col, grid[row, col]);
            }

            verticalPosition -= 2;
            horizontalPosition = initialHorizontalPosition;

        }
    }

    private void InstantiateRandomGemType(int i, int j, GemType randomGem)
    {
        for (int gem = 0; gem < objectTyps.Count; gem++)
        {
            if (objectTyps[gem].GemType == randomGem)
            {
                GameObject newGameobject = (GameObject)Instantiate(objectTyps[gem].gameObject);
                newGameobject.transform.position = new Vector2(horizontalPosition, verticalPosition);
                newGameobject.transform.SetParent(parentObject.transform);
                horizontalPosition += 3;
                break;
            }
        }
    }

    private GemType GetRandomGemType()
    {
        return (GemType)typeof(GemType).RandomValue();

    }


    private GemType GetUniqueGemType(int row, int col)
    {
        GemType newGemType = GetRandomGemType();

        int nextRow = row + 1 < rows ? row + 1 : row;
        int nextCol = col + 1 < columns ? col + 1 : col;
        int previousRow = row - 1 > 0 ? row - 1 : row;
        int previousCol = col - 1 > 0 ? col - 1 : col;


        while (newGemType == grid[nextRow, col] || newGemType == grid[row, nextCol] || newGemType == grid[row, previousCol] || newGemType == grid[previousRow, col])
        {
            newGemType = GetRandomGemType();
            Debug.Log("NewGemType: " + newGemType);
        }

        return newGemType;
    }
}
