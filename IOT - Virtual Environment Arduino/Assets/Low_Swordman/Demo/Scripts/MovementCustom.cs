using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCustom : PlayerController
{
    public VariableJoystick variableJoystick;
    private bool isGoingLeft = false;
    private bool isGoingRight = false;
    private bool isGoingDown = false;
    private bool isGoingUp = false;

    private void Start()
    {

        m_CapsulleCollider  = this.transform.GetComponent<CapsuleCollider2D>();
        m_Anim = this.transform.Find("model").GetComponent<Animator>();
        m_rigidbody = this.transform.GetComponent<Rigidbody2D>();
  

    }



    private void Update()
    {
        isGoingRight = variableJoystick.Horizontal > 0.2;
        isGoingLeft = variableJoystick.Horizontal < -0.2;
        isGoingUp = variableJoystick.Vertical > 2;
        isGoingDown = variableJoystick.Vertical < 2;
       
        //Debug.Log(isGoingRight);        
              

        checkInput();

        if (m_rigidbody.velocity.magnitude > 30)
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x - 0.1f, m_rigidbody.velocity.y - 0.1f);

        }
    }

    public void checkInput()
    {
        bool isMoving = isGoingRight || isGoingLeft;

        if (Input.GetKeyDown(KeyCode.S) || (isGoingDown && !isMoving))
        {

            //IsSit = true;
            //m_Anim.Play("Sit");


        }
        else if ((isGoingUp && !isMoving) || (!isGoingDown && !isMoving))
        {

            m_Anim.Play("Idle");
            IsSit = false;

        }


        if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Sit") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentJumpCount < JumpCount)  // 0 , 1
                {
                    DownJump();
                }
            }

            return;
        }


        if(isGoingLeft || isGoingRight){
            m_MoveX = variableJoystick.Horizontal;
        }else{
            m_MoveX = variableJoystick.Horizontal;
        }
        //m_MoveX = 5;


   
        GroundCheckUpdate();


        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            //TODO: DISABLED ATTACK
            if (false && Input.GetKey(KeyCode.Mouse0))
            {


                m_Anim.Play("Attack");
            }
            else
            {

                if (m_MoveX == 0)
                {
                    if (!OnceJumpRayCheck)
                        m_Anim.Play("Idle");

                }
                else
                {

                    m_Anim.Play("Run");
                }

            }
        }


        if (Input.GetKey(KeyCode.Alpha1))
        {
            m_Anim.Play("Die");

        }

        if (Input.GetKey(KeyCode.D) || isGoingRight)
        {
            if (isGrounded)  // 땅바닥에 있었을때. 
            {



                if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    return;

                transform.transform.Translate(Vector2.right* m_MoveX * MoveSpeed * Time.deltaTime);



            }
            else
            {

                transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));

            }




            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                return;
            
                

            if (!(Input.GetKey(KeyCode.A) || isGoingLeft))
                Filp(false);
            

        }
        else if (Input.GetKey(KeyCode.A) || isGoingLeft)
        {


            if (isGrounded)
            {



                if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    return;


                transform.transform.Translate(Vector2.right * m_MoveX * MoveSpeed * Time.deltaTime);

            }
            else
            {

                transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));

            }


            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                return;

            if (!(Input.GetKey(KeyCode.D) || isGoingRight))
                Filp(true);


        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                return;


            if (currentJumpCount < JumpCount)  // 0 , 1
            {

                if (!IsSit)
                {
                    prefromJump();


                }
                else
                {
                    DownJump();

                }

            }


        }



    }


  


    protected override void LandingEvent()
    {


        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Run") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            m_Anim.Play("Idle");

    }





}
