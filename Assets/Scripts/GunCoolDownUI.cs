using UnityEngine;
using UnityEngine.UI;

public class GunCoolDownUI : MonoBehaviour
{
    public Gun GunScript;
    public Image CoolDownImage;

    void Update()
    {
        UpdateCooldownUI();
    }
    private void UpdateCooldownUI()
    {
        if (Time.time >= GunScript.nextTimeToFire)
        {
            CoolDownImage.fillAmount = 1f;
        }
        else
        {
            float timeRemaining = GunScript.nextTimeToFire - Time.time;
            
            CoolDownImage.fillAmount = 1f - (timeRemaining / GunScript.fireRate);
        }
    }
}
