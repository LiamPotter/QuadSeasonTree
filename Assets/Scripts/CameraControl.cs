using UnityEngine;
using System.Collections;
using TriTools;
public class CameraControl : MonoBehaviour {

    public float rotationSpeed;

    public float damping;

    private float horizontalInput;

    private GameController gControl;
	void Start ()
    {
        gControl = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
        if(gControl.currentGameState==GameController.GameState.Playing)
            TriToolHub.Rotate(gameObject, TriToolHub.XYZ.Y, 1* Time.deltaTime*rotationSpeed, true, Space.World);
	}
}
