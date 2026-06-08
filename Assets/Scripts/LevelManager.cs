using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("=== Level Settings ===")]
    public int killsRequired = 10;
    public PortalGun portalGun;

    private int currentKills = 0;

    void Awake()
    {
        Instance = this;
    }

    public void OnEnemyKilled()
    {
        currentKills++;
        Debug.Log($"Kills: {currentKills}/{killsRequired}");

        if (currentKills >= killsRequired)
        {
            portalGun.Charge();
        }
    }
}