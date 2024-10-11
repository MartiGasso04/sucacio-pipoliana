using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public int damage = 10; // Da�o que inflige la bala

    private void Start()
    {
        // Destruye la bala despu�s de 2 segundos si no ha colisionado
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que choc� NO tiene el tag "BALA"
        if (other.gameObject.tag != "BALA")
        {
            // Si el objeto tiene el tag "enemy"
            if (other.gameObject.tag == "enemy")
            {
                // Obtener el componente EnemyMovement en el objeto con el que colisiona
                EnemyMovement enemy = other.GetComponent<EnemyMovement>();

                // Verificar si el enemigo tiene el componente y aplicar da�o
                if (enemy != null)
                {
                    enemy.TakeDamage(damage); // Solo pasamos el da�o
                }
            }

            // Destruir la bala
            Destroy(gameObject);
        }
    }
}
