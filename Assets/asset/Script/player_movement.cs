using UnityEngine;

public class player_movement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10f;
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f; 

    private int currentLane = 1;
    private Vector3 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
    }

    void Update()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane > 0) currentLane--;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentLane < 2) currentLane++; 
        }

        targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * laneChangeSpeed);
        rb.MovePosition(new Vector3(newPos.x, transform.position.y, transform.position.z + speed * Time.fixedDeltaTime));
    }
}
