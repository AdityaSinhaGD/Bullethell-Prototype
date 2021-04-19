using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 15f;

    [SerializeField] private Transform projectileSpawn;
    [SerializeField] public GameObject projectilePrefab;

    private CharacterController characterController;
    private Camera playerCam;

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

    }

    private void ProcessPlayerInput()
    {
        ProcessPlayerTranslation();
        ProcessPlayerRotation();
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
            Instantiate(projectilePrefab, projectileSpawn.position, transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Powerup pickups.
        Debug.Log(other.transform.name);
    }
}
