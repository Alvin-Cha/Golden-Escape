using UnityEngine; // REQUIRED for Unity types
using System.Collections; // only needed if you use IEnumerator

public class destruct : MonoBehaviour
{
    public GameObject destroyed_version;
    public float min_force = 5f;
    public float max_force = 12f;  
    public float upward_bias = 0.5f; 

    private camera_shake camShake;

    void Start()
    {
        camShake = Camera.main.GetComponent<camera_shake>();
    }

    void OnCollisionEnter(Collision info)
    {
        if (info.collider.CompareTag("player"))
        {
            // Reduce player speed by 1
            player_movement player = info.collider.GetComponent<player_movement>();
            if (player != null)
            {
                player.speed -= 1f;
                if (player.speed < 0f) player.speed = 0f; // clamp to 0
            }

            SpawnDestroyedVersion(info.collider);

            if (camShake != null)
            {
                camShake.TriggerShake(0.5f, 3f);
            }

            Destroy(gameObject);
        }

        // Check for giant
        if (info.collider.CompareTag("giant"))
        {
            SpawnDestroyedVersion(info.collider);
            Destroy(gameObject);
        }
    }

    private void SpawnDestroyedVersion(Collider colliderToIgnore)
    {
        GameObject newRock = Instantiate(destroyed_version, transform.position, transform.rotation);

        Collider[] rockColliders = newRock.GetComponentsInChildren<Collider>();
        foreach (Collider rockCol in rockColliders)
        {
            Physics.IgnoreCollision(rockCol, colliderToIgnore);
        }

        Rigidbody[] rbs = newRock.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            Vector3 random_dir = Random.insideUnitSphere + Vector3.up * upward_bias;
            random_dir.Normalize();

            float force = Random.Range(min_force, max_force);
            rb.AddForce(random_dir * force, ForceMode.Impulse);
        }
    }
}
