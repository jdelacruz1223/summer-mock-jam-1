using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerScript : MonoBehaviour
{
    private enum PlayerState {
        alive, dead
    }
    [SerializeField] private GameObject sword;
    [SerializeField] private int maxHealth = 4;
    [SerializeField] private float moveSpeed = 5;
    private int curHealth;
    private PlayerState playerstate;

    // Start is called before the first frame update
    void Start()
    {
        playerstate = PlayerState.alive;
        curHealth = maxHealth;
        sword.SetActive(false);
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
            if(Input.GetMouseButton(0)) {
                StartCoroutine(Attack());
            }
            if(curHealth <= 0) {
                playerstate = PlayerState.dead;
            }
        }
        
    }
    
    private IEnumerator Attack(){
        sword.SetActive(true);
        yield return new WaitForSeconds(2f);
        sword.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Enemy")) {
            //Player will get hurt
            curHealth--;
        }
    }
}