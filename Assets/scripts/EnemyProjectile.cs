using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 20;



    private void Start()
    {
        // Destruye la bala después de 2 segundos si no ha colisionado
        Destroy(gameObject, 4f);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile collided with: " + other.gameObject.name);

        if (other.gameObject.tag != "BalaEnemiga"&& other.gameObject.tag != "enemy")
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Projectile hit the player.");

                Player player = other.GetComponent<Player>();

                if (player != null)
                {
                    Debug.Log("Player component found, applying damage.");
                    player.TakeDamage(damage);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Player component not found!");
                }
            }

            Destroy(gameObject);
        }
    }
}

