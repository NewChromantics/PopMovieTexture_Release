using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MovieController : MonoBehaviour {

	public MeshRenderer			mTarget;
	public PopMovie				mMovie;
	public string				mFilename;
	public List<Texture>		mStreamTextures;
	public List<Texture>		mPerformanceTextures;
	public List<Texture>		mAudioTextures;
	public bool					mLocalisedPerformanceGraph = false;
	public bool					mEnableDebugLog = false;

	public void StartMovie()
	{
		var Params = new PopMovieParams ();
		mMovie = new PopMovie (mFilename, Params, true);

		if ( mEnableDebugLog )
			mMovie.AddDebugCallback (Debug.Log);
	}

	void Update()
	{
		if (mMovie != null)
			mMovie.Update ();

		for (int s=0; s<mStreamTextures.Count; s++) {
			var texture = mStreamTextures [s];
			if (texture == null)
				continue;
			
			//	gr: the updateTexture() can cause mMovie to be deleted. mono seems to miss throwing exceptions if mMovie is null so it will take down unity
			if (mMovie != null)
				mMovie.UpdateTexture (texture, s);
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
		mTarget.gameObject.SetActive (true);
		mTarget.material.mainTexture = mStreamTextures[0];
	}

}
