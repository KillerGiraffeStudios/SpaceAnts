using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block
{
    public int width { get; set; }
    public int height { get; set; }

    //Top left corner
    public Vector2 position { get; set; }

    public Block(int w, int h, Vector2 p)
    {
        width = w;
        height = h;
        position = p;
    }
}


public class Generate_Ship : MonoBehaviour{

    //These will connect to game options
    public int buildingDensity = 5;
    //Some others for amount of industry etc.

    private int splitVariance = 4;

    private int minBlockSize =3;
    private int maxBlockSize =9;


    //Types of buildings
    [SerializeField]
    private GameObject RoadTile;

    [SerializeField]
    private GameObject EndWall;

    [SerializeField]
    private GameObject LargeBuilding;

    [SerializeField]
    private GameObject MediumBuilding;

    [SerializeField]
    private GameObject SmallBuilding;


    private char[,] codeLocations;

    private List<Block> buildingBlockList;

    public GameObject[,] generate(int size)
    {

        buildingBlockList = new List<Block>();

        //This should not be hard coded
        codeLocations = new char[size, size];

        Block startingBlock = new Block(size,size,new Vector2(0,0));
        generateBlocks(startingBlock, true);

        foreach(Block i in buildingBlockList)
        {
            generateRoads(i);
            generateBuildings(i);
        }


        //Set outer wall
        for(int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                 if(i == 0 || j == 0 || i == size-1 || j == size - 1)
                {
                    codeLocations[i, j] = 'E';
                }
            }
        }

        //convert codeLocations to buildingArray
        GameObject[,] buildingArray = convertChars();
        instantiateWorld(buildingArray);
        return buildingArray;
    }

    private void generateBlocks(Block square, bool vertical)
    {

 
        if(square.width <= maxBlockSize && square.height <= maxBlockSize)
        {
            buildingBlockList.Add(square);
            return;
        }

        
        int split;
        
        if (square.width <= maxBlockSize)
        {
            split = Random.Range((int)square.position.x + splitVariance, (int)square.position.x + square.height - splitVariance);
            Block block1 = new Block(square.width, split - (int)square.position.x, new Vector2(square.position.x, square.position.y));
            Block block2 = new Block(square.width, (int)square.position.x + square.height - split, new Vector2(split, square.position.y));
            generateBlocks(block1, false);
            generateBlocks(block2, false);
            return;
        }
        if (square.height <= maxBlockSize)
        {
            split = Random.Range((int)square.position.y + splitVariance, (int)square.position.y + square.width - splitVariance);
            Block block1 = new Block(split - (int)square.position.y, square.height, new Vector2(square.position.x, square.position.y));
            Block block2 = new Block((int)square.position.y + square.width - split, square.height, new Vector2(square.position.x, split));
            generateBlocks(block1, true);
            generateBlocks(block2, true);
            return;
        }

        if (vertical)
        {
            split = Random.Range((int)square.position.y + splitVariance, (int)square.position.y + square.width - splitVariance);
            Block block1 = new Block(split - (int)square.position.y, square.height, new Vector2(square.position.x, square.position.y));
            Block block2 = new Block((int)square.position.y + square.width - split, square.height, new Vector2(square.position.x, split));
            generateBlocks(block1, !vertical);
            generateBlocks(block2, !vertical);
        }
        else
        {
            split = Random.Range((int)square.position.x + splitVariance, (int)square.position.x + square.height - splitVariance);
            Block block1 = new Block(square.width, split - (int)square.position.x, new Vector2(square.position.x, square.position.y));
            Block block2 = new Block(square.width, (int)square.position.x + square.height - split, new Vector2(split, square.position.y));
            generateBlocks(block1, !vertical);
            generateBlocks(block2, !vertical);
        }


    }

    private void generateBuildings(Block block)
    {
        for(int i = 0; i < block.height; i++)
        {
            for (int j = 0; j < block.width; j++)
            {
                if(codeLocations[(int)block.position.x + i, (int)block.position.y + j] == '\0')
                {
                    determineBuilding((int)block.position.x + i, (int)block.position.y + j);
                }
            }
        }
    }

    private void generateRoads(Block block)
    {
        for (int i = 0; i < block.height; i++)
        {
            for (int j = 0; j < block.width; j++)
            {
                if (i == block.height - 1 || j == block.width - 1)
                {
                    codeLocations[(int)block.position.x + i, (int)block.position.y + j] = 'R';
                }
            }
        }
    }

    private GameObject[,] convertChars()
    {
        GameObject[,] gameObjects = new GameObject[codeLocations.GetLength(0), codeLocations.GetLength(1)];
        for(int i = 0; i < codeLocations.GetLength(0); i++)
        {
            for (int j = 0; j < codeLocations.GetLength(1); j++)
            {
                if(codeLocations[i,j] == 'R')
                {
                    gameObjects[i, j] = RoadTile;
                }
                if (codeLocations[i, j] == 'E')
                {
                    gameObjects[i, j] = EndWall;
                }
                if (codeLocations[i, j] == 'S')
                {
                    gameObjects[i, j] = SmallBuilding;
                }
                if (codeLocations[i, j] == 'M')
                {
                    gameObjects[i, j] = MediumBuilding;
                }
                if (codeLocations[i, j] == 'L')
                {
                    gameObjects[i, j] = LargeBuilding;
                }

            }
        }
        return gameObjects;
    }

    private void instantiateWorld(GameObject[,] gameObjects)
    {
        for (int i = 0; i < gameObjects.GetLength(0); i++)
        {
            for (int j = 0; j < gameObjects.GetLength(1); j++)
            {
                if (gameObjects[i, j] != null)
                {
                    if (gameObjects[i, j] == MediumBuilding)
                    {
                        Instantiate(gameObjects[i, j], new Vector2(i + 0.5f, j + 0.5f), Quaternion.identity);
                    }
                    else if (gameObjects[i, j] == LargeBuilding)
                    {
                        Instantiate(gameObjects[i, j], new Vector2(i + 1, j + 1), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(gameObjects[i, j], new Vector2(i, j), Quaternion.identity);
                    }
                }

            }
        }
    }

    private void determineBuilding(int x, int y)
    {
        int chanceM = 60;
        int chanceL = 90;

        int num = Random.Range(0, 100);
        if(num < chanceM)
        {
            codeLocations[x, y] = 'S';
        }
        else if(num < chanceL)
        {
            
            //get medium building
            if(!checkFit(x,y,new Vector2(2, 2)))
            {
                determineBuilding(x, y);
            }
            else
            {
                for(int i = x; i < x+2; i++)
                {
                    for (int j = y; j < y + 2; j++)
                    {
                        
                        codeLocations[i, j] = ' ';
                    }
                }
                codeLocations[x, y] = 'M';
            }
        }
        else
        {
            //get medium building
            if (!checkFit(x, y, new Vector2(3, 3)))
            {
                determineBuilding(x, y);
            }
            else
            {
                for (int i = x; i < x + 3; i++)
                {
                    for (int j = y; j < y + 3; j++)
                    {
                        codeLocations[i, j] = ' ';
                    }
                }
                codeLocations[x, y] = 'L';
            }
        }
    }

    private bool checkFit(int x, int y, Vector2 buildingSize)
    {
        for (int i = x; i < x + buildingSize.x; i++)
        {
            for (int j = y; j < y + buildingSize.y; j++)
            {
                if (codeLocations[i, j] != '\0')
                    return false;
            }
        }
        return true;
    }




}
