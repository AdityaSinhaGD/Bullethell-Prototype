using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Powerup : MonoBehaviour {

    public float effectTime = 5.0f;
    [SerializeField] private float rotateSpeed = 180f; //Rotation speed in degrees/sec
    protected HUDManager hudManager;

    private void Start()
    {
        GameObject hud = GameObject.Find("HUDManager");
        if (hud)
            hudManager = hud.GetComponent<HUDManager>();
    }

    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        if (transform.rotation.eulerAngles.y >= 360)
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.y % 360);
    }

    public abstract void applyEffect(PlayerController controller);
    public abstract void removeEffect(PlayerController controller);

    public void applyPowerup(PlayerController controller) {
        if (controller.removePowerup != null) //Replaces current powerup
            controller.removePowerup(controller);
        applyEffect(controller);
        //controller.powerupTimer = effectTime;
        controller.StopPowerupTimer();
        controller.StartPowerupTimer(effectTime);
        controller.removePowerup = removeEffect;
        Destroy(this.gameObject);


    }
}