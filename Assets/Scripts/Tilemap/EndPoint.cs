using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Friendly>()) {
            InGameManager.instance.ToNextLevel();
        }
    }
}
