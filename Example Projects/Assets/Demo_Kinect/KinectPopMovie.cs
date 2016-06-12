using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif




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


/* gr: not sure why this won't reslve PopMovieSimpleInspector
#if UNITY_EDITOR
[CustomEditor(typeof(KinectPopMovie))]
public class KinectPopMovieInspector : PopMovieSimpleInspector
{
}
#endif
*/

