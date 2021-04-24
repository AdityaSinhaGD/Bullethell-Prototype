using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] public float movementSpeed = 15f;
    [SerializeField] private float maxHealth = 30f;
    private float currentHealth;
    private bool isDead;
    private bool atGoal;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] public GameObject projectilePrefab;

    public bool multiBullet = false;

    public Action<PlayerController> removePowerup;
    public GameObject shield;

    private CharacterController characterController;
    private Camera playerCam;
    public float powerupTimer;

    public float MaxHealth
    {
        get { return maxHealth; }
    }
    public float CurrentHealth
    {
        get { return currentHealth; }
    }
    public bool IsDead
    {
        get { return isDead; }
    }

    public bool AtGoal
    {
        get { return atGoal; }
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerCam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
        atGoal = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessPlayerInput();
        ProcessFire();
        //updatePowerupStatus();
    }

    private void ProcessPlayerInput()
    {
        ProcessPlayerTranslation();
        ProcessPlayerRotation();
    }

    void updatePowerupStatus()
    {
        powerupTimer -= Time.deltaTime;
        if (powerupTimer <= 0 && removePowerup != null)
        {
            removePowerup(this);
        }
    }

    public void StartPowerupTimer(float duration)
    {
        StartCoroutine(UpdatePowerupStatus(duration));
    }

    private IEnumerator UpdatePowerupStatus(float duration)
    {
        powerupTimer = duration;
        while (powerupTimer > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            powerupTimer -= Time.deltaTime;
        }

        if (removePowerup != null)
            removePowerup(this);
    }

    private void ProcessPlayerRotation()
    {
        Plane playerObjectPlane = new Plane(transform.up, transform.position);
        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
        if (playerObjectPlane.Raycast(ray, out float intersectionDistace))
        {
            Vector3 planeIntersectionPoint = ray.GetPoint(intersectionDistace);
            Vector3 targetLookDirection = (planeIntersectionPoint - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(targetLookDirection, transform.up);
            transform.rotation = lookRotation;
        }
    }

    private void ProcessPlayerTranslation()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        Vector3 velocity = movementSpeed * inputDirection;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void ProcessFire()
    {
        //todo Check Fire rate and other factors.
        if (Input.GetButtonDown("Fire1"))
        {
            //todo bullet spawn effects
            if (multiBullet)
            {
                //int directionMultiplier = -1;
                for (int i = -1; i < 2; i++)
                {
                    Vector3 bulletDirection = Quaternion.AngleAxis(i * 15, Vector3.up) * transform.forward;
                    Vector3 bulletPosition = projectileSpawn.position + transform.right * 0.1f * i;
                    Instantiate(projectilePrefab, bulletPosition, Quaternion.LookRotation(bulletDirection));
                }
            }
            else
            {
                Instantiate(projectilePrefab, projectileSpawn.position, transform.rotation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Powerup")
        {
            other.GetComponent<Powerup>().applyPowerup(this);
        }
        else if (other.gameObject.tag == "SeekingEnemy")
        {
            TakeDamage(other.gameObject.GetComponent<SeekingEnemy>().damageGiven);
            other.GetComponent<SeekingEnemy>().TakeDamage(51f);
        }
        else if (other.gameObject.tag == "HostileBarrier")
        {
            if (other.gameObject.GetComponent<HostileBarrier>())
                TakeDamage(other.gameObject.GetComponent<HostileBarrier>().Damage);
        }
        else if (other.gameObject.tag == "Goal")
        {
            atGoal = true;
        }
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;
        //todo Damage Effects and shield.
        Debug.Log("Player Hit");
        if (currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("player dead");
        }
    }
}
