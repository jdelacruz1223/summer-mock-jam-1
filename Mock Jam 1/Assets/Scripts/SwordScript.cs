using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Vector2 PointerPos { get; set; }
    private Camera cam;
    [SerializeField] private Transform circleOrigin;
    [SerializeField] private float radius;
    [SerializeField] private GameObject fist;
    [SerializeField] private float knockbackForce = 1;
    [SerializeField] public float knockbackDuration = 0.5f;
    private bool canAttack;
    void Start()
    {
        cam = Camera.main;
        canAttack = true;
    }

    void Update()
    {
        PointerPos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.right = (PointerPos - (Vector2)transform.position).normalized;
        if(fist.activeSelf && canAttack) {
            DetectColliders();
            canAttack = false;
        } else if (!fist.activeSelf && !canAttack) {
            canAttack = true;
        }
    }
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }
    private void DetectColliders(){
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius)){
            if(collider.gameObject.CompareTag("Enemy")) {
                Debug.Log("EnemyHit!");
                collider.gameObject.GetComponent<EnemyParent>().Damage();

                Rigidbody2D enemyRigidbody = collider.GetComponent<Rigidbody2D>();
                Vector2 direction = (collider.transform.position - transform.position).normalized;
                Vector2 knockbackForceVector = direction * knockbackForce;
                enemyRigidbody.velocity = Vector2.zero;
                enemyRigidbody.AddForce(knockbackForceVector, ForceMode2D.Impulse);

                StartCoroutine
                (
                    ApplyKnockback
                    (
                        collider.gameObject, 
                        collider.transform.position, 
                        knockbackDuration
                    )
                );
            }
        }
    }

    IEnumerator ApplyKnockback(GameObject target, Vector2 targetPosition, float duration)
    {
        yield return new WaitForSeconds(duration);
        
        try
        {
            Rigidbody2D enemyRigidbody = target.GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                enemyRigidbody.velocity = Vector2.zero; // Stop the enemy after knockback
            } 
        } 
        catch (MissingReferenceException e)
        {
            Debug.Log("");
        }
        
        
    }
}
