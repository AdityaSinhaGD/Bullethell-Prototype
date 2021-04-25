using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridEnemy : MonoBehaviour,IDamageable
{
    enum AIState { seek, target }
    AIState controlState = AIState.seek;

    public float seekDetectionRadius = 100f;
    public float engageDetectionRadius = 100f;
    public float health = 50f;

    public float fireRate = 25f;
    public float moveSpeed = 30f;

    public GameObject projectilePrefab;
    public Transform projectileSpawn;

    private Transform player;

    private float nextTimeToFire = 0f;
    private Quaternion lookRotation;

    [SerializeField] GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*switch (controlState)
        {
            case AIState.seek:
                ProcessSeekPlayer();
                break;
            case AIState.target:
                ProcessEngagePlayer();
                break;
            default:
                break;
        }*/

        ProcessEngagePlayer();
    }


    private void ProcessEngagePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < engageDetectionRadius)
        {
            TargetPlayer();
            Shoot();

        }
        else if (Vector3.Distance(transform.position, player.position) < seekDetectionRadius)
        {
            //controlState = AIState.seek;
            TargetPlayer();
            ProcessSeekPlayer();
        }
    }

    private void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            PlayAudio("event:/Enemy/Shoot");
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

    private void ProcessSeekPlayer()
    {
        /*if(Vector3.Distance(transform.position, player.position) < seekDetectionRadius)
        {
            controlState = AIState.target;
            return;
        }*/
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        if (health <= 0)
        {
            Debug.Log("enemyDown");
            PlayAudio("event:/Enemy/Explosion");
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            PlayAudio("event:/Player/Damage");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, seekDetectionRadius);
        Gizmos.DrawWireSphere(transform.position, engageDetectionRadius);
    }

    private void PlayAudio(string path) //Plays audio found at path
    {
        FMODUnity.RuntimeManager.PlayOneShot(path);
    }
}
