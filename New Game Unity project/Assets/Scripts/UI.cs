using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour
{
    private ChangeLocation changeLocation;

    private void Start() {
        changeLocation = FindObjectOfType<ChangeLocation>();
    }

    public void ToRange(){
        changeLocation.MoveToRange();
    }

    public void ToWorkbeanch(){
        changeLocation.MoveToWorkbench();
    }
}
