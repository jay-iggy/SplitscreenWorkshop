using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody rb;
    [SerializeField] float speed = 10;

    public CinemachineVirtualCamera virtualCamera;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        /*playerInput.currentActionMap.FindAction("Fire").performed += OnFire;
        playerInput.currentActionMap.FindAction("Move").performed += OnFire;*/
    }

    public void OnMove(InputAction.CallbackContext context) {
        Vector2 input = context.ReadValue<Vector2>();



        rb.velocity = new Vector3(input.x * speed, 0, input.y * speed); ;
    }


    public float sizeIncrease = 1;
    public void OnFire(InputAction.CallbackContext context) {
        transform.localScale = transform.localScale + Vector3.one * sizeIncrease;
    }




}
