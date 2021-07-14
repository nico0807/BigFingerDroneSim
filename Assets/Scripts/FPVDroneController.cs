using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPVDroneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public AnimationCurve myExpoCurve;

    public float thrustWeightRatio = 8f;
    public float dragFactor = 2f;
    public float dragForceValue = 0f;
    private Vector3 dragForceVector = new Vector3(0f, 0f, 0f);

    public float thrust = 0.0f;
    public float roll = 0.0f;
    public float pitch = 0.0f;
    public float yaw = 0.0f;
    Vector3 Rotate = new Vector3(0f, 0f, 0f);
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //this.gameObject.transform.Translate(Vector3.up);
            //this.GetComponent<Rigidbody>().velocity = Vector3.up*3;
        }
        thrust = ((Input.GetAxisRaw("Thrust")+1.0f)/2f) * (this.GetComponent<Rigidbody>().mass*9.81f*thrustWeightRatio   /*9.81f*/);
        yaw = Input.GetAxisRaw("Yaw"); //Y
        pitch = Input.GetAxisRaw("Pitch"); //X
        roll = Input.GetAxisRaw("Roll");    //Z
        //Rotate = new Vector3(pitch, yaw, roll);
        Rotate.Set(pitch, yaw, roll);
    }

    //Physics engine update (100Hz)
    private void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up*thrust, ForceMode.Force);
        this.GetComponent<Rigidbody>().angularVelocity = transform.TransformDirection(Rotate*4f);

        //Drag simulation
        dragForceValue = this.GetComponent<Rigidbody>().velocity.sqrMagnitude * dragFactor;
        dragForceVector = this.GetComponent<Rigidbody>().velocity.normalized * -dragForceValue;
        this.GetComponent<Rigidbody>().AddForce(dragForceVector, ForceMode.Force);
    }
}
