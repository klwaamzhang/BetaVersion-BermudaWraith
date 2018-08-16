using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;

public class CharacterController_2D : MonoBehaviour
{

    Rigidbody2D m_rigidbody;
    Animator m_Animator;
    Transform m_tran;

    private float h = 0;
    private float v = 0;

    public float MoveSpeed = 40;
    public float jumpPower = 300;

    // to check if player is grounded or not
    [HideInInspector] public bool isJumping = false;

    public SpriteRenderer[] m_SpriteGroup;

    public bool Once_Attack = false;


    // Use this for initialization
    void Start()
    {
        m_rigidbody = this.GetComponent<Rigidbody2D>();
        m_Animator = this.transform.Find("BURLY-MAN_1_swordsman_model").GetComponent<Animator>();
        m_tran = this.transform;
        m_SpriteGroup = this.transform.Find("BURLY-MAN_1_swordsman_model").GetComponentsInChildren<SpriteRenderer>(true);
        if (SceneManager.GetActiveScene().name == "level1")
        {
            Time.timeScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Console.WriteLine("called from escape");
            Resume();
        }

        if (Time.timeScale == 0) return;

        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Mouse0))
        {

            StartCoroutine(Attack1WaitSeconds());

        }

        else if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(Attack2WaitSeconds());
        }

        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Die") ||
            m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            return;

        Move_Fuc();
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        m_Animator.SetFloat("MoveSpeed", Mathf.Abs(h) + Mathf.Abs(v));
    }

    IEnumerator Attack1WaitSeconds()
    {
        Once_Attack = true;
        //Debug.Log("Lclick");
        m_Animator.SetTrigger("Attack");
        m_rigidbody.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.25f);
        Once_Attack = false;
    }

    IEnumerator Attack2WaitSeconds()
    {
        Once_Attack = true;
        //Debug.Log("Rclick");
        m_Animator.SetTrigger("Attack2");
        m_rigidbody.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.25f);
        Once_Attack = false;
    }

    // character Move Function
    void Move_Fuc()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //  Debug.Log("Left");
            m_rigidbody.AddForce(Vector2.left * MoveSpeed);
            if (B_FacingRight)
                Filp();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //  Debug.Log("Right");
            m_rigidbody.AddForce(Vector2.right * MoveSpeed);
            if (!B_FacingRight)
                Filp();
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && !isJumping)
        {
            m_rigidbody.AddForce(Vector2.up * jumpPower);
            isJumping = true;
        }
    }

    void Hurt()
    {
        ScoreBoardController.instance.HealthDecrease();

        if (ScoreBoardController.health <= 0)
        {
            m_Animator.Play("Die");

            StartCoroutine(DieSceneIn2Seconds());


        }
        else
        {
            m_Animator.Play("Hit");
            TriggerHurt();
        }
    }

    IEnumerator DieSceneIn2Seconds()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
        yield return new WaitForSeconds(2);
        //ScoreBoardController.ResetStatics(typeof(ScoreBoardController));
        SceneManager.LoadScene(5);
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
        Destroy(this.gameObject);
    }

    public void TriggerHurt()
    {
        StartCoroutine(CannotBeHurtIn2Seconds());
    }

    IEnumerator CannotBeHurtIn2Seconds()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");

        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);

        yield return new WaitForSeconds(1f);

        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SteakMove steak = collision.collider.GetComponent<SteakMove>();
        EnemyFollowPlayer pumpkin = collision.gameObject.GetComponent<EnemyFollowPlayer>();
        MushroomBehavior mushroom = collision.gameObject.GetComponent<MushroomBehavior>();
        EnemyController zombie = collision.gameObject.GetComponent<EnemyController>();
        FlyingBatController bat = collision.gameObject.GetComponent<FlyingBatController>();

        if (steak != null || pumpkin != null || mushroom != null || zombie != null || bat != null)
        {
            Hurt();
        }

        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }

    bool B_FacingRight = true;

    void Filp()
    {
        B_FacingRight = !B_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;

        m_tran.localScale = theScale;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Debug.Log(ScoreBoardController.instance.transform.GetChild(9).gameObject.activeInHierarchy);
        if (!ScoreBoardController.instance.transform.GetChild(9).gameObject.activeInHierarchy)
        {
            ScoreBoardController.instance.transform.GetChild(9).gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            ScoreBoardController.instance.transform.GetChild(9).gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
