using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform targetTransform;
    public Vector3 offset = new Vector3(0, 15f, -9f);

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = FindObjectOfType<PlayerController>().transform;
        transform.localEulerAngles = new Vector3(60f, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = targetTransform.position + offset;
    }
}
