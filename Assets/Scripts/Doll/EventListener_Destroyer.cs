using UnityEngine;
public class EventListener_Destroyer : MonoBehaviour
{
    private Doll doll;

    private void Start() {
        doll = GetComponentInParent<Doll>();
    }

    void fire() {
        doll.GetEvent("fire");
    }

    void fire2() {
        doll.GetEvent("fire2");
    }
}
