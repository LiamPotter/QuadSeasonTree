using UnityEngine;
using System.Collections;
using TriTools;
public class CameraControl : MonoBehaviour {

    public float rotationSpeed;

    public float damping;

    private float horizontalInput;
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        TriToolHub.Rotate(gameObject, TriToolHub.XYZ.Y, horizontalInput * Time.deltaTime*rotationSpeed, true, Space.World);
	}
}
