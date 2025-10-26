public class Kar98K : Friendly {
    protected override void Awake() {
        base.Awake();

        speed = 4;
        dmg = 90;
        attackInterval = 2;
        maxHP = 100;
        currHP = 100;
        range = 16;
    }
}
