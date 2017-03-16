using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerAmmo : MonoBehaviour {

    public int startingAmmo = 50;
    public int currentAmmo;
    public Slider AmmoSlider;
    public GameObject gunObject;
    private RaycastShoot gun;
     
    
	void Start () {
        gun = gunObject.GetComponent<RaycastShoot>();
        currentAmmo = startingAmmo;
    }
	
	void Update () {
		
	}
    public void AddAmmo(int amount)
    {

        currentAmmo += amount;
        AmmoSlider.value = currentAmmo;
        if (currentAmmo > 0)
        {
            gun.enabled = true;
        }
    }

    public void fireAmmo(int amount)
    {
        currentAmmo -= amount;
        AmmoSlider.value = currentAmmo;

        if(currentAmmo <= 0)
        {
            gun.enabled = false;
        }
    }
}
