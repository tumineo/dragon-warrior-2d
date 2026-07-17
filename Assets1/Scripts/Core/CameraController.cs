using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player Following")]
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.15f;

    [Header("Camera Bounds")]
    [SerializeField] private bool useBounds = true;
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 20f;

    [Header("Fixed Y Position")]
    [SerializeField] private float fixedY = 0f; // Set this to your camera's starting Y

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        // Automatically saves the Y position your camera starts at
        fixedY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // Only follow player on X axis, Y stays 
        Vector3 targetPosition = new Vector3(
            player.position.x,
            fixedY,              
            transform.position.z
        );

        // Smooth follow
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothSpeed
        );

        // Clamp X only
        if (useBounds)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minX, maxX),
                fixedY,          
                transform.position.z
            );
        }
    }

    public void MoveToNewRoom(Transform newRoom)
    {
        
    }
}