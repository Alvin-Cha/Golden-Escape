using UnityEngine;

public class skill_2 : MonoBehaviour
{
    public skill_button skillManager; 
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player") && skillManager != null)
        {
            skillManager.SetPlayerInGrab(true); // player entered
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player") && skillManager != null)
        {
            skillManager.SetPlayerInGrab(false); // player exited
        }
    }
}
