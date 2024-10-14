using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject puntDisparEnemic; // Punto desde donde dispara el enemigo
    public GameObject projectil; // Prefab del proyectil
    public Transform puntDisparloc; // Posición del punto de disparo
    public float projectilSpeed = 5f; // Velocidad del proyectil
    public float fireRate = 1.5f; // Frecuencia de disparo en segundos
    public float shootDurationAfterExit = 3f; // Tiempo que seguirá disparando después de que el jugador salga del collider

    private float nextFireTime = 0f; // Tiempo para el siguiente disparo
    private Transform player; // Referencia al jugador
    private bool isShooting = false; // Indica si el enemigo está disparando
    private float shootEndTime = 0f; // Tiempo en el que dejará de disparar después de que el jugador salga

    void OnTriggerEnter(Collider other)
    {
        // Si el objeto que entra tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Guardar la referencia al jugador y comenzar a disparar
            player = other.transform;
            isShooting = true; // El enemigo comienza a disparar
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Si el jugador está en el rango y es tiempo de disparar
        if (player != null && Time.time > nextFireTime && isShooting)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + fireRate; // Actualizar el tiempo del próximo disparo
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Si el jugador sale del rango, el enemigo sigue disparando por shootDurationAfterExit segundos
        if (other.CompareTag("Player"))
        {
            shootEndTime = Time.time + shootDurationAfterExit; // Definir el tiempo en que debe dejar de disparar
        }
    }

    void ShootAtPlayer()
    {
        if (player != null)
        {
            // Obtener la dirección hacia el jugador
            Vector3 directionToPlayer = (player.position - puntDisparEnemic.transform.position).normalized;

            // Instanciar el proyectil en el punto de disparo
            GameObject firedProjectil = Instantiate(projectil, puntDisparEnemic.transform.position, Quaternion.identity);

            // Añadir un Rigidbody al proyectil
            Rigidbody rb = firedProjectil.AddComponent<Rigidbody>();
            rb.useGravity = false; // Desactivar la gravedad

            // Aplicar velocidad al proyectil en la dirección del jugador
            rb.velocity = directionToPlayer * projectilSpeed;
        }
    }

    void Update()
    {
        // Mantener el punto de disparo del enemigo en la posición correcta
        if (puntDisparEnemic != null && puntDisparloc != null)
        {
            puntDisparEnemic.transform.position = puntDisparloc.position;
        }

        // Si el jugador ha salido, el enemigo sigue disparando hasta que pase el tiempo
        if (player == null && Time.time >= shootEndTime)
        {
            isShooting = false; // Dejar de disparar cuando pase el tiempo
        }

        // Si el jugador ha salido del rango pero el tiempo de disparo no ha terminado, sigue disparando
        if (isShooting && Time.time > nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + fireRate;
        }
    }
}
