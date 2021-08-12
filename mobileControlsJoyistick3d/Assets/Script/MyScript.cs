using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class MyScript : MonoBehaviour
{

    public FixedJoystick MoveJoistick;
    public FixedTouchField TouchField;
    public FixedButton JumpButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var fps = GetComponent<RigidbodyFirstPersonController>();
        fps.RunAxis = MoveJoistick.inputVector;
        fps.JumpAxis = JumpButton.Pressed;
        fps.mouseLook.LookAxis = TouchField.TouchDist;
    }
}
