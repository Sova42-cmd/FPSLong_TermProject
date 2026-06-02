using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public Image healthFill;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        healthFill.fillAmount = enemyHealth.health / enemyHealth.maxHealth;
        transform.rotation = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f);
    }
}