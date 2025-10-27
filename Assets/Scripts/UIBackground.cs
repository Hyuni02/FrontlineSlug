using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackground : MonoBehaviour {
    public GameObject pnl_target;

    public void Active(GameObject target) {
        pnl_target = target;
    }
    
    public void click_Close() {
        pnl_target?.SetActive(false);
        gameObject.SetActive(false);
    }
}
