using UnityEngine;
using System.Collections;

public class PopMovieAutoSubtitles : MonoBehaviour {

	public PopMovieSimple		Movie;
	public PopMovie				Instance;
	public UnityEngine.UI.Text	SubtitlesTarget;
	public float				SubtitlesTimeOffsetSecs = 0;
	public PopMovieParams		Parameters;


	bool CreateInstance()
	{
		if (Movie == null)
			return false;

		//	look for subtitle file that goes with the movie filename
		var MovieFilename = Movie.Filename;
		//	remove extension
		var ExtPos = MovieFilename.LastIndexOf('.');
		if (ExtPos == -1)
		{
			Debug.LogWarning ("PopMovieAutoSubtitles could not determine subtitle filename from " + MovieFilename);
			return false;
		}
		var SubtitleFilename = MovieFilename.Substring (0,ExtPos);
		SubtitleFilename += ".srt";

		try
		{
			Instance = new PopMovie( SubtitleFilename, Parameters, Movie.MovieTime );
		}
		catch(System.Exception e) {
			Debug.LogWarning("PopMovieAutoSubtitles failed to create movie: " + e.Message );
			return false;
		}

		return true;
	}

	void Awake () {

		if (!CreateInstance ()) {
			gameObject.SetActive (false);
			return;
		}
	}
	
	void Update () {
	
		if (Instance == null)
			return;

		Instance.SetTime (Movie.MovieTime + SubtitlesTimeOffsetSecs);

		//	get current subtitles
		var FrameSubtitle = Instance.UpdateString(true );
		if (FrameSubtitle != null) {
			if (SubtitlesTarget != null) {
				SubtitlesTarget.text = FrameSubtitle;
			} else if (FrameSubtitle != null) {
				Debug.Log ("Subtitle: " + FrameSubtitle);
			}
		}
	}
}

