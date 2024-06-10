using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerMovementAdvanced : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;
    public float airMinSpeed;
    public float airMaxSpeed;
    public float glideSpeed;
    public float glideDownSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Joystick1Button1;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    public bool onslope;

    [Header("References")] 
    [HideInInspector] public Animator animator;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        freeze,
        unlimited,
        walking,
        sprinting,
        crouching,
        sliding,
        air,
        dashing
    }

    public bool sliding;
    public bool crouching;
    public bool gliding;

    public bool dashing;
    
    public bool freeze;
    public bool unlimited;
    
    public bool restricted;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        onslope = OnSlope();
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down,out RaycastHit hit, playerHeight * 0.5f + 0.2f, whatIsGround);
        //Debug.DrawLine(transform.position, hit.point);
        // if (hit.transform != null)
        // {
        //     Debug.Log(hit.transform.name);
        // }

        if (dashing)
        {
            verticalInput = 0;
            horizontalInput = 0;
            return;
        }

        
        MyInput();
        SpeedControl();
        StateHandler();

        if (gliding)
            return;
        
        if (!dashing && (state == MovementState.walking || state == MovementState.sprinting || state == MovementState.crouching))
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        //grounded = Physics.Raycast(transform.position, Vector3.down,out RaycastHit hit, playerHeight * 0.5f + 0.2f, whatIsGround);
        MovePlayer();
    }

    private void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("MoveInput", Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded && !gliding)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKey(KeyCode.Joystick1Button0) && readyToJump && grounded && !gliding)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKey(KeyCode.Joystick1Button1) && readyToJump && grounded && !gliding)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // start crouch


        // stop crouch

    }

    public void StartDigging()
    {
        animator.SetBool("Digging", true);
        //transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        //rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        crouching = true;
    }

    public void StopDigging()
    {
        animator.SetBool("Digging", false);
        //transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);

        crouching = false;
    }

    bool keepMomentum;
    private void StateHandler()
    {
        try
        {
            if (freeze)
            {
                state = MovementState.freeze;
                rb.velocity = Vector3.zero;
                desiredMoveSpeed = 0f;
            }
            else if (unlimited)
            {
                state = MovementState.unlimited;
                desiredMoveSpeed = 999f;
            }
            
            else if (sliding)
            {
                state = MovementState.sliding;

                if (OnSlope() && rb.velocity.y < 0.1f)
                {
                    desiredMoveSpeed = slideSpeed;
                    keepMomentum = true;
                }

                else
                    desiredMoveSpeed = sprintSpeed;
            }

            else if (crouching)
            {
                state = MovementState.crouching;
                desiredMoveSpeed = crouchSpeed;
            }
            
            else if (gliding)
            {
                desiredMoveSpeed = glideSpeed;
            }

            else if (grounded && Input.GetKey(sprintKey))
            {
                state = MovementState.sprinting;
                desiredMoveSpeed = sprintSpeed;
            }

            else if (grounded)
            {
                state = MovementState.walking;
                desiredMoveSpeed = walkSpeed;
            }
            
            

            else
            {
                state = MovementState.air;

                if (moveSpeed < airMinSpeed)
                    desiredMoveSpeed = airMinSpeed;
                if (moveSpeed > airMaxSpeed)
                    desiredMoveSpeed = airMaxSpeed;
                keepMomentum = true;
            }

            bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;

            if (desiredMoveSpeedHasChanged)
            {
                if (keepMomentum)
                {
                    StopAllCoroutines();
                    StartCoroutine(SmoothlyLerpMoveSpeed());
                }
                else
                {
                    StopAllCoroutines();
                    moveSpeed = desiredMoveSpeed;
                }
            }

            lastDesiredMoveSpeed = desiredMoveSpeed;

            if (Mathf.Abs(desiredMoveSpeed - moveSpeed) < 0.1f) keepMomentum = false;
        }
        catch
        {
            Debug.Log("SOMETHING FUCKED UP");
        }
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            else
                time += Time.deltaTime * speedIncreaseMultiplier;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer()
    {
        if (restricted || dashing) return;

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y < 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            if (rb.velocity.y < 0.5 && !gliding)
            {
                Debug.Log("Fall");
                rb.AddForce(Vector3.down * 13f, ForceMode.Acceleration);
                
            }
            
        }

        if (gliding && rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, -glideDownSpeed, rb.velocity.z);
        }

        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        exitingSlope = true;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    // void OnDrawGizmosSelected()
    // {
    //     Debug.DrawLine(transform.position,transform.position + Vector3.down * (playerHeight * 0.5f + 0.2f));
    // }
}
