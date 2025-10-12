using UnityEngine;

public class giant_movement : MonoBehaviour
{
    public Transform player;     // assign your player here
    public float speed = 10f;
    public float followDistance = 5f; // distance to stay behind player

    void Update()
    {
        if (player == null) return;

        // Calculate target Z position (follow behind the player)
        float targetZ = player.position.z - followDistance;

        // Only move forward if the giant is behind the target
        if (transform.position.z < targetZ)
        {
            float newZ = transform.position.z + speed * Time.deltaTime;

            // Clamp so it never goes past the player
            if (newZ > targetZ) newZ = targetZ;

            transform.position = new Vector3(0f, 0f, newZ);
        }

        // Keep X and Y locked (no drift)
        transform.position = new Vector3(0f, 0f, transform.position.z);
    }
}
