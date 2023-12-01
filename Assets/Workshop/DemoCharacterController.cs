using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DemoCharacterController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    PlayerInput playerInput;
    Rigidbody rb;
    [SerializeField] float speed = 10;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }


    Vector2 input;
    public void OnMove(InputAction.CallbackContext context) {
        input = context.ReadValue<Vector2>();
    }


    public float sizeIncrease = 1;
    public void OnFire(InputAction.CallbackContext context) {
        transform.localScale = transform.localScale + Vector3.one * sizeIncrease;
    }


    private void Update() {
        transform.localEulerAngles = playerInput.camera.transform.localEulerAngles.y * Vector3.up;

        rb.velocity = (transform.right * input.x + transform.forward * input.y) * speed;
    }

}
