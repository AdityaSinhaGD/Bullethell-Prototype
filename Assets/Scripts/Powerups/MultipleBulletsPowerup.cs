using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MultipleBulletsPowerup : Powerup {


    public override void applyEffect(PlayerController controller) {
        controller.multiBullet = true;
    }

    public override void removeEffect(PlayerController controller) {
        controller.multiBullet = false;
    }
}