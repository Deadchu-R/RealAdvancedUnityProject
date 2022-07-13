using UnityEngine;
using UnityEngine.Serialization;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private GameObject switchPoint;
    [FormerlySerializedAs("Platform")] [SerializeField] private GameObject platform;
    [SerializeField] private GameObject leftPlatformCollider;
    [SerializeField] private GameObject rightPlatformCollider;
    private bool _moving = false;
    private bool _forward = true;




    private void FixedUpdate()
    {
        if (_moving && _forward)
        {
            transform.position += (velocity * Time.deltaTime);
        }
        else if (_moving && !_forward)
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
            _moving = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            leftPlatformCollider.gameObject.SetActive(false);
            rightPlatformCollider.gameObject.SetActive(false);
            _moving = false;
            collision.collider.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SwitchPoint"))
        {
            switchPoint.transform.SetParent(transform);
            platform.transform.SetParent(null);
        }
        else if (collision.gameObject.CompareTag("ReturnPoint"))
        {
            switchPoint.transform.SetParent(null);
            platform.transform.SetParent(transform);
        }

        else if (collision.gameObject.CompareTag("SecondP"))
        {
            _moving = false;
            _forward = false;
            Debug.Log("2nd point");

        }
        else if (collision.gameObject.CompareTag("FirstP"))
        {
            _moving = false;
            _forward = true;
        }



    }




}
