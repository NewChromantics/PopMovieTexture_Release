using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnumMovies : MonoBehaviour {

	[Range(0,10)]
	public float					RefreshRate = 5;
	public UnityEngine.UI.Text		Target;

	[Range(0,10)]
	public float					mRefreshCountdown = 1;

	private string					mGuiString;

	private List<string> mSources;
		
	void UpdateOutput(string Text)
	{
		if (Target == null) {
			mGuiString = Text;
			return;
		}

		Target.text = Text;
		mGuiString = null;
	}

	void EnumSources()
	{
		var Sources = PopMovie.EnumSources ();
		if (Sources == null)
		{
			UpdateOutput ("No sources found, error?");
			return;
		}
		string Output = "";
		foreach( string Source in Sources )
		{
			Output += Source + "\n";
		}
		UpdateOutput (Output);
		return;
	}

	void Update () {
	
		mRefreshCountdown -= Time.deltaTime;
		if (mRefreshCountdown < 0) {
			EnumSources ();
			mRefreshCountdown = RefreshRate;
		}

		PopMovie.FlushDebug ( (string s)=>{Debug.Log(s);});
	}

	void OnGUI()
	{
		//	fallback and render to old gui if not target assigned
		if (mGuiString == null)
			return;

		GUI.Label (new Rect (0, 0, Screen.width, Screen.height), mGuiString);
	}
}
