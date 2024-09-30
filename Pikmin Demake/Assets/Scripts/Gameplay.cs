using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    private Camera MainCamera;

    public int CameraMoveSpeed;

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
            if (Physics.Raycast(CastPoint, out HitInfo))
            {
                Character ClickedCharacter = HitInfo.transform.GetComponent<Character>();

                if (ClickedCharacter != null)
                {
                    Debug.Log("Selected Character");
                    ClickedCharacter.Selected();
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(CastPoint, out HitInfo))
            {
                Character ClickedCharacter = HitInfo.transform.GetComponent<Character>();

                if (ClickedCharacter != null)
                {
                    Debug.Log("Unselected Character");
                    ClickedCharacter.Unselected();
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MainCamera.transform.Translate(Vector3.left * CameraMoveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MainCamera.transform.Translate(Vector3.right * CameraMoveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MainCamera.transform.Translate(Vector3.forward * CameraMoveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MainCamera.transform.Translate(-Vector3.forward * CameraMoveSpeed * Time.deltaTime);
        }
    }
}
