using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
  [SerializeField] private Sprite[] _sprites;
  [SerializeField] private GameObject _checkPointCanvas;
  private bool canPress = false;

  private void Update()
  {
      
      if (Input.GetKeyDown(KeyCode.KeypadEnter))
      {
          if (canPress == true)
          {
           _checkPointCanvas.SetActive(true);
           Time.timeScale = 0;
          }

      }
  }



  public void CloseCheckPointCanvas()
  {
      _checkPointCanvas.SetActive(false);
      Time.timeScale = 1;
  }

  private void OnTriggerEnter2D(Collider2D col)
  {
      if (col.gameObject.CompareTag("Player"))
      {
          Debug.Log("Player in checkPoint");
          canPress = true;
      }
      
  }

  private void OnTriggerExit2D(Collider2D col)
  {
      if (col.gameObject.CompareTag("Player"))
      {
          canPress = false;
      }
  }
}
