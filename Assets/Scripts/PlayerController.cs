
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Animations Properties:")]
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
    private static readonly int AttackState = Animator.StringToHash("AttackState");
    private static readonly int Shield = Animator.StringToHash("Shield");
    private static readonly int Jumping = Animator.StringToHash("Jumping");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int Retry = Animator.StringToHash("Retry");
    [SerializeField] private Animator playerAni;
    [SerializeField] private AnimationClip[] attackAnimations;

    [Header("Actions Booleans and numbers")]
    private float _moveHorizontal;
    private bool _canMove = true;
    private bool _canAttack = true;
    private bool _moreJump;
    private bool _doJump ;
    private bool _shieldUp;
    private bool _attack;
    private int _attackNum;
    private float _moveSpeed = 1;
    private bool _canWalk = true;
    private bool _isFacingRight = true;
    
    [Header("UI Elements")]
    [SerializeField] private GameObject checkPointCanvas;
    [SerializeField]  private Image healthBar;
    [SerializeField] private GameObject gameOver;

    [Header("PlayerElements:")]
    public float maxHealth;
    [SerializeField] private float walkSpeed;
    [SerializeField] private Text jumpText;
    [SerializeField] private float runSpeed;
    [SerializeField] private int jumpTimes = 2;
    [SerializeField] private float jumpForce = 7;
    [SerializeField] private float fallMulti;

    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform cameraLocationForPlayer;
    private int _remainingJumps;
    private float _currentHealth;
    private float _moveDirection;
    

    [Header("Wall Jump Properties:")] 
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask stickyWallMask;
   [SerializeField] private GameObject groundCheckObject;
   [SerializeField] private GameObject sidesCheckObject;
   private bool _grounded = true;
    private bool _wallJumping;
    private float _currentJumpForce;
    private bool _isTouchingRight;
    private bool _isTouchingLeft;
    private int _touchingLeftOrRight;


    
    [Header("RigidBodies and Colliders:")]
    [SerializeField] private Rigidbody2D rigidBody;


    private void Awake()
    {
        _currentJumpForce = jumpForce;
        _remainingJumps = jumpTimes;
        _currentHealth = maxHealth;
    }

    void Flip()
    {

        transform.Rotate(0f, 180f,0f);
        
        //  if (moveDirection > 0)
        // {
        //     sidesCheckObject.transform.localPosition = new Vector3(-0.091f, -0.01f, 0f);
        //    
        // }
        // if (moveDirection < 0)
        // {
        //  
        //     sidesCheckObject.transform.localPosition = new Vector3(-0.144f, -0.01f, 0f);
        // }
        _isFacingRight = !_isFacingRight;
    }

    private void Update()
    {
        jumpText.text = ":" + _remainingJumps;
        playerCamera.transform.position = new Vector3(cameraLocationForPlayer.transform.position.x, cameraLocationForPlayer.transform.position.y, -10);
        if (_isFacingRight && _moveDirection < 0)
        {
            
            Flip();
            
        }
        else if (!_isFacingRight && _moveDirection > 0)
        {
      
            Flip();
            
        }
      
        LayerCheck();
        Testing();
        HealthBar();
        InputBool();
        if (_canMove)
        {
         Inputs();
        }

        

    }



    private void HealthBar()
    {
        healthBar.fillAmount = _currentHealth / maxHealth;
    }

    private void Inputs()
    {
        if (Input.GetButtonDown("Attack") && _canAttack)
        {
            if (_attackNum >= attackAnimations.Length)
            {
                _attackNum = 1;
            }
            else
            {
                _attackNum++;
            }
            _attack = true;
        }
        if (Input.GetButtonDown("Shield"))
        {
            _shieldUp = true;
        }
        if (Input.GetButton("Horizontal"))
        {
            _moveDirection = Input.GetAxisRaw("Horizontal");
            playerAni.SetBool(Walking, true);
        }
        if (!Input.GetButton("Horizontal"))
        {
            playerAni.SetBool(Walking, false);
        }
        if (Input.GetButton("Run"))
        {
            _moveSpeed = runSpeed;
            playerAni.SetBool(Running, true);
        }
        if (!Input.GetButton("Run"))
        {
            _moveSpeed = walkSpeed;
            playerAni.SetBool(Running, false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (_remainingJumps > 0)
            { 
                _moreJump = true;
                _remainingJumps--;
            }
            else
            {
                _moreJump = false;
            }
            
            if ((_isTouchingRight || _isTouchingLeft) && !_grounded)
            {
                _wallJumping = true;
                Invoke(nameof(SetWallJumpingToFalse), 0.16f);
            }
            else
            {
             _doJump = true;
            }
            _canWalk = true;
        }
    }

    private void SetWallJumpingToFalse()
    {
        _wallJumping = false;
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
         Actions();
        }
    }
    private void Actions()
    {
        if (_canAttack)
        {
         Attack(_attackNum);
        }
        

        ShieldBlock();
        if (_canWalk)
        {
         Move();
        }
        Jump();
        WallJump();

    }

    private void WallJump()
    {
        if (_wallJumping)
        {
            rigidBody.velocity = new Vector2(0f, 0f);
            rigidBody.AddForce(new Vector2(jumpForce / 2 * _touchingLeftOrRight, jumpForce / 2), ForceMode2D.Impulse);
            playerAni.SetTrigger(Jumping);
        }
    }

    private void Attack(int attackNumber)
    {
        if (_attack)
        {
            playerAni.SetTrigger(AttackTrigger);
            playerAni.SetInteger(AttackState, attackNumber);
            _attack = false;
        }

    }

    private void ShieldBlock()
    {
        if (_shieldUp)
        {
            playerAni.SetTrigger(Shield);
         _shieldUp = false;
         Debug.Log("shield");
        }

    }

    private void Move()
    {
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        
        if (_moveHorizontal > 0.1f || _moveHorizontal < -0.1f)
        {
            rigidBody.AddForce(new Vector2(_moveHorizontal * _moveSpeed, 0f), ForceMode2D.Impulse);
        }
        
    }
    private void Jump()
    {
  
        if (_doJump)
        {
            if (_grounded)
            {
                rigidBody.velocity = new Vector2(0f, 0f);
                rigidBody.AddForce(new Vector2(0f , _currentJumpForce), ForceMode2D.Impulse);
                playerAni.SetTrigger(Jumping);
            }
            else
            {
                if (_moreJump)
                {
                    rigidBody.velocity = new Vector2(0f, 0f);
                    rigidBody.AddForce(new Vector2(0f ,_currentJumpForce), ForceMode2D.Impulse);
                  playerAni.SetTrigger(Jumping);
                }
            }
            _doJump = false;
        }
        
        if (rigidBody.velocity.y < 0 && !_wallJumping)
        {
            rigidBody.gravityScale += fallMulti;
        } 
        else if (rigidBody.velocity.y >= 0 )
        {
            rigidBody.gravityScale = 1;
        }
    }
    
    private void Testing()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Damage(5);
        }
    }

    private void LayerCheck()
    {
        var groundPosition = groundCheckObject.transform.position;
        var sidesPosition = sidesCheckObject.transform.position;
        _grounded = Physics2D.OverlapBox(new Vector2(groundPosition.x , groundPosition.y), new Vector2(0.9f, 0.1f), 0f, groundMask); //checks if player touching the ground
        _isTouchingLeft = Physics2D.OverlapBox(new Vector2(sidesPosition.x -0.9f , sidesPosition.y), new Vector2(0.1f,2.7f), 0f , stickyWallMask); // checks if player touching a stickWall from right
         _isTouchingRight = Physics2D.OverlapBox(new Vector2(sidesPosition.x  , sidesPosition.y), new Vector2(0.1f,2.7f), 0f , stickyWallMask); // checks if player touching a stickWall from left
         
         if (_isTouchingLeft)
         {
             _touchingLeftOrRight = 1;
             _doJump = false;
         }
         else if (_isTouchingRight)
         {
             _touchingLeftOrRight = -1;
             _doJump = false;
         }
    }
    private void OnDrawGizmos()
    {
        var groundPosition = groundCheckObject.transform.position;
        var sidesPosition = sidesCheckObject.transform.position;
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(groundPosition.x , groundPosition.y), new Vector2(0.9f,0.1f));
        
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(new Vector2(sidesPosition.x -0.9f , sidesPosition.y), new Vector2(0.1f,2.7f));
        
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(new Vector2(sidesPosition.x  , sidesPosition.y), new Vector2(0.1f,2.7f));
    }



    public void Damage(float dmg)
    {
        playerAni.SetTrigger(Hit);
        _currentHealth -= dmg;
        Debug.Log($"got:{dmg} dmg");

        if (_currentHealth <= 0)
        {
            Died();
        }
    }

    private void Died()
    {
        
        Debug.Log("Died");
            playerAni.SetTrigger(Dead);
            playerAni.SetBool(Retry, false);
            _canMove = false;
            Invoke(nameof(GameOver), 2f);
    }

    private void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public void InputBool()
    {
        if (checkPointCanvas.activeSelf || gameOver.activeSelf)
        {
            _canAttack = false;
            _canMove = false;
        }
        else
        {
         _canMove = true;
         _canAttack = true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            _remainingJumps = jumpTimes;
            Debug.Log($"refilled jumps to {_remainingJumps}");
        }

        if (col.gameObject.CompareTag("FallLimiter"))
        {
            rigidBody.gravityScale = 1;
            GameOver();
        }
    }

    public void SavePlayerAtCheckPoint()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayerSave()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
         
        _currentHealth = playerData.health;
        transform.position = new Vector3(playerData.position[0], playerData.position[1] +1, playerData.position[2]);
        Time.timeScale = 1;
        gameOver.SetActive(false);
        _canMove = true;
        playerAni.SetBool(Retry, true);
    }

}
