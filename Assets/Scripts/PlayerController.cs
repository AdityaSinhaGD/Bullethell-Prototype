using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IDamageable
{
    [SerializeField] public  float movementSpeed = 15f;
    [SerializeField] public float playerHealth = 30f;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] public GameObject projectilePrefab;

    public bool multiBullet = false;

    public Action<PlayerController> removePowerup;
    public GameObject shield;


    private CharacterController characterController;
    private Camera playerCam;
    public float powerupTimer;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerCam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessPlayerInput();
        ProcessFire();
        updatePowerupStatus();
    }

    private void ProcessPlayerInput()
    {
        ProcessPlayerTranslation();
        ProcessPlayerRotation();
    }

    void updatePowerupStatus () {
        powerupTimer -= Time.deltaTime;
        if(powerupTimer<=0 && removePowerup != null) {
            removePowerup(this);
        }
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
            if(multiBullet) {
                int directionMultiplier = -1;
                for(int i = 0; i < 3; i++, directionMultiplier++) {
                    Vector3 bullterDirection = Quaternion.AngleAxis(directionMultiplier*  20, Vector3.up) * transform.forward;
                    Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.LookRotation(bullterDirection));    
                }
            } else {
                Instantiate(projectilePrefab, projectileSpawn.position, transform.rotation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Powerup") {
            other.GetComponent<Powerup>().applyPowerup(this);
        }
    }

    public void TakeDamage(float damageTaken)
    {
        playerHealth -= damageTaken;
        //todo Damage Effects and shield.
        Debug.Log("Player Hit");
        if (playerHealth <= 0)
        {
            Debug.Log("player dead");
        }
    }
}
