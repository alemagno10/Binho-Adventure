using UnityEngine;
using UnityEngine.UI; 
using TMPro;  

public class HealthDisplay : MonoBehaviour {
    public Entity player; 
    public TextMeshProUGUI healthText;  

    void Update(){
        if (player != null && healthText != null){
            float hp = player.GetHP() > 0 ? player.GetHP() : 0;
            healthText.text = "HP: " + hp.ToString() + " / " + player.GetTotalHp().ToString();
            if (player.GetHP() <= 10){
                healthText.color = Color.red;
            } else {
                healthText.color = Color.white;
            }
        }
    }
}
