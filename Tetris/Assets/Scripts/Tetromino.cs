using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    float fall = 0;
    public float fallSpeed = 1;

    public bool allowRotation = true;
    public bool limitRotation = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
    }
    void CheckUserInput()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            /*if(CheckIsValidPosition())
            {

            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }*/
            if(CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W))
        {
            if(allowRotation)
            {
                if(limitRotation)
                {
                    if(transform.rotation.eulerAngles.z>=90)
                    {
                        transform.Rotate(0,0,-90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
                if(CheckIsValidPosition())
                {
                    FindObjectOfType<Game>().UpdateGrid(this);
                }
                else
                {
                    if(limitRotation)
                    {
                        if(transform.rotation.eulerAngles.z>=90)
                        {
                            transform.Rotate(0, 0, -90);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    
                }
            }
            
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.S)||(Time.time-fall >=fallSpeed))
        {
            transform.position += new Vector3(0, -1, 0);
            fall = Time.time;
            if(CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                enabled = false;
                FindObjectOfType<Game>().SpawnNextTetromino();
            }
        }
    }
    bool CheckIsValidPosition()
    {
        foreach (Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<Game>().Round(mino.position);
            if(FindObjectOfType<Game>().CheckIsInsidegrid(pos) == false)
            {
                return false;
            }
            if(FindObjectOfType<Game>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<Game>().GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}
