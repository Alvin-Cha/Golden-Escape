using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_manager : MonoBehaviour
{
    public GameObject container;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                pause_game();
            else
                resume_game();
        }
    }

    public void resume_button()
    {
        resume_game();
    }

    public void main_menu_button()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    void pause_game()
    {
        container.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    void resume_game()
    {
        container.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
