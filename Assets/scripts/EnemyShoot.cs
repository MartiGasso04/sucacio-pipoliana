using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float fireRate = 1f; // Tasa de disparo
    public float shootDurationAfterExit = 10f; // Duración de disparo después de que el jugador salga del trigger
    public GameObject projectil; // Prefab del proyectil
    public Transform puntDisparEnemic; // Punto de disparo del enemigo
    public float projectilSpeed = 6f; // Velocidad del proyectil

    private float nextFireTime = 0f; // Tiempo para el siguiente disparo
    private Transform player; // Referencia al jugador
    private bool isShooting = false; // Indica si el enemigo está disparando
    private float shootEndTime = 0f; // Tiempo en el que dejará de disparar después de que el jugador salga
    private EnemyMovement enemyMovement; // Referencia al componente de movimiento del enemigo (si existe)

    void Start()
    {
        // Obtener el componente de movimiento del enemigo (si existe)
        enemyMovement = GetComponent<EnemyMovement>();
    }

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
            player = null; // El jugador ya no está en el rango
        }
    }

    void Update()
    {
        // Si el tiempo actual es mayor que shootEndTime y el jugador no está en el rango, dejar de disparar y volver a moverse
        if (isShooting && Time.time > shootEndTime && player == null)
        {
            isShooting = false;
            if (enemyMovement != null)
            {
                enemyMovement.StartMoving(); // Volver a moverse
            }
        }

        // Si el jugador está en el rango y es tiempo de disparar
        if (player != null && Time.time > nextFireTime && isShooting)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + fireRate; // Actualizar el tiempo del próximo disparo
        }
    }

    void ShootAtPlayer()
    {
        if (player != null)
        {
            // Detener el movimiento del enemigo
            if (enemyMovement != null)
            {
                enemyMovement.StopMoving();
            }

            // Girar el enemigo hacia el jugador
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Obtener la dirección hacia el jugador desde el punto de disparo
            Vector3 directionToPlayerFromShootPoint = (player.position - puntDisparEnemic.position).normalized;

            // Instanciar el proyectil en el punto de disparo
            GameObject firedProjectil = Instantiate(projectil, puntDisparEnemic.position, Quaternion.identity);

            // Añadir un Rigidbody al proyectil
            Rigidbody rb = firedProjectil.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = firedProjectil.AddComponent<Rigidbody>();
            }
            rb.useGravity = false; // Desactivar la gravedad

            // Aplicar velocidad al proyectil en la dirección del jugador
            rb.velocity = directionToPlayerFromShootPoint * projectilSpeed;
        }
    }
}