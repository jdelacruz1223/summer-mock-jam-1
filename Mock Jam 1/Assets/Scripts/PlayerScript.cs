using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerScript : MonoBehaviour
{
    private enum PlayerState {
        alive,
        dead
    }
    public GameObject fist;
    [SerializeField] private int maxHealth = 4;
    [SerializeField] public float moveSpeed = 5;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] public float collisionOffset = 0.0f;
    [SerializeField] private float iFrameDuration = 1.5f;
    private bool isInvulnerable;
    private int curHealth;
    private Rigidbody2D rb;
    
    private PlayerState playerstate;

    // Start is called before the first frame update
    void Start()
    {
        playerstate = PlayerState.alive;
        curHealth = maxHealth;
        isInvulnerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerstate != PlayerState.dead) {
            if(Input.GetKey(KeyCode.W)) {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.A)) {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.S)) {
                transform.position += Vector3.down * moveSpeed * Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.D)) {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
            if(Input.GetMouseButtonDown(0) && !fist.activeSelf) {
                StartCoroutine(Attack());
            }
            if(curHealth <= 0) {
                playerstate = PlayerState.dead;
            }
        } 
    }

    
    
    private IEnumerator Attack(){
        Debug.Log("Attacking");
        fist.SetActive(true);
        yield return new WaitForSeconds(attackDelay);
        fist.SetActive(false);
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
