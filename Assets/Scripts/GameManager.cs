using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get { return instance_; } }

    public GameObject[] Player { get => player; set => player = value; }
    public BoxCollider ClickProtection { get => clickProtection; set => clickProtection = value; }

    private static GameManager instance_;

    public static int boardSize;
    public static int num_Players;
    public int playerTurn = 0;
    public int firstRound;
    public GameObject obj_wCube, obj_bCube, obj_Samurai;
    public InputField charName;
    public TextMeshProUGUI tmp_turn, tmp_p1, tmp_p2;
    public PauseManager pauseManager;
    GameObject[] player = new GameObject[2];

    int turnCount = 3;
    BoxCollider clickProtection;

    void Awake()
    {

        playerTurn = 0;
        instance_ = this;
        firstRound = num_Players;
    }

    void Start()
    {
        if (boardSize > 128) boardSize = 128;
        else if (boardSize < 6) boardSize = 6;
        StartCoroutine(GenerateBoard(boardSize));
    }

    public void PopulatePlayerVariables(string name, int health, int attack)
    {
        ClickProtection.enabled = false;
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
            ClickProtection.enabled = true;
        }
        else if (playerTurn == 1)
        {
            Player[playerTurn].transform.rotation = Quaternion.Euler(0, 180, 0);
            ClickProtection.enabled = false;
            UpdateHealth();
        }
        EndTurn();
    }

    void UpdateHealth()
    {
        tmp_p1.text = Player[0].GetComponent<Player>().charName + "\nHealth: " + Player[0].GetComponent<Player>().health;
        tmp_p2.text = Player[1].GetComponent<Player>().charName + "\nHealth: " + Player[1].GetComponent<Player>().health;
    }

    public void Move(Vector3 pos, int id, int rot)
    {
        StartCoroutine(Move(Player[playerTurn], new Vector3(pos.x, 0.975f, pos.z)));
        Player[playerTurn].transform.rotation = Quaternion.Euler(0, rot, 0);
        Player[playerTurn].GetComponent<Player>().idPosition = id;
        if (turnCount <= 1)
        {
            turnCount = 3;
            Debug.Log(turnCount);
            EndTurn();
        }
        else
        {
            turnCount--;
        }

        tmp_turn.text = "Moves: " + turnCount;
    }

    IEnumerator Move(GameObject player, Vector3 pos)
    {
        ClickProtection.enabled = true;
        while (player.transform.position != pos)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, pos, .3f);
            yield return new WaitForSeconds(.01f);
        }
        ClickProtection.enabled = false;
        TryBattle();
    }

    IEnumerator GenerateBoard(int boardSize)
    {
        GameObject cube = obj_wCube;
        float size = GameManager.boardSize / 2;
        float originX = size * -1 + 0.5f;
        float x = originX;
        float originZ = size * -1 + 0.5f;
        float z = originZ;
        float generateSpeed;

        if (GameManager.boardSize == 16) generateSpeed = .005f;
        else if (GameManager.boardSize == 32) generateSpeed = .001f;
        else if (GameManager.boardSize == 64) generateSpeed = .0001f;
        else generateSpeed = .01f;



        GameObject s = GameObject.CreatePrimitive(PrimitiveType.Cube);
        s.transform.position = new Vector3(0, .5f, 0);
        s.transform.localScale = new Vector3(GameManager.boardSize + 0.3f, 1, GameManager.boardSize + 0.3f);
        s.GetComponent<MeshRenderer>().enabled = false;
        ClickProtection = s.GetComponent<BoxCollider>();

        for (int i = 1; i <= Mathf.Pow(boardSize, 2); i++)
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
        boardBase.transform.localScale = new Vector3(GameManager.boardSize + 0.3f, 1, GameManager.boardSize + 0.3f);
    }

    void TryBattle()
    {
        if (Player[0].GetComponent<Player>().idPosition == Player[1].GetComponent<Player>().idPosition + 1 ||
        Player[0].GetComponent<Player>().idPosition == Player[1].GetComponent<Player>().idPosition - 1 ||
        Player[0].GetComponent<Player>().idPosition == Player[1].GetComponent<Player>().idPosition + boardSize ||
        Player[0].GetComponent<Player>().idPosition == Player[1].GetComponent<Player>().idPosition - boardSize)
        {
            Battle();
            return;
        }
    }

    void Battle()
    {
        int[] p1Dice = new int[3];
        int[] p2Dice = new int[3];

        for (int i = 0; i < 3; i++)
        {
            p1Dice[i] = Random.Range(1, 7);
            p2Dice[i] = Random.Range(1, 7);
        }
            OrgnanizeDices(p1Dice);
            OrgnanizeDices(p2Dice);

        CompareDices(p1Dice, p2Dice);
    }

    void CompareDices(int[] p1DiceArray, int[] p2DiceArray)
    {
        int score = 0;
        string p1_DicesText = player[0].GetComponent<Player>().charName + "\n";
        string p2_DicesText = player[1].GetComponent<Player>().charName + "\n";
        for (int i = 0; i < 3; i++)
        {
            if (p1DiceArray[i] > p2DiceArray[i])
            {
                p1_DicesText += "<color=green> " + p1DiceArray[i] + " </color>";
                p2_DicesText += "<color=red> " + p2DiceArray[i] + " </color>";
                score++;
            }
            else if (p1DiceArray[i] < p2DiceArray[i])
            {
                p1_DicesText += "<color=red> " + p1DiceArray[i] + " </color>";
                p2_DicesText += "<color=green> " + p2DiceArray[i] + " </color>";
                score--;
            }
            else if (p1DiceArray[i] == p2DiceArray[i])
            {
                if (playerTurn == 0)
                {
                    p1_DicesText += "<color=green> " + p1DiceArray[i] + " </color>";
                    p2_DicesText += "<color=red> " + p2DiceArray[i] + " </color>";
                    score++;
                }
                else
                {
                    p1_DicesText += "<color=red> " + p1DiceArray[i] + " </color>";
                    p2_DicesText += "<color=green> " + p2DiceArray[i] + " </color>";
                    score--;
                }
            }
        }

        if (score > 0)
        {
            player[1].GetComponent<Player>().health -= player[0].GetComponent<Player>().attack;
            pauseManager.BattleScreen(player[0].GetComponent<Player>().charName, p1_DicesText, p2_DicesText);
        }
        else
        {
            player[0].GetComponent<Player>().health -= player[1].GetComponent<Player>().attack;
            pauseManager.BattleScreen(player[1].GetComponent<Player>().charName, p1_DicesText, p2_DicesText);
        }
        UpdateHealth();
    }

    int[] OrgnanizeDices(int[] diceArray)
    {
        for (int i = 0; i <= diceArray.Length - 1; i++)
        {
            for (int j = 0; j < diceArray.Length - 1; j++)
            {
                if (diceArray[j] < diceArray[j + 1])
                {
                    int temp = diceArray[j];
                    diceArray[j] = diceArray[j + 1];
                    diceArray[j + 1] = temp;
                }
            }
        }
        return diceArray;
    }
        void EndTurn()
        {
            if (firstRound != 0)
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

        public void CheckVictory()
        {
            if (Player[0].GetComponent<Player>().health <= 0)
            {
                Debug.Log("Player 2 ganhou");
                pauseManager.VictoryScreen(player[1].GetComponent<Player>().charName);
                ClickProtection.enabled = true;
            }
            else if (Player[1].GetComponent<Player>().health <= 0)
            {
                Debug.Log("Player 1 ganhou");
                pauseManager.VictoryScreen(player[0].GetComponent<Player>().charName);
                ClickProtection.enabled = true;
            }
        }

    }
