using UnityEngine;

public class Hitable : MonoBehaviour
{
    public void Hit(BulletData bulletData) {
        GetComponent<Doll_V2>().Hit(bulletData);
    }
}
