using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : MonoBehaviour,IDamageable
{
    public float detectionRadius = 200f;
    public float health = 50f;

    public float fireRate = 25f;

    public GameObject projectilePrefab;
    public Transform projectileSpawn;

    private Transform player;

    private float nextTimeToFire = 0f;
    private Quaternion lookRotation;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            TargetPlayer();
            Shoot();

        }
    }

    private void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            //Instantiate(projectilePrefab, projectileSpawn.position, transform.rotation);
            Instantiate(projectilePrefab, projectileSpawn.position, lookRotation);
        }
    }

    private void TargetPlayer()
    {
        Vector3 targetDirection = (player.position - (transform.position + new Vector3(0, 0.5f, 0))).normalized;
        lookRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        if (health <= 0)
        {
            Debug.Log("enemyDown");
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
