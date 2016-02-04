using UnityEngine;
using System.Collections;

public class Demo_Enum : MonoBehaviour {

	[Range(0,10)]
	public float					RefreshRate = 1;
	public UnityEngine.UI.Text		Target;

	[Range(0,10)]
	private float					RefreshCountdown = 0.5f;

	private uint 					mIndex = 0;

	//	gui can only display so many characters.
	public uint				MaxStringLength = 10000;

	private string			mGuiString;

	void AddSource(string Text,uint Index)
	{
		if (Target == null) {
			mGuiString += Text;
			return;
		}

		if (Index == 0)
			Target.text = "";

		//	full!
		if (Target.text.Length > MaxStringLength)
			return;

		//	about to overflow
		if (Target.text.Length + Text.Length >= MaxStringLength)
			Target.text += " <reached UI text string limit> \n";
		else
			Target.text += Text + "\n";
	
		mGuiString = null;
	}

	void EnumSources()
	{
		PopMovie.EnumDirectory( Application.streamingAssetsPath, true );
		PopMovie.EnumDirectory( Application.persistentDataPath, true );
		PopMovie.EnumDirectory( PopMovie.FilenamePrefix_Sdcard, true );

		var Source = PopMovie.EnumSource (mIndex);
		if (Source == null)
			return;
		AddSource (Source,mIndex);

		mIndex++;
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
