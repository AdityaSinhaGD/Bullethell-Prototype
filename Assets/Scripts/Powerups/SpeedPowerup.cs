using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpeedPowerup : Powerup {

    public float powerupSpeed;
    float playerOriginalSped;

    public override void applyEffect(PlayerController controller) {
        playerOriginalSped = controller.movementSpeed;
        controller.movementSpeed = powerupSpeed;
        if (hudManager)
            hudManager.StartPowerupTimer(this, effectTime);
    }

    public override void removeEffect(PlayerController controller) {
        controller.movementSpeed = playerOriginalSped;
    }
}