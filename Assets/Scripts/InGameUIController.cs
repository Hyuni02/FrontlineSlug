using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour {
    public static InGameUIController instance;

    [Header("Player UI")]
    public Image img_portrait;
    public Slider sld_HP;
    public Image img_portrait_res;
    public Slider sld_HP_res;
    [Header("Boss UI")]
    private Boss boss;
    public Image img_bossPortrait;
    public Slider sld_bossHP;
    
    private void Awake() {
        if (instance == null) {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }

    private void Start() {
        img_bossPortrait.gameObject.SetActive(false);
        sld_bossHP.gameObject.SetActive(false);
        
        img_portrait_res.gameObject.SetActive(false);
        sld_HP_res.gameObject.SetActive(false);
        
        SetSlider(PlayerController.instance.player, PlayerController.instance.player_rescue);
    }

    public void SetSlider(Friendly player, Friendly rescue) {
        img_portrait.sprite = player.img_face;
        sld_HP.maxValue = player.maxHP;
        sld_HP.value = player.currHP;

        if (InGameManager.instance.level >= 2) {
            img_portrait_res.gameObject.SetActive(true);
            img_portrait_res.sprite = rescue.img_face;
            sld_HP_res.gameObject.SetActive(true);
            sld_HP_res.maxValue = rescue.maxHP;
            sld_HP_res.value = rescue.currHP;
        }
    }

    public void SetBossUI(Boss _boss, GameObject block) {
        boss = _boss;
        img_bossPortrait.gameObject.SetActive(true);
        sld_bossHP.gameObject.SetActive(true);
        
        img_bossPortrait.sprite = boss.img_face;
        sld_bossHP.maxValue = boss.maxHP;
        sld_bossHP.value = boss.currHP;
        
        //block.SetActive(true);
    }

    private void LateUpdate() {
        sld_HP.value = PlayerController.instance.player.currHP;
        
        if(boss)
            sld_bossHP.value = boss.currHP;
    }
}
