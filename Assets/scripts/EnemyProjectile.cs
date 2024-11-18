using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 20;



    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "BalaEnemiga" && other.gameObject.tag != "enemy"&& other.gameObject.tag != "door1")
        {
            if (other.gameObject.tag == "Player")
            {
                Player player = other.GetComponent<Player>();

                if (player != null)
                {
                    player.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}

