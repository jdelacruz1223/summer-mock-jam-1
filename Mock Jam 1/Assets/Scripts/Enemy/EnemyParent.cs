using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    public enum MovementState
    {
        Chasing,
        Attacking
    }
    public MovementState currentState;
    [SerializeField] protected int health;
    [SerializeField] public bool isChasing;
    [SerializeField] public int speed;
    [SerializeField] public GameObject player;
    public Rigidbody2D rb;
    [SerializeField] public float attackRange = 0.5f;
    [SerializeField] public int attackRecoveryDelay = 1; 
    void Start() {
        rb = GetComponent<Rigidbody2D>();

        health = 2;
        currentState = MovementState.Chasing;
    }
    void Update() {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;
        }

        if(health <= 0) {
            Destroy(gameObject);
        }


        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= attackRange)
        {
            currentState = MovementState.Attacking;
            
        }
        else
        {
            currentState = MovementState.Chasing;
        }
        HandleState();
    }
    public void Damage() {
        health--;
    }

    private void DebugRay()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, direction);

        Debug.DrawRay(ray.origin, ray.direction * attackRange, Color.red);
    }

    private void HandleState()
    {
        switch(currentState)
        {
            case MovementState.Chasing:
                ChasePlayer();
                break;
            case MovementState.Attacking:
                StartCoroutine(PauseCoroutine());
                break;
            default:
                Debug.LogWarning("Unknown State");
                break;
        }
    }

    private void ChasePlayer()
    {
        isChasing = true;
        transform.position = Vector2.MoveTowards
        (
            this.transform.position,
            player.transform.position,
            speed * Time.deltaTime
        );
    }

    public IEnumerator PauseCoroutine()
    {
        yield return new WaitForSeconds(attackRecoveryDelay);
    }

    
}
