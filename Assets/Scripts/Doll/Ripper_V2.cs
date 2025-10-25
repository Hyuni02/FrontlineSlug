public class Ripper_V2 : Enemy_V2 {
    protected override void Awake() {
        base.Awake();

        speed = 3;
        dmg = 2;
        attackInterval = 3;
    }
}
