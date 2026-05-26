using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    //public GameObject MuzzleFlash;
    //public Transform MuzzleFLashPoint;

    public Camera fpsCam;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }   
    }

    private void Shoot()
    {
        RaycastHit hit;
        //GameObject flash = Instantiate(MuzzleFlash, MuzzleFLashPoint.position, MuzzleFLashPoint.rotation);
        //Destroy(flash, 0.05f); 
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
