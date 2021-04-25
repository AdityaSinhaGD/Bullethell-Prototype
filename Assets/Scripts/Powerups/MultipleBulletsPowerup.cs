using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MultipleBulletsPowerup : Powerup {
    public Material currentMaterial;
    public Material rimLighting;

    public override void applyEffect(PlayerController controller) {
        controller.multiBullet = true;
        if (hudManager)
            hudManager.StartPowerupTimer(this, effectTime);
        MeshRenderer bodyRenderer = controller.gameObject.transform.Find("Mesh").GetComponent<MeshRenderer>();
        bodyRenderer.material = rimLighting;
    }

    public override void removeEffect(PlayerController controller) {
        controller.multiBullet = false;
        MeshRenderer bodyRenderer = controller.gameObject.transform.Find("Mesh").GetComponent<MeshRenderer>();
        bodyRenderer.material = currentMaterial;
    }
}