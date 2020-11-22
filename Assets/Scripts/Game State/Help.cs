using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    public GameObject helpPopup1; // link in inspector
    public GameObject helpPopup2; // link in inspector
    public GameObject helpPopup3; // link in inspector
    public GameObject helpPopup4; // link in inspector
    public GameObject helpPopup5; // link in inspector
    [SerializeField] int currentHelpPage = 0;
    [SerializeField] int lastHelpPage = 0;
    [SerializeField] int nextHelpPage = 0;
    [SerializeField] int maxHelpPage = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHelpPage < maxHelpPage)
        {
            nextHelpPage = currentHelpPage + 1;
        }
        else nextHelpPage = 1;

        if (currentHelpPage > 1)
        {
            lastHelpPage = currentHelpPage - 1;
        }
        else lastHelpPage = maxHelpPage;
    }

    public void OpenHelpPopup()
    {
        currentHelpPage = 1;
        // display settings popup image with buttons
        helpPopup1.SetActive(true);
        
                    
    }

    public void CloseHelpPopup()
    {     
        switch (currentHelpPage)
        {
            case 1:
                // close settings popup image with buttons
                helpPopup1.SetActive(false);
                break;
            case 2:
                // close settings popup image with buttons
                helpPopup2.SetActive(false);
                break;
            case 3:
                // close settings popup image with buttons
                helpPopup3.SetActive(false);
                break;
            case 4:
                // close settings popup image with buttons
                helpPopup4.SetActive(false);
                break;
            case 5:
                // close settings popup image with buttons
                helpPopup5.SetActive(false);
                break;
            default:
                // close settings popup image with buttons
                helpPopup1.SetActive(false);
                break;
        }

    }

    public void OpenNextPopup()
    {
        CloseHelpPopup();

        switch (nextHelpPage)
        {
            case 1:
                // close settings popup image with buttons
                helpPopup1.SetActive(true);
                currentHelpPage = 1;
                break;
            case 2:
                // close settings popup image with buttons
                helpPopup2.SetActive(true);
                currentHelpPage = 2;
                break;
            case 3:
                // close settings popup image with buttons
                helpPopup3.SetActive(true);
                currentHelpPage = 3;
                break;
            case 4:
                // close settings popup image with buttons
                helpPopup4.SetActive(true);
                currentHelpPage = 4;
                break;
            case 5:
                // close settings popup image with buttons
                helpPopup5.SetActive(true);
                currentHelpPage = 5;
                break;
            default:
                // close settings popup image with buttons
                helpPopup1.SetActive(true);
                currentHelpPage = 1;
                break;
        }
    }

    public void OpenLastPopup()
    {
        CloseHelpPopup();

        switch (lastHelpPage)
        {
            case 1:
                // close settings popup image with buttons
                helpPopup1.SetActive(true);
                currentHelpPage = 1;
                break;
            case 2:
                // close settings popup image with buttons
                helpPopup2.SetActive(true);
                currentHelpPage = 2;
                break;
            case 3:
                // close settings popup image with buttons
                helpPopup3.SetActive(true);
                currentHelpPage = 3;
                break;
            case 4:
                // close settings popup image with buttons
                helpPopup4.SetActive(true);
                currentHelpPage = 4;
                break;
            case 5:
                // close settings popup image with buttons
                helpPopup5.SetActive(true);
                currentHelpPage = 5;
                break;
            default:
                // close settings popup image with buttons
                helpPopup1.SetActive(true);
                currentHelpPage = 1;
                break;
        }
    }


}
