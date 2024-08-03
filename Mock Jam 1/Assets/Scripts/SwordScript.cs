using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Vector2 PointerPos { get; set; }
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        PointerPos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.right = (PointerPos - (Vector2)transform.position).normalized;
    }
    
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Enemy")) {
            //Enemy will get hurt upon collision with the sword
        }
    }
}
