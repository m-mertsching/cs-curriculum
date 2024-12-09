using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [System.Serializable]
    public struct LightData
    {
        public Vector4 position; // xy: position, z: rotation, w: radius
        public Vector4 shape;    // x: cone angle, y: aspect ratio, zw: unused
    }

    [Header("Flashlight Properties")]
    [SerializeField] private float lightRadius = 2f;
    [SerializeField] private float coneAngle = 45f;
    [SerializeField] private float aspectRatio = 1.5f;
    
    [Header("Throw Properties")]
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private KeyCode throwKey = KeyCode.E;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float throwOffset = 1f; // Distance from player when throwing
    
    private Rigidbody2D rb;
    private bool isAttached = true;
    private Vector2 attachOffset;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false; // Disable physics while attached
        
        // Store initial offset from player
        attachOffset = transform.position - playerTransform.position;
    }

    private void Update()
    {
        if (isAttached)
        {
            // Update position and rotation to match player
            transform.position = playerTransform.position + (Vector3)attachOffset;
            transform.rotation = playerTransform.rotation;

            // Check for throw input
            if (Input.GetKeyDown(throwKey))
            {
                ThrowFlashlight();
            }
        }
    }

    private void ThrowFlashlight()
    {
        isAttached = false;
        rb.simulated = true;

        // Calculate throw direction based on player's facing direction
        Vector2 throwDirection = playerTransform.right;
        
        // Position the flashlight slightly in front of the player
        transform.position = playerTransform.position + (Vector3)(throwDirection * throwOffset);
        
        // Apply force and rotation
        rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
        rb.AddTorque(rotationSpeed * Mathf.Sign(Random.value - 0.5f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Optional: Add logic for what happens when the flashlight hits something
    }

    public LightData GetLightData()
    {
        return new LightData
        {
            position = new Vector4(transform.position.x, transform.position.y, 
                                 transform.rotation.eulerAngles.z * Mathf.Deg2Rad, lightRadius),
            shape = new Vector4(coneAngle, aspectRatio, 0, 0)
        };
    }

    // Optional: Method to reattach flashlight to player
    public void ReattachToPlayer()
    {
        isAttached = true;
        rb.simulated = false;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        transform.position = playerTransform.position + (Vector3)attachOffset;
        transform.rotation = playerTransform.rotation;
    }
}

