using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

    public Vector3 rotationAngle;
    public float rotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
      transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);          
	}
}
