using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    [SerializeField] float movementSpeed = 10f;
    Vector3 movementVector;
    Vector3 rotationVector;

    private float gravity = -20f;
    public float jumpHeight = 15f;

    public Transform cameraPosition;
    public float mouseSensitivity;
    public bool invertX;
    public bool invertY;

    void Start() 
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        var verticalInput = Input.GetAxis("Vertical");
        if (characterController.isGrounded)
        {
            movementVector = transform.forward * movementSpeed * verticalInput;
            if (Input.GetButtonDown("Jump"))
            {
                movementVector.y = jumpHeight;
            }
        }
        movementVector.y += gravity * Time.deltaTime;
        characterController.Move(movementVector * Time.deltaTime);
        transform.Rotate(rotationVector * Time.deltaTime);

        Vector2 mouseVector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        if (invertX)
        {
            mouseVector.x = -mouseVector.x;
        }
        if (invertY)
        {
            mouseVector.y = -mouseVector.y;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseVector.x, transform.rotation.eulerAngles.z);
        cameraPosition.rotation = Quaternion.Euler(cameraPosition.rotation.eulerAngles + new Vector3(mouseVector.y, 0f, 0f));
    }
}