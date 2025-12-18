using UnityEngine;
using UnityEngine.SceneManagement;

public class link_button : MonoBehaviour
{
    public void OpenGitHub()
    {
        Application.OpenURL("https://github.com/Alvin-Cha");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
