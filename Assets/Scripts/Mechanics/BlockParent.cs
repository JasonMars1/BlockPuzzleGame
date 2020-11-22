using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BColour { Red, Green, Blue, Delete}


public class BlockParent : MonoBehaviour
{
    [SerializeField] AudioClip placeBlockSound;
    [SerializeField] float yBlockOffset;
    [HideInInspector] public Animator anim;
    GameState gameState; // scriptable object


    [HideInInspector] public List<Block> blocks = new List<Block>();
    
    [HideInInspector]public bool dragging = false;
    public List<Vector2> blockProfile = new List<Vector2>();

    [HideInInspector] public BlockSpawn spawn;

    public bool locked = false;
    public BColour bcolour;

    public void Start()
    {
        GetComponentsInChildren(blocks);
        anim = GetComponent<Animator>();
        gameState = FindObjectOfType<Level>().gameState;
        
    }

    void Update()
    {
        //Moving the parent
        if(dragging)
        {
            blocks[0].dragging = true;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10.0f;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 yOffset = new Vector3(0, yBlockOffset, 0); 

            transform.position = mousePos + yOffset;
        }
        else
        {
            blocks[0].dragging = false;
        }
    }

    public void DeleteBlock()
    {
        print("Delete called");
        GridManager.instance.currentBlocks.Remove(this);
        Destroy(this.gameObject);
        spawn.CheckReady();
        GridManager.instance.CheckForMoves();
    }

    public void CallNewBlock()
    {
        spawn.New();
        GridManager.instance.CheckForMoves();
    }


    private void OnMouseDown()
    {
        // stops mouse click through if UI gameobject above spawn point
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (!locked)
        {
            dragging = true;
            anim.SetBool("Dragging", true);
            if(GetComponentInChildren<BlockBomb>())
            {
                
            }
        }
    }

  
    // On Mouse Release check if the blocks are valid
    private void OnMouseUp()
    {
        GridManager.instance.ClearTelegraphs();
        dragging = false;
        if (CheckBlocks())
        {
            spawn.CheckReady();
            foreach(Block current in blocks)
            {
                current.ApplyBlock(bcolour);
            }
            GridManager.instance.CheckForLines();
            GridManager.instance.currentBlocks.Remove(this);
            GridManager.instance.CheckForGameOver();

            // add sound effect here
            if (gameState.GetAudioMode() == 1)
            { 
                AudioSource.PlayClipAtPoint(placeBlockSound, Camera.main.transform.position, 0.75f);
            }
            Destroy(this.gameObject);
        }
        else
        {
            transform.position = spawn.transform.position;
            anim.SetBool("Dragging", false);
        }
    }

    //Function thats called in OnMouseUp, checks all blocks
    //thats part of the parent to see if they can be placed
    bool CheckBlocks()
    {                                     //Iterate through blocks
        foreach (Block current in blocks) //in structure
        {
            if (!current.Check())
            {
                return false; //Exit with false if one cant be placed
            }
        }

        return true;
    }

    public IEnumerator DelayCheckForMoves()
    {
        yield return new WaitForSeconds(Time.deltaTime*2);
        GridManager.instance.CheckForMoves();
        
    }

    

}
