using System;
using UnityEngine;

public class ChangePos : MonoBehaviour {
    public Vector3 offset;
    
    public void LateUpdate() {
        Friendly target = PlayerController.instance.curDoll;

        transform.position = target.transform.position + offset;
    }
}
