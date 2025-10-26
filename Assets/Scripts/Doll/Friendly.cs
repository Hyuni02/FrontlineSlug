using System.Collections;
using UnityEngine;
public class Friendly : Doll {
    public Sprite img_face;
    
    protected override void Die() {
        base.Die();
        InGameManager.instance.DollDie();
        StartCoroutine(cor_RemoveBody());
    }

    IEnumerator cor_RemoveBody() {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
