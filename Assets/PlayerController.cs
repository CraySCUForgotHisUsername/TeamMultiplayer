using UnityEngine;
using System.Collections;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float 
        speed = 10.0f,
        lookSensitivity = 100.0f;

    private PlayerMotor motor;
	// Use this for initialization
	void Start () {
        motor = GetComponent<PlayerMotor>();
        

    }
	
	// Update is called once per frame
	void Update () {
        //Calculate movement velocity 
        float xMove = Input.GetAxisRaw("Horizontal"),
            zMove = Input.GetAxisRaw("Vertical");
        Vector3 velocity = (transform.right * xMove + transform.forward * zMove ).normalized* speed;
        //
        motor.move(velocity);
        motor.rotate(new Vector3(0, Input.GetAxisRaw("Mouse X"), 0) * lookSensitivity);
        motor.rotateCamera(new Vector3(Input.GetAxisRaw("Mouse Y"), 0, 0) * lookSensitivity);
    }
}
