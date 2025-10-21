using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable : MonoBehaviour
{
    public void Hit(BulletData bulletData) {
        GetComponent<Doll>().Hit(bulletData);
    }
}
