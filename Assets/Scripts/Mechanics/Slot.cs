using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{

    SpriteRenderer sRenderer;
    public Sprite redBlock;
    public Sprite greenBlock;
    public Sprite blueBlock;
    public Vector2 coord;
    public BColour holdingColour = new BColour();
    public GameObject telegraph;

    [Header("Stats")]
    public bool isEmpty;


    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        EmptySlot();
    }

    public void FillSlot(BColour colour)
    {
        isEmpty = false;
        sRenderer.enabled = true;

        switch (colour)
        {
            case BColour.Red:
                holdingColour = BColour.Red;
                sRenderer.sprite = redBlock;
                return;
            case BColour.Green:
                holdingColour = BColour.Green;
                sRenderer.sprite = greenBlock;
                return;
            case BColour.Blue:
                holdingColour = BColour.Blue;
                sRenderer.sprite = blueBlock;
                return;
            default:
                isEmpty = true;
                sRenderer.enabled = false;
                return;


        }


    }

    public void EmptySlot()
    {
        isEmpty = true;
        sRenderer.enabled = false;
    }


}
