using UnityEngine;

public class rock_spawn : MonoBehaviour
{
    public GameObject spike;
    public Transform player;
    public float spawnDistance = 10f;

    public float lane1X = -4.5f;
    public float lane2X = 0f;
    public float lane3X = 4.5f;

    public skill_button skillManager;

    // 🔹 The rotation you want for all spikes
    private Quaternion spikeRotation = Quaternion.Euler(-89f, 78f, -78f);

    void Update()
    {
        if (skillManager == null || skillManager.isCoolDown3) return;

        bool spawned = false;

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SpawnAt(lane2X);
            SpawnAt(lane3X);
            spawned = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SpawnAt(lane1X);
            SpawnAt(lane3X);
            spawned = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SpawnAt(lane1X);
            SpawnAt(lane2X);
            spawned = true;
        }

        if (spawned)
        {
            skillManager.UseCooldown3();
        }
    }

    private void SpawnAt(float laneX)
    {
        Vector3 pos = new Vector3(
            laneX,
            player.position.y,
            player.position.z + spawnDistance
        );

        // 🔹 Use your custom rotation here
        Instantiate(spike, pos, spikeRotation);
    }
}
