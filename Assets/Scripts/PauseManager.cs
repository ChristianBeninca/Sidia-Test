using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{

    public GameObject chooseRole, battleScreen, victoryScreen;
    public TextMeshProUGUI playerShow, battleShow, victoryShow, p1_Dices, p2_Dices;
    public InputField charName;
    int health;
    int attack;

    public void Awake()
    {
        chooseRole.SetActive(true);
        playerShow.text = "Player 1";
    }

    public void ChooseRoleInteraction(string selection)
    {
        switch (selection)
        {
            case "Samurai":
                health = 50;
                attack = 3;
                break;
            case "Ronin":
                health = 40;
                attack = 4;
                break;
            case "Ninja":
                health = 30;
                attack = 5;
                break;
        }
        if (!string.IsNullOrEmpty(charName.text) )
        {
            GameManager.instance.PopulatePlayerVariables(charName.text, health, attack);
            chooseRole.SetActive(false);
        }
    }
    public void EndBattle()
    {
        GameManager.instance.ClickProtection.enabled = false;
        battleScreen.SetActive(false);
        GameManager.instance.CheckVictory();
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    } 
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    } 

    public void ChooseRolePlayer2()
    {
        GameManager.instance.ClickProtection.enabled = true;
        chooseRole.SetActive(true);
        playerShow.text = "Player 2";
    }

    public void BattleScreen(string winner, string p1Dices, string p2Dices)
    {
        GameManager.instance.ClickProtection.enabled = true;
        battleScreen.SetActive(true);
        battleShow.text = winner + " won this Fight";
        p1_Dices.text = p1Dices;
        p2_Dices.text = p2Dices;
    }

    public void VictoryScreen(string winner)
    {
        GameManager.instance.ClickProtection.enabled = true;
        victoryScreen.SetActive(true);
        victoryShow.text = "Congratulations " + winner;
    }
}
