using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SheildPowerup : Powerup {

    public override void applyEffect(PlayerController controller) {
        controller.shield.SetActive(true);
        if (hudManager)
            hudManager.StartPowerupTimer(this, effectTime);
    }

    public override void removeEffect(PlayerController controller) {
        controller.shield.SetActive(false);
    }
}