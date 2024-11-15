using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoReloadRevolver : MonoBehaviour
{
    public static AmmoReloadRevolver instance;
    public Text municioTextRevolver;
    public AudioSource reloadSound;
    public int comptadorBalesRevolver = 0;
    public int maxBalesRevolver = 18;
    public int balesPerCarregadorRevolver = 6;

    public void Start()
    {
        instance = this;

        comptadorBalesRevolver = balesPerCarregadorRevolver;
        municioTextRevolver.text = balesPerCarregadorRevolver + "/" + maxBalesRevolver;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && WeaponSwitcher.instance.currentWeaponIndex == 1)
        {
            reloadSound.Play();
            ReloadRevolver();
        }
    }

    public void ReloadRevolver()
    {
        if (comptadorBalesRevolver < balesPerCarregadorRevolver)
        {
            int balesToReloadRevolver = balesPerCarregadorRevolver - comptadorBalesRevolver;

            if (maxBalesRevolver >= balesToReloadRevolver)
            {
                comptadorBalesRevolver = balesPerCarregadorRevolver;
                maxBalesRevolver -= balesToReloadRevolver;
            }
            else
            {
                comptadorBalesRevolver += maxBalesRevolver;
                maxBalesRevolver = 0;
            }
            municioTextRevolver.text = comptadorBalesRevolver + "/" + maxBalesRevolver;
        }
    }
}
