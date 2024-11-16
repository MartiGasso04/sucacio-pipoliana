using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private void Start()
    {
        // Destruye la bala después de 2 segundos si no ha colisionado
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que chocó NO tiene el tag "BALA"
        if (other.gameObject.tag != "BALA")
        {
            // Destruir la bala
            Destroy(gameObject);
        }
    }
}