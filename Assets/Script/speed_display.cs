using UnityEngine;
using UnityEngine.UI;

public class speed_display : MonoBehaviour
{
    [Header("References")]
    public GameObject girl;      // assign Girl prefab (with player_movement)
    public GameObject giant;     // assign Giant prefab (with giant_movement)
    public Text girlSpeedText;   // assign Text UI for girl
    public Text giantSpeedText;  // assign Text UI for giant

    void Update()
    {
        if (girl != null)
        {
            var girlScript = girl.GetComponent<player_movement>();
            if (girlScript != null)
                girlSpeedText.text = girlScript.speed.ToString("F2") + " km/h";
        }

        if (giant != null)
        {
            var giantScript = giant.GetComponent<giant_movement>();
            if (giantScript != null)
                giantSpeedText.text = giantScript.speed.ToString("F2") + " km/h";
        }
    }
}
