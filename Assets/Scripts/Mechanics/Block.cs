using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    protected BlockParent parent;
    protected GameState game;
    public bool dragging = false;
    // Start is called before the first frame update
    public virtual void Start()
    {
        parent = GetComponentInParent<BlockParent>();
        game = FindObjectOfType<Level>().gameState;
    }

   

    //Checking the best distance slot
    public virtual bool Check()
    {
        if (GridManager.instance.OverSlot(GetSquareOn()))
        {
            if (GridManager.instance.ReturnSlotFromCoords(GetSquareOn()).isEmpty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }


    }

    //Applying the best distance slot
    public virtual void ApplyBlock(BColour colour)
    {
        if(Check())
        {
            game.AddToScore(1);
            GridManager.instance.ReturnSlotFromCoords(GetSquareOn()).FillSlot(colour);
            
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
