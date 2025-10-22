using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other) {
        InGameManager.instance.ToNextLevel();
    }
}
