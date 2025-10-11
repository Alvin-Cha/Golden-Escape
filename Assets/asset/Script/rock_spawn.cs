using UnityEngine;

public class rock_spawn : MonoBehaviour
{
    public GameObject spike;
    public Transform player;  
    public float spawnDistance = 10f; 

    public float lane1X = -2f;
    public float lane2X = 0f;
    public float lane3X = 2f;

    public void lane_1()
    {
        SpawnAt(lane1X);
    }

    public void lane_2()
    {
        SpawnAt(lane2X);
    }

    public void lane_3()
    {
        SpawnAt(lane3X);
    }

    private void SpawnAt(float laneX)
    {
        // Spawn in front of the player
        Vector3 pos = new Vector3(
            laneX,
            player.position.y,
            player.position.z + spawnDistance
        );

        Instantiate(spike, pos, Quaternion.identity);
    }
}
