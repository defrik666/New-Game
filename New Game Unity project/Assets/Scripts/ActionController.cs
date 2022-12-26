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
    }

    private void Start() {
        controls.Workbench.Enable();
        controls.Workbench.Interact.performed += cameraInt.Interact;
    }

    public void ChangeToRangeStart(){
        controls.Workbench.Disable();
        controls.MouseLock.Disable();
    }

    public void ChangeToRangeEnd(Gun gun, ChangeLocation changeLocation){
        controls.ShootingRange.Enable();
        controls.ShootingRange.Shoot.performed += gun.Shoot;
        controls.ShootingRange.Shoot.canceled += gun.Shoot;
        controls.ShootingRange.Reload.performed += gun.Reload;
        controls.ShootingRange.Scope.performed += gun.Scope;
        controls.ShootingRange.UnlockCursor.performed += changeLocation.UnLockCursor;
    }

    public void ChangeToWorkbenchStart(){    
        controls.ShootingRange.Disable();
        controls.MouseLock.Disable();
    }

    public void ChangeToWorkbenchEnd(){    
        controls.Workbench.Enable();
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
