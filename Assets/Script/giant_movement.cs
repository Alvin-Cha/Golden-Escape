using UnityEngine;

public class giant_movement : MonoBehaviour
{
    public Transform player;
    public float startSpeed = 20f;
    public float maxSpeed = 30f;
    public float acceleration = 0.2f;
    public float followDistance = 5f;

    private float currentSpeed;

    void Start()
    {
        currentSpeed = startSpeed;
    }

    void Update()
    {
        if (player == null) return;

        currentSpeed += acceleration * Time.deltaTime;
        if (currentSpeed > maxSpeed)
            currentSpeed = maxSpeed;

        float targetZ = player.position.z - followDistance;

        if (transform.position.z < targetZ)
        {
            float newZ = transform.position.z + currentSpeed * Time.deltaTime;

            if (newZ > targetZ)
                newZ = targetZ;

            transform.position = new Vector3(0f, 0f, newZ);
        }

        transform.position = new Vector3(0f, 0f, transform.position.z);
    }
}
