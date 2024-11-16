using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBox : MonoBehaviour
{
    public int balesAK = 25;
    public int balesRevolver = 6;

    public GameObject bafarada;
    public Text pressFText;
    private bool playerInRange = false;

    private Light highlightLight;

    public AudioSource soRecarrega;

    private void Start()
    {
        bafarada.SetActive(false);
        pressFText.gameObject.SetActive(false);

        // Añadir un Light al objeto AmmoBox
        highlightLight = gameObject.AddComponent<Light>();
        highlightLight.type = LightType.Point;
        highlightLight.range = 5f; // Ajusta el rango según sea necesario
        highlightLight.intensity = 2f; // Ajusta la intensidad según sea necesario
        highlightLight.color = Color.yellow; // Ajusta el color según sea necesario
        highlightLight.enabled = false; // Desactivar el Light al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bafarada.SetActive(true);
            pressFText.gameObject.SetActive(true);
            playerInRange = true;
            highlightLight.enabled = true; // Activar el Light cuando el jugador está en rango
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            bafarada.SetActive(false);
            pressFText.gameObject.SetActive(false);
            highlightLight.enabled = false; // Desactivar el Light cuando el jugador sale del rango
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {       
            if (WeaponSwitcher.instance.currentWeaponIndex == 0)
            {
                if(AmmoReload.instance.comptadorBalesAK < AmmoReload.instance.balesPerCarregadorAK || AmmoReload.instance.maxBalesAK < 50)
                {
                    AmmoReload.instance.comptadorBalesAK = AmmoReload.instance.balesPerCarregadorAK;
                    AmmoReload.instance.maxBalesAK = 50;
                    AmmoReload.instance.municioTextAK.text = AmmoReload.instance.comptadorBalesAK + "/" + AmmoReload.instance.maxBalesAK;
                    soRecarrega.Play();
                    bafarada.SetActive(false);
                    pressFText.gameObject.SetActive(false);
                    StartCoroutine(DestroyAfterDelay(2.5f));
                }
            }
            else if (WeaponSwitcher.instance.currentWeaponIndex == 1)
            {
                if(AmmoReloadRevolver.instance.comptadorBalesRevolver < AmmoReloadRevolver.instance.balesPerCarregadorRevolver || AmmoReloadRevolver.instance.maxBalesRevolver < 18)
                {
                    AmmoReloadRevolver.instance.comptadorBalesRevolver = AmmoReloadRevolver.instance.balesPerCarregadorRevolver;
                    AmmoReloadRevolver.instance.maxBalesRevolver = 18;
                    AmmoReloadRevolver.instance.municioTextRevolver.text = AmmoReloadRevolver.instance.comptadorBalesRevolver + "/" + AmmoReloadRevolver.instance.maxBalesRevolver;
                    soRecarrega.Play();
                    bafarada.SetActive(false);
                    pressFText.gameObject.SetActive(false);
                    StartCoroutine(DestroyAfterDelay(2.5f));
                }
            }                         
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}