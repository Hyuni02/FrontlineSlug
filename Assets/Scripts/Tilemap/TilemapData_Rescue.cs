using UnityEngine;
using Random = UnityEngine.Random;
public class TilemapData_Rescue : TilemapData {
    public Transform trans_rescue;
    public RescueTrigger trigger;
    private void Start() {
        //구출 대상 선택
        GameObject rescue;
        while (true) {
            int idx = Random.Range(0, InGameManager.instance.lst_rescuable.Count);
            if (InGameManager.instance.lst_rescuable[idx].name != PlayerPrefs.GetString("main")) {
                rescue = InGameManager.instance.lst_rescuable[idx].doll;
                break;
            }
        }
        GameObject rescueDoll = Instantiate(rescue, trans_rescue.position, Quaternion.identity);
        trigger.doll = rescueDoll.AddComponent<Rescuable>();
    }
}
