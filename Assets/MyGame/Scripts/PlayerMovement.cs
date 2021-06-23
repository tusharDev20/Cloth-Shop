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
        characterController.Move(movementVector * Time.deltaTime);
    }

    #endregion

    #region || ----- PlayerMovement Methods ----- ||

    /// <summary>
    /// Callback Function for PlayerInput.
    /// </summary>
    private void HandleOnMovementInput ( CallbackContext context )
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        movementVector.x = inputVector.x;
        movementVector.z = inputVector.y;
        canMove = inputVector != Vector2.zero;
    }

    /// <summary>
    /// It Animates the Player Character.
    /// </summary>
    private void HandleAnimation ()
    {
        bool isWalking = animator.GetBool("walking");
        if ( !isWalking && canMove )
        {
            animator.SetBool("walking", true);
        }
        else if ( isWalking && !canMove )
        {
            animator.SetBool("walking", false);
        }
    }

    /// <summary>
    /// It Rotates the Player Character in Input Direction.
    /// </summary>
    private void HandleRotation ()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = movementVector.x;
        positionToLookAt.y = 0;
        positionToLookAt.z = movementVector.z;

        Quaternion currentRotation = transform.rotation;
        if ( canMove )
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationTargetPerFrame * Time.deltaTime);
        }
    }

    #endregion
}