using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    [SerializeField] protected int health { get; set; }

    void Start() {
        health = 2;
    }

    protected void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("PlayerAttack")) {
            health--;
            Debug.Log("Enemy hit!");
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
