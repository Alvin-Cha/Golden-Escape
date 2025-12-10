using UnityEngine;
using UnityEngine.UI;

public class giant_movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 20f;
    public float maxSpeed = 30f;
    public float acceleration = 0.4f;
    public float laneDistance = 2f;
    public float moveSpeed = 8f;
    public bool reverseControls = false;

    [Header("UI")]
    public Image speedBar;

    private int currentLane = 1;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        // Normal forward movement
        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;

        float targetX = (currentLane - 1) * laneDistance;
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);

        if (speedBar != null)
        {
            speedBar.fillAmount = speed / maxSpeed;
        }
    }

    void FixedUpdate()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
    }

    //-----------------------------------------
    // REDUCE SPEED FROM SKILL 2
    //-----------------------------------------
    public void ReduceSpeed(float amount)
    {
        speed -= amount;
        if (speed < 0f) speed = 0f;
    }
}
