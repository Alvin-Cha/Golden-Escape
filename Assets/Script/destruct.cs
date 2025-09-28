using UnityEngine;

public class destruct : MonoBehaviour
{
    public player_movement pm;
    public GameObject destroyed_version;
    public float minForce = 5f;
    public float maxForce = 12f;  
    public float upwardBias = 0.5f; 

    void OnCollisionEnter(Collision info)
    {
        if (info.collider.CompareTag("player"))
        {
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
                Vector3 randomDir = Random.insideUnitSphere + Vector3.up * upwardBias;
                randomDir.Normalize();

                float force = Random.Range(minForce, maxForce);

                rb.AddForce(randomDir * force, ForceMode.Impulse);
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
