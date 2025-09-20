using UnityEngine;

public class player_movement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10f;
    public float laneDistance = 3f; // distance between lanes
    public float laneChangeSpeed = 10f; // smooth move speed

    private int currentLane = 1; // 0 = left, 1 = middle, 2 = right
    private Vector3 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
    }

    void Update()
    {
        // Forward movement
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);

        // Input for lane change
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane > 0) currentLane--; // move left
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentLane < 2) currentLane++; // move right
        }

        // Update target lane position
        targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
        // Smooth transition to lane
        Vector3 newPos = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * laneChangeSpeed);
        rb.MovePosition(new Vector3(newPos.x, transform.position.y, transform.position.z + speed * Time.fixedDeltaTime));
    }
}
