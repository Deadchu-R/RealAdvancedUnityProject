using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //1 object that is platform(the player's floor) , 1 object that is a tree , the tree's position to move with the platform. (tree's transform position y)
    //disable player's movement abilities when the platform is moving
    
    [SerializeField] private GameObject platForm;
    [SerializeField] private GameObject platFormTree;

    [SerializeField] private float platformTimer = 2;
    [SerializeField] private GameObject FirstP;
    [SerializeField] private GameObject Fatform;
    [SerializeField] private GameObject SecondP;

    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private GameObject player;

    [SerializeField] float offsetLeft = 0, offsetRight = 0, speed = 1;
    private bool ReachedRight = false, ReachedLeft = false;
    [SerializeField] private bool playerOnPlat = false;
    Vector3 startposition = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        startposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnPlat)
            PlatformForward();
    }

    void Move(float offset)  //platform's movement
    {
        transform.position = Vector3.MoveTowards
            (
            transform.position,
            new Vector3(startposition.x + offset,
            transform.position.y,
            transform.position.z),
            speed * Time.deltaTime
            );
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.CompareTag("SecondP"))
        {
            Fatform.transform.SetParent(SecondP.transform, false);
            //disable player movement
            //platFormTree.transform.position.y;
            Debug.Log("new parent");
        }

        else if (coll.gameObject.CompareTag("FirstP"))
        {
            Fatform.transform.SetParent(FirstP.transform, false);
            //disable player movement
            //platFormTree.transform.position.y;
            Debug.Log("new parent");
            
        }

        else if (coll.gameObject.CompareTag("Player"))
        {
            playerOnPlat = true;
            
            Debug.Log("on platform");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            player.transform.parent = null;
        }
    }

    private void PlatformForward()
    {
        if (!ReachedRight)
        {

            //player movement disabled during platform movement
            if (transform.position.x < startposition.x + offsetRight)
            {                   
                Move(offsetRight);
                player.transform.SetParent(FirstP.transform, false);
            }
            else if (transform.position.x >= startposition.x + offsetRight)
            {
                ReachedRight = true;
                player.transform.DetachChildren();
                ReachedLeft = false;
            }
            Invoke(nameof(PlatformBackward), platformTimer);

        }
    }

    private void PlatformBackward()
    {
        
        if (!ReachedLeft)
        {
            if (transform.position.x > startposition.x + offsetLeft)
            {
                Move(offsetLeft);
            }
            else if (transform.position.x <= startposition.x + offsetLeft)
            {
                ReachedRight = false;
                ReachedLeft = true;
            }
        }
        playerOnPlat = false;
    }

}
