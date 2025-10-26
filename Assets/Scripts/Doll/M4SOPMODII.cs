public class M4SOPMODII : Friendly {
    protected override void Awake() {
        base.Awake();

        speed = 5;
        dmg = 20;
        attackInterval = 1.5f;
        maxHP = 100;
        currHP = 100;
        range = 13;
    }
}
