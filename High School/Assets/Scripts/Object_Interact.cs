using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Object_Interact : MonoBehaviour
{
    public GameObject offset;
    private PlayerInput _playerInput;
    GameObject targetObject;

    public bool isExamining = false;

    public Canvas _canva;

    private Vector3 lastMousePosition;
    private Transform examinedObject;

    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> originalRotations = new Dictionary<Transform, Quaternion>();

    private bool isHoveringObject = false;

    private FirstPersonController firstPersonController;
    private bool isCursorVisible = false;  // Declare this at the class level


    //Test mtee crosshair w Press E to interact 
    public GameObject interactPrompt;
    public GameObject crosshair;

    void Start()
    {
        targetObject = GameObject.Find("PlayerCapsule");
        _playerInput = targetObject.GetComponent<PlayerInput>();
        firstPersonController = targetObject.GetComponent<FirstPersonController>();

        // Ensure UI starts in correct state
        if (interactPrompt != null) interactPrompt.SetActive(false);
        if (crosshair != null) crosshair.SetActive(true);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool hoveringObject = Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Object");
        float distance = hoveringObject ? Vector3.Distance(targetObject.transform.position, hit.collider.transform.position) : Mathf.Infinity;

        // Show interaction prompt when hovering and close enough
        if (!isExamining && hoveringObject && distance < 2f)
        {
            if (!isCursorVisible)
            {
                firstPersonController.SetCursorVisibility(true);
                isCursorVisible = true;
            }

            if (interactPrompt != null) interactPrompt.SetActive(true);
        }
        else if (!isExamining)
        {
            if (isCursorVisible)
            {
                firstPersonController.SetCursorVisibility(false);
                isCursorVisible = false;
            }

            if (interactPrompt != null) interactPrompt.SetActive(false);
        }

        // Interaction key (E)
        if (Input.GetKeyDown(KeyCode.E) && firstPersonController.Grounded)
        {
            if (isExamining)
            {
                ToggleExamination();
            }
            else if (hoveringObject && distance < 2f)
            {
                examinedObject = hit.transform;
                originalPositions[examinedObject] = examinedObject.position;
                originalRotations[examinedObject] = examinedObject.rotation;
                ToggleExamination();
            }
        }

        if (isExamining && examinedObject != null)
        {
            Examine();
            StartExamination();
        }
        else if (!isExamining)
        {
            NonExamine();
            StopExamination();
        }
    }

    public void ToggleExamination()
    {
        isExamining = !isExamining;
    }

    void StartExamination()
    {
        lastMousePosition = Input.mousePosition;

        Cursor.lockState = CursorLockMode.None;
        firstPersonController.SetCursorVisibility(true);

        _playerInput.enabled = false;
    }

    void StopExamination()
    {
        Cursor.lockState = CursorLockMode.Locked;
        firstPersonController.SetCursorVisibility(false);

        _playerInput.enabled = true;
    }

    void Examine()
    {
        if (examinedObject != null)
        {
            examinedObject.position = Vector3.Lerp(examinedObject.position, offset.transform.position, 0.2f);

            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            float rotationSpeed = 1.0f;
            examinedObject.Rotate(deltaMouse.x * rotationSpeed * Vector3.up, Space.World);
            examinedObject.Rotate(deltaMouse.y * rotationSpeed * Vector3.left, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }

    void NonExamine()
    {
        if (examinedObject != null)
        {
            if (originalPositions.ContainsKey(examinedObject))
            {
                examinedObject.position = Vector3.Lerp(examinedObject.position, originalPositions[examinedObject], 0.2f);
            }
            if (originalRotations.ContainsKey(examinedObject))
            {
                examinedObject.rotation = Quaternion.Slerp(examinedObject.rotation, originalRotations[examinedObject], 0.2f);
            }
        }
    }
}
