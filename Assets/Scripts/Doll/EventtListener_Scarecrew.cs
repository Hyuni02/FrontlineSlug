using UnityEngine;

public class EventtListener_Scarecrew : MonoBehaviour
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
    
    void fire3() {
        doll.GetEvent("fire3");
    }
}
