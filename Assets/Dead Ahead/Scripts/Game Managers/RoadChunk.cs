using UnityEngine;

public class RoadChunk : Chunk
{
    [SerializeField] private SpriteRenderer _sprite;
    public SpriteRenderer Sprite => _sprite;
}
