using UnityEngine;

public class WeaponSwtiching : MonoBehaviour
{
    public int SelectedWeapon = 0;
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int PreviousSelectedWeapon = SelectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (SelectedWeapon >= transform.childCount - 1)
                SelectedWeapon = 0; // wrap around to first weapon
            else
                SelectedWeapon++;
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (SelectedWeapon <= 0)
                SelectedWeapon = transform.childCount - 1; // wrap around to last weapon
            else
                SelectedWeapon--;
        }

        if(PreviousSelectedWeapon != SelectedWeapon)
        {
            SelectWeapon();
        }
    }
    void SelectWeapon()
    {
        
        //Debug.Log("Switching to weapon: " + SelectedWeapon);


        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == SelectedWeapon)
                weapon.gameObject.SetActive(true);
            else 
                weapon.gameObject.SetActive(false);    
            i++;
        }
        
    }
}
