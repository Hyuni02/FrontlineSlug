using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other) {
        InGameManager.instance.ToNextLevel();
    }
}
