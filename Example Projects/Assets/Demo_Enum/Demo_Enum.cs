using UnityEngine;
using System.Collections;

public class Demo_Enum : MonoBehaviour {

	[Range(0,10)]
	public float			RefreshRate = 5;
	public UnityEngine.UI.Text			Target;

	private float			mRefreshCountdown = 1;

	private string			mGuiString;

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
		try
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
		catch(System.Exception e)
		{
			string Error = e.GetType().Name + "/" + e.Message;
			UpdateOutput("Exception getting sources: " + Error);
		}
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
