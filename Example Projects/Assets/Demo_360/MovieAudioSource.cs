using UnityEngine;
using System.Collections;

public class MovieAudioSource : MonoBehaviour {

	public MovieController		mMovie;
	public int					mAudioStream = 0;

	private int					mAudioSampleRate = 0;

	void Start()
	{
		mAudioSampleRate = AudioSettings.outputSampleRate;
	}

	
	void OnAudioFilterRead(float[] data,int Channels)
	{
		var Movie = mMovie ? mMovie.mMovie : null;
		if (Movie == null)
			return;
		
		int SampleCount = data.Length / Channels;
		float SampleCountf = SampleCount;
		float SampleRate = mAudioSampleRate;
		float Duration = SampleRate / SampleCountf;
		
		ulong StartTime = Movie.GetTimeMs ();
		ulong EndTime = StartTime + (ulong)Duration;
		
		Movie.GetAudioBuffer (data, Channels, StartTime, EndTime, mAudioStream );
	}
}
