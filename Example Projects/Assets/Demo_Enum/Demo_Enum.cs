using UnityEngine;
using System.Collections;

public class Demo_Enum : MonoBehaviour {

	[Range(0,10)]
	public float			RefreshRate = 1;
	public UnityEngine.UI.Text			Target;

	[Range(0,10)]
	private float			RefreshCountdown = 0.5f;

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
	
		RefreshCountdown -= Time.deltaTime;
		if (RefreshCountdown < 0) {
			EnumSources ();
			RefreshCountdown = RefreshRate;
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
