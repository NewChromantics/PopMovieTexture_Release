using UnityEngine;
using System.Collections;

public class MovieController : MonoBehaviour {

	public MeshRenderer			mTarget;
	public PopMovie				mMovie;
	public string				mFilename;
	public Texture				mTargetTexture;

	public void StartMovie()
	{
		var Params = new PopMovieParams ();
		mMovie = new PopMovie (mFilename, Params, true);
		mMovie.AddDebugCallback (Debug.Log);
	}

	void Update()
	{
		if (mMovie != null)
			mMovie.Update ();
		if (mMovie != null && mTargetTexture!=null)
			mMovie.UpdateTexture(mTargetTexture);

		//	waiting for first frame
		if (!HaveAppliedTexture () && mMovie!=null ) {
			var LastFrameCopied = mMovie.GetLastFrameCopiedMs ();
			if (LastFrameCopied != 0)
				OnMovieFrameReady ();
		}
	}

	bool HaveAppliedTexture()
	{
		return mTarget.material.mainTexture == mTargetTexture;
	}

	void OnMovieFrameReady()
	{
		mTarget.material.mainTexture = mTargetTexture;
	}

}
