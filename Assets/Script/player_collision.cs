
using UnityEngine;

public class player_collision : MonoBehaviour{

    public player_movement pm;

    void OnCollisionEnter (Collision info) {
        if(info.collider.tag == "obsticle"){
            pm.enabled = false;
        }
    }
}
