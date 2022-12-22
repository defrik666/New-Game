using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionController : MonoBehaviour{

    private Controls controls;
    private CameraInteraction cameraInt;

    private void Awake() {
        controls = new Controls();
        cameraInt = FindObjectOfType<CameraInteraction>();

        controls.Workbench.Enable();
        controls.Workbench.Interact.performed += cameraInt.Interact;
    }

    public void ChangeToRange(Gun gun){
        controls.Workbench.Disable();

        controls.ShootingRange.Enable();
        controls.ShootingRange.Shoot.performed += gun.Shoot;
        controls.ShootingRange.Shoot.canceled += gun.Shoot;
        controls.ShootingRange.Reload.performed += gun.Reload;
    }
}
