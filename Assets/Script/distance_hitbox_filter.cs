using UnityEngine;

public class distance_hitbox_filter : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Only respond if it's the giant
        if (collision.collider.CompareTag("giant"))
        {
            // Do whatever logic you want here
            Debug.Log("Giant touched distance hitbox!");

            // Example: trigger pushback, slow down, etc.
            // giant_movement giant = collision.collider.GetComponent<giant_movement>();
            // if (giant != null) giant.speed -= 1f;
        }
        else
        {
            // Ignore any non-giant collisions (like spikes)
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
