using UnityEngine;
using System.Collections;

public class MovieAudioSource : MonoBehaviour {

	public MovieController		mMovie;
	public int					mAudioStream = 0;

	void OnAudioFilterRead(float[] data,int Channels)
	{
		var Movie = mMovie ? mMovie.mMovie : null;
		if (Movie == null)
			return;
		
		uint StartTime = Movie.GetTimeMs ();
		Movie.GetAudioBuffer (data, Channels, StartTime, mAudioStream );
	}
}
