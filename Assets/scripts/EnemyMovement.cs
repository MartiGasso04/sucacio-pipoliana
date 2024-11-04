using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Velocidad del movimiento
    public int health = 50; // Salud del enemigo
    public Transform[] waypoints; // Array de waypoints
    private int currentWaypointIndex = 0; // Índice del waypoint actual
    private bool reachedInitialPosition = false; // Estado para saber si llegó a la posición inicial
    private int comptadorEnemicsMorts;

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
        else
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

                // Si ya pasamos el último waypoint, volver al primero
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

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        // Restar el daño a la salud del enemigo
        health -= damage;

        // Si la salud llega a 0, destruir el enemigo
        if (health <= 0)
        {
            comptadorEnemicsMorts++;
            if (comptadorEnemicsMorts == 2)
            {
                Key.instance.gameObject.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
