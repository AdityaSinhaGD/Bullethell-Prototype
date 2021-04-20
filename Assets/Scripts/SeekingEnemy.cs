using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float seekingRadius = 10000f;
    [SerializeField] float maxSpeed = 2f;
    GameObject player;

    protected Vector3 vehiclePosition;
    public Vector3 acceleration;
    public Vector3 direction;
    public Vector3 velocity;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        vehiclePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        acceleration = Vector3.zero;
        if(Vector3.Distance(transform.position, player.transform.position)<seekingRadius)
        {
            acceleration =  Seek(player.transform.position);
        }

        acceleration.y = 0;
        velocity += acceleration * Time.deltaTime;
        vehiclePosition += velocity * Time.deltaTime;
        direction = velocity.normalized;
        acceleration = Vector3.zero;
        transform.position = vehiclePosition;
    }

    // SEEK METHOD
    // All enemies have the knowledge of how to seek
    // They just may not be calling it all the time
    /// <summary>
    /// Seek
    /// </summary>
    /// <param name="targetPosition">Vector3 position of desired target</param>
    /// <returns>Steering force calculated to seek the desired target</returns>
    public Vector3 Seek(Vector3 targetPosition)
    {
        // Step 1: Find DV (desired velocity)
        // TargetPos - CurrentPos
        Vector3 desiredVelocity = targetPosition - vehiclePosition;
        //desiredVelocity.y = 0;

        // Step 2: Scale vel to max speed
        desiredVelocity.Normalize();
        desiredVelocity = desiredVelocity * maxSpeed;

        // Step 3:  Calculate seeking steering force
        Vector3 seekingForce = desiredVelocity - velocity;

        // Step 4: Return force
        return seekingForce;
    }
}
