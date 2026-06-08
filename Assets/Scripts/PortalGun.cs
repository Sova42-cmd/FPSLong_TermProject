using UnityEngine;

public class PortalGun : MonoBehaviour
{
    [Header("=== Portal Gun ===")]
    public Camera fpsCam;
    public float range = 100f;
    public GameObject portalPrefab;
    public LayerMask ignoreMask;

    private bool isCharged = false;
    private bool hasShot = false;

    void Update()
    {
        if (!isCharged || hasShot) return;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Charge()
    {
        isCharged = true;
        Debug.Log("Portal gun charged!");
        // add sfx later
    }

    private void Shoot()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward,
            out RaycastHit hit, range, ~ignoreMask))
        {
            Quaternion rotation = Quaternion.LookRotation(hit.normal);
            Instantiate(portalPrefab, hit.point, rotation);
            hasShot = true;
        }
    }
}