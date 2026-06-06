using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Image dashFill;
    public Image speedLines; 
    //CanvasGroup linesGroup; 

    void Update()
    {
        if (playerMovement.dashCooldown > 0f)
            dashFill.fillAmount = 1f - (playerMovement.dashCooldownTimer / playerMovement.dashCooldown);
        else
            dashFill.fillAmount = 1f;

        speedLines.enabled = playerMovement.isDashing;

       
    }
}