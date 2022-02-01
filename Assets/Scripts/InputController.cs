using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private charMove charController;

    private void Awake()
    {
        charController = GetComponent<charMove>();
    }

    private void Update()
    {
        int vertical = Mathf.RoundToInt(Input.GetAxis("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
        bool jump = Input.GetKey(KeyCode.Space);

        charController.forwardInput = vertical;
        charController.turnInput = horizontal;
        charController.jumpInput = jump;

    }

}
