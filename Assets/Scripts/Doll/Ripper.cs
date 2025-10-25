public class Ripper : Enemy {
    protected override void Awake() {
        base.Awake();

        speed = 3;
        dmg = 2;
        attackInterval = 3;
    }
}
