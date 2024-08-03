using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    [SerializeField] private float swordOffset = 10;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(Input.mousePosition);
        transform.position += transform.forward * swordOffset;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(Input.mousePosition);
    }
    
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Enemy")) {
            //Enemy will get hurt upon collision with the sword
        }
    }
}
