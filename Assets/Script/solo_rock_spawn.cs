using UnityEngine;

public class solo_rock_spawn : MonoBehaviour
{
    public GameObject spike;
    public Transform player;

    // Increase this for further spawning
    public float spawnDistance = 48f;

    public float lane1X = -4.5f;
    public float lane2X = 0f;
    public float lane3X = 12f;

    public solo_skill_manager skillManager;

    private Quaternion spikeRotation = Quaternion.Euler(-89f, 78f, -78f);

    void Update()
    {
        if (skillManager == null)
            return;

        // Spawn immediately when manager signals
        if (skillManager.buttonPressed3)
        {
            skillManager.buttonPressed3 = false;
            TriggerSkill3();
            return;
        }

        // Prevent double-spawning during cooldown
        if (skillManager.isCoolDown3)
            return;
    }

    // Random 3-lane spike pattern
    public void TriggerSkill3()
    {
        int r = Random.Range(1, 4);

        if (r == 1)
        {
            SpawnAt(lane2X);
            SpawnAt(lane3X);
        }
        else if (r == 2)
        {
            SpawnAt(lane1X);
            SpawnAt(lane3X);
        }
        else
        {
            SpawnAt(lane1X);
            SpawnAt(lane2X);
        }
    }

    private void SpawnAt(float laneX)
    {
        if (spike == null || player == null)
            return;

        Vector3 pos = new Vector3(
            laneX,
            player.position.y,
            player.position.z + spawnDistance
        );

        Instantiate(spike, pos, spikeRotation);
    }
}
