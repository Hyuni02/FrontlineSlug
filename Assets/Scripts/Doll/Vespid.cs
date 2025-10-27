public class Vespid : Enemy {
    protected override void Awake() {
        base.Awake();

        speed = 2;
        dmg = 5;
        attackInterval = 5;
        range = 7;
    }
}
