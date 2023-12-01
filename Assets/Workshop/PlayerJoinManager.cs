using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerJoinManager : MonoBehaviour
{
    PlayerInputManager inputManager;
    private void Awake() {
        inputManager= GetComponent<PlayerInputManager>();
    }

    [SerializeField] LayerMask[] cameraLayers;


    public void OnPlayerJoined(PlayerInput playerInput) {
        int cameraLayer = (int)Mathf.Log(cameraLayers[playerInput.playerIndex].value, 2);

        playerInput.camera.cullingMask |= (1 << cameraLayer);

        DemoCharacterController character = playerInput.GetComponent<DemoCharacterController>();

        character.virtualCamera.gameObject.layer= cameraLayer;
        character.virtualCamera.GetComponent<CinemachineInputProvider>().PlayerIndex = playerInput.playerIndex;
    }
    public void OnPlayerLeft(PlayerInput playerInput) {

    }

}
