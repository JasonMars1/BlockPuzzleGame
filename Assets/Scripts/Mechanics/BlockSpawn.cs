using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawn : MonoBehaviour
{

    [SerializeField]List<GameObject> blockTypes = new List<GameObject>();

    public List<BlockSpawn> spawns = new List<BlockSpawn>();
    public BlockParent reference;
    public bool isReady = false;
    public bool hasBlock = false;
    public GameObject blockBomb;
    public BlockBomb bombRef;
    public bool hasBomb;

    void Start()
    {
        SpawnNew();
        
    }


    void SpawnNew()
    {
        if (!hasBlock) //not holding holding
        {
            GameObject block = Instantiate(blockTypes[Random.Range(0, blockTypes.Count)], transform.position, Quaternion.identity);
            reference = block.GetComponent<BlockParent>();
            block.GetComponent<BlockParent>().spawn = this;
            GridManager.instance.currentBlocks.Add(block.GetComponent<BlockParent>()); //adds to the current block list on the grid manager
            block.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        isReady = false;
        hasBlock = true;
       
        
    }

    void SpawnBonus()
    {

    }

    public void Hold()
    {
        CheckReady(true);
        reference.anim.SetTrigger("Holding");
        GridManager.instance.CheckForMoves();
        GridManager.instance.CheckForGameOver();

    }

    public void New()
    {
        GridManager.instance.currentBlocks.Remove(reference);
        Destroy(reference.gameObject);

        GameObject block = Instantiate(blockTypes[Random.Range(0, blockTypes.Count)], this.transform.position, Quaternion.identity);
        reference = block.GetComponent<BlockParent>();
        reference.spawn = this;
        GridManager.instance.currentBlocks.Add(reference); //adds to the current block list on the grid manager
        block.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        GridManager.instance.CheckForMoves();
        GridManager.instance.CheckForGameOver();
    }

    public void Delete()
    {
        GridManager.instance.currentBlocks.Remove(reference);
        Destroy(reference.gameObject);
        CheckReady();
        GridManager.instance.CheckForMoves();
        GridManager.instance.CheckForGameOver();

    }

    public void SpawnBlockBomb()
    {
        if (reference != null) //if block is still present
        {
            GridManager.instance.currentBlocks.Remove(reference);
            Destroy(reference.gameObject);
        }

        bombRef = Instantiate(blockBomb, transform.position, Quaternion.identity).GetComponentInChildren<BlockBomb>();
        hasBomb = true;
        bombRef.GetComponentInParent<BlockParent>().spawn = this;
        
    }


    public void CheckReady() //called in block parent when the block is
                             // successfully placed
    {
        isReady = true;
        hasBlock = false;
        foreach(BlockSpawn current in spawns)// check if others are ready
        {
            if(!current.isReady)
            {
                return; //exit if one isnt
            }
        }

        foreach (BlockSpawn current in spawns) //spawn the remaining
        {
            current.SpawnNew();
        }
        SpawnNew();
    }

    public void CheckReady(bool hold) //check ready but for hold
    {
        isReady = true;
       
        foreach (BlockSpawn current in spawns)// check if others are ready
        {
            if (!current.isReady)
            {
                return; //exit if one isnt
            }
        }

        foreach (BlockSpawn current in spawns) //spawnm the remaining
        {
            current.SpawnNew();
        }
        SpawnNew();
    }
}
