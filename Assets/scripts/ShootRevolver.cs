using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRevolver : MonoBehaviour
{
    public Camera playerCamera; // C�mara del jugador
    public GameObject puntDispar; // Punto desde donde se dispara
    public GameObject bulletPrefab; // Prefab de la bala
    public AudioSource audioSource; // AudioSource para el sonido de disparo
    public float bulletSpeed = 15f; // Velocidad de la bala (puedes ajustar esta velocidad para el revolver)
    public Transform playerTransform; // Transform del jugador o el objeto que quieres que puntDispar siga
    public float fireRate = 1.0f; // Tiempo de espera entre disparos en segundos
    private float nextFireTime = 0f; // Tiempo en el que se puede volver a disparar

    void Start()
    {
        // Inicializa el componente de AudioSource
        audioSource = GetComponent<AudioSource>();

        // Si no se ha asignado una c�mara en el inspector, usar la c�mara principal
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        // Si no se ha asignado un Transform del jugador, usa el Transform de este GameObject
        if (playerTransform == null)
        {
            playerTransform = this.transform;
        }
    }

    void Update()
    {
        // Asegurar que el punto de disparo siga la posici�n del jugador
        puntDispar.transform.position = playerTransform.position;

        // Detecta un clic izquierdo del rat�n y verifica si puede disparar seg�n el delay
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            ShootRaycast();
            nextFireTime = Time.time + fireRate; // Establece el tiempo para el siguiente disparo
        }
    }

    void ShootRaycast()
    {
        // Reproduce el sonido del disparo
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Instanciar la bala en el punto de disparo
        GameObject bullet = Instantiate(bulletPrefab, puntDispar.transform.position, puntDispar.transform.rotation);

        // Ajustar la rotaci�n de la bala para que el eje X sea fijo a 90 grados
        Quaternion bulletRotation = bullet.transform.rotation;
        bullet.transform.rotation = Quaternion.Euler(90, bulletRotation.eulerAngles.y, bulletRotation.eulerAngles.z);

        // A�adir un Rigidbody a la bala para que se mueva
        Rigidbody rb = bullet.AddComponent<Rigidbody>();
        rb.useGravity = false; // Desactivar la gravedad

        // Aseg�rate de que la bala se mueva en la direcci�n correcta
        rb.velocity = puntDispar.transform.forward * bulletSpeed; // Mover la bala hacia adelante

        // Adjuntar el script de colisi�n
        bullet.AddComponent<Bala>(); // Aqu� se a�ade el script Bala
    }
}
