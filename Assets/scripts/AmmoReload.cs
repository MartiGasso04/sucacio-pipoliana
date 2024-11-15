using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoReload : MonoBehaviour
{
    public static AmmoReload instance;
    public AudioSource reloadSound;
    public Text municioTextAK;
    public int comptadorBalesAK = 0;
    public int maxBalesAK = 50;
    public int balesPerCarregadorAK = 25;

    public void Start()
    {
        instance = this;

        comptadorBalesAK = balesPerCarregadorAK;
        municioTextAK.text = balesPerCarregadorAK + "/" + maxBalesAK;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && WeaponSwitcher.instance.currentWeaponIndex == 0)
        {
            reloadSound.Play();
            ReloadAK();
        }
    }

    public void ReloadAK()
    {

        if (comptadorBalesAK < balesPerCarregadorAK)
        {
            int balesToReloadAK = balesPerCarregadorAK - comptadorBalesAK;

            if (maxBalesAK >= balesToReloadAK)
            {
                comptadorBalesAK = balesPerCarregadorAK;
                maxBalesAK -= balesToReloadAK;
            }
            else
            {
                comptadorBalesAK += maxBalesAK;
                maxBalesAK = 0;
            }
            municioTextAK.text = comptadorBalesAK + "/" + maxBalesAK;
        }
    }
}