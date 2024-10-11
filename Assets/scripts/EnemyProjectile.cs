using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 20; // Daño que inflige la bala

    private void Start()
    {
        // Destruye la bala después de 2 segundos si no ha colisionado
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que chocó NO tiene el tag "BALA"
        if (other.gameObject.tag != "BalaEnemiga")
        {
            // Si el objeto tiene el tag "enemy"
            if (other.gameObject.tag == "Player")
            {
                // Obtener el componente EnemyMovement en el objeto con el que colisiona
                Player player = other.GetComponent<Player>();

                // Verificar si el enemigo tiene el componente y aplicar daño
                if (player != null)
                {
                    player.TakeDamage(damage); // Solo pasamos el daño
                }
            }

            // Destruir la bala
            Destroy(gameObject);
        }
    }
}
