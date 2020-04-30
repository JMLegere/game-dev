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
    public float m_AirJumpPower;
    public float m_MoveSpeed;
    public float m_Weight;

    private float m_DistanceToGround;
    private bool m_CanAirJump = false;
    public bool m_WasGrounded = false;
    public bool m_IsGrounded = false;

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
        if(m_IsGrounded)
        {
            m_CanAirJump = false;
        }
    }

    private void LateUpdate() 
    {
        UpdateAnimation();

        //if just hit the ground, spawn an animated dust sprite
        if(m_IsGrounded && !m_WasGrounded)
        {

        }
    }

    private void FixedUpdate() {

        // More gravity if falling down
        if(m_Rigidbody.velocity.y > 0)
        {
            m_Rigidbody.gravityScale = 0.75f * m_Weight;
        } else {
            m_Rigidbody.gravityScale = 1.0f * m_Weight;
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
        m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y + power*m_JumpPower);
        if(m_IsGrounded)
        {
            m_CanAirJump = true;
        }
    }

    public void AirJump()
    {
        if(m_CanAirJump)
        {
            m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, m_AirJumpPower);
        }
        m_CanAirJump = false;
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

    public bool IsGrounded()
    {
        return m_IsGrounded;
    }

    public bool WasGrounded()
    {
        return m_WasGrounded;
    }
}
