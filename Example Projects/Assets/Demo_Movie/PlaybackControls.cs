using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PlaybackControls : MonoBehaviour
{
    public PopMovieSimple Movie;
	public TimeSliderControl TimeSlider;
    public Slider SpeedSlider;
	public AspectRatioFitter MovieAspectRatio;

	public Text CurrentTimeText;
    [Header("Amount to skip ahead")]
    public float SkipAmount = 10;

	public GameObject	SkipForwardButton;
	public GameObject	SkipBackwardButton;

	bool Initialised = false;
 

    void Update()
    {
		if (!Initialised)
			OnMovieChanged ();
			
		if (CurrentTimeText != null) {

			float CurrentTime = Movie.MovieTime;

			TimeSpan t = TimeSpan.FromSeconds (CurrentTime);
			CurrentTimeText.text = string.Format ("{0:D2}:{1:D2}:{2:D3}",
				t.Minutes,
				t.Seconds,
				t.Milliseconds);
		}

    }
		
	public void OnMovieChanged()
	{
		if (Movie.Movie == null)
			return;

		var Meta = Movie.Movie.GetMeta ();
		if (Meta == null)
			return;
		bool CanSkip = Meta.CanSeekBackwards;

		bool DurationInitialised = false;
		bool RatioInitialised = false;

		//	duration of 0 is unknown (still loading or a live stream)
		float Duration = Movie.Movie.GetDurationMs();
		if (Duration != 0) {
			DurationInitialised = true;
		}				

		if ( TimeSlider != null )
			TimeSlider.gameObject.SetActive (DurationInitialised);



		if (MovieAspectRatio != null) {
			
			//	sometimes video isn't first stream!
			var Width0 = Meta.Stream0_Width;
			var Height0 = Meta.Stream0_Height;
			var Width1 = Meta.Stream1_Width;
			var Height1 = Meta.Stream1_Height;

			if (Width0 != 0 && Height0 != 0) {
				MovieAspectRatio.aspectRatio = Width0 / (float)Height0;
				RatioInitialised = true;
			}

			if (Width1 != 0 && Height1 != 0) {
				MovieAspectRatio.aspectRatio = Width1 / (float)Height1;
				RatioInitialised = true;
			}
		}

		if (SkipForwardButton != null)
			SkipForwardButton.SetActive (CanSkip);

		if (SkipBackwardButton != null)
			SkipBackwardButton.SetActive (CanSkip);
		
		Initialised = DurationInitialised && RatioInitialised;
	}

	public void Play()
    {
		Movie.Play();
    }

	public void Pause()
    {
		Movie.Pause();
    }

	public void Stop()
    {
		Movie.Stop();

		//	do an explicit texture update so in the render loop it will copy the first frame to the screen
		Movie.UpdateTextures ();
		Movie.MovieTime = 0;
    }

    public void SetSpeed(float SpeedScalar)
    { 
        if ( Movie != null )
		{
			Movie.MovieTimeScalar = SpeedScalar;
		}
    }
		
	public void ResetMovieSpeed()
    {
        Movie.MovieTimeScalar = 1;
        UpdateSpeedSlider();
    }

	public void UpdateSpeedSlider()
    {
        SpeedSlider.value = Movie.MovieTimeScalar;
    }

    public void SkipFwd()
    {
        float NewTime = Movie.MovieTime + SkipAmount;
		var Duration = Movie.Movie.GetDuration ();
        if (NewTime > Duration)
        {
            NewTime = Duration;
        }
        Movie.MovieTime = NewTime;
    }

	public void SkipBwd()
    {
        float NewTime = Movie.MovieTime - SkipAmount;
        if (NewTime < 0)
        {         
            NewTime = 0;
        }
        Movie.MovieTime = NewTime;
    }

	public void SetPlaybackSpeed(float SpeedScalar)
	{
		if ( Movie != null )
		{
			Movie.MovieTimeScalar = SpeedScalar;
		}
	}
}
