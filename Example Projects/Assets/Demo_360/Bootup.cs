using UnityEngine;
using System.Collections;

public class Bootup : MonoBehaviour {

	public UnityEngine.Events.UnityEvent	mBootupFunction;
	private bool			mStarted = false;

	public bool				ExecuteInAndroid = false;
	public bool				ExecuteInGearVr = true;
	public bool				ExecuteInIos = false;
	public bool				ExecuteInDesktop = false;
	public bool				ExecuteInDesktopVr = true;
	public bool				AllowSleep = false;



	void Start()
	{
		if (!AllowSleep) {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}
	}

	void Update () {

		bool Execute = false;
		bool HasVr = MouseLook.UsingVr ();
#if UNITY_EDITOR
		Execute = HasVr ? ExecuteInDesktopVr : ExecuteInDesktop;
#elif UNITY_ANDROID
		Execute = HasVr ? ExecuteInDesktopVr : ExecuteInDesktop;
#elif UNITY_IOS
		Execute = ExecuteInIos;
#elif UNITY_STANDALONE_OSX
		Execute = HasVr ? ExecuteInDesktopVr : ExecuteInDesktop;
#elif UNITY_STANDALONE_WINDOWS
		Execute = HasVr ? ExecuteInDesktopVr : ExecuteInDesktop;
#endif

		if (!mStarted && mBootupFunction!=null && Execute ) {
			mBootupFunction.Invoke();
			mStarted = true;
		}
	
	}

}
