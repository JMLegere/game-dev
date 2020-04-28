using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroKnightController : MonoBehaviour, IPlayable
{

    SpriteRenderer m_SpriteRenderer;
    Animator m_Animator;
    Rigidbody2D m_Rigidbody;
    Collider2D m_Collider;

    public float m_JumpPower;
    public float m_MoveSpeed;

    public float m_DistanceToGround;
    private bool m_WasGrounded = false;
    private bool m_IsGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<Collider2D>();

        m_DistanceToGround = m_Collider.bounds.extents.y;

    }

    // Update is called once per frame
    void Update()
    {
        m_WasGrounded = m_IsGrounded;
        m_IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, m_DistanceToGround + 0.1f);
        Debug.Log(m_IsGrounded);
    }

    private void LateUpdate() 
    {
        UpdateAnimation();

        //if just hit the ground, spawn an animated dust sprite
        if(m_IsGrounded && !m_WasGrounded)
        {

        }
    }

    //=============================================================================================
    //HeroKnightController specific
    private void UpdateAnimation()
    {
        if(m_Rigidbody.velocity.x > 0)
            m_SpriteRenderer.flipX = false;
        else if (m_Rigidbody.velocity.x < 0)
            m_SpriteRenderer.flipX = true;

        if(m_IsGrounded)
        {
            if(m_Rigidbody.velocity.x == 0)
            {
                m_Animator.Play("idle");
            }
            else
            {
                m_Animator.Play("run");
            }
        }
        else
        {
            if(m_Rigidbody.velocity.y >= 0)
            {
                m_Animator.Play("jump");                
            }
            else
            {
                m_Animator.Play("fall");
            }
        }
        
    }

    //=============================================================================================
    // IPlayable interface
    public void Move(float direction)
    {
        m_Rigidbody.velocity = new Vector2(direction * m_MoveSpeed, m_Rigidbody.velocity.y);
    }

    public void Jump(float power)
    {
        m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, power*m_JumpPower);
    }
}
