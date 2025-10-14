using UnityEngine;
using UnityEngine.SceneManagement;


public class grab_manager : MonoBehaviour
{
    private bool player_a_pressed = false;
    private bool s_pressed = false;
    private bool player_b_pressed = false;
    private bool down_pressed = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)){
            player_a_pressed = true;
            s_pressed = false;
        }else if (Input.GetKeyDown(KeyCode.S)){
            player_a_pressed = true;
            s_pressed = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)){
            player_b_pressed = true;
            down_pressed = false;
        }else if (Input.GetKeyDown(KeyCode.DownArrow)){
            player_b_pressed = true;
            down_pressed = true;
        }

        if (player_a_pressed && player_b_pressed)
        {
            if ((s_pressed && down_pressed) || (!s_pressed && !down_pressed)){
                SceneManager.LoadScene("Giant_victory_scene");
            }else{
                SceneManager.LoadScene("SampleScene 1");
            }

            player_a_pressed = false;
            player_b_pressed = false;
            s_pressed = false;
            down_pressed = false;
        }
    }
}
