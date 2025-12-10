using UnityEngine;
using System.Collections;

public class ball_spawner : MonoBehaviour
{
    public GameObject ballPrefab;       // Assign your FBX prefab here
    public Transform spawnOrigin;       // Usually your player or a fixed point in front
    public float laneDistance = 2f;     // Distance between lanes
    public float spawnZOffset = 30f;    // How far ahead the ball spawns (adjust as needed)

    private bool spawning = true;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (spawning)
        {
            float waitTime = Random.Range(1f, 2f);
            yield return new WaitForSeconds(waitTime);

            int lane = Random.Range(0, 3); // 0 = left, 1 = middle, 2 = right
            float laneX = (lane - 1) * laneDistance;

            Vector3 spawnPos = new Vector3(laneX, spawnOrigin.position.y, spawnOrigin.position.z + spawnZOffset);
            Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        }
    }
}
