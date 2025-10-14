using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class skill_button : MonoBehaviour
{
    public player_movement playerMove;
    public Transform girlTransform;
    public Transform giantTransform;

    public float reverseDuration = 3f;
    private bool isReversed = false;

    public Button skill2Button;
    private Color defaultColor; 
    private bool playerInGrab = false;

    void Start()
    {
        if (skill2Button != null)
            defaultColor = skill2Button.image.color;
    }

    void Update()
    {
        if (skill2Button != null)
            skill2Button.image.color = playerInGrab ? Color.green : defaultColor;
    }

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

    // Skill 2
    public void Skill2()
    {
        if (playerInGrab)
        {
            Debug.Log("Skill 2 activated while in grab range!");

            if (girlTransform != null && giantTransform != null)
            {
                game_data.SavePositions(girlTransform, giantTransform);
            }

            SceneManager.LoadScene("grab_scene");
        }
        else
        {
            Debug.Log("Skill 2 failed — not in grab range.");
        }
    }

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
