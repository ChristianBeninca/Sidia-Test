using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get { return instance_; } }
    private static GameManager instance_;
    void Awake()
    {
        instance_ = this;
    }

    public int boardSize;
    public GameObject w_Cube, b_Cube;
    // Start is called before the first frame update
    void Start()
    {
        if (boardSize > 128) boardSize = 128;
        else if(boardSize < 6) boardSize = 6;
        GenerateBoard(boardSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateBoard(int boarSize)
    {
        GameObject cube = w_Cube;
        float size = boardSize / 2;
        float x = size * -1 + 0.5f;
        float z = size * -1 + 0.5f;
        bool dir = true;

        for (int i = 0; i < Mathf.Pow(boarSize, 2); i++)
        {

            Instantiate(cube,new Vector3 (x, 0, z), Quaternion.identity);

            if (dir)
            {
                if(x < size - 0.5f)
                {
                    x++;
                }
                else
                {
                    dir = !dir;
                    z++;
                }
            } 
            else if (!dir)
            {
                if (x > -size + 0.5f)
                {
                    x--;
                }
                else
                {
                    dir = !dir;
                    z++;
                }
            }



            if(cube == w_Cube)cube = b_Cube;
            else if(cube == b_Cube)cube = w_Cube;
        }
    }
}
