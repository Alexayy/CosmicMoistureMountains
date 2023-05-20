using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float smoothTime = 0.2f;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Animator animator;
    private Vector3 cameraVelocity;

    public UIManager uiManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;

        bool isMoving = movement.magnitude > 0f;
        animator.SetBool("IsMoving", isMoving);

        // calculate target position for the camera.
        Vector3 targetPosition = transform.position;
        targetPosition.z = mainCamera.transform.position.z;

        // smothly move the camera to the target position
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetPosition, ref cameraVelocity, smoothTime);
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Open or close the inventory UI
            uiManager.ToggleInventory(!uiManager._inventory.activeSelf);
        }
    }
}