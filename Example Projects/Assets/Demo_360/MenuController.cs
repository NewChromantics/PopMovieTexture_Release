using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public Canvas	mCanvas;

	[Range(0,360)]
	public float	mRotationSpeed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if ( mCanvas != null )
		{
			float Rotation = mRotationSpeed * Time.deltaTime;
			mCanvas.transform.Rotate( new Vector3( 0, Rotation, 0 ) );
		}

	}
}
