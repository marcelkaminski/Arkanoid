using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int points;
    public int hitsToBreak;
    public Sprite secondSprite;
    public Sprite thirdSprite;

    public void BreakBrick()
    {
        hitsToBreak--;
        if(hitsToBreak == 1)
        {
            GetComponent<SpriteRenderer> ().sprite = secondSprite;
        }
        else if(hitsToBreak == 2)
        {
            GetComponent<SpriteRenderer> ().sprite =thirdSprite;
        }
    }
}
