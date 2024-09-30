using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gameplay : MonoBehaviour
{
    private Camera MainCamera;                  // The Game's Camera.
    private bool CharacterSelected = false;     // Tells whether or not a character has been selected.
    private bool IsLooking = false;             // Set to true when free looking (on right mouse button).

    [Header("Camera\n")]
    public float NormalSpeed = 10f;             // Speed of camera movement.
    public float FastSpeed = 100f;              // Speed of camera movement when shift is held down.
    public float LookSensitivity = 3f;          // Sensitivity for free look.
    public float NormalSensitivity = 10f;       // Amount to zoom the camera when using the mouse wheel.
    public float FastSensitivity = 50f;         // Amount to zoom the camera when using the mouse wheel (fast mode).

    [Header("Text\n")]
    public TextMeshProUGUI CharacterText;       // Tells the player if they have a specific character selected or not.
    public TextMeshProUGUI GoalText;            // Tells the player if they have all characters at the goal.

    void Start()
    {
        MainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 MousePosition = Input.mousePosition;
        Ray CastPoint = MainCamera.ScreenPointToRay(MousePosition);
        RaycastHit HitInfo;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(CastPoint, out HitInfo, Mathf.Infinity))
            {
                Character ClickedCharacter = HitInfo.transform.GetComponent<Character>();

                if (ClickedCharacter != null && CharacterSelected == false)
                {
                    Debug.Log("Selected Character");
                    ClickedCharacter.Selected();
                    CharacterSelected = true;

                    if (ClickedCharacter.tag == "Red")
                    {
                        CharacterText.text = "Red";
                        CharacterText.color = new Color(255, 0, 0);
                    }
                    else if (ClickedCharacter.tag == "Yellow")
                    {
                        CharacterText.text = "Yellow";
                        CharacterText.color = new Color(255, 255, 0);
                    }
                    else if (ClickedCharacter.tag == "Blue")
                    {
                        CharacterText.text = "Blue";
                        CharacterText.color = new Color(0, 0, 255);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(CastPoint, out HitInfo, Mathf.Infinity))
            {
                Character ClickedCharacter = HitInfo.transform.GetComponent<Character>();

                if (ClickedCharacter != null)
                {
                    Debug.Log("Unselected Character");
                    ClickedCharacter.Unselected();
                    CharacterSelected = false;

                    CharacterText.text = "None";
                    CharacterText.color = new Color(0, 0, 0);
                }
            }
        }

        /// Keys:
        ///	WASD / Arrows	    - Movement
        ///	Q / E 			    - Up / Down (Local Space)
        ///	R / F 			    - Up / Down (World Space)
        ///	PageUp / PageDown	- Up / Down (World Space)
        ///	Hold Shift		    - Enable Fast Movement Mode
        ///	Right Mouse  	    - Enable Free Look
        ///	Mouse			    - Free Look / Rotation

        var FastMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        var MovementSpeed = FastMode ? FastSpeed : NormalSpeed;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MainCamera.transform.position = MainCamera.transform.position + (-MainCamera.transform.right * MovementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MainCamera.transform.position = MainCamera.transform.position + (MainCamera.transform.right * MovementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MainCamera.transform.position = MainCamera.transform.position + (MainCamera.transform.forward * MovementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MainCamera.transform.position = MainCamera.transform.position + (-MainCamera.transform.forward * MovementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            MainCamera.transform.position = MainCamera.transform.position + (MainCamera.transform.up * MovementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            MainCamera.transform.position = MainCamera.transform.position + (-MainCamera.transform.up * MovementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp))
        {
            MainCamera.transform.position = MainCamera.transform.position + (Vector3.up * MovementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown))
        {
            MainCamera.transform.position = MainCamera.transform.position + (-Vector3.up * MovementSpeed * Time.deltaTime);
        }

        if (IsLooking)
        {
            float NewRotationX = MainCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * LookSensitivity;
            float NewRotationY = MainCamera.transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * LookSensitivity;
            MainCamera.transform.localEulerAngles = new Vector3(NewRotationY, NewRotationX, 0f);
        }

        float Axis = Input.GetAxis("Mouse ScrollWheel");
        if (Axis != 0)
        {
            var zoomSensitivity = FastMode ? FastSensitivity : NormalSensitivity;
            MainCamera.transform.position = MainCamera.transform.position + MainCamera.transform.forward * Axis * zoomSensitivity;
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartLooking();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopLooking();
        }
    }

    void OnDisable()
    {
        StopLooking();
    }

    public void StartLooking()     // Enable free looking.
    {
        IsLooking = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void StopLooking()      // Disable free looking.
    {
        IsLooking = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
