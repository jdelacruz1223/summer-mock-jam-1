using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerScript : MonoBehaviour
{
    private enum PlayerState {
        alive,
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
     
    
    [SerializeField] private PlayerState currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = PlayerState.alive;
        curHealth = maxHealth;
        isInvulnerable = false;
        canMove = true;
    }

    void Update()
    {
        // if(currentState != PlayerState.dead) {
        //     if(Input.GetKey(KeyCode.W)) {
        //         transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        //     }
        //     if(Input.GetKey(KeyCode.A)) {
        //         transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        //     }
        //     if(Input.GetKey(KeyCode.S)) {
        //         transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        //     }
        //     if(Input.GetKey(KeyCode.D)) {
        //         transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        //     }
        // }

        if(currentState != PlayerState.dead && curHealth <= 0) {
            currentState = PlayerState.dead;
        } 
        Move();
        HandleState();
    }

    void HandleState()
    {
        switch (currentState)
        {
            case PlayerState.alive:
            gameActive = true;
            break;
            case PlayerState.moving:
            //
            break;
            case PlayerState.attacking:
            StartCoroutine(Attack());
            break;
            case PlayerState.dead:
            gameActive = false;
            //game over scene
            break;
        }
            
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
            Debug.Log(count);
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
        Debug.Log("Attacking");
        fist.SetActive(true);
        yield return new WaitForSeconds(attackDelay);
        fist.SetActive(false);
        currentState = PlayerState.alive;
    }
    private IEnumerator Iframes(){
        isInvulnerable = true;
        yield return new WaitForSeconds(iFrameDuration);
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
}
