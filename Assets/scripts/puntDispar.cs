using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puntDispar : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform armaTransform; // El transform del arma
    public Vector3 offsetPosition = Vector3.zero; // Desplazamiento relativo desde el arma al punto de disparo
    public Vector3 offsetRotation = Vector3.zero; // Desplazamiento relativo de rotaci�n

    void Update()
    {
        // Asegura que la posici�n del puntDispar sea la del ca��n del arma con el offset aplicado
        if (armaTransform != null)
        {
            // Actualiza la posici�n del puntDispar
            transform.position = armaTransform.position + armaTransform.TransformDirection(offsetPosition);

            // Actualiza la rotaci�n del puntDispar
            transform.rotation = armaTransform.rotation * Quaternion.Euler(offsetRotation);
        }
    }
}