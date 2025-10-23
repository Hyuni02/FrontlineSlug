using UnityEngine;

public enum TilemapType
{
    Start,
    Battle,
    Event,
    Boss,
    Rescue,
}

public class TilemapData : MonoBehaviour
{
    public int level;
    public TilemapType tilemapType;
    public Transform startPos;
    public Transform endPos;
}
