using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleShield : MonoBehaviour
{
    public GameObject bafarada;
    public Text pressFText;
    public AudioSource soBeure;
    private Light highlightLight;
    private bool playerInRange = false;

    private void Start()
    {
        bafarada.SetActive(false);
        pressFText.gameObject.SetActive(false);

        // Añadir un Light al objeto BottleShield
        highlightLight = gameObject.AddComponent<Light>();
        highlightLight.type = LightType.Point;
        highlightLight.range = 5f; // Ajusta el rango según sea necesario
        highlightLight.intensity = 2f; // Ajusta la intensidad según sea necesario
        highlightLight.color = Color.cyan; // Ajusta el color según sea necesario
        highlightLight.enabled = false; // Desactivar el Light al inicio
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            soBeure.Play();
            bafarada.SetActive(false);
            pressFText.gameObject.SetActive(false);
            StartCoroutine(DestroyAfterSound());               
        }
    }

    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitWhile(() => soBeure.isPlaying);
        Destroy(gameObject);
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
            highlightLight.enabled = false; // Desactivar el Light cuando el jugador sale del rango
            bafarada.SetActive(false);
            pressFText.gameObject.SetActive(false);
        }
    }
}