using UnityEngine;

public class rock_spawn : MonoBehaviour
{
    public GameObject spike;
    public Transform player;
    public float spawnDistance = 10f;
    public float cooldown = 1f;
    private float spawnTimer = 0f;

    public float lane1X = -2f;
    public float lane2X = 0f;
    public float lane3X = 2f;

    void Update()
    {
        spawnTimer += 1f;

        if (spawnTimer < cooldown * 100f) return;

        bool spawned = false;

        if (Input.GetKey(KeyCode.Keypad1))
        {
            SpawnAt(lane2X);
            SpawnAt(lane3X);
            spawned = true;
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            SpawnAt(lane1X);
            SpawnAt(lane3X);
            spawned = true;
        }
        if (Input.GetKey(KeyCode.Keypad3))
        {
            SpawnAt(lane1X);
            SpawnAt(lane2X);
            spawned = true;
        }

        if (spawned)
            spawnTimer = 0f;
    }

    private void SpawnAt(float laneX)
    {
        Vector3 pos = new Vector3(
            laneX,
            player.position.y,
            player.position.z + spawnDistance
        );

        Instantiate(spike, pos, Quaternion.identity);
    }
}
