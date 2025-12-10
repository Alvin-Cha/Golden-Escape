using UnityEngine;

public class restore_pos : MonoBehaviour
{
    public Transform girl;
    public Transform giant;
    public player_movement girlMove;
    public player_movement giantMove;

    [Header("Giant Adjustments After Grab")]
    public float giantBackOffset = 300f;     // how much the giant moves backward after restoring
    public float speedPenalty = 5f;        // giant speed = girl speed - this

    void Start()
    {
        if (data_game.HasSavedPosition())
        {
            // Restore girl Z
            Vector3 girlPos = girl.position;
            girlPos.z = data_game.GetGirlZ();
            girl.position = girlPos;

            // Restore giant Z AND move him back a bit
            Vector3 giantPos = giant.position;
            float restoredZ = data_game.GetGiantZ();
            giantPos.z = restoredZ - giantBackOffset;
            giant.position = giantPos;

            if (girlMove != null && giantMove != null)
            {
                giantMove.speed = girlMove.speed - speedPenalty;

                if (giantMove.speed < 1f)
                    giantMove.speed = 1f;
            }

            Debug.Log("Restored positions + adjusted giant.");
        }
    }
}
