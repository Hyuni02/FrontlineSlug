using UnityEngine;

public class RescueTrigger : MonoBehaviour {
    public Rescuable doll;
    public void OnTriggerEnter2D(Collider2D other) {
        doll.rescued = true;
        PlayerPrefs.SetString("rescue", doll.gameObject.name.Replace("(Clone)", ""));
    }
}
