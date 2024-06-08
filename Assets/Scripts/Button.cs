using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] Door door;
    private SpriteRenderer buttonSprite;

    private void Start()
    {
        buttonSprite = GetComponent<SpriteRenderer>();
    }
    public void TurnButton()
    {
        buttonSprite.color = Color.green;

        door.OpenDoor();
    }
}
