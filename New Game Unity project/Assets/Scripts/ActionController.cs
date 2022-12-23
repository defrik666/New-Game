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

    public void ChangeToRange(Gun gun, ChangeLocation changeLocation){
        controls.Workbench.Disable();
        controls.MouseLock.Disable();

        controls.ShootingRange.Enable();
        controls.ShootingRange.Shoot.performed += gun.Shoot;
        controls.ShootingRange.Shoot.canceled += gun.Shoot;
        controls.ShootingRange.Reload.performed += gun.Reload;
        controls.ShootingRange.UnlockCursor.performed += changeLocation.UnLockCursor;
    }

    public void ChangeToWorkbench(){    
        controls.Workbench.Enable();
        controls.ShootingRange.Disable();
        controls.MouseLock.Disable();
    }

    public void ChangeMouseLock(ChangeLocation changeLocation){     
        if(controls.MouseLock.enabled){
            controls.MouseLock.Disable();
            controls.ShootingRange.Enable();
            return;
        }
        controls.MouseLock.Enable();
        controls.ShootingRange.Disable();
        controls.MouseLock.LockMouse.performed += changeLocation.LockCursor;
    }
}
