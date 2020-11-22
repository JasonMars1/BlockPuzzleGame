using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BonusControl : MonoBehaviour
{
    public GameObject BonusPopup;
    public GameObject BonusPopupLeft;
    public GameObject BonusPopupCenter;
    public GameObject BonusPopupRight;
    public GameObject GreyOutLeft;
    public GameObject GreyOutCenter;
    public GameObject GreyOutRight;
    public GameObject MoveGreyOutLeft;
    public GameObject MoveGreyOutCenter;
    public GameObject MoveGreyOutRight;
    public GameObject ExBonusPtsStop;
    public GameObject BonusBtn1Glow;
    public GameObject BonusBtn2Glow;
    public GameObject BonusBtn3Glow;
    public BlockSpawn BlockSpawnLeft;
    public BlockSpawn BlockSpawnCenter;
    public BlockSpawn BlockSpawnRight;
    public GameState gameState;


    BlockSpawn bonusButton = null;
    int bonusHoldAmount = 0;
    int bonusNewAmount = 0;
    int bonusDeleteAmount = 0;
    int bonusBombAmount = 0;
    int bonusScoreSetPoint = 0;
    [SerializeField] int advertUseCounter = 0;
    [SerializeField] int advertUseCounterSetPoint = 0;
    [SerializeField] int advertUseScore = 0;
    [SerializeField] int advertUseScoreOffset = 0;
    [SerializeField] int advertUseScoreSetPoint = 0;

    [SerializeField] Text bonusHoldAmountText;
    [SerializeField] Text bonusNewAmountText;
    [SerializeField] Text bonusDeleteAmountText;
    [SerializeField] Text bonusBombAmountText;

    bool buttonGlow = false;
    bool pulseOn = false;
    float pulseTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bonusScoreSetPoint = gameState.GetBonusScore();
        advertUseCounter = gameState.GetAdvertUseCounter();
        advertUseScore = gameState.GetScore();
        advertUseCounterSetPoint = gameState.GetAdvertUseCounterSetPoint();
        advertUseScoreOffset = gameState.GetAdvertUseScoreOffset();

        // advert ready == true, not ready == false
        if (advertUseCounter >= advertUseCounterSetPoint || gameState.GetAdvertStatus() == false)
        {
            // turn on blocking image
            ExBonusPtsStop.SetActive(true);
        }
        else
        {
            // turn off blocking image
            ExBonusPtsStop.SetActive(false);
            advertUseScoreSetPoint = advertUseScore + advertUseScoreOffset;
        }
        
        if(advertUseScore >= advertUseScoreSetPoint)
        {
            gameState.AdvertUseCounterReset();
        }


        pulseTime = pulseTime + Time.deltaTime;

        if (pulseTime >= 1)
        {
            pulseTime = 0.0f;
        }

        if(pulseTime >= 0.0f && pulseTime <= 0.5f)
        {
            pulseOn = true;
        }
        else
        {
            pulseOn = false;
        }


        if(buttonGlow && bonusScoreSetPoint > 0)
        {
            if (pulseOn)
            {
                if (BlockSpawnLeft.hasBlock)
                {
                    BonusBtn1Glow.SetActive(true);
                }
                if (BlockSpawnCenter.hasBlock)
                {
                    BonusBtn2Glow.SetActive(true);
                }
                if (BlockSpawnRight.hasBlock)
                {
                    BonusBtn3Glow.SetActive(true);
                }
                
            }
            else
            {
                BonusBtn1Glow.SetActive(false);
                BonusBtn2Glow.SetActive(false);
                BonusBtn3Glow.SetActive(false);
            }
        }
        else
        {
            BonusBtn1Glow.SetActive(false);
            BonusBtn2Glow.SetActive(false);
            BonusBtn3Glow.SetActive(false);
        }
        
    }

    // called from bonus buttons
    public void OpenBonusPopup(BlockSpawn spawn)
    {
        

        if (spawn.hasBlock)
        {
            GreyOutOff();

            // which button was pressed Left, Center , Right
            bonusButton = spawn;

            // update bonus button text amounts
            bonusHoldAmountText.text = gameState.GetBonusHoldAmount().ToString();
            bonusNewAmountText.text = gameState.GetBonusNewAmount().ToString();
            bonusDeleteAmountText.text = gameState.GetBonusDeleteAmount().ToString();
            bonusBombAmountText.text = gameState.GetBonusBombAmount().ToString();

            // display bonus popup image with buttons
            BonusPopup.SetActive(true);

            if(spawn.name == "LeftSpawn")
            {
                BonusPopupLeft.SetActive(true);
                GreyOutCenter.SetActive(true);
                GreyOutRight.SetActive(true);
            }

            if (spawn.name == "CenterSpawn")
            {
                BonusPopupCenter.SetActive(true);
                GreyOutLeft.SetActive(true);
                GreyOutRight.SetActive(true);

            }

            if (spawn.name == "RightSpawn")
            {
                BonusPopupRight.SetActive(true);
                GreyOutCenter.SetActive(true);
                GreyOutLeft.SetActive(true);
            }
        }
    }
           
    public void BonusHoldButton()
    {
        bonusHoldAmount = gameState.GetBonusHoldAmount();

        // is there enough bonus points to use
        if (bonusScoreSetPoint >= bonusHoldAmount)
        {
            gameState.UseBonus(bonusHoldAmount);
            bonusButton.Hold();
            
        }
        CloseBonusPopup();
    }

    public void BonusNewButton()
    {
        bonusNewAmount = gameState.GetBonusNewAmount();

        // is there enough bonus points to use
        if (bonusScoreSetPoint >= bonusNewAmount)
        {
            gameState.UseBonus(bonusNewAmount);
            bonusButton.reference.anim.SetTrigger("New");
        }
        CloseBonusPopup();
    }

    public void BonusDeleteButton()
    {
        bonusDeleteAmount = gameState.GetBonusDeleteAmount();

        // is there enough bonus points to use
        if (bonusScoreSetPoint >= bonusDeleteAmount)
        {
            gameState.UseBonus(bonusDeleteAmount);
            bonusButton.reference.anim.SetTrigger("Deleting");            
        }
        CloseBonusPopup();
    }

    public void BonusBombButton()
    {
        bonusBombAmount = gameState.GetBonusBombAmount();

        // is there enough bonus points to use
        if (bonusScoreSetPoint >= bonusBombAmount)
        {
            gameState.UseBonus(bonusBombAmount);
            bonusButton.SpawnBlockBomb();
        }
        CloseBonusPopup();
    }

    public void CloseBonusPopup()
    {
        BonusPopup.SetActive(false);
        BonusPopupLeft.SetActive(false);
        BonusPopupCenter.SetActive(false);
        BonusPopupRight.SetActive(false);
        GreyOutLeft.SetActive(false);
        GreyOutCenter.SetActive(false);
        GreyOutRight.SetActive(false);

        if(GridManager.instance.blocksLocked)
        { GreyOutOn(); }
    }

    public void GreyOutOn()
    {
        MoveGreyOutLeft.SetActive(true);
        MoveGreyOutCenter.SetActive(true);
        MoveGreyOutRight.SetActive(true);

        

        buttonGlow = true;
    }

    public void GreyOutOff()
    {
        MoveGreyOutLeft.SetActive(false);
        MoveGreyOutCenter.SetActive(false);
        MoveGreyOutRight.SetActive(false);

        
        buttonGlow = false;
    }

}
