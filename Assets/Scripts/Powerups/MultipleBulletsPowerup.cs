using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MultipleBulletsPowerup : Powerup {
    public Material currentMaterial;
    public Material rimLighting;

    public override void applyEffect(PlayerController controller) {
        controller.multiBullet = true;
        controller.gameObject.GetComponent<MeshRenderer>().material = rimLighting;
    }

    public override void removeEffect(PlayerController controller) {
        controller.multiBullet = false;
        controller.gameObject.GetComponent<MeshRenderer>().material = currentMaterial;
    }
}