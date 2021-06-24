using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// Handles the Player Character Movement.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    #region || ----- Fields & Properties ----- ||

    private PlayerInput playerInput;
    private CharacterController characterController;
    private Animator animator;

    /// <summary>
    /// Normalized Vector for Movement Direction.
    /// </summary>
    private Vector3 movementVector;

    /// <summary>
    /// Input Vetor.
    /// </summary>
    private Vector2 inputVector;

    /// <summary>
    /// Wheather to Set Move Animation or Not.
    /// </summary>
    private bool canMove;

    /// <summary>
    /// Rotaion of Player Character per Frame.
    /// </summary>
    [SerializeField] private float rotationTargetPerFrame = 1.0f;

    #endregion

    // ------------------------------------------------------------------------------------------------------

    #region || ----- MonoBehaviour Methods ----- ||

    private void Awake ()
    {
        playerInput = new PlayerInput();

        playerInput.CharacterControls.Move.started += HandleOnMovementInput;
        playerInput.CharacterControls.Move.canceled += HandleOnMovementInput;
        playerInput.CharacterControls.Move.performed += HandleOnMovementInput;

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable ()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable ()
    {
        playerInput.CharacterControls.Disable();
    }

    private void Update ()
    {
        HandleAnimation();
        HandleRotation();
        characterController.Move(movementVector * Time.deltaTime * 2);
    }

    #endregion

    #region || ----- PlayerMovement Methods ----- ||

    /// <summary>
    /// Callback Function for PlayerInput.
    /// </summary>
    private void HandleOnMovementInput ( CallbackContext context )
    {
        inputVector = context.ReadValue<Vector2>();
        movementVector.x = inputVector.x;
        movementVector.y = 0;
        movementVector.z = inputVector.y;
        movementVector = transform.TransformDirection(movementVector);
        canMove = inputVector != Vector2.zero;
    }

    /// <summary>
    /// It Animates the Player Character.
    /// </summary>
    private void HandleAnimation ()
    {
        bool isWalking = animator.GetBool("walking");
        bool isWalkingBackwords = animator.GetBool("backwords");
        if ( inputVector.y < 0 && !isWalkingBackwords && canMove )
        {
            animator.SetBool("backwords", true);
        }
        else if ( !isWalking && canMove )
        {
            animator.SetBool("walking", true);
        }
        else if ( isWalking && !canMove )
        {
            animator.SetBool("walking", false);
            animator.SetBool("backwords", false);
        }
    }

    /// <summary>
    /// It Rotates the Player Character in Input Direction.
    /// </summary>
    private void HandleRotation ()
    {
        Vector3 positionToLookAt;
        positionToLookAt.y = 0;
        if ( inputVector.y < 0 )
        {
            positionToLookAt.x = -movementVector.x;
            positionToLookAt.z = -movementVector.z;
        }
        else
        {
            positionToLookAt.x = movementVector.x;
            positionToLookAt.z = movementVector.z;
        }

        Quaternion currentRotation = transform.rotation;
        if ( canMove )
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationTargetPerFrame * Time.deltaTime);
        }
    }

    #endregion
}