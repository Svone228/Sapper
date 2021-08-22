using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FieldScript : MonoBehaviour
{
    public Difficulty difficulty;
    public int height;
    public int width;
    public int minesCount;
    Tile[][] tilesMatrix;
    List<Tile> TilesList;
    public GameObject Cell;
    new RectTransform transform;
    const int cellSize = 30;
    List<Vector2Int> positionsAround;
    private void Awake()
    {
        transform = GetComponent<RectTransform>();
        CreatePositionsAround();

    }
    void CreatePositionsAround() 
    {
        positionsAround = new List<Vector2Int>();
        positionsAround.Add(Vector2Int.left);
        positionsAround.Add(Vector2Int.up);
        positionsAround.Add(Vector2Int.right);
        positionsAround.Add(Vector2Int.down);
        positionsAround.Add(Vector2Int.right + Vector2Int.down);
        positionsAround.Add(Vector2Int.right + Vector2Int.up);
        positionsAround.Add(Vector2Int.left + Vector2Int.down);
        positionsAround.Add(Vector2Int.left + Vector2Int.up);
    }
    void Start()
    {
        PressStartButton();
    }
    public void PressStartButton() 
    {
        ObjectManager.GetSmile().text = ":)";
        DeleteAllChilds(transform);
        SetDifficulty();
        ObjectManager.GetInterfaceHandler().SetMinesCount(minesCount);
        SetFieldSize();
        CreateTilesMatrix();
        CreateField();
        GenerateBoms();
    }
    void SetDifficulty() 
    {
        if (difficulty == Difficulty.Begginer)
        {
            height = 10;
            width = 9;
            minesCount = 10;
        }
        if (difficulty == Difficulty.Amauter)
        {
            height = 16;
            width = 16;
            minesCount = 40;
        }
        if (difficulty == Difficulty.Pro)
        {
            width = 29;
            height = 16;
            minesCount = 99;
        }
    }
    void DeleteAllChilds(Transform transform) 
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
    void SetFieldSize() 
    {
        transform.sizeDelta = new Vector2(width * cellSize, height * cellSize);
        var transform1 = (RectTransform) transform.parent;
        float scale1 = transform1.sizeDelta.y / transform.sizeDelta.y;
        float scale2 = transform1.sizeDelta.x / transform.sizeDelta.x;
        if(scale1 < scale2) 
        {
            transform.localScale = new Vector3(scale1, scale1, 1);
        }
        else 
        {
            transform.localScale = new Vector3(scale2, scale2, 1);
        }
        transform.anchoredPosition = new Vector2(0, 0);
    }
    void CreateTilesMatrix() 
    {
        tilesMatrix = new Tile[width][];
        for (int i = 0; i < tilesMatrix.Length; i++)
        {
            tilesMatrix[i] = new Tile[height];
        }
    }
    void CreateField() 
    {
        TilesList = new List<Tile>();
        GameObject temp;
        for (int j = 0; j < width; j++)
        {
            for (int i = 0; i < height; i++)
            {
                temp = Instantiate(Cell, transform);
                temp.GetComponent<Tile>().position = new Vector2Int(j, i);
                tilesMatrix[j][i] = temp.GetComponent<Tile>();
                tilesMatrix[j][i].mainScript = this;
                TilesList.Add(temp.GetComponent<Tile>());
            }
        }
    }

    void GenerateBoms() 
    {
        for (int i = 0; i < minesCount; i++)
        {
            var position = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            if (!tilesMatrix[position.x][position.y].IsPlanted())
                tilesMatrix[position.x][position.y].SetBomb();
            else
                i--;
        }
    }



    public void OpenTile(Vector2Int position) 
    {
        if (tilesMatrix[position.x][position.y].IsPlanted())
            Lose();
        else if (CountMines(tilesMatrix[position.x][position.y]) == 0)
        {
            if (!tilesMatrix[position.x][position.y].opened)
            {
                tilesMatrix[position.x][position.y].OpenTile();
                OpenAround(tilesMatrix[position.x][position.y]);
            }
        }
        else
        {
            tilesMatrix[position.x][position.y].OpenTile(CountMines(tilesMatrix[position.x][position.y]));
        }
    }
    public void ShowAllTiles() 
    {
        for (int i = 0; i < TilesList.Count; i++)
        {
            TilesList[i].OpenTile(CountMines(TilesList[i]));
        }
    }
    public void OpenAround(Tile tile) 
    {
        for (int i = 0; i < positionsAround.Count; i++)
        {
            Vector2Int temp = tile.position + positionsAround[i];
            if (CheckCoords(temp) && !tilesMatrix[temp.x][temp.y].opened && !tilesMatrix[temp.x][temp.y].IsFlagged())
            {
                OpenTile(temp);
            }
            else
            {
                continue;
            }
        }
    }


    int CountMines(Tile tile)
    {
        int minesCount = 0;
        for (int i = 0; i < positionsAround.Count; i++)
        {
            Vector2Int temp = tile.position + positionsAround[i];
            if (CheckCoords(temp)) 
            {
                if(tilesMatrix[temp.x][temp.y].IsPlanted())
                    minesCount++;
            }
            else 
            {
                continue;
            }
        }
        return minesCount;
    }
    bool CheckCoords(Vector2Int position) 
    {
        if (position.x < 0 || position.y < 0 || position.x >= width || position.y >= height) 
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public void WinCheck()
    {
        for (int i = 0; i < TilesList.Count; i++)
        {
            if (TilesList[i].IsPlanted() && !TilesList[i].IsFlagged())
            {
                return;
            }
            if (!TilesList[i].IsPlanted() && TilesList[i].IsFlagged())
            {
                return;
            }
        }
        Win();
    }

    void Win() 
    {
        StopGame();
        ShowAllTiles();
        ObjectManager.GetSmile().text = "B)";
        Debug.Log("YOU WIN");
    }
    void Lose() 
    {
        ShowAllTiles();
        ShowBombs();
        StopGame();
        ObjectManager.GetSmile().text = ":(";
        Debug.Log("YOU LOSE");
    }
    void StopGame() 
    {
        for (int i = 0; i < TilesList.Count; i++)
        {
            
            TilesList[i].opened = true;

        }
    }
    void ShowBombs()
    {
        for (int i = 0; i < TilesList.Count; i++)
        {
            if (TilesList[i].IsPlanted())
            {
                TilesList[i].ShowBomb();
            }

        }

    }
    public void DifficultySwitch(int val) 
    {
        difficulty = (Difficulty)val;
    }
}

public enum Difficulty 
{
    Begginer = 0,
    Amauter = 1, 
    Pro = 2, 
    Custom = 3
}
