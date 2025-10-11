using UnityEngine.UI;
using UnityEngine;

public class lives : MonoBehaviour
{
    public int hp = 100;
    public Text hp_info;

    void Start(){
        update_hp_text();
    }

    public void take_damage(int amount){
        hp = hp - amount;
        update_hp_text();
    }

    void update_hp_text(){
        hp_info.text = "HP:" + hp;
    }

}
