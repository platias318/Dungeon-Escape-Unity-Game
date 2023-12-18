using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
    using UnityEditor;
    using System.Net;
#endif

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rb;
    //Third person to first person variables
    [SerializeField] private GameObject FirstPersonTransform;// the coordinates of the first person camera
    [SerializeField] private GameObject ThirdPersonTransform; // the coordinates of the third person camera
    private bool isThirdPerson = true; // the game starts putting you in third person perspective

    #region Audio Variables
    //Player Audio variables
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource walkingSoundEffect;
    [SerializeField] private AudioSource crouchingSoundEffect;
    [SerializeField] private AudioSource runningSoundEffect;
    #endregion

    #region Camera Movement Variables

    public Camera playerCamera;

    public float fov = 60f;
    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;

    // Crosshair
    public bool lockCursor = true;
    public bool crosshair = true;
    public Sprite crosshairImage;
    public Color crosshairColor = Color.white;

    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Image crosshairObject;

    #region Camera Zoom Variables

    public bool enableZoom = true;
    public bool holdToZoom = false;
    public KeyCode zoomKey = KeyCode.Mouse1;
    public float zoomFOV = 30f;
    public float zoomStepTime = 5f;

    // Internal Variables
    private bool isZoomed = false;

    #endregion
    #endregion

    #region Movement Variables

    public bool playerCanMove = true;
    public float maxVelocityChange = 10f;

    // Internal Variables
    private bool isWalking = false;
    private float walkSpeed = 3.5f;

    private int frameCounter = 0;

    #region Sprint

    public bool enableSprint = true;

    // Internal Variables
    private float sprintFOV = 79.1f;
    private float sprintFOVStepTime = 10f;
    private float sprintSpeed = 7f;
    private float sprintDuration = 5f;
    private float sprintCooldown = .5f;
    private KeyCode sprintKey = KeyCode.LeftShift;
    private CanvasGroup sprintBarCG;
    private bool isSprinting = false;
    private float sprintRemaining;
    private float sprintBarWidth;
    private float sprintBarHeight;
    private bool isSprintCooldown = false;
    private float sprintCooldownReset;
    private bool unlimitedSprint = true;
    private bool startedSprinting = false;

    #endregion

    #region Jump

    public bool enableJump = true;

    // Internal Variables
    private bool isGrounded = false;
    private KeyCode jumpKey = KeyCode.Space;
    private float jumpPower = 5f;

    #endregion

    #region Crouch

    public bool enableCrouch = true;
    public bool holdToCrouch = true;
    public float crouchHeight = 1f;
    public float speedReduction = .5f;

    // Internal Variables
    private bool isCrouched = false;
    private Vector3 originalScale;

    #endregion
    #endregion

    #region Head Bob

    public bool enableHeadBob = true;
    public Transform joint;
    public float bobSpeed = 10f;
    public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

    // Internal Variables
    private Vector3 jointOriginalPos;
    private float timer = 0;

    #endregion

    private void Awake()
    {
        if (!PauseMenu.isPaused)
        {
            rb = GetComponent<Rigidbody>();

            crosshairObject = GetComponentInChildren<Image>();

            // Set internal variables
            playerCamera.fieldOfView = fov;
            originalScale = transform.localScale;
            jointOriginalPos = joint.localPosition;

            if (!unlimitedSprint)
            {
                sprintRemaining = sprintDuration;
                sprintCooldownReset = sprintCooldown;
            }
        }
    }

    void Start()
    {
        if (!PauseMenu.isPaused) { 
            if (lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if(crosshair)
            {
                crosshairObject.sprite = crosshairImage;
                crosshairObject.color = crosshairColor;
            }
            else
            {
                crosshairObject.gameObject.SetActive(false);
            }
        }

    }
    float camRotation;

    private void Update()
    {
        if (!PauseMenu.isPaused)
        {
            PerspectiveSwitch(); // switches from third person to first and vise versa


            #region Camera

            // Control camera movement
            if (cameraCanMove)
            {
                yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

                if (!invertCamera)
                {
                    pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
                }
                else
                {
                    // Inverted Y
                    pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
                }

                // Clamp pitch between lookAngle
                pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

                transform.localEulerAngles = new Vector3(0, yaw, 0);
                playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
            }

            #region Camera Zoom

            if (enableZoom)
            {
                // Changes isZoomed when key is pressed
                // Behavior for toogle zoom
                if (Input.GetKeyDown(zoomKey) && !holdToZoom && !isSprinting)
                {
                    if (!isZoomed)
                    {
                        isZoomed = true;
                    }
                    else
                    {
                        isZoomed = false;
                    }
                }

                // Changes isZoomed when key is pressed
                // Behavior for hold to zoom
                if (holdToZoom && !isSprinting)
                {
                    if (Input.GetKeyDown(zoomKey))
                    {
                        isZoomed = true;
                    }
                    else if (Input.GetKeyUp(zoomKey))
                    {
                        isZoomed = false;
                    }
                }

                // Lerps camera.fieldOfView to allow for a smooth transistion
                if (isZoomed)
                {
                    playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, zoomStepTime * Time.deltaTime);
                }
                else if (!isZoomed && !isSprinting)
                {
                    playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, fov, zoomStepTime * Time.deltaTime);
                }
            }

            #endregion
            #endregion

            #region Sprint

            if (enableSprint)
            {
                if (isSprinting)
                {
                    isZoomed = false;
                    playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, sprintFOV, sprintFOVStepTime * Time.deltaTime);

                }
                else
                {
                    // Regain sprint while not sprinting
                    sprintRemaining = Mathf.Clamp(sprintRemaining += 1 * Time.deltaTime, 0, sprintDuration);
                }

            }

            #endregion

            #region Jump

            // Gets input and calls jump method
            if (enableJump && Input.GetKeyDown(jumpKey) && isGrounded)
            {
                Jump();
            }

            #endregion

            #region Crouch

            if (enableCrouch)// if enabled courch is turned on
            {
                if (Input.GetKeyDown(KeyCode.C) && !holdToCrouch)
                {
                    Crouch();
                }

                if (Input.GetKeyDown(KeyCode.C) && holdToCrouch)
                {
                    isCrouched = false;
                    Crouch();
                }
                else if (Input.GetKeyUp(KeyCode.C) && holdToCrouch)
                {
                    isCrouched = true;
                    Crouch();
                }
            }

            #endregion

            CheckGround();

            if (enableHeadBob)
            {
                HeadBob();
            }
        }

    }

    void FixedUpdate()
    {
        if (!PauseMenu.isPaused)
        {
            #region Movement

            if (playerCanMove)
            {
                // Calculate how fast we should be moving
                Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                // Checks if player is walking and isGrounded
                // Will allow head bob
                if (targetVelocity.x != 0 || targetVelocity.z != 0 && isGrounded)
                {
                    isWalking = true;
                    if (!walkingSoundEffect.isPlaying && !isCrouched && !isSprinting)
                    {
                        if (frameCounter % 7 == 0)
                        { // make the effect play every 10 frames so it matches the footsteps
                            walkingSoundEffect.Play();
                            frameCounter = 0;
                        }

                    }
                    if (isCrouched)
                    {
                        if (!crouchingSoundEffect.isPlaying)
                        {
                            frameCounter++;
                            if (frameCounter % 10 == 0) // make the effect play every 10 frames so it matches the footsteps
                            {
                                crouchingSoundEffect.Play();
                                frameCounter = 0;
                            }
                        }
                    }

                }
                else
                {
                    isWalking = false;
                    if (walkingSoundEffect.isPlaying)
                    {
                        walkingSoundEffect.Stop();
                    }
                }

                // All movement calculations shile sprint is active
                if (enableSprint && Input.GetKey(sprintKey) && sprintRemaining > 0f && !isSprintCooldown)
                {
                    targetVelocity = transform.TransformDirection(targetVelocity) * sprintSpeed;

                    // Apply a force that attempts to reach our target velocity
                    Vector3 velocity = rb.velocity;
                    Vector3 velocityChange = (targetVelocity - velocity);
                    velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                    velocityChange.y = 0;

                    // Player is only moving when valocity change != 0
                    // Makes sure fov change only happens during movement
                    if (velocityChange.x != 0 || velocityChange.z != 0)
                    {
                        isSprinting = true;

                        if (walkingSoundEffect.isPlaying && !startedSprinting) // if the sound effect was playing because the player was walking earlier
                        {
                            walkingSoundEffect.Stop();
                            startedSprinting = true;
                        }
                        if (!runningSoundEffect.isPlaying && startedSprinting)
                        {
                            if (frameCounter % 5 == 0)
                            { // make the effect play every 5 frames so it matches the footsteps
                                runningSoundEffect.Play();
                                frameCounter = 0;
                            }
                        }
                        if (isCrouched)
                        {
                            Crouch();
                        }
                    }

                    rb.AddForce(velocityChange, ForceMode.VelocityChange);
                }
                // All movement calculations while walking
                else
                {
                    isSprinting = false;
                    startedSprinting = false;

                    targetVelocity = transform.TransformDirection(targetVelocity) * walkSpeed;

                    // Apply a force that attempts to reach our target velocity
                    Vector3 velocity = rb.velocity;
                    Vector3 velocityChange = (targetVelocity - velocity);
                    velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                    velocityChange.y = 0;

                    rb.AddForce(velocityChange, ForceMode.VelocityChange);
                }
            }

            #endregion
        }
    }

    // Sets isGrounded based on a raycast sent straigth down from the player object
    private void CheckGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = .75f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            Debug.DrawRay(origin, direction * distance, Color.red);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    private void PerspectiveSwitch() // Changes the perspective from third person to first and in reverse
    {
        if (Input.GetKeyUp(KeyCode.T)) //If the player wants to change the camera from first person to third and the opossite in reverse
            if (isThirdPerson)// it is 3rd person and we want to go to 1st person
            {
                playerCamera.transform.position = FirstPersonTransform.transform.position;
                isThirdPerson = false;
            }
            else // we are in 1st person and we want to go to 3rd person
            {
                playerCamera.transform.position = ThirdPersonTransform.transform.position;
                isThirdPerson = true;
            }
     }

    private void Jump()
    {
        jumpSoundEffect.Play();
        // Adds force to the player rigidbody to jump
        if (isGrounded)
        {
            rb.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
            isGrounded = false;
        }

        // When crouched and using toggle system, will uncrouch for a jump
        if(isCrouched && !holdToCrouch)
        {
            Crouch();
        }
    }

    private void Crouch()
    {
        // Stands player up to full height
        // Brings walkSpeed back up to original speed
        if(isCrouched)
        {
            walkSpeed /= speedReduction;

            isCrouched = false;
        }
        // Crouches player down to set height
        // Reduces walkSpeed
        else
        {
            walkSpeed *= speedReduction;

            isCrouched = true;
            if (isWalking)
            {
                crouchingSoundEffect.Play();
            }
        }
    }

    private void HeadBob()
    {
        if(isWalking)
        {
            // Calculates HeadBob speed during sprint
            if(isSprinting)
            {
                timer += Time.deltaTime * (bobSpeed + sprintSpeed);
            }
            // Calculates HeadBob speed during crouched movement
            else if (isCrouched)
            {
                timer += Time.deltaTime * (bobSpeed * speedReduction);
            }
            // Calculates HeadBob speed during walking
            else
            {
                timer += Time.deltaTime * bobSpeed;
            }
            // Applies HeadBob movement
            joint.localPosition = new Vector3(jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x, jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y, jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z);
        }
        else
        {
            // Resets when play stops moving
            timer = 0;
            joint.localPosition = new Vector3(Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed));
        }
    }

}

