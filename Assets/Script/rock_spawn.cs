using UnityEngine;

public class rock_spawn : MonoBehaviour
{
    public GameObject spike;
    public Transform player;
    public float spawnDistance = 10f;

    public float lane1X = -2f;
    public float lane2X = 0f;
    public float lane3X = 2f;

    public skill_button skillManager;

    void Update()
    {
        if (skillManager == null || skillManager.isCoolDown3) return;

        // check which numpad key pressed
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

        // start cooldown if any spike was spawned
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

        Instantiate(spike, pos, Quaternion.identity);
    }
}
