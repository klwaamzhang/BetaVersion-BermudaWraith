using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [Header("Variables for Enemy")]

    public float maxTravelDistance;
    //max travel distance is the maximum distance the enemy can travel in every direction equally
    //         x                     x           x = maxTravelDistance
    //   <---------------- O ---------------->

    //movement speed of the enemy
    public int speed;

    public float attackRange = 1.3f;

    public int health = 2;

    //time it waits before damaging the player again
    public float damageWait = 2;

    private CharacterController_2D playerController;

    //reference to the player object's transform
    private Transform playerTrans;

    //reference to the animator of the enemy to change animations
    private Animator anim;

    //check if the enemy is dead
    private bool isDead = false;

    //reference Transform for the enemy to move towards.
    private Transform relativePlayerTransform;

    //check if the enemy is on ground
    private bool grounded;

    //check to damage or not the player
    private bool playerDamage = false;

    //***********************************
    //* Variables for constant movement *
    //***********************************

    Transform leftPoint, rightPoint;

    //immidiate position to move to
    private Transform currentPoint;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController_2D>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

        relativePlayerTransform = new GameObject().transform;

        //left point
        leftPoint = new GameObject().transform;
        leftPoint.position = new Vector3(this.transform.position.x - maxTravelDistance, this.transform.position.y, this.transform.position.z);

        //right point
        rightPoint = new GameObject().transform;
        rightPoint.position = new Vector3(this.transform.position.x + maxTravelDistance, this.transform.position.y, this.transform.position.z);

        currentPoint = rightPoint;
    }

    // Update is called once per frame
    void Update()
    {
        // stop updating when enemy is dead
        if (isDead) return;

        // only follow play when enemy is not too high/low from the enemy (another platform)
        float yDiff = Math.Abs(playerTrans.position.y - this.transform.position.y);
        if (Math.Abs(playerTrans.position.x - this.transform.position.x) <= maxTravelDistance && !playerDamage && yDiff < 2)
        {
            // follow and attack only when player is grounded            
            if (grounded && !playerController.isJumping)
            {
                // face player
                Vector3 temp = this.transform.localScale;
                if ((playerTrans.position.x < this.transform.position.x && temp.x > 0) ||
                    (playerTrans.position.x > this.transform.position.x && temp.x < 0))
                {
                    temp.x *= -1;
                    this.transform.localScale = temp;
                }

                // hit
                if (Math.Abs(playerTrans.position.x - this.transform.position.x) < attackRange)
                {
                    anim.SetBool("attack", true);
                }
                else
                {
                    anim.SetBool("attack", false);
                    relativePlayerTransform.position = new Vector3(playerTrans.transform.position.x, this.transform.position.y, 0);
                    //otherwise follow player
                    this.transform.position = Vector3.MoveTowards(this.transform.position, relativePlayerTransform.position, Time.deltaTime * speed);//Max distance delta = speed of movement                    
                }                
            }
        }
        else
        {
            // set to walking
            anim.SetBool("attack", false);

            // flip before moving
            Vector3 temp = this.transform.localScale;
            if ((currentPoint == rightPoint && temp.x < 0) ||
                (currentPoint == leftPoint && temp.x > 0))
            {
                temp.x *= -1;
            }
            this.transform.localScale = temp;


            //move whithin the max distance
            //makes enemy move towwards that location
            this.transform.position = Vector3.MoveTowards(this.transform.position, currentPoint.position, Time.deltaTime * speed);//Max distance delta = speed of movement

            //check if the thing had moved to the point
            if (this.transform.position == currentPoint.position)
            {
                if (currentPoint == rightPoint)
                {
                    currentPoint = leftPoint;
                }
                else
                {
                    currentPoint = rightPoint;
                }
            }
        }

        if (playerDamage)
        {
            if (damageWait >= 1f)
            {
                //Debug.Log("Damaging");
                damageWait = 0;
            }
            else
            {
                //Debug.Log("Waiting for timer");
                damageWait += Time.deltaTime;
            }
        }

        int enemyLayer = LayerMask.NameToLayer("Enemy");

        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
        if (collision.gameObject.tag == "Player")
        {
            playerDamage = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
        if (collision.gameObject.tag == "Player")
        {
            playerDamage = false;
        }
    }



    public void Sword_Hitted()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (health > 0)
        {
            health--;
            sr.color = Color.white;
        }

        if (health == 0)
        {
            isDead = true;
            StartCoroutine(Dead());
        }
    }

    IEnumerator Dead ()
    {
        anim.SetTrigger("isDead");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
