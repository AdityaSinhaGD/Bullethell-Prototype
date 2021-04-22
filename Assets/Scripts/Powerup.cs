using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Powerup : MonoBehaviour {

    public float effectTime = 5.0f;
    protected HUDManager hudManager;

    private void Start()
    {
        GameObject hud = GameObject.Find("HUDManager");
        if (hud)
            hudManager = hud.GetComponent<HUDManager>();
    }

    public abstract void applyEffect(PlayerController controller);
    public abstract void removeEffect(PlayerController controller);

    public void applyPowerup(PlayerController controller) {
        applyEffect(controller);
        controller.powerupTimer = effectTime;
        controller.removePowerup = removeEffect;
        Destroy(this.gameObject);


    }
}