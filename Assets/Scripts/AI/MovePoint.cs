using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour {
    public List<Transform> lst_pos;
    
    //현재가 아닌 무작위 하나 획득
    public Transform GetPos(Transform pos) {
        while (true) {
            var dest = lst_pos[Random.Range(0, lst_pos.Count)];
            if (dest != pos) {
                return dest;
            }
        }
    }
}
