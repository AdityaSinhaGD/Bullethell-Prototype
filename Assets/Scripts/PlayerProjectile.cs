using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float projectileForce = 30f;

    public Vector3 direction;

    private new Rigidbody rigidbody;

    public float damageDealt = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(projectileForce * transform.forward, ForceMode.Impulse);
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.transform.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageDealt);
        }
    }
}
