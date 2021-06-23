using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Camera Movement which Follows the Targeted Object Regardless of Target Rotation.
/// </summary>
public class CameraFollowPlayer : MonoBehaviour
{
    #region || ----- Fields & Properties ----- ||

    /// <summary>
    /// Player Transform to Follow.
    /// </summary>
    [SerializeField] private Transform playerTransform;

    /// <summary>
    /// Distance of Camera from Target Player Character.
    /// </summary>
    private Vector3 cameraOffset;

    #endregion

    // ------------------------------------------------------------------------------------------------------------------

    #region || ----- MonoBehaviour Methods ----- ||

    private void Awake ()
    {
        Assert.IsNotNull(playerTransform);

        cameraOffset = transform.position - playerTransform.position;
    }

    private void Update ()
    {
        transform.position = playerTransform.position + cameraOffset;
    }

    #endregion
}
