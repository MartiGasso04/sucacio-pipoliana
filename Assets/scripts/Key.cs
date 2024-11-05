using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public GameObject bafarada;
    public Text pressFText;
    public Text keySpawnText;
    
    public static Key instance;
    public int comptadorEnemicsMorts = 0;
    private bool pitxarF = false;  // Para detectar si el jugador ha presionado "F"
    private bool playerInRange = false;  // Para saber si el jugador está en el trigger
    private bool keyActivated = false;  // Para saber si la llave ya ha sido activada

    public AudioSource keySpawnSound;


    private MeshRenderer meshRenderer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        // Inicializa el MeshRenderer
        meshRenderer = GetComponent<MeshRenderer>();

        // Inicialmente desactiva el mesh y los elementos de UI
        meshRenderer.enabled = false;
        bafarada.SetActive(false);
        pressFText.gameObject.SetActive(false);
        keySpawnText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Activa el objeto cuando el contador de enemigos muertos llega a 2
        if (comptadorEnemicsMorts == 2 && !keyActivated)
        {
            keyActivated = true;
            meshRenderer.enabled = true;
            keySpawnSound.Play();
            bafarada.SetActive(true);
            keySpawnText.gameObject.SetActive(true);
            StartCoroutine(Sucatione());
        }

        // Comprueba si el jugador está en el área y ha presionado "F"
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            pitxarF = true;
        }

        // Si el jugador ha presionado "F", desactiva los elementos y destruye el objeto
        if (pitxarF)
        {
            bafarada.SetActive(false);
            pressFText.gameObject.SetActive(false);
            Destroy(gameObject);   
        }
    }

    private IEnumerator Sucatione()
    {
        // Espera 2 segundos
        yield return new WaitForSeconds(2f);

        // Desactiva `bafarada` y `keySpawnText` después de 2 segundos
        bafarada.SetActive(false);
        keySpawnText.gameObject.SetActive(false);
        meshRenderer.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Marca que el jugador está en el rango del trigger
            playerInRange = true;
            bafarada.SetActive(true);
            pressFText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Marca que el jugador ha salido del rango del trigger
            playerInRange = false;
            bafarada.SetActive(false);
            pressFText.gameObject.SetActive(false);
        }
    }
}