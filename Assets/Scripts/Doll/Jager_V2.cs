public class Jager_V2 : Enemy_V2 {
    protected override void Awake() {
        base.Awake();

        speed = 1;
        dmg = 15;
        attackInterval = 8;
    }
}
