using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    private bool moving = false;
    private bool forward = true;
    [SerializeField] private GameObject firstPoint;
    [SerializeField] private GameObject secondPoint;
    [SerializeField] private GameObject switchPoint;
    [SerializeField] private GameObject returnPoint;
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject leftPlatformCollider;
    [SerializeField] private GameObject rightPlatformCollider;




    private void FixedUpdate()
    {
        if (moving && forward)
        {
            transform.position += (velocity * Time.deltaTime);
        }
        else if (moving && !forward)
        {
            transform.position -= (velocity * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            leftPlatformCollider.gameObject.SetActive(true);
            rightPlatformCollider.gameObject.SetActive(true);
            collision.collider.transform.SetParent(transform);
            moving = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            leftPlatformCollider.gameObject.SetActive(false);
            rightPlatformCollider.gameObject.SetActive(false);
            moving = false;
            collision.collider.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SwitchPoint"))
        {
            switchPoint.transform.SetParent(transform);
            Platform.transform.SetParent(null);
        }
        else if (collision.gameObject.CompareTag("ReturnPoint"))
        {
            switchPoint.transform.SetParent(null);
            Platform.transform.SetParent(transform);
        }

        else if (collision.gameObject.CompareTag("SecondP"))
        {
            moving = false;
            forward = false;
            Debug.Log("2nd point");

        }
        else if (collision.gameObject.CompareTag("FirstP"))
        {
            moving = false;
            forward = true;
        }



    }




}
