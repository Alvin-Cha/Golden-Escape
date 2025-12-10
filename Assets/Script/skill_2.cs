using UnityEngine;
using UnityEngine.SceneManagement;

public class skill_2 : MonoBehaviour
{
    public skill_button skillManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player") && skillManager != null)
        {
            skillManager.SetPlayerInGrab(true);
            SceneManager.LoadScene("solo_grab_scene");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player") && skillManager != null)
        {
            skillManager.SetPlayerInGrab(false);
        }
    }
}
