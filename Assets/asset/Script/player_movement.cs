using UnityEngine;

public class player_movement : MonoBehaviour
{
    public float speed = 20f;
    public float maxSpeed = 30f;
    public float acceleration = 0.1f;

    public float laneDistance = 2f;
    public float moveSpeed = 8f;
    public bool reverseControls = false;

    private int currentLane = 1; 
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;

        int direction = 0;
        if (Input.GetKeyDown(KeyCode.A)) direction = reverseControls ? 1 : -1;
        if (Input.GetKeyDown(KeyCode.D)) direction = reverseControls ? -1 : 1;

        currentLane = Mathf.Clamp(currentLane + direction, 0, 2);

        float targetX = (currentLane - 1) * laneDistance;
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
    }
}
