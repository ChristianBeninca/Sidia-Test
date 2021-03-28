using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    GameObject child;
    public int id;
    int spawnId;

    // Start is called before the first frame update
    void Start()
    {
        child = this.transform.GetChild(0).gameObject;
        child.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (GameManager.instance.playerTurn == 1 && GameManager.instance.firstRound >= 1)
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
            child.SetActive(true);
        }
        

        else if (GameManager.instance.playerTurn == 2 && GameManager.instance.firstRound >= 1)
        {
            spawnId = (GameManager.boardSize * GameManager.boardSize) -(GameManager.boardSize * 2);
            if (id >= spawnId)
            {
                child.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                child.GetComponent<Renderer>().material.color = Color.red;
            }
            child.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        child.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.playerTurn == 1 && GameManager.instance.firstRound >= 1) 
        {
            spawnId = GameManager.boardSize * 2;
            if(id <= spawnId)
            {
                GameManager.instance.ChooseSpawn(this.transform.position);
                Debug.Log(this.transform.position);
            }
        }
        else if (GameManager.instance.playerTurn == 2 && GameManager.instance.firstRound >= 1)
        {
            spawnId = (GameManager.boardSize * GameManager.boardSize) - (GameManager.boardSize * 2);
            if (id >= spawnId)
            {
                GameManager.instance.ChooseSpawn(this.transform.position);
                Debug.Log(this.transform.position);
            }
        }
    }
}
