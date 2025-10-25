using UnityEngine;

public class EventListener : MonoBehaviour {
    private Doll_V2 doll;

    private void Start() {
        doll = GetComponentInParent<Doll_V2>();
    }

    void fire() {
        doll.GetEvent("fire");
    }
}
