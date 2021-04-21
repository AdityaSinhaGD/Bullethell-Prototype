using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileForce = 30f;

    public Vector3 direction;

    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(projectileForce * transform.forward, ForceMode.Impulse);
        Destroy(gameObject, 3f);
    }

}
