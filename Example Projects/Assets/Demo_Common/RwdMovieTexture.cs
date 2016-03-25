using UnityEngine;
using System.Collections;					// required for Coroutines
using System.Runtime.InteropServices;		// required for DllImport
using System;								// requred for IntPtr
using System.Collections.Generic;


public class RwdMovieTexture : MonoBehaviour {

	public List<Texture>		mStreamTextures;
	public int					mAudioStreamIndex = 0;
	public List<RenderTexture>	mPerformanceTextures;
	public List<RenderTexture>	mAudioTextures;
	public bool					mLocalisedPerformanceGraph = true;

	private PopMovie			mMovie;
	private int					mAudioSampleRate = 1;

	[Range(0,1)]
	public float				mDebugGuiScale = 0.25f;
	public bool					mEnableDebugLog = true;
	public float				mCreateDelay = 1;
	private float				mInternalCreateDelay = 0;
	public bool					mLooping = true;

	public PopMovieParams		MovieParams;


	public String filename = "/sdcard/Test.mp4";
	
	void DebugLog(string debug)
	{
		if (mEnableDebugLog) {
			//	gr: add a delegate here that app can subscribe to
			Debug.Log (debug);
		}
	}

	void OnFinished()
	{
		Debug.Log ("Movie has finished");

		if ( mLooping )
		{
			mMovie = null;
			GC.Collect();
			mInternalCreateDelay = mCreateDelay;
		}
	}

	void CreateMovie()
	{
		if (mInternalCreateDelay > 0) {
			mInternalCreateDelay -= Time.deltaTime;
			return;
		}

		DebugLog ("Creating movie...");

		try
		{
			mMovie = new PopMovie (filename, MovieParams );
			if ( mEnableDebugLog )
				PopMovie.EnableDebugLog = mEnableDebugLog;

			//	gr: temporarily removed from PopMovie, should be moved to your own controller
			//mMovie.AddOnFinishedCallback( OnFinished );
		}
		catch (System.Exception e)
		{
			Debug.LogError ("Error creating PopMovieTexture: " + e.Message);
		}
	}

	void Update()
	{
		//	cannot read this in the audio thread, so we have to cache it
		mAudioSampleRate = AudioSettings.outputSampleRate;

		//	create movie
		if ( mMovie == null ) {
		
			try {
				CreateMovie();
			} catch (System.Exception e) {
				DebugLog ("failed to create pop movie: " + e.ToString () + " " + e.Message);
			}
		}


		for (int s=0; s<mPerformanceTextures.Count; s++) {
			var texture = mPerformanceTextures [s];
			if (texture == null)
				continue;
			
			//	gr: the updateTexture() can cause mMovie to be deleted. mono seems to miss throwing exceptions if mMovie is null so it will take down unity
			if (mMovie != null)
				mMovie.UpdatePerformanceGraphTexture (texture, s, mLocalisedPerformanceGraph );
		}
		
		for (int s=0; s<mAudioTextures.Count; s++) {
			var texture = mAudioTextures [s];
			if (texture == null)
				continue;
			
			//	gr: the updateTexture() can cause mMovie to be deleted. mono seems to miss throwing exceptions if mMovie is null so it will take down unity
			if (mMovie != null)
				mMovie.UpdateAudioTexture (texture, s );
		}
		
		for (int s=0; s<mStreamTextures.Count; s++) {
			var texture = mStreamTextures [s];
			if (texture == null)
				continue;
			
			//	gr: the updateTexture() can cause mMovie to be deleted. mono seems to miss throwing exceptions if mMovie is null so it will take down unity
			if (mMovie != null)
				mMovie.UpdateTexture (texture, s);
		}
	}

	void OnDisable()
	{
		mMovie = null;
		//	force cleanup now
		GC.Collect();
	}
	
	
	void OnGUI()
	{
		if (mDebugGuiScale <= 0 || mMovie == null )
			return;

		//	draw in the bottom left and move along
		int wh = (int)Mathf.Min ( Screen.height*mDebugGuiScale, Screen.width*mDebugGuiScale );
		Rect debugRect = new Rect (0, Screen.height - wh, wh, wh);

		//	draw graphs on the row below
		if (mPerformanceTextures.Count > 0)
			debugRect.y -= debugRect.height;

		for ( int t=0;	t<mStreamTextures.Count;	t++ )
		{
			var texture = mStreamTextures[t];
			if ( texture == null )
				continue;
			
			GUI.DrawTexture( debugRect, texture );
			debugRect.x += debugRect.width;
		}
		
		//	draw graphs on the row below
		if (mPerformanceTextures.Count > 0) {
			debugRect.y += debugRect.height;
			debugRect.x = 0;
		}

		for ( int t=0;	t<mPerformanceTextures.Count;	t++ )
		{
			var texture = mPerformanceTextures[t];
			if ( texture == null )
				continue;
			
			GUI.DrawTexture( debugRect, texture );
			debugRect.x += debugRect.width;
		}
		
		//	draw some other debug
		String Text = "";
		Text += "Last Frame Requested: " + mMovie.GetTimeMs() + "\n";
		Text += "Last Frame Copied: " + mMovie.GetLastFrameCopiedMs() + "\n";
		Text += "OutOfSync: " + ((long)mMovie.GetLastFrameCopiedMs() - (long)mMovie.GetTimeMs()) + "\n";
		GUI.Label (new Rect (0, Screen.height - (wh*3), wh*2, wh), Text);
	}


	void OnAudioFilterRead(float[] data, int channels)
	{
		if (mMovie == null) {
			for (int i=0; i<data.Length; i++)
				data [i] = 0;
			return;
		}
		
		int SampleCount = data.Length / channels;
		float SampleCountf = SampleCount;
		float SampleRate = mAudioSampleRate;
		float Duration = SampleRate / SampleCountf;
		
		//Debug.Log ("duration=" + Duration);
		//Debug.Log ("Time diff = " + (mRealTime - mTime));
		
		ulong StartTime = mMovie.GetTimeMs ();
		ulong EndTime = StartTime + (ulong)Duration;
		
		mMovie.GetAudioBuffer (data, channels, StartTime, EndTime, mAudioStreamIndex);
	}

}
