using UnityEngine;

public class collector : MonoBehaviour
{
    public int score = 0;
    public girl_skill girlSkill;

    void Start()
    {
        // auto-find girl_skill safely
        if (girlSkill == null)
        {
            girlSkill = FindObjectOfType<girl_skill>();

            if (girlSkill == null)
            {
                Debug.LogError("⚠️ No 'girl_skill' found in scene!");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("ball")) return;

        score++;

        if (girlSkill != null)
        {
            girlSkill.AddEnergyFromBall();
        }
        else
        {
            Debug.LogWarning("⚠️ collector: girlSkill reference missing!");
        }

        Destroy(other.gameObject);
    }
}
