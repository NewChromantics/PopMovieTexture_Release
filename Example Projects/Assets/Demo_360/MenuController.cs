using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject	mCanvas;
	public GameObject	m360Geo;		//	maybe this should be in a different context
	public bool			mToggleSkybox = true;

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

	public void HideMenu()
	{
		if (mCanvas != null)
			mCanvas.gameObject.SetActive (false);

		Camera.main.clearFlags = CameraClearFlags.Depth;
	}

	public void ShowMenu()
	{
		if (mCanvas != null)
			mCanvas.gameObject.SetActive (true);

		Camera.main.clearFlags = CameraClearFlags.Skybox;
	}

	public void Hide360Geo()
	{
		if (m360Geo != null)
			m360Geo.SetActive (false);

		if (mToggleSkybox)
			Camera.main.clearFlags = CameraClearFlags.Skybox;
	}
	public void Show360Geo()
	{
		if (m360Geo != null)
			m360Geo.SetActive (true);
	
		if (mToggleSkybox)
			Camera.main.clearFlags = CameraClearFlags.Depth;
	}

}
