Unity Splitscreen Workshop
By Jay Ignatowski
Hosted by WPI IGDA on 12/1/2023


Packages to download:
- Input System
- Cinemachine


Steps:
1. Create input action asset (should be at the bottom of the Create menu)
	- Create Action Map and define desired actions
	- Ensure there are control schemes

2. Create a layer for every Player
	* This is necessary for splitscreen to display the correct cameras

3. Create player prefab from an empty GameObject
	- Add a camera as a child 
		- Remove Audio Listener
		- Add Cinemachine Brain if it doesn't have one
		- In the Culling Mask, disable all the Player layers
	- Create player object as a child 
		- Add Player Input component
			- Plug in Action Asset 
			- Plug in Camera
			- Change Behavior to Unity Events or C Sharp Events
			* Unity Events are recommended, C# events are more technical
	- Create virtual camera as a child
		- If you want a controllable camera, add a CinemachineInputProvider component
			- Set XY Axis to the action that controls the camera

4. Create PlayerManager from empty GameObject
	- Add Player Input Manager component
		- Plug in the player prefab we just created to the field in the Player Input Manager
		- Tick "Enable Splitscreen"

5. Create PlayerManager script and add it to PlayerManager object
	- Add variable for PlayerInputManager called inputManager
		- Set with GetComponent in Awake
	- Add public void OnPlayerJoined function with a PlayerInput parameter named playerInput
	- Add public void OnPlayerLeft function with a PlayerInput parameter named playerInput
	- Subscribe these functions to the corresponding events
		* If using UnityEvents (recommended)
			- Go to PlayerInputManager component on PlayerManager object
			- Expand Events tab
				- Add OnPlayerJoined function to PlayerJoinedEvent
				- Add OnPlayerLeft function to PlayerLeftEvent
				* Now these functions will be called
		* If using C# Events (more technical, but don't have to do manually every time you )
			* You can subscribe methods to a delegate using the += operator
					Example: delegate += method
			- Add OnEnable function(this is a default method that will be called when object is enabled)
				- subscribe OnPlayerJoined to the onPlayerJoined delegate of inputManager
				- subscribe OnPlayerLeft to the onPlayerLeft delegate of inputManager
				* Now these functions will be called
			- Add OnDisable function
				- unsubscribe OnPlayerJoined from the onPlayerJoined delegate of inputManager
				- unsubscribe OnPlayerLeft from the onPlayerLeft delegate of inputManager
				* Now these functions will not be called when they are invalid
`				* This is necessary

6. In the PlayerManager script, configure cameras for splitscreen
	- Create a public LayerMask array called cameraLayers
	- In the inspector, add an element to the array for every layer
		- Select the layer corresponding to that player (only one layer per element)
	- In OnPlayerJoined, create an integer called cameraLayer and set it to the following:
		int cameraLayer = (int)Mathf.Log(cameraLayers[playerInput.playerIndex].value, 2);
		* This will get the layer id of the layer we set in the inspector
	- Add cameraLayer to the playerInput camera's culling mask with the following operation
		playerInput.camera.cullingMask |= (1 << cameraLayer);
	***********- Set the layer of the virtual camera's GameObject to the cameraLayer
	***********- Set the cinemachine input provider's PlayerIndex to the playerIndex variable of playerInput

7. Create a character controller script and add it the player object
	- Create functions that process input, they must take in a InputAction.CallbackContext parameter
		* To get the value of an input, use context.ReadValue<T>
	- Subscribe these functions to the corresponding events
		* If using UnityEvents (recommended)
			- In the PlayerInput component in the inspector, expand PlayerActions in the Events tab
				- Now, add the functions to the events
		* If using C# events
			- Get Input Action Map from Player Input
				InputActionMap actionMap = playerInput.currentActionMap;
			- Get action from action map
				InputAction jumpAction = actionMap.FindAction("Jump");
			- Subscribe a method to a delegate in OnEnable
				jumpAction.performed += JumpFunction;
			- Unsubscribe the method to a delegate in OnDisable
				jumpAction.performed -= JumpFunction;
			*To have a function be called when the action is pressed, use the 'performed' delegate
			*To have a function be called when the action is released use the 'canceled' delegate
				


			


			






