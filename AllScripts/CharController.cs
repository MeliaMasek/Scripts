using UnityEngine;
using UnityEngine.InputSystem;

//code borrowed and modified from user iHeartGameDev on youtube https://www.youtube.com/watch?v=bXNFxQpp2qk
//commented out sections for animation controls, will add in later once character is done
public class CharController : MonoBehaviour
{
    Movement playerMove;
    private CharacterController characterController;
    //private Animator animator;
    
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private Vector3 currentRun;
    private bool isMovementPressed;
    private bool isRunPressed;
    public float rotationPerFrame = 15f;
    private float runMultiply = 3f;
    
    private void Awake()
    {
        playerMove = new Movement();
        characterController = GetComponent<CharacterController>();

        //animator = GetComponent<Animator>();
        
        playerMove.CharacterControls.Movement.started += onMovementInput;
        playerMove.CharacterControls.Movement.canceled += onMovementInput;
        playerMove.CharacterControls.Movement.performed += onMovementInput;
        //playerMove.CharacterControls.Run.started += onRun;
        //playerMove.CharacterControls.Run.canceled += onRun;
    }

    //Testing for UI buttons

    private void Update()
    {
        RotatationHandling();
        //AnimationHandling();

        if (isRunPressed)
        {
            characterController.Move(currentRun * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * (Time.deltaTime * 2f));
        }
    }
    private void OnEnable()
    {
        playerMove.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerMove.CharacterControls.Disable();
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRun.x = currentMovementInput.x * runMultiply;
        currentRun.z = currentMovementInput.y * runMultiply;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    /*
    void AnimationHandling()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool("isRunning", true);
        }
        
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool("isRunning", false);
        }
    }
    */

    void RotatationHandling()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = currentMovement.z;
        
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationPerFrame * Time.deltaTime);
        }
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    public void ResetPosition()
    {
        
    }
}
