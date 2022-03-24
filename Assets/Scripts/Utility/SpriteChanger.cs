using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite leftSprite;

    private SpriteRenderer spriteRenderer;

    public void Init(SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
    }

    public void ChangeSprite(Direction direction)
    {
        switch(direction)
        {
            case Direction.Up:
                spriteRenderer.sprite = upSprite; break;
            case Direction.Right:
                spriteRenderer.sprite = rightSprite; break;
            case Direction.Down:
                spriteRenderer.sprite = downSprite; break;
            case Direction.Left:
                spriteRenderer.sprite = leftSprite; break;
            default:
                break;
        }
    }
}
