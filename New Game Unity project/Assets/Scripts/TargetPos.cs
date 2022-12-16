using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPos : MonoBehaviour
{
    public GameObject fixedPos;
    public LayerMask fixedPosLayerMask;
    public LayerMask posLayerMask;

    private float rayLength = 100;

    private void FixedUpdate(){

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out RaycastHit hitFixed, rayLength, fixedPosLayerMask);

        if (hitFixed.collider != null){
            fixedPos.transform.position = hitFixed.point;
        }

        Physics.Raycast(ray, out RaycastHit hit, rayLength, posLayerMask);
        if (hit.collider != null){
            transform.position = hit.point;
        }
    }
}
