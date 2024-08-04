using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    [SerializeField] protected int health;

    void Start() {
        health = 2;
    }
    void Update() {
        if(health <= 0) {
            Destroy(gameObject);
        }
    }
    public void Damage() {
        health--;
    }
}
