using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class skill_button : MonoBehaviour
{
    public player_movement playerMove;  // assign your player
    public float reverseDuration = 3f;
    private bool isReversed = false;

    public Button skill2Button; // assign the Skill 2 button in inspector

    private Color defaultColor;      // original button color
    private bool playerInGrab = false; // is player inside grab?

    void Start()
    {
        if (skill2Button != null)
            defaultColor = skill2Button.image.color; // store original color
    }

    void Update()
    {
        // constantly update Skill 2 button color based on player presence
        if (skill2Button != null)
            skill2Button.image.color = playerInGrab ? Color.green : defaultColor;
    }

    // Called by grab trigger when player enters/exits
    public void SetPlayerInGrab(bool inside)
    {
        playerInGrab = inside;
    }

    // Skill 1
    public void Skill1()
    {
        if (!isReversed)
        {
            Debug.Log("Skill 1 activated: Reverse Controls");
            StartCoroutine(ReverseControls());
        }
    }

    // Skill 2 (manual press, optional)
    public void Skill2()
    {
        Debug.Log("Skill 2 activated!");
        if (skill2Button != null)
            skill2Button.image.color = Color.green;

        // SceneManager.LoadScene("NextSceneName"); // uncomment to change scene
    }

    // Skill 3
    public void Skill3()
    {
        Debug.Log("Skill 3 activated!");
    }

    private IEnumerator ReverseControls()
    {
        isReversed = true;
        playerMove.reverseControls = true;
        yield return new WaitForSeconds(reverseDuration);
        playerMove.reverseControls = false;
        isReversed = false;
        Debug.Log("Controls restored!");
    }
}
