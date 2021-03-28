using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{

    public GameObject chooseRole;
    public TextMeshProUGUI playerShow;
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
        if (!string.IsNullOrEmpty(charName.text))
        {
            GameManager.instance.PopulatePlayerVariables(charName.text, health, attack);
            chooseRole.SetActive(false);
        }
    }

    public void ChooseRolePlayer2()
    {
        chooseRole.SetActive(true);
        playerShow.text = "Player 2";
    }
}
