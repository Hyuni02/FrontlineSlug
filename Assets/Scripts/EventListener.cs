using UnityEngine;

public class EventListener : MonoBehaviour {
    private Doll doll;

    private void Start() {
        doll = GetComponentInParent<Doll>();
    }

    void fire() {
        doll.GetEvent("fire");
    }
}
