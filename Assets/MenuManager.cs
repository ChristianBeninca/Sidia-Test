using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance { get { return instance_; } }
    private static MenuManager instance_;

    void Awake()
    {
        instance_ = this;
    }

    public void ButtonInteraction(string a)
    {
        switch (a)
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
