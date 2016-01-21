using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MovieController : MonoBehaviour {

	public MeshRenderer			mTarget;
	public PopMovie				mMovie;
	public List<string>			mFilenames;
	public List<Texture>		mStreamTextures;
	public List<Texture>		mPerformanceTextures;
	public List<Texture>		mAudioTextures;
	public bool					mLocalisedPerformanceGraph = false;
	public bool					mEnableDebugLog = false;
	public bool					mEnableDebugTextures = false;
	public UnityEngine.UI.Text	mErrorText;
	public UnityEngine.Events.UnityEvent	mOnFinished;
	public UnityEngine.Events.UnityEvent	mOnStarted;

	private List<string>		mFilenameQueue;
	
	public void StartMovie()
	{
		//	setup the queue
		mFilenameQueue = new List<string> ();
		mFilenameQueue.AddRange (mFilenames);

		UpdateMovie ();
	}

	void OnFinished()
	{
		//	signal we're no longer "started"
		mFilenameQueue = null;

		if (mOnFinished!=null)
			mOnFinished.Invoke ();
	}

	void UpdateMovie()
	{
		//	queue not initialised (we haven't started)
		if ( mFilenameQueue == null )
			return;

		//	see if movie is finished to move onto next
		if (mMovie != null) {
			//	check duration
			var Duration = mMovie.GetDurationMs();
			var CurrentTime = mMovie.GetTimeMs();
			if ( Duration > 0 && CurrentTime >= Duration )
			{
				mMovie = null;
				System.GC.Collect();
			}
			else
			{
				Debug.Log("Current duration: " + CurrentTime + "/" + Duration );
			}
		}

		//	need to alloc next
		if (mMovie == null) {


			if (mFilenameQueue.Count == 0) {
				Debug.Log("queue finished.... ");
				OnFinished ();
				return;
			}

			var Filename = mFilenameQueue [0];
			mFilenameQueue.RemoveAt (0);

			var Params = new PopMovieParams ();
			//Params.mSkipPushFrames = true;
			try {
				mMovie = new PopMovie (Filename, Params, true);
				
				if (mEnableDebugLog)
					mMovie.AddDebugCallback (Debug.Log);
			} catch (System.Exception e) {
				Debug.LogError ("Error creating movie; " + e.Message);
				if (mErrorText != null)
					mErrorText.text = e.Message;
			}
		}

		if (mMovie != null)
			mMovie.Update ();
	}

	void Update()
	{
		UpdateMovie ();

		for (int s=0; s<mStreamTextures.Count; s++) {
			var texture = mStreamTextures [s];
			if (texture == null)
				continue;
			
			//	gr: the updateTexture() can cause mMovie to be deleted. mono seems to miss throwing exceptions if mMovie is null so it will take down unity
			if (mMovie != null)
				mMovie.UpdateTexture (texture, s);
		}
		
		for (int s=0; s<mPerformanceTextures.Count && mEnableDebugTextures; s++) {
			var texture = mPerformanceTextures [s];
			if (texture == null)
				continue;
			
			//	gr: the updateTexture() can cause mMovie to be deleted. mono seems to miss throwing exceptions if mMovie is null so it will take down unity
			if (mMovie != null)
				mMovie.UpdatePerformanceGraphTexture (texture, s, mLocalisedPerformanceGraph );
		}
		
		for (int s=0; s<mAudioTextures.Count && mEnableDebugTextures; s++) {
			var texture = mAudioTextures [s];
			if (texture == null)
				continue;
			
			//	gr: the updateTexture() can cause mMovie to be deleted. mono seems to miss throwing exceptions if mMovie is null so it will take down unity
			if (mMovie != null)
				mMovie.UpdateAudioTexture (texture, s );
		}

		//	waiting for first frame
		if (!HaveAppliedTexture () && mMovie!=null ) {
			var LastFrameCopied = mMovie.GetLastFrameCopiedMs ();
			if (LastFrameCopied != 0)
				OnMovieFrameReady ();
		}
	}

	bool HaveAppliedTexture()
	{
		return mTarget.material.mainTexture == mStreamTextures[0];
	}

	void OnMovieFrameReady()
	{
		mTarget.material.mainTexture = mStreamTextures[0];

		if (mOnStarted != null)
			mOnStarted.Invoke ();
	}

}
