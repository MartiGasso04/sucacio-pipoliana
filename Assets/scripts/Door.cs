using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roof_Door : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private bool isDoorOpen = false;
    private bool isAnimating = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private float animationDuration = 1.0f; // Duración de la animación en segundos
    private Transform playerTransform;

    public GameObject Bafada;
    public Text opendoorText;
    public Text closedoorText;
    public AudioSource doorSound;
    public AudioSource doorcloseSound;

    void Start()
    {
        closedRotation = transform.rotation;
        closedoorText.gameObject.SetActive(false);
        opendoorText.gameObject.SetActive(false);
        Bafada.SetActive(false);
        gameObject.tag = "Untagged"; // Asigna el tag "Untagged" al inicio
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F) && !isAnimating)
        {
            if (isDoorOpen)
            {
                StartCoroutine(CloseDoor());
                doorcloseSound.Play();
            }
            else
            {
                CalculateOpenRotation();
                StartCoroutine(OpenDoor());
                doorSound.Play();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerTransform = other.transform;
            Bafada.SetActive(true);
            if (isDoorOpen)
            {
                closedoorText.gameObject.SetActive(true);
                opendoorText.gameObject.SetActive(false);
            }
            else
            {
                opendoorText.gameObject.SetActive(true);
                closedoorText.gameObject.SetActive(false);
            }
            Debug.Log("Pulsa F para abrir la puerta");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            playerTransform = null;
            Bafada.SetActive(false);
            opendoorText.gameObject.SetActive(false);
            closedoorText.gameObject.SetActive(false);
        }
    }

    void CalculateOpenRotation()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.forward, directionToPlayer, Vector3.up);
        if (angle > 0)
        {
            openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -90, 0));
        }
        else
        {
            openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 90, 0));
        }
    }

    IEnumerator OpenDoor()
    {
        if (!isDoorOpen)
        {
            isAnimating = true;
            float elapsedTime = 0;

            while (elapsedTime < animationDuration)
            {
                transform.rotation = Quaternion.Slerp(closedRotation, openRotation, elapsedTime / animationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = openRotation;
            isDoorOpen = true;
            isAnimating = false;
            closedoorText.gameObject.SetActive(true);
            opendoorText.gameObject.SetActive(false);
            gameObject.tag = "door1"; // Cambia el tag a "door1" cuando la puerta está abierta
        }
    }

    IEnumerator CloseDoor()
    {
        if (isDoorOpen)
        {
            isAnimating = true;
            float elapsedTime = 0;

            while (elapsedTime < animationDuration)
            {
                transform.rotation = Quaternion.Slerp(openRotation, closedRotation, elapsedTime / animationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = closedRotation;
            isDoorOpen = false;
            isAnimating = false;
            closedoorText.gameObject.SetActive(false);
            opendoorText.gameObject.SetActive(true);
            gameObject.tag = "Untagged"; // Cambia el tag a "Untagged" cuando la puerta está cerrada
        }
    }
}