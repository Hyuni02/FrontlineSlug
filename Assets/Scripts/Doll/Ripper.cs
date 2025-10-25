public class Ripper : Enemy {
    protected override void Start() {
        base.Start();

        speed = 3;
        dmg = 2;
        attackInterval = 3;
    }
}
