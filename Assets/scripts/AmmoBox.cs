
using UnityEngine;
using UnityEngine.UI;

public class AmmoBox : MonoBehaviour
{
    public int balesAK = 25;
    public int balesRevolver = 6;

    public GameObject bafarada;
    public Text pressFText;
    private bool playerInRange = false;

    private void Start()
    {
        bafarada.SetActive(false);
        pressFText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bafarada.SetActive(true);
            pressFText.gameObject.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (WeaponSwitcher.instance.currentWeaponIndex == 0)
            {
                AmmoReload.instance.comptadorBalesAK = 25;
                AmmoReload.instance.maxBalesAK = 50;
                AmmoReload.instance.municioTextAK.text = AmmoReload.instance.comptadorBalesAK + "/" + AmmoReload.instance.maxBalesAK;
            }
            else if (WeaponSwitcher.instance.currentWeaponIndex == 1)
            {
                AmmoReloadRevolver.instance.comptadorBalesRevolver = 6;
                AmmoReloadRevolver.instance.maxBalesRevolver = 18;
                AmmoReloadRevolver.instance.municioTextRevolver.text = AmmoReloadRevolver.instance.comptadorBalesRevolver + "/" + AmmoReloadRevolver.instance.maxBalesRevolver;
            }
            bafarada.SetActive(false);
            pressFText.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
