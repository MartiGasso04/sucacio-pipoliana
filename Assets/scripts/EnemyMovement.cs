using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Velocidad del movimiento
    public float distance = 5f; // Distancia máxima a la que se moverá de lado a lado
    public int health = 50; // Salud del enemigo
    private Vector3 startPosition;

    void Start()
    {
        // Guardar la posición inicial del enemigo al iniciar el juego
        startPosition = transform.position;
    }

    void Update()
    {
        // Calcular el movimiento de lado a lado usando PingPong
        float movement = Mathf.PingPong(Time.time * speed, distance * 2) - distance;

        // Actualizar la posición del enemigo
        transform.position = new Vector3(startPosition.x + movement, startPosition.y, startPosition.z);
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        // Restar el daño a la salud del enemigo
        health -= damage;
        
        // Si la salud llega a 0, destruir el enemigo
        if (health <= 0)
        {
            
            Destroy(gameObject);
        }
    }
}
