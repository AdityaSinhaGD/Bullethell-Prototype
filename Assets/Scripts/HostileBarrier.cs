using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileBarrier : MonoBehaviour
{
    [SerializeField] private float damage;

    public float Damage
    {
        get { return damage; }
    }
}
