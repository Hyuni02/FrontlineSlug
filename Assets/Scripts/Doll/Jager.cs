public class Jager : Enemy {
    protected override void Awake() {
        base.Awake();

        speed = 1;
        dmg = 15;
        attackInterval = 8;
        range = 16;
    }
}
