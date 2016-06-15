using UnityEngine;
using System.Collections;


public class KinectPopMovie : PopMovieSimple {

	public Texture DepthTexture;
	public int DepthStream = 1;	//	the 2nd video stream is depth

	public Material MergeShader;
	public RenderTexture MergeOutput;


	public new void Update () {
	
		base.Update ();

		if (Movie != null && DepthTexture != null ) {
			Movie.UpdateTexture (DepthTexture, DepthStream);
		}

		if (MergeShader && MergeOutput) {
			Graphics.Blit (null, MergeOutput, MergeShader);
		}
	}
}

