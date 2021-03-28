using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get { return instance_; } }
    private static GameManager instance_;
 
    public static int boardSize;
    public static int num_Players;
    public int playerTurn = 1;
    public int firstRound;
    public GameObject w_Cube, b_Cube, tkn_Samurai;
    public InputField charName;
    public PauseManager pauseManager;
    
    GameObject obj_Player1, obj_Player2;

    //player 1
    public string p1_Name;
    public int p1_Health;
    public int p1_Attack;

    //player 2
    public string p2_Name;
    public int p2_Health;
    public int p2_Attack;

    void Awake()
    {
        instance_ = this;
        firstRound = num_Players;
    }

    void Start()
    {
        if (boardSize > 128) boardSize = 128;
        else if(boardSize < 6) boardSize = 6;
        GenerateBoard(boardSize);
    }


    public void PopulatePlayerVariables(string name, int health, int attack)
    {
        if (playerTurn == 1) 
        {
            p1_Name = name;
            p1_Health = health;
            p1_Attack = attack;
        }
        else if (playerTurn == 2)
        {
            p2_Name = name;
            p2_Health = health;
            p2_Attack = attack;
        }
    }

    public void ChooseSpawn(Vector3 pos)
    {
        if (playerTurn == 1)
        {
            obj_Player1 = Instantiate(tkn_Samurai, new Vector3(pos.x, 0.975f, pos.z), Quaternion.identity);
            ChangeTurn();
        }
        else if (playerTurn == 2)
        {
            obj_Player2 = Instantiate(tkn_Samurai, new Vector3(pos.x, 0.975f, pos.z), Quaternion.identity);
            ChangeTurn();
        }
    }

    void GenerateBoard(int boarSize)
    {
        GameObject cube = w_Cube;
        float size = boardSize / 2;
        float x = size * -1 + 0.5f;
        float z = size * -1 + 0.5f;
        bool dir = true;

        GameObject boardBase = Instantiate(b_Cube, new Vector3(0, -0.1f, 0), Quaternion.identity);
        boardBase.gameObject.GetComponent<BoxCollider>().enabled = false;
        boardBase.transform.localScale = new Vector3(boardSize + 0.3f, 1, boardSize  + 0.3f);


        for (int i = 1; i <= Mathf.Pow(boarSize, 2); i++)
        {

            GameObject tile = Instantiate(cube, new Vector3(x, 0, z), Quaternion.identity);
            tile.GetComponent<Tiles>().id = i;

            if (dir)
            {
                if (x < size - 0.5f)
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

            if (cube == w_Cube) cube = b_Cube;
            else if (cube == b_Cube) cube = w_Cube;
        }
    }

    void ChangeTurn()
    {
        if (firstRound >= 1)
        {
            firstRound--;
            if (playerTurn == 1)
            {
                pauseManager.ChooseRolePlayer2();
            }
        }

        if (playerTurn == 1) playerTurn = 2;
        else if (playerTurn == 2) playerTurn = 1;
    }

}
