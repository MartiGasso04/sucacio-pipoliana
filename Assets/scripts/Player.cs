using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public int shield = 50;

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
