public class MP7 : Friendly {
    protected override void Awake() {
        base.Awake();

        speed = 7;
        dmg = 10;
        attackInterval = 1;
        maxHP = 80;
        currHP = 80;
        range = 10;
    }
}
