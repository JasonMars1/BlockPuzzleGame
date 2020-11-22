using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRemove
{
    public int[] coords = new int[2];
    public bool sameColour;

    public LineRemove(int x, int y, bool _sameColour)
    {
        coords[0] = x;
        coords[1] = y;
        sameColour = _sameColour;
    }
}

public class GridManager : MonoBehaviour
{

    public static GridManager instance; //Singleton Reference

    public List<BlockParent> currentBlocks = new List<BlockParent>();

    public bool blocksLocked;

    [Header("References")]
    [SerializeField] GameObject slotFab;
    
    [Header("Settings")]
    [SerializeField] float xSpacing; 
    [SerializeField] float ySpacing;

    GameState game;
    BonusControl bonusControl;
    public List<BlockSpawn> spawners = new List<BlockSpawn>();

    Slot[,] slots = new Slot[10, 10];

    void Awake()
    {
        if(instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        PopulateGrid();
        game = FindObjectOfType<Level>().gameState;
        bonusControl = FindObjectOfType<BonusControl>();
        
    }


    void PopulateGrid()
    {
        float newXSpace = transform.position.x;
        float newYSpace = transform.position.y;

        for(int x = 0; x< 10; x++)
        {
            for(int y = 0; y < 10; y++)
            {
                slots[x, y] = Instantiate(slotFab, new Vector2(newXSpace,newYSpace), Quaternion.identity, transform).GetComponent<Slot>();
                slots[x, y].coord = new Vector2(x, y);
                newYSpace += ySpacing;
            }
            newYSpace = transform.position.y; //reset y
            newXSpace += xSpacing;
        }
    }

    public void ClearTelegraphs()
    {
        foreach(Slot x in slots)
        {
            x.telegraph.SetActive(false);
        }
    }


    public Slot ReturnSlotFromCoords(Vector2 coords)
    {
        Vector2 testV = new Vector2();

        foreach (Slot current in slots)
        {
            testV.x = current.transform.position.x;
            testV.y = current.transform.position.y;

            if (testV == coords)
            {
                return current;
            }
        }
        return slots[1, 1]; //incase of fail, shouldnt happen tho as safety stuff happens before this is called
    }

    public bool OverSlot(Vector2 coords)
    {
        Vector2 testV = new Vector2();

        foreach(Slot current in slots)
        {
            testV.x = current.transform.position.x;
            testV.y = current.transform.position.y;

            if(testV == coords)
            {
                return true;
            }
        }
        return false;
    }

    public void CheckForLines()
    {
        bool checkFlag = true;
        bool colourFlag = true;
        BColour bColourFlag = new BColour();

        List<LineRemove> coords = new List<LineRemove>();

        for(int x = 0; x<10; x++) //Vertical Check for Lines
        {
            bColourFlag = slots[x, 0].holdingColour;
            checkFlag = true; // reset the flag to true
            colourFlag = true; // reset colour flag
            for(int y = 0; y<10; y++) //Check to see if row has any empties
            {
                if(slots[x,y].isEmpty)
                {
                    checkFlag = false; // turn off the flag
                    break; //no need to carry on looking
                }
                else if(slots[x,y].holdingColour != bColourFlag)
                {
                    colourFlag = false;
                }

            }
            if(checkFlag == true) // if the row is all full
            {
                for (int y = 0; y < 10; y++)
                {
                    if(colourFlag == true) //all same colours
                    {
                        coords.Add(new LineRemove(x,y, true)); //add that coord to the list to be removed at the end
                    }
                    else // not same
                    {
                        coords.Add(new LineRemove(x, y, false));
                    }
                    
                }

                List<Sprite> colours = new List<Sprite>();
                int count = 0;

               foreach(LineRemove current in coords)
               {
                    count++;
                    colours.Add(slots[current.coords[0], current.coords[1]].GetComponent<SpriteRenderer>().sprite);
                    print("Added colour y");

                    if(count%10 == 0) //for each set of 10 colours to avoid a line crash
                    {
                        FadeControl.instance.FadeOnLineY(x, colours);
                        count = 0;
                        colours = new List<Sprite>();
                    }
                }
                
                
            }


        }

        for (int y = 0; y < 10; y++) //Horizontal Check for Lines
        {
            bColourFlag = slots[0, y].holdingColour;
            checkFlag = true; // reset the flag to true
            colourFlag = true; // reset colour flag

            for (int x = 0; x < 10; x++) //Check to see if row has any empties
            {
                if (slots[x, y].isEmpty)
                {
                    checkFlag = false; // turn off the flag
                    break; // no need to carry on down
                }
                else if (slots[x, y].holdingColour != bColourFlag)
                {
                    colourFlag = false;
                }

            }
            if (checkFlag == true) // if the row is all full
            {
                for (int x = 0; x < 10; x++)
                {
                    if(colourFlag == true)
                    {
                        coords.Add(new LineRemove(x,y,true)); //add that coord to the list to be removed at the end
                    }
                    else
                    {
                        coords.Add(new LineRemove(x, y, false));
                    }
                    
                }

                List<Sprite> colours = new List<Sprite>();
                int count = 0;//count to reset the colours list

                foreach (LineRemove current in coords)
                {
                    count++;
                    colours.Add(slots[current.coords[0], current.coords[1]].GetComponent<SpriteRenderer>().sprite);
                    print("Added colour");

                    if(count%10 == 0)
                    {
                        FadeControl.instance.FadeOnLineX(y, colours);
                        count = 0;
                        colours = new List<Sprite>();
                    }
                }
                 // do a  for loop for every 10, problem is that the colours arent reset on second line check i think
                
                
            }
        }

        foreach(LineRemove current in coords) //remove them
        {
            slots[current.coords[0], current.coords[1]].EmptySlot();

            if(current.sameColour)
            {
                game.AddToScore(2);
                print("x2");
            }
            else
            {
                game.AddToScore(1);
            }
        }
    }

    public void CheckForGameOver()
    {
        if(!CheckForMoves() && game.GetBonusScore() < game.GetBonusHoldAmount())
        {
            FindObjectOfType<NoMoves>().OpenNoMovesPopUp();
            
            
        }
    }

    public bool CheckForMoves()
    {
        foreach(BlockSpawn x in spawners)
        {
            if(x.hasBomb)
            {
                return true;
            }
        }
        foreach(BlockParent current in currentBlocks)
        {
            for(int x = 0; x < 10; x++) // go through each slot
            {
                for(int y = 0; y < 10; y++)
                {
                    if(CheckAgainstProfile(x,y,current))
                    {
                        BlockLockState(false);
                        
                        return true;
                    }
                }
            }
        }
        BlockLockState(true);
        return false;
    }

    bool CheckAgainstProfile(int x, int y, BlockParent block)
    {
        foreach(Vector2 current in block.blockProfile)
        {

            if(OutOfBounds(x + (int)current.x, 0, 9) || OutOfBounds(y + (int)current.y, 0, 9))
            {
                return false;
            }

            if (!slots[x+(int)current.x, y+(int)current.y].isEmpty)
            {
                return false;
            }
        }

        return true;
    }

    public Slot[,] returnSlots()
    {
        return slots;
    }

    bool OutOfBounds(int test, int x, int y)
    {
        if (test < x || test > y)
            return true;
        else
            return false;
    }

    void BlockLockState(bool state)
    {
        //set it here, you can use the state parameter passed, state = true if they need to be greyed out
        if(state == true)
        {
            bonusControl.GreyOutOn();
        }
        else
        {
            bonusControl.GreyOutOff();
        }

        foreach(BlockParent current in currentBlocks)
        {
            current.locked = state;
        }

        blocksLocked = state;
    }

}
