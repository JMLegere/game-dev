using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class InputHandler : MonoBehaviour
{
    //=============================================================================================
    // Member Attributes
    //=============================================================================================

    //Variables
    public float m_ShortHop_Time;
    private float m_Player1_JumpTimer;

    //Effectors
    public IPlayable m_Player1;
    InputDevice m_Player1_Device;

    // Keybindings
    KeyCode m_Player1_Left_Keyboard     = KeyCode.A;
    KeyCode m_Player1_Right_Keyboard    = KeyCode.D;
    KeyCode m_Player1_Jump_Keyboard     = KeyCode.Space;

    // Input Flags
    bool m_Player1_Left_Pressed         = false;
    bool m_Player1_Left_Held            = false;
    bool m_Player1_Left_Released        = false;

    bool m_Player1_Right_Pressed        = false;
    bool m_Player1_Right_Held           = false;
    bool m_Player1_Right_Released       = false;

    bool m_Player1_Jump_Pressed        = false;
    bool m_Player1_Jump_Held           = false;
    bool m_Player1_Jump_Released       = false;

    //=============================================================================================
    // Unity Functions
    //=============================================================================================

    // Start is called before the first frame update
    void Start()
    {
        m_Player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<IPlayable>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate() 
    {
        ProcessInput();
    }

    //=============================================================================================
    // Member Functions
    //=============================================================================================

    private void GetInput()
    {
        GetInputPlayer1();
    }

    private void ProcessInput()
    {
        ProcessInputPlayer1();
    }

    //=============================================================================================
    // Player 1 Functions
    //=============================================================================================

    private void GetInputPlayer1()
    {
        //Player1 Left Keyboard Input
        if(Input.GetKeyDown(m_Player1_Left_Keyboard))
        {
            m_Player1_Left_Pressed = true;
        }
        if(Input.GetKeyUp(m_Player1_Left_Keyboard))
        {
            m_Player1_Left_Released = true;
        }
        
        m_Player1_Left_Held = Input.GetKey(m_Player1_Left_Keyboard);
        
        //Player1 Right Keyboard Input        
        if(Input.GetKeyDown(m_Player1_Right_Keyboard))
        {
            m_Player1_Right_Pressed = true;
        }
        if(Input.GetKeyUp(m_Player1_Right_Keyboard))
        {
            m_Player1_Right_Released = true;
        }

        m_Player1_Right_Held = Input.GetKey(m_Player1_Right_Keyboard);

        //Player1 Jump Keyboard Input        
        if(Input.GetKeyDown(m_Player1_Jump_Keyboard))
        {
            m_Player1_Jump_Pressed = true;
        }
        if(Input.GetKeyUp(m_Player1_Jump_Keyboard))
        {
            m_Player1_Jump_Released = true;
        }

        m_Player1_Jump_Held = Input.GetKey(m_Player1_Jump_Keyboard);

    }

    private void ProcessInputPlayer1()
    {
        //Player1 Left Keyboard Input        
        if(m_Player1_Left_Pressed)
        {
            OnPlayer1LeftPressed();
            m_Player1_Left_Pressed = false;
        }
        if(m_Player1_Left_Held)
        {
            OnPlayer1LeftHeld();
        }
        if(m_Player1_Left_Released)
        {
            OnPlayer1LeftReleased();
            m_Player1_Left_Released = false;
        }
        
        //Player1 Right Keyboard Input        
        if(m_Player1_Right_Pressed)
        {
            OnPlayer1RightPressed();
            m_Player1_Right_Pressed = false;
        }
        if(m_Player1_Right_Held)
        {
            OnPlayer1RightHeld();
        }
        if(m_Player1_Right_Released)
        {
            OnPlayer1RightReleased();
            m_Player1_Right_Released = false;
        }

        //Player1 Jump Keyboard Input        
        if(m_Player1_Jump_Pressed)
        {
            OnPlayer1JumpPressed();
            m_Player1_Jump_Pressed = false;
        }
        if(m_Player1_Jump_Held)
        {
            OnPlayer1JumpHeld();
        }
        if(m_Player1_Jump_Released)
        {
            OnPlayer1JumpReleased();
            m_Player1_Jump_Released = false;
        }

    }

    private void OnPlayer1LeftPressed()
    {

    }
    private void OnPlayer1LeftHeld()
    {
        m_Player1.Move(-1);
    }
    private void OnPlayer1LeftReleased()
    {
        m_Player1.Move(0);
    }

    private void OnPlayer1RightPressed()
    {

    }
    private void OnPlayer1RightHeld()
    {
        m_Player1.Move(1);
    }
    private void OnPlayer1RightReleased()
    {
        m_Player1.Move(0);
    }

    private void OnPlayer1JumpPressed()
    {
        if(m_Player1.IsGrounded())
        {
            //Reset shorthop timer
            m_Player1_JumpTimer = m_ShortHop_Time;
        } else {
            m_Player1.AirJump();
        }
    }
    private void OnPlayer1JumpHeld()
    {
        if (m_Player1_JumpTimer > 0)
        {
            m_Player1_JumpTimer = m_Player1_JumpTimer - Time.deltaTime;
            m_Player1.Jump(m_Player1_JumpTimer * (1 / m_ShortHop_Time));
        }
    }
    private void OnPlayer1JumpReleased()
    {
        m_Player1_JumpTimer = 0.0f;
    }
}
