using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private PlayerInputManager inputManager;
    [SerializeField] LayerMask[] cameraLayers;


    private void Awake() {
        inputManager = GetComponent<PlayerInputManager>();
    }

    private void OnEnable() {
        inputManager.onPlayerJoined += OnPlayerJoined;
        inputManager.onPlayerLeft += OnPlayerLeft;
    }
    private void OnDisable() {
        inputManager.onPlayerJoined -= OnPlayerJoined;
        inputManager.onPlayerLeft -= OnPlayerLeft;
    }

    public void OnPlayerJoined(PlayerInput playerInput) {
        int cameraLayer = (int)Mathf.Log(cameraLayers[playerInput.playerIndex].value, 2);
        playerInput.camera.cullingMask |= (1 << cameraLayer);

        CharacterController character = playerInput.GetComponent<CharacterController>();

        CinemachineVirtualCamera virtualCamera = character.virtualCamera;
        virtualCamera.gameObject.layer = cameraLayer;

        CinemachineInputProvider inputProvider = virtualCamera.GetComponent<CinemachineInputProvider>();
        inputProvider.PlayerIndex = playerInput.playerIndex;
    }
    public void OnPlayerLeft(PlayerInput playerInput) {

    }


}
