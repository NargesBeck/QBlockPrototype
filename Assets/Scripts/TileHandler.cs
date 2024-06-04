using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    [SerializeField] private Sprite DefaultSprite;
    [SerializeField] private Sprite FullSprite;
    [SerializeField] private Sprite HoverSprite;

    public enum TileStates { Empty, Hover, Full }
    public TileStates CurrentState = TileStates.Empty;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
            return spriteRenderer;
        }
    }

    public void FillIn()
    {
        CurrentState = TileStates.Full;
        SpriteRenderer.sprite = FullSprite;
    }

    public void Hover()
    {
        CurrentState = TileStates.Hover;
        SpriteRenderer.sprite = HoverSprite;
    }

    public void UnHover()
    {
        CurrentState = TileStates.Empty;
        SpriteRenderer.sprite = DefaultSprite;
    }

    public void Empty()
    {
        CurrentState = TileStates.Empty;
        SpriteRenderer.sprite = DefaultSprite;
    }
}
