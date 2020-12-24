using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static int gridHeight = 20;
    public static int gridWidth =10;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    public int[] scoreLine =new int[5] {0, 40, 100, 300, 1200};
    public int numberOfRowsThisTurn = 0;
    public Text hud_score;
    private int currentScore=0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNextTetromino();
    }
    void Update() 
    {
        UpdateScore();
        UpdateUI();
        //Debug.Log(currentScore);
    }
    public void UpdateUI()
    {
        hud_score.text = currentScore.ToString();
    }
    public void UpdateScore()
    {
        if(numberOfRowsThisTurn>0)
        {
            switch(numberOfRowsThisTurn)
            {
                case 1:
                    //Debug.Log("Cleared");
                    ClearedOne();
                    break;
                case 2:
                    //Debug.Log("Cleared");
                    ClearedTwo();
                    break;
                case 3:
                    ClearedThree();
                    break;
                case 4:
                    ClearedFour();
                    break;
            }
            
        }
        numberOfRowsThisTurn = 0;
    }
    public void ClearedOne()
    {
        currentScore += scoreLine[1];
    }
    public void ClearedTwo()
    {
        currentScore += scoreLine[2];
    }
    public void ClearedThree()
    {
        currentScore += scoreLine[3];
    }
    public void ClearedFour()
    {
        currentScore += scoreLine[4];
    }
    public bool IsFullRowAt(int y)
    {
        for(int x = 0; x<gridWidth; ++x)
        {
            if(grid[x, y] == null)
            {
                return false;
            }
        }
        //since we found a full row, we will increment full row variable
        numberOfRowsThisTurn += 1;
        //Debug.Log(numberOfRowsThisTurn);
        return true;
    }
    public void DeleteMinoAt(int y)
    {
        for(int x = 0; x<gridWidth; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    
    public void MoveRowDown(int y)
    {
        for(int x = 0; x<gridWidth; ++x)
        {
            if(grid[x, y] !=  null)
            {
                grid[x, y-1] = grid[x, y];
                grid[x,y] = null;
                grid[x, y-1].position += new Vector3(0, -1, 0); 
            }
        }
    }
    public void moveAllRowsDown(int y)
    {
        for(int i=y; i<gridHeight; ++i)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow()
    {
        for(int y =0; y<gridHeight; ++y)
        {
            if(IsFullRowAt(y))
            {
                DeleteMinoAt(y);
                moveAllRowsDown(y + 1);
                y--;
            }
        }
    }

    public bool CheckIsAboveGrid(Tetromino tetromino)
    {
        for(int x=0; x<gridWidth; x++)
        {
            foreach(Transform mino in tetromino.transform)
            {
                Vector2 pos = Round(mino.position);
                if(pos.y > gridHeight-1)
                {
                    return true;
                }

            }
        }
        return false;
    }
    
    public bool CheckIsInsidegrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
    }

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public void SpawnNextTetromino()
    {
        GameObject nextTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), new Vector2(5.0f, 20.0f),  Quaternion.identity);
    }

    private string GetRandomTetromino()
    {
        int randomTetromino = Random.Range(1,8);
        string randomTetrominoName = "Prefabs/Tetromino_T";
        switch(randomTetromino)
        {
        case 1:
            randomTetrominoName = "Prefabs/Tetromino_T";
            break;
        case 2:
            randomTetrominoName = "Prefabs/Tetromino_Long";
            break;
        case 3:
            randomTetrominoName = "Prefabs/Tetromino_Square";
            break;
        case 4:
            randomTetrominoName = "Prefabs/Tetromino_J";
            break;
        case 5:
            randomTetrominoName = "Prefabs/Tetromino_L";
            break;
        case 6:
            randomTetrominoName = "Prefabs/Tetromino_S";
            break;
        case 7:
            randomTetrominoName = "Prefabs/Tetromino_Z";
            break;
        }
        return randomTetrominoName;
    }

    public void UpdateGrid (Tetromino tetromino)
    {
        for(int y=0; y<gridHeight; y++)
        {
            for(int x=0; x<gridWidth; x++)
            {
                if(grid[x,y] != null)
                {
                    if(grid[x, y].parent == tetromino.transform)
                    {
                        grid[x, y]= null;
                    }
                }
            }
        }
        foreach(Transform mino in  tetromino.transform)
        {
            Vector2 pos = Round(mino.position);
            if(pos.y<gridHeight)
            {
                grid[(int)pos.x,(int)pos.y]= mino;
            }
        }
    }

    public Transform GetTransformAtGridPosition(Vector2 pos)
    {
        if(pos.y > gridHeight-1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }


}