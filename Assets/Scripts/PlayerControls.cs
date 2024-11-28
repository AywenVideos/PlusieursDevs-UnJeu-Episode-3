using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saidus2;
using UnityEngine.InputSystem;
using System;
using LevelGenerator.Scripts;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Light theLight;
    CharacterController characterController;
    [Header("Movement Parameters")]
    public float speed = 5f;
    public float rotationSpeed = 3f;
    bool endJump = false;


    [SerializeField]
    bool isGrounded;
    Controls inputs;

    Interactable interactableNearMe;
    public Interactable InteractableNearMe { get => interactableNearMe; set => interactableNearMe = value; }



    Vector3 playerSpeed;

    [Header("Camera Angles Clamp")]
    public float upCameraAngle = 330, downCameraAngle = 400;


    private bool isPlayerFreeze = false;
    /// <summary>
    /// Controls if the player can move and look around him or yield for any interaction or event.
    /// </summary>
    public bool Freeze
    {
        get
        {
            return isPlayerFreeze;
        }
       set {
            isPlayerFreeze = value;

            SetPlayerFreeze(isPlayerFreeze);
        }

    }
    private float baseIntensity;
    void Start()
    {
        inputs = new Controls();
        inputs.Enable();
        characterController = GetComponent<CharacterController>();
        inputs.Inputs.Interact.performed += Interact;
        inputs.Inputs.Spotlight.performed += Spotlight;
        inputs.Inputs.Pause.performed += Pause;
        GameManager.Instance.OnDeath += Dead;
        if(theLight != null)
            baseIntensity = theLight.intensity;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnDestroy()
    {
        inputs.Inputs.Interact.performed -= Interact;
        inputs.Inputs.Pause.performed -= Pause;
        GameManager.Instance.OnDeath -= Dead;
        inputs.Inputs.Spotlight.performed -= Spotlight;
    }

    private void SetPlayerFreeze(bool isFreeze)
    {   
        if (!isFreeze) if (coroutine_ForcePlayerToLookAt != null) StopCoroutine(coroutine_ForcePlayerToLookAt);
    }

    private void Interact(InputAction.CallbackContext obj)
    {
       // print("interact button pressed");
       if(interactableNearMe != null)
        {
            interactableNearMe.Interact();
        }
    }

    private void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("Open");
        UIManager.Instance.OpenPauseMenu();
    }

    private void Spotlight(InputAction.CallbackContext context)
    {
        if (theLight != null)
        {
            theLight.intensity = theLight.intensity == 0 ? baseIntensity : 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerFreeze)
        {
            Vector3 move = new Vector3();
            move += PlayerMoves();
            PlayerRotations();
            move -= 9.81f * Vector3.up;
            characterController.Move(move * Time.deltaTime);
        }
    }
    void Dead()
    {
        inputs.Disable();
    }


    Coroutine coroutine_ForcePlayerToLookAt;
    public void ForcePlayerToLookAt(Vector3 point, float LerpSpeed)
    {
        if (coroutine_ForcePlayerToLookAt != null) StopCoroutine(coroutine_ForcePlayerToLookAt);

        coroutine_ForcePlayerToLookAt = StartCoroutine(ForcePlayerToLookAt_Coroutine(point, LerpSpeed));
    }

    IEnumerator ForcePlayerToLookAt_Coroutine(Vector3 point, float LerpSpeed)
    {
        Vector3 directionToLook = point - transform.position;
        directionToLook.y = 0;
        Quaternion targetRotationPlayer = Quaternion.LookRotation(directionToLook);
        Vector3 directionToLookCamera = point - Camera.main.transform.position;
        Quaternion targetRotationCamera = Quaternion.LookRotation(directionToLookCamera);

        while (Quaternion.Angle(transform.rotation, targetRotationPlayer) > 0.1f || Quaternion.Angle(Camera.main.transform.rotation, targetRotationCamera) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotationPlayer, LerpSpeed * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, targetRotationCamera, LerpSpeed * Time.deltaTime);

            yield return null;
        }

        // On s'assure que la rotation finale est exactement celle voulue
        transform.rotation = targetRotationPlayer;
        Camera.main.transform.rotation = targetRotationCamera;
    }

    private void PlayerRotations()
    {
        this.gameObject.transform.Rotate(Vector3.up, inputs.Inputs.Rotations.ReadValue<Vector2>().x * rotationSpeed * Time.deltaTime);
        //Camera.main.transform.Rotate(Vector3.right,-inputs.Inputs.Rotations.ReadValue<Vector2>().y * rotationSpeed * Time.deltaTime);
        float cameraRotationValue = Camera.main.transform.eulerAngles.x - inputs.Inputs.Rotations.ReadValue<Vector2>().y * rotationSpeed * Time.deltaTime;
        cameraRotationValue = ClampAngle(cameraRotationValue, upCameraAngle, downCameraAngle);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(cameraRotationValue,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z));
    }
    public float ClampAngle(float current, float min, float max)
    {
        float dtAngle = Mathf.Abs(((min - max) + 180) % 360 - 180);
        float hdtAngle = dtAngle * 0.5f;
        float midAngle = min + hdtAngle;

        float offset = Mathf.Abs(Mathf.DeltaAngle(current, midAngle)) - hdtAngle;
        if (offset > 0)
            current = Mathf.MoveTowardsAngle(current, midAngle, offset);
        return current;
    }


    Vector3 PlayerMoves()
    {
        Vector3 inputMoves = new Vector3(inputs.Inputs.Move.ReadValue<Vector2>().x, 0,
            inputs.Inputs.Move.ReadValue<Vector2>().y);
        Vector3 move = (transform.forward * inputMoves.z + transform.right * inputMoves.x) * speed;
        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}
        return move;
        //characterController.Move(move * Time.deltaTime * speed);
    }
}
