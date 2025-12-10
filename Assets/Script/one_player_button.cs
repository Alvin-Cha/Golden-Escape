using UnityEngine;
using UnityEngine.SceneManagement;

public class one_player_button : MonoBehaviour
{
    public void play_game_solo()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("game_solo");
    }
}
