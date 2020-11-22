using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlace : MonoBehaviour
{
    // script linked to TestPlace area grid object

    // reference to TestBlockPrefab
    [SerializeField] GameObject testBlockPrefab;


    // OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider.
    // This function is called on Colliders marked as Trigger if and only if Physics.queriesHitTriggers is true.
    // if Testblock in the way and Testblock collider has is trigger it wont work (stops duplicate)
    // blocks placed z = 0 and test mouse grid z = -1 so blocks go infront of grid
    private void OnMouseDown()
    {
        //getSquareclicked returns Vector2 MousePos as world pos
        // this is then passed to SpawnTestBlock
        AttemptToPlaceBlockAt(GetSquareClicked());
    }
    
    
    private void AttemptToPlaceBlockAt(Vector2 gridPos)
    {
        
       SpawnTestBlock(gridPos);
        
    }



    // returns vector2 x,y coord of mous click pos
    private Vector2 GetSquareClicked()
    {
        // get mouse x and y and stor in clickPos
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // convert clickPos into world coords and store in worldpos
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);

        // round world pos to integer value
        Vector2 gridPos = SnapToGrid(worldPos);

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

    // SpanWorldPos is a local variable to this function only
    private void SpawnTestBlock(Vector2 RoundedGridPos)
    {
        Instantiate(testBlockPrefab, RoundedGridPos, transform.rotation);
    }
}
