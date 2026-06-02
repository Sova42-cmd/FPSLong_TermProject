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
     // later

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
    }

    private void Shoot()
    {
        //MuzzleFlash.Play();

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, Range, ~ignoreMask))
        {
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            enemy?.TakeDamage(damage);   

            GameObject ImpactGo = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ImpactGo, 2f);
        }
    }
}