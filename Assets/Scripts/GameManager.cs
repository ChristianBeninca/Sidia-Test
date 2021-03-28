using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get { return instance_; } }

    public GameObject[] Player { get => player; set => player = value; }

    private static GameManager instance_;
 
    public static int boardSize;
    public static int num_Players;
    public int playerTurn = 0;
    public int firstRound;
    public GameObject obj_wCube, obj_bCube, obj_Samurai;
    public InputField charName;
    public TextMeshProUGUI tmp_turn;
    public PauseManager pauseManager;
    GameObject[] player = new GameObject[2];
    public int[] asdasd = new int[2];
    
    int turnCount = 3;
    BoxCollider clickProtection;

    ////player 1
    //public string p1_Name;
    //public int p1_Health;
    //public int p1_Attack;
    //public int p1_Position;

    ////player 2
    //public string p2_Name;
    //public int p2_Health;
    //public int p2_Attack;
    //public int p2_Position;

    void Awake()
    {
        playerTurn = 0;
        instance_ = this;
        firstRound = num_Players;
    }

    void Start()
    {
        if (boardSize > 128) boardSize = 128;
        else if(boardSize < 6) boardSize = 6;
        StartCoroutine(GenerateBoard(boardSize));
    }


    public void PopulatePlayerVariables(string name, int health, int attack)
    {
        clickProtection.enabled = false;
        GameObject pj = Instantiate(obj_Samurai, new Vector3(999, 999, 999), Quaternion.identity);
        Player[playerTurn] = pj;
        Player[playerTurn].GetComponent<Player>().charName = name;
        Player[playerTurn].GetComponent<Player>().health = health;
        Player[playerTurn].GetComponent<Player>().attack = attack;
    }

    public void ChooseSpawn(Vector3 pos, int id)
    {
        Player[playerTurn].transform.position = new Vector3(pos.x, 0.975f, pos.z);
        Player[playerTurn].GetComponent<Player>().idPosition = id;
        if (playerTurn == 0)
        {
            clickProtection.enabled = true;
        }
        else if (playerTurn == 1)
        {
            Player[playerTurn].transform.rotation = Quaternion.Euler(0, 180, 0);
            clickProtection.enabled = false;
        }
        ChangeTurn();
    }

    public void Move(Vector3 pos, int id, int rot)
    {
        StartCoroutine(Move(Player[playerTurn], new Vector3(pos.x, 0.975f, pos.z)));
        Player[playerTurn].transform.rotation = Quaternion.Euler(0, rot, 0);
        Player[playerTurn].GetComponent<Player>().idPosition = id;
        if (turnCount <= 1) { EndTurn(); turnCount = 3; }
        else { turnCount--; }

        tmp_turn.text = "Turn Count: " + turnCount;
    }

    IEnumerator Move(GameObject player, Vector3 pos)
    {
        clickProtection.enabled = true;
        while (player.transform.position != pos)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, pos, .3f);
            yield return new WaitForSeconds(.01f);
        }
        clickProtection.enabled = false;
    }

    IEnumerator GenerateBoard(int boarSize)
    {
        GameObject cube = obj_wCube;
        float size = boardSize / 2;
        float originX = size * -1 + 0.5f;
        float x = originX;
        float originZ = size * -1 + 0.5f;
        float z = originZ;
        float generateSpeed;

        if (boardSize == 16) generateSpeed = .005f;
        else if (boardSize == 32) generateSpeed = .001f;
        else if (boardSize == 64) generateSpeed = .0001f;
        else generateSpeed = .01f;



        GameObject s = GameObject.CreatePrimitive(PrimitiveType.Cube);
        s.transform.position = new Vector3(0, .5f, 0);
        s.transform.localScale = new Vector3(boardSize + 0.3f, 1, boardSize + 0.3f);
        s.GetComponent<MeshRenderer>().enabled = false;
        clickProtection = s.GetComponent<BoxCollider>();

        for (int i = 1; i <= Mathf.Pow(boarSize, 2); i++)
        {

            GameObject tile = Instantiate(cube, new Vector3(x, 0, z), Quaternion.identity);
            tile.GetComponent<Tiles>().id = i;

            if (x < size - 0.5f)
            {
                x++;
            }

            else
            {
                x = originX;
                z++;
                if (cube == obj_wCube) cube = obj_bCube;
                else if (cube == obj_bCube) cube = obj_wCube;
            }

            if (cube == obj_wCube) cube = obj_bCube;
            else if (cube == obj_bCube) cube = obj_wCube;
            yield return new WaitForSeconds(generateSpeed);
        }

        yield return new WaitForSeconds(.5f);
        GameObject boardBase = GameObject.CreatePrimitive(PrimitiveType.Cube);
        boardBase.GetComponent<Renderer>().material.color = Color.black;
        boardBase.transform.position = new Vector3(0, -0.1f, 0);
        boardBase.GetComponent<BoxCollider>().enabled = false;
        boardBase.transform.localScale = new Vector3(boardSize + 0.3f, 1, boardSize + 0.3f);
    }

    void EndTurn()
    {
        if (Player[0].GetComponent<Player>().idPosition == Player[1].GetComponent<Player>().idPosition + 1 ||
                Player[0].GetComponent<Player>().idPosition == Player[1].GetComponent<Player>().idPosition - 1 ||
                Player[0].GetComponent<Player>().idPosition == Player[1].GetComponent<Player>().idPosition + boardSize ||
                Player[0].GetComponent<Player>().idPosition == Player[1].GetComponent<Player>().idPosition - boardSize)
        {
            StartBattle();
        }
        else
        {
            ChangeTurn();
        }
    }

    void StartBattle()
    {
        Debug.Log("battle, hayyyaaaa");
        ChangeTurn();
    }

    void ChangeTurn()
    {
        if (firstRound >= 1)
        {
            firstRound--;
            if (playerTurn == 0)
            {
                pauseManager.ChooseRolePlayer2();
            }
        }

        if (playerTurn == 0) playerTurn = 1;
        else if (playerTurn == 1) playerTurn = 0;
    }

}
