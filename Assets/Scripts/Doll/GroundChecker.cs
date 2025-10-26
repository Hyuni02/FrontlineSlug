using UnityEngine;

public class GroundChecker : MonoBehaviour {
    private BoxCollider2D checkBox;
    [HideInInspector]
    public bool isGrounded = false;

    private void Start() {
        checkBox = GetComponent<BoxCollider2D>();
    }
    public void OnTriggerStay2D(Collider2D collision) {
        isGrounded = true;
    }
    public void OnTriggerExit2D(Collider2D collision) {
        isGrounded = false;
    }
}