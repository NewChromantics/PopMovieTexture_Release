using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Demo_Subtitles : MonoBehaviour {

	PopMovie			mMovie;
	public string		mFilename;
	private List<string>	mLines;
	public int			mMaxLines = 30;

	[Range(0,100)]
	public float		mTime = 1;
	[Range(0,1000)]
	public float		mTimeMultipler = 1;

	void Start()
	{
		try
		{
			PopMovieParams Params = new PopMovieParams ();
			mMovie = new PopMovie (mFilename, Params, false);
			mMovie.AddDebugCallback (Debug.Log);
		}
		catch(System.Exception e)
		{
			Debug.LogError (e.Message);
		}
	}

	void Update () {
	
		if ( mMovie == null )
				return;

		mTime += Time.deltaTime * mTimeMultipler;
		mMovie.SetTime (mTime);
		mMovie.Update ();
		string Line = mMovie.UpdateString (false);
		if ( Line != null )
		{
			if ( mLines == null )
				mLines = new List<string>();

			mLines.Add (Line);

			if ( mLines.Count > mMaxLines )
				mLines.RemoveRange( 0, mLines.Count - mMaxLines ); 
		}

	}

	void OnGUI()
	{
		string AllLines = "";

		if (mMovie != null)
			AllLines += mMovie.GetTime () + "\n";

		if (mLines != null) {
			for (int i=mLines.Count-1; i>=0; i--)
				AllLines += mLines [i] + "\n";
		}

		GUI.Label (new Rect (0, 0, Screen.width, Screen.height), AllLines);
	}
}
