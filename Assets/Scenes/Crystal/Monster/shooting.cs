using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    public float attackRange = 10.0f;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < attackRange)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = directionToPlayer * bullet.GetComponent<Bullet>().speed;

            bullet.GetComponent<Bullet>().target= player.transform;
        }

    }

}
