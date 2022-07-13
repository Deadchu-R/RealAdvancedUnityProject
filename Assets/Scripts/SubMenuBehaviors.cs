using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class SubMenuBehaviors : MonoBehaviour
{
    public Transform startSubMenu;
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
    [FormerlySerializedAs("SFXSlider")] public GameObject sfxSlider;

    private bool _start = false;
    private bool _options = false;
    private bool _masterSlideOn = false;
    private bool _musicSlideOn = false;
    private bool _sfxSlideOn = false;

    private float animDuration = 1.2f;

    public void StartSubMenu()
    {
        if (_start == false)
        {
            startSub.SetActive(true);
            startSubMenu.DOMoveX(750, animDuration);
            _start = true;
        }
        else if (_start)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(startSubMenu.DOMoveX(450, animDuration));
            seq.OnComplete(ShutDown);
            _start = false;
        }
    }

    public void OptionsSubMenu()
    {
        if (_options == false)
        {
            optionSub.SetActive(true);
            optionSubMenu.DOMoveX(1280, animDuration);
            _options = true;
        }
        else if (_options)
        {
            masterSlider.SetActive(false);
            musicSlider.SetActive(false);
            sfxSlider.SetActive(false);
            Sequence seq2 = DOTween.Sequence();
            seq2.Append(optionSubMenu.DOMoveX(970, animDuration));
            seq2.OnComplete(ShutDown);
            _options = false;
        }
    }
    private void ShutDown()
    {
        startSub.SetActive(false);    
        optionSub.SetActive(false);
    }

    public void MasterSlide()
    {
        if (_masterSlideOn == false )
        {
            masterSlider.SetActive(true);
            _masterSlideOn = true;
        }
        else if (_masterSlideOn)
        {
            masterSlider.SetActive(false);
            _masterSlideOn = false;
        }
        
    }

    public void MusicSlide()
    {
        if (_musicSlideOn == false)
        {
            musicSlider.SetActive(true);
            _musicSlideOn = true;
        }
        else if (_musicSlideOn)
        {
            musicSlider.SetActive(false);
            _musicSlideOn = false;
        }
    }

    public void SfxSlide()
    {
        if (_sfxSlideOn == false)
        {
            sfxSlider.SetActive(true);
            _sfxSlideOn = true;
        }
        else if (_sfxSlideOn == true)
        {
            sfxSlider.SetActive(false);
            _sfxSlideOn = false;
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

