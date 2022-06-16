using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubMenuBehaviors : MonoBehaviour
{
    public Transform startsubMenu;
    public Transform optionSubMenu;
    public GameObject startSub;
    public GameObject optionSub;
    public GameObject subMenu;
    public int Start = 0;
    public int Options = 0;
    public float animDuration;

    public void StartSubMenu()
    {
        if (Start == 0)
        {
            startSub.SetActive(true);
            startsubMenu.DOMoveX(1270, animDuration);
            Start++;
        }
        else if (Start == 1)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(startsubMenu.DOMoveX(970, animDuration));
            seq.OnComplete(ShutDown);
            Start--;
        }
    }

    public void OptionsSubMenu()
    {
        if (Options == 0)
        {
            optionSub.SetActive(true);
            optionSubMenu.DOMoveX(1270, animDuration);
            Options++;
        }
        else if (Options == 1)
        {
            Sequence seq2 = DOTween.Sequence();
            seq2.Append(optionSubMenu.DOMoveX(970, animDuration));
            seq2.OnComplete(ShutDown);
            Options--;
        }
    }


    private void ShutDown()
    {
        startSub.SetActive(false);    
        optionSub.SetActive(false);
    }

    //IEnumerator StartMenuDelay()
    //{
    ////StartCoroutine(OptionsMenuDelay());
    //IEnumerator StartMenuDelay()
    //{
    //    yield return new WaitForSeconds(1);
    //    startSub.SetActive(false);
    //}

    //IEnumerator OptionsMenuDelay()
    //{
    //    yield return new WaitForSeconds(1);
    //    optionSub.SetActive(false);
    //}
}

