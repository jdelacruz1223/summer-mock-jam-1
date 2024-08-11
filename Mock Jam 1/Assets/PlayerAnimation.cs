using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public enum MovementState 
    {
        idle,
        run,
        attack
    }
    MovementState state;
    private Animator anim;
    private SpriteRenderer sprite;
    private string prevDir;

    //
    //public PlayerScript playerScript;
    //
    
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        //playerScript = GetComponent<PlayerScript>();
    }
    
    public void animationUpdate(string direction, bool isMoving)
    {
        switch (direction)
        {
            case "down":
                if(prevDir == "left")
                {
                    sprite.flipX = true;
                }
                else sprite.flipX = false;
                if(isMoving)
                {
                    state = MovementState.run;
                    anim.SetBool("isMoving", true);
                    anim.SetInteger("state", 4);
                }
                else
                {
                    state = MovementState.idle;
                    anim.SetBool("isMoving", false);
                    anim.SetInteger("state", 0);
                }
                break;
            case "up":
                if(prevDir == "left")
                {
                    sprite.flipX = true;
                }
                else sprite.flipX = false;
                if(isMoving)
                {
                    state = MovementState.run;
                    anim.SetBool("isMoving", true);
                    anim.SetInteger("state", 4);
                }
                else
                {
                    state = MovementState.idle;
                    anim.SetBool("isMoving", false);
                    anim.SetInteger("state", 0);
                }
                break;
            case "left":
                sprite.flipX = true;
                if(isMoving)
                {
                    state = MovementState.run;
                    anim.SetBool("isMoving", true);
                    anim.SetInteger("state", 4);
                }
                else
                {
                    state = MovementState.idle;
                    anim.SetBool("isMoving", false);
                    anim.SetInteger("state", 0);
                }
            break;
            case "right":
                sprite.flipX = false;
                if(isMoving)
                {
                    state = MovementState.run;
                    anim.SetBool("isMoving", true);
                    anim.SetInteger("state", 3); 
                }
                else 
                {
                    state = MovementState.idle;
                    anim.SetBool("isMoving", false);
                    anim.SetInteger("state", 0);
                }
            break;
            case "default":
            break;
        }
        prevDir = direction;
    }

    public void attackAnimation()
    {
        state = MovementState.attack;
        anim.SetTrigger("isAttacking");
    }
}