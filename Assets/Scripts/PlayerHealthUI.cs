using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image healthFill;

    void Update()
    {
        healthFill.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth;
    }
}