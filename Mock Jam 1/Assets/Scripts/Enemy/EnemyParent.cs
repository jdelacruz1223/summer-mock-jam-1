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

        ChasePlayer();
        //HandleState();
    }
    public void Damage() {
        health--;
    }

    private void HandleState()
    {
        switch(currentState)
        {
            case MovementState.Chasing:
            Debug.Log("chasing!");
                ChasePlayer();
                break;
            case MovementState.Attacking:
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
}
