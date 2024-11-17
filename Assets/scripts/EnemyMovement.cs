using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public int health = 50;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private bool reachedInitialPosition = false;
    private bool isMoving = true; // Indica si el enemigo está en movimiento

    private Vector3 startPosition;

    void Start()
    {
        // Guardar la posición inicial del enemigo
        startPosition = transform.position;
    }

    void Update()
    {
        if (waypoints.Length == 0)
            return; // Si no hay waypoints, salir de Update

        if (!reachedInitialPosition)
        {
            // Moverse hacia el primer waypoint desde la posición inicial
            MoveTowards(startPosition);

            // Si ya está cerca de la posición inicial, cambiar el estado para moverse a los waypoints
            if (Vector3.Distance(transform.position, startPosition) < 0.1f)
            {
                reachedInitialPosition = true;
            }
        }
        else if (isMoving)
        {
            // Moverse hacia el waypoint actual
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            MoveTowards(targetWaypoint.position);

            // Rotar el dron hacia el waypoint actual
            RotateTowards(targetWaypoint.position);

            // Verificar si llegó al waypoint actual
            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                // Avanzar al siguiente waypoint
                currentWaypointIndex++;

                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }
        }
    }

    // Método para mover el enemigo hacia un destino
    private void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    // Método para rotar el enemigo hacia un destino
    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }

    // Método para detener el movimiento del enemigo
    public void StopMoving()
    {
        isMoving = false;
    }

    // Método para iniciar el movimiento del enemigo
    public void StartMoving()
    {
        isMoving = true;
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if (Key.instance != null)
            {
                Key.instance.comptadorEnemicsMorts++;
            }
            Destroy(gameObject);           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que colisionó tiene el tag "BALA"
        if (other.CompareTag("BALA"))
        {
            if(WeaponSwitcher.instance.currentWeaponIndex == 0) {
                TakeDamage(20);
                Debug.Log("Vida dron: " + health);
            } 
            else if(WeaponSwitcher.instance.currentWeaponIndex == 1) TakeDamage(30);
        }
    }
}