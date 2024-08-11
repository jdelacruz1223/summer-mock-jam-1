using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public enum PlayerState {
        alive,
        standing,
        attacking,
        moving,
        dead
    }
    public GameObject fist;
    [SerializeField] private int maxHealth = 4;
    [SerializeField] public float moveSpeed = 5;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] public float collisionOffset = 0.0f;
    [SerializeField] private float iFrameDuration = 1.5f;
    [SerializeField] private bool isInvulnerable;
    [SerializeField] private int curHealth;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private bool gameActive;
    [SerializeField] public Vector2 moveInput;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    [SerializeField] public bool canMove;
    [SerializeField] public int invincibleLayer;
    [SerializeField] public int defaultLayer;
    [SerializeField] public bool isWalking;
    [SerializeField] public bool isAttacking;
    [SerializeField] public string currentDirection;
    
    
     public PlayerAnimation anim;
    
    [SerializeField] private PlayerState currentState;
    private TimeHandler timeHandler;
    private float timeNotMoving;
    private string direction;

    private bool isMoving;

    void Start()
    {
        anim = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody2D>();
        defaultLayer = gameObject.layer;
        invincibleLayer = LayerMask.NameToLayer("Invincible");
        currentState = PlayerState.standing;
        curHealth = maxHealth;
        isInvulnerable = false;
        canMove = true;
        isWalking = false;
        timeHandler = GameObject.FindGameObjectWithTag("Time Manager").GetComponent<TimeHandler>();
        direction = "right";
        isMoving = true;
    }

    void Update()
    {
        if(currentState != PlayerState.dead && curHealth <= 0) {
            currentState = PlayerState.dead;
        } 
        Move();
        direction = getDirection();
        if (moveInput == Vector2.zero) isMoving = false;
        HandleState();
    }

    void HandleState()
    {
        switch (currentState)
        {
            case PlayerState.standing:
            timeNotMoving += Time.deltaTime;
            if (timeHandler.difficultyLevel > 1 && timeNotMoving >= 1f && !isInvulnerable) {
                timeNotMoving = 0;
                curHealth--;
                StartCoroutine(Iframes());
            } else if (timeHandler.difficultyLevel > 3 && !isInvulnerable) {
                curHealth--;
                StartCoroutine(Iframes());
            }
            isMoving = false;
            break;
            case PlayerState.moving:
            isMoving = true;
            timeNotMoving = 0;
            break;
            case PlayerState.attacking:
            StartCoroutine(Attack());
            isMoving = false;
            break;
            case PlayerState.dead:
            Application.Quit();
            break;
        }
        anim.animationUpdate(direction, isMoving);
            
    }

    void OnAttack()
    {
        if(!fist.activeSelf && currentState != PlayerState.dead)
        {
            currentState = PlayerState.attacking;
        }
        
    }

    void OnMove(InputValue moveValue)
    {
        //Debug.Log("OnMove");
        // if(!canMove)
        // {
        //     return;
        // }
        currentState = PlayerState.moving;
        moveInput = moveValue.Get<Vector2>();
    }

    public void Move()
    {
        if(moveInput != Vector2.zero)
        {
            bool success = TryMove(moveInput);

            if(!success)
            {
                success = TryMove(new Vector2(moveInput.x, 0));
                if(!success)
                {
                    success = TryMove(new Vector2(0, moveInput.y));
                }
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );
        if(count == 0) 
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } 
        else
        {
            return false;
        }
    }
    
    private IEnumerator Attack(){
        //Debug.Log("Attacking");
        fist.SetActive(true);
        yield return new WaitForSeconds(attackDelay);
        fist.SetActive(false);
        currentState = PlayerState.standing;
    }
    private IEnumerator Iframes(){
        isInvulnerable = true;
        gameObject.layer = invincibleLayer;
        yield return new WaitForSeconds(iFrameDuration);
        gameObject.layer = defaultLayer;
        isInvulnerable = false;
    }
    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Enemy") && !isInvulnerable) {
            //Player will get hurt
            Debug.Log("ouch");
            curHealth--;
            StartCoroutine(Iframes());
        }
    }
    
    public PlayerState getPlayerState(){
        return currentState;
    }
    public float getTimeNotMoving() {
        return timeNotMoving;
    }

    public string getDirection()
    {
        if (moveInput.y > 0f) //up
        {
            return currentDirection = "up";
        }
        else if (moveInput.y < 0f) //down
        {
            return currentDirection = "down";
        }
        else if (moveInput.x < 0f) //left
        {
            return currentDirection = "left";
        }
        else if (moveInput.x > 0f) //right
        {
            return currentDirection = "right";
        }
        else
        {
            return currentDirection;
        }
    }
}
