using UnityEngine;

public class collector : MonoBehaviour
{
    public int score = 0;
    public girl_skill girlSkill;

    void Start()
    {
        if (girlSkill == null)
        {
            girlSkill = FindObjectOfType<girl_skill>();
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
        Destroy(other.gameObject);
    }
}
