using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    [SerializeField] protected int health { get; set; }

    void Start() {
        health = 2;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PlayerAttack")) {
            health--;
            Debug.Log("Enemy hit!");
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
