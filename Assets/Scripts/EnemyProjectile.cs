using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
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
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.transform.GetComponent<IDamageable>();
        if (damageable != null && collision.gameObject.tag == "Player")
        {
            damageable.TakeDamage(damageDealt);
        }
    }

}
