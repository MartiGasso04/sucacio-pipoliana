using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static Key instance;
    public int comptadorEnemicsMorts = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        gameObject.SetActive(false);


    }

    void Update()
    {
        if (comptadorEnemicsMorts == 2)
        {
            gameObject.SetActive(true);
        }
    }
}
