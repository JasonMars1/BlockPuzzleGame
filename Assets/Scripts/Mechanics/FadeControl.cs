using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class FadeLine
{
    public GameObject[] blocks; //0 being first to fade
}


public class FadeControl : MonoBehaviour
{

    public List<FadeLine> fadersY = new List<FadeLine>();
    public List<FadeLine> fadersX = new List<FadeLine>();
    public float fadeInterval = 0.1f;
    public static FadeControl instance;
    [SerializeField] AudioClip winLineSound;
    public GameState gameState; // scriptable object

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    
    public void FadeOnLineY(int y, List<Sprite> colours)
    {
        for(int i = 0; i < colours.Count; i++)
        {
            fadersY[y].blocks[i].GetComponent<SpriteRenderer>().sprite = colours[i];
            print(colours[i]);
        }

        StartCoroutine(BeginFadeOnLineY(y));

        if (gameState.GetAudioMode() == 1)
        {
            AudioSource.PlayClipAtPoint(winLineSound, Camera.main.transform.position, 0.75f);
        }
    }

    public void FadeOnLineX(int x, List<Sprite> colours)
    {
        for (int i = 0; i < colours.Count; i++)
        {
            fadersX[x].blocks[i].GetComponent<SpriteRenderer>().sprite = colours[i];
        }

        StartCoroutine(BeginFadeOnLineX(x));

        if (gameState.GetAudioMode() == 1)
        {
            AudioSource.PlayClipAtPoint(winLineSound, Camera.main.transform.position, 1.0f);
        }
    }

    IEnumerator BeginFadeOnLineY(int y)
    {
        for (int i = 9; i > -1; i--)
        {
            fadersY[y].blocks[i].GetComponent<Animator>().SetTrigger("Return");
        }

        for (int i = 9; i > -1; i--)
        {
            fadersY[y].blocks[i].GetComponent<Animator>().SetTrigger("Fade");
            yield return new WaitForSeconds(fadeInterval);
        }
        yield return new WaitForSeconds(1);

    }

    IEnumerator BeginFadeOnLineX(int x)
    {
        for (int i = 0; i <10; i++)
        {
            fadersX[x].blocks[i].GetComponent<Animator>().SetTrigger("Return");
        }

        for (int i = 0; i < 10; i++)
        {
            fadersX[x].blocks[i].GetComponent<Animator>().SetTrigger("Fade");
            yield return new WaitForSeconds(fadeInterval);
        }
        yield return new WaitForSeconds(1);

    }
}
