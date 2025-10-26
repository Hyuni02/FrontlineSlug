using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour {
    public List<Transform> lst_pos;
    private int index = -1;
    //현재가 아닌 무작위 하나 획득
    public Transform GetPos() {
        index += Random.Range(1, lst_pos.Count);
        index %= lst_pos.Count;
        return lst_pos[index];
    }
}
