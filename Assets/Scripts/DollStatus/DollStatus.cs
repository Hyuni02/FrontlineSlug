using UnityEngine;

[CreateAssetMenu(fileName = "DollStatus", menuName = "ScriptableObjects/DollStatus", order = 1)]
public class DollStatus : ScriptableObject {
    public float speed = 5;
    public float attackInterval = .5f;
    public float attackDuration = .5f;
    public int maxHP = 100;
    public int dmg = 10;
    public int range = 0;
}
