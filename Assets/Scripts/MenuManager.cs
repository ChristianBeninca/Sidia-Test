using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu, startMenu, optionsMenu;
    public TMP_Dropdown dp_Rival, dp_Board;

    private void Awake()
    {
        ButtonInteraction("Main");
    }

    public void ButtonInteraction(string selection)
    {
        switch (selection)
        {
            case "Play":
                mainMenu.SetActive(false);
                optionsMenu.SetActive(false);
                startMenu.SetActive(true);
                break;
            case "Settings":
                mainMenu.SetActive(false);
                startMenu.SetActive(false);
                optionsMenu.SetActive(true);
                break;
            case "Main":
                mainMenu.SetActive(true);
                startMenu.SetActive(false);
                optionsMenu.SetActive(false);
                break;
            case "Start":
                switch (dp_Board.value)
                {
                    case 0:
                        GameManager.boardSize = 8;
                        break;
                    case 1:
                        GameManager.boardSize = 16;
                        break;
                    case 2:
                        GameManager.boardSize = 32;
                        break;
                    case 3:
                        GameManager.boardSize = 64;
                        break;
                }
                GameManager.num_Players = dp_Rival.value + 1;
                if (GameManager.num_Players >= 2)
                {
                    SceneManager.LoadScene(1);
                }
                break;
            case "Quit":
                Application.Quit();
                break;
            default:
                Debug.LogWarning("Função não atribuida ao botão");
                break;
        }
    }
}
