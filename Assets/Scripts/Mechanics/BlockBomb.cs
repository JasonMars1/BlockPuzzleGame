using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockBomb : Block
{
    [SerializeField] Sprite redBomb;
    [SerializeField] Sprite blueBomb;
    [SerializeField] Sprite greenBomb;
    [SerializeField] Sprite deleteBomb;

    

    GameState gameState;
    BColour type;
    BoxCollider2D col;

    public override void Start()
    {
        base.Start();
        gameState = FindObjectOfType<Level>().gameState;
        col = GetComponent<BoxCollider2D>();
        SetBombType();
    }
    private void Update()
    {
        if(dragging)
        {
            foreach(Slot x in GridManager.instance.returnSlots())
            {
                if(col.bounds.Contains(x.transform.position))
                {
                    x.telegraph.SetActive(true);
                }
                else
                {
                    x.telegraph.SetActive(false);
                }
            }
        }
    }


    public override void ApplyBlock(BColour colour)
    {
        foreach(Slot x in GridManager.instance.returnSlots())
        {
            if(col.bounds.Contains(x.transform.position))
            { 
                if(type == BColour.Delete)
                {
                    x.EmptySlot();
                    game.AddToScore(1);
                }
                else
                {
                    x.FillSlot(type);
                    game.AddToScore(1);
                }
            }
        }
        
    }

    public override bool Check()
    {
        if (GridManager.instance.OverSlot(GetSquareOn()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SetBombType()
    {
        int random = Random.Range(0, 4);
        switch(random)
        {
            case 0:
                type = BColour.Red;
                transform.parent.GetComponentInChildren<SpriteRenderer>().sprite = redBomb;
                break;
            case 1:
                type = BColour.Blue;
                transform.parent.GetComponentInChildren<SpriteRenderer>().sprite = blueBomb;
                break;
            case 2:
                type = BColour.Green;
                transform.parent.GetComponentInChildren<SpriteRenderer>().sprite = greenBomb;
                break;
            case 3:
                transform.parent.GetComponentInChildren<SpriteRenderer>().sprite = deleteBomb;
                type = BColour.Delete;
                break;
        }
    }



    //Checking the best distance slot
    public bool overPlaceable()
    {
        if (GridManager.instance.OverSlot(GetSquareOn()))
        {
            return true;
        }
        else
        {
            return false;
        }


    }
    // returns vector2 x,y coord of mous click pos
    private Vector2 GetSquareOn()
    {


        // round world pos to integer value
        Vector2 gridPos = SnapToGrid(transform.position);

        // 0,0 to 10x,10y - collision area
        return gridPos;
    }

    // round world pos to integer value
    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        return new Vector2(newX, newY);
    }
}

