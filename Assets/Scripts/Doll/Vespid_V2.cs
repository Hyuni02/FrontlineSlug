public class Vespid_V2 : Enemy_V2
{
    protected override void Awake() {
        base.Awake();

        speed = 2;
        dmg = 5;
        attackInterval = 5;
    }
}
