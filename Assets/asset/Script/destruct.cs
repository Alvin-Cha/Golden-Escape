using UnityEngine;

public class destruct : MonoBehaviour
{
    public lives lv;
    public GameObject destroyed_version;
    public int hp_update = 3;
    public float min_force = 5f;
    public float max_force = 12f;  
    public float upward_bias = 0.5f; 

    void OnCollisionEnter(Collision info)
    {
        if (info.collider.CompareTag("player"))
        {
            lv.take_damage(10);

            GameObject newRock = Instantiate(destroyed_version, transform.position, transform.rotation);

            Collider[] rockColliders = newRock.GetComponentsInChildren<Collider>();
            Collider playerCollider = info.collider;

            foreach (Collider rockCol in rockColliders)
            {
                Physics.IgnoreCollision(rockCol, playerCollider);
            }

            Rigidbody[] rbs = newRock.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rbs)
            {
                Vector3 random_dir = Random.insideUnitSphere + Vector3.up * upward_bias;
                random_dir.Normalize();

                float force = Random.Range(min_force, max_force);

                rb.AddForce(random_dir * force, ForceMode.Impulse);
            }

            camera_shake camShake = Camera.main.GetComponent<camera_shake>();
            if (camShake != null)
            {
                StartCoroutine(camShake.Shake(0.3f, 0.3f));
            }

            Destroy(gameObject);
        }
    }
}
