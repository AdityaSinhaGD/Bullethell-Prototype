using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpeedPowerup : Powerup {

    public float powerupSpeed;
    float playerOriginalSped;
    public Material currentMaterial;
    public Material rimLighting;

    public override void applyEffect(PlayerController controller) {
        playerOriginalSped = controller.movementSpeed;
        controller.movementSpeed = powerupSpeed;
        if (hudManager)
            hudManager.StartPowerupTimer(this, effectTime);
        MeshRenderer bodyRenderer = controller.gameObject.transform.Find("Mesh").GetComponent<MeshRenderer>();
        bodyRenderer.material = rimLighting;
    }

    public override void removeEffect(PlayerController controller) {
        controller.movementSpeed = playerOriginalSped;
        MeshRenderer bodyRenderer = controller.gameObject.transform.Find("Mesh").GetComponent<MeshRenderer>();
        bodyRenderer.material = currentMaterial;
    }
}