using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubMenuBehaviors : MonoBehaviour
{
    public Transform startsubMenu;
    public Transform optionSubMenu;

    public GameObject mainMenu;
    public GameObject controlMenu;
    public GameObject startSub;
    public GameObject optionSub;
    public GameObject subMenu;
    public GameObject keyboardControls;
    public GameObject controllerControls;

    public GameObject masterSlider;
    public GameObject musicSlider;
    public GameObject SFXSlider;

    private bool start = false;
    private bool options = false;
    private bool masterSlideOn = false;
    private bool musicSlideOn = false;
    private bool SFXSlideOn = false;

    private float animDuration = 1.2f;

    public void StartSubMenu()
    {
        if (start == false)
        {
            startSub.SetActive(true);
            startsubMenu.DOMoveX(750, animDuration);
            start = true;
        }
        else if (start == true)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(startsubMenu.DOMoveX(450, animDuration));
            seq.OnComplete(ShutDown);
            start = false;
        }
    }

    public void OptionsSubMenu()
    {
        if (options == false)
        {
            optionSub.SetActive(true);
            optionSubMenu.DOMoveX(1280, animDuration);
            options = true;
        }
        else if (options == true)
        {
            masterSlider.SetActive(false);
            musicSlider.SetActive(false);
            SFXSlider.SetActive(false);
            Sequence seq2 = DOTween.Sequence();
            seq2.Append(optionSubMenu.DOMoveX(970, animDuration));
            seq2.OnComplete(ShutDown);
            options = false;
        }
    }
    private void ShutDown()
    {
        startSub.SetActive(false);    
        optionSub.SetActive(false);
    }

    public void MasterSlide()
    {
        if (masterSlideOn == false )
        {
            masterSlider.SetActive(true);
            masterSlideOn = true;
        }
        else if (masterSlideOn == true)
        {
            masterSlider.SetActive(false);
            masterSlideOn = false;
        }
        
    }

    public void MusicSlide()
    {
        if (musicSlideOn == false)
        {
            musicSlider.SetActive(true);
            musicSlideOn = true;
        }
        else if (musicSlideOn == true)
        {
            musicSlider.SetActive(false);
            musicSlideOn = false;
        }
    }

    public void SFXSlide()
    {
        if (SFXSlideOn == false)
        {
            SFXSlider.SetActive(true);
            SFXSlideOn = true;
        }
        else if (SFXSlideOn == true)
        {
            SFXSlider.SetActive(false);
            SFXSlideOn = false;
        }
    }

    public void EnterControlMenu()
    {
        mainMenu.SetActive(false);
        controlMenu.SetActive(true);
    }

    public void ExitControlMenu()
    {
        controlMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void KeyboardControls()
    {
        keyboardControls.SetActive(true);
        controllerControls.SetActive(false);
    }
    public void ControllerControls()
    {
        keyboardControls.SetActive(false);
        controllerControls.SetActive(true);
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

