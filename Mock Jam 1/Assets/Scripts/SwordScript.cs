using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Vector2 PointerPos { get; set; }
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        PointerPos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.right = (PointerPos - (Vector2)transform.position).normalized;
    }
}
