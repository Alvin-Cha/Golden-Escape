using UnityEngine;
using UnityEngine.SceneManagement;

public class two_player_button : MonoBehaviour
{
    public void play_game_duo()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("game_duo");
    }
}
