using UnityEngine;

public class Gun : MonoBehaviour
{
    public LayerMask ignoreMask;
    public float damage = 10f;
    public float Range = 100f;
    public Camera fpsCam;

    [Header("=== Cooldown ===")]
    public float fireRate = 0.5f;
    public float nextTimeToFire = 0f;

    [Header("=== Recoil ===")]
    public Transform weaponHolder;
    public float recoilBackAmount = 0.05f; 
    public float recoilUpAmount = 0.02f;  
    public float kickSpeed = 30f;        
    public float recoverySpeed = 8f;

    private Vector3 recoilPosTarget = Vector3.zero;
    private Vector3 recoilPosCurrent = Vector3.zero;

    [Header("=== VFX ===")]
    //public ParticleSystem MuzzleFlash;
    public GameObject ImpactEffect;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }

        HandleRecoil();
    }

    private void Shoot()
    {
        // push backward on local Z axis
        recoilPosTarget += new Vector3(0f, 0f, -recoilBackAmount);
        recoilPosTarget += new Vector3(0f, recoilUpAmount, -recoilBackAmount);

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, Range, ~ignoreMask))
        {
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            enemy?.TakeDamage(damage);

            GameObject ImpactGo = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ImpactGo, 2f);
        }

        CameraShake.Instance.Shake();
    }

    private void HandleRecoil()
    {
        // target bleeds back to zero (recovery)
        recoilPosTarget = Vector3.Lerp(recoilPosTarget, Vector3.zero, recoverySpeed * Time.deltaTime);

        // current position chases the target (kick)
        recoilPosCurrent = Vector3.Lerp(recoilPosCurrent, recoilPosTarget, kickSpeed * Time.deltaTime);

        // apply only position, no rotation touched at all
        weaponHolder.localPosition = recoilPosCurrent;
    }
}