using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    public string nextSceneName = "Level2_Casino";
    
    void Start()
    {
        Debug.Log("portal trigger na etom objetce: " + gameObject.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}