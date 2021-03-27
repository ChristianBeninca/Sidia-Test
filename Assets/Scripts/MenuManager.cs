using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ButtonInteraction(string selection)
    {
        switch (selection)
        {
            case "Start":
                SceneManager.LoadScene(1);
                break;
            case "Quit":
                Application.Quit();
                break;
            case "Options":
                Debug.LogWarning("Menu de opções em desenvolvimento");
                break;
            case "Reserva":
                break;
            default:
                Debug.LogWarning("Função não atribuida ao botão");
                break;
        }
    }
}
