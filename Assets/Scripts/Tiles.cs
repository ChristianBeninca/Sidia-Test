using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    GameObject child;
    public int id;
    int spawnId;
    bool canMove;
    int rot;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(StartScale());
        child = this.transform.GetChild(0).gameObject;
        child.SetActive(false);
    }

    IEnumerator StartScale()
    {
        while (gameObject.transform.localScale != new Vector3(1, 1, 1))
        {
            this.gameObject.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), .1f);
            yield return new WaitForSeconds(.01f);
        }

    }

    private void OnMouseEnter()
    {
        // First Round
        if (GameManager.instance.firstRound > 0) // P1 Turn
        {
            if (GameManager.instance.playerTurn == 0)
            {
                spawnId = GameManager.boardSize * 2;
                if (id <= spawnId)
                {
                    child.GetComponent<Renderer>().material.color = Color.green;
                }
                else
                {
                    child.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            else if (GameManager.instance.playerTurn == 1) // P2 Turn
            {
                spawnId = (GameManager.boardSize * GameManager.boardSize) - (GameManager.boardSize * 2);
                if (id >= spawnId)
                {
                    child.GetComponent<Renderer>().material.color = Color.green;
                }
                else
                {
                    child.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
        // After First Round
        else
        {
            if (GameManager.instance.playerTurn == 0)
            {

                if ((id == GameManager.instance.Player[0].GetComponent<Player>().idPosition - 1 ||
                    id == GameManager.instance.Player[0].GetComponent<Player>().idPosition + 1 ||
                    id == GameManager.instance.Player[0].GetComponent<Player>().idPosition - GameManager.boardSize ||
                    id == GameManager.instance.Player[0].GetComponent<Player>().idPosition + GameManager.boardSize) &&
                    (id != GameManager.instance.Player[1].GetComponent<Player>().idPosition))
                {
                    child.GetComponent<Renderer>().material.color = Color.green;
                    canMove = true;
                }
                else
                {
                    child.GetComponent<Renderer>().material.color = Color.red;
                    canMove = false;
                }
            }
            if (GameManager.instance.playerTurn == 1)
            {

                if ((id == GameManager.instance.Player[1].GetComponent<Player>().idPosition - 1 ||
                    id == GameManager.instance.Player[1].GetComponent<Player>().idPosition + 1 ||
                    id == GameManager.instance.Player[1].GetComponent<Player>().idPosition - GameManager.boardSize ||
                    id == GameManager.instance.Player[1].GetComponent<Player>().idPosition + GameManager.boardSize) &&
                    (id != GameManager.instance.Player[0].GetComponent<Player>().idPosition))
                {
                    child.GetComponent<Renderer>().material.color = Color.green;
                    canMove = true;
                }
                else
                {
                    child.GetComponent<Renderer>().material.color = Color.red;
                    canMove = false;
                }
            }
        }
        child.SetActive(true);
    }

    private void OnMouseExit()
    {
        child.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.firstRound >= 1)
        {
            if (GameManager.instance.playerTurn == 0)
            {
                spawnId = GameManager.boardSize * 2;
                if (id <= spawnId)
                {
                    GameManager.instance.ChooseSpawn(this.transform.position, id);
                }
            }
            else if (GameManager.instance.playerTurn == 1)
            {
                spawnId = (GameManager.boardSize * GameManager.boardSize) - (GameManager.boardSize * 2);
                if (id >= spawnId)
                {
                    GameManager.instance.ChooseSpawn(this.transform.position, id);
                }
            }
        }
        else
        {
            if (canMove)
            {
                if (id == GameManager.instance.Player[GameManager.instance.playerTurn].GetComponent<Player>().idPosition - 1) { rot = -90; }
                else if (id == GameManager.instance.Player[GameManager.instance.playerTurn].GetComponent<Player>().idPosition + 1) { rot = 90; }
                else if (id == GameManager.instance.Player[GameManager.instance.playerTurn].GetComponent<Player>().idPosition - GameManager.boardSize) { rot = 180; }
                else if (id == GameManager.instance.Player[GameManager.instance.playerTurn].GetComponent<Player>().idPosition + GameManager.boardSize) { rot = 0; }

                GameManager.instance.Move(this.transform.position, id, rot);
            }
        }
    }
}
