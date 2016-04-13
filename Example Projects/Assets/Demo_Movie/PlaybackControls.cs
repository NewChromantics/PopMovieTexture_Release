using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PlaybackControls : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public Slider TimeSlider;
    public PopMovieSimple Movie;
    public Button PlayButton;
    public Button PauseButton;
    public Button StopButton;
    public Button SkipFwdButton;
    public Button SkipBwdButton;
    public Slider SpeedSlider;
    public Button ResetSpeedButton;
    public Text CurrentTimeText;
    [Header("Amount to skip ahead")]
    public float SkipAmount = 10;
    float Duration;
    float CurrentTime;
    bool UpdateSliderValue = true;

    void Start()
    {
        PlayButton.onClick.AddListener(Play);
        PauseButton.onClick.AddListener(Pause);
        StopButton.onClick.AddListener(Stop);
        SkipBwdButton.onClick.AddListener(SkipBwd);
        SkipFwdButton.onClick.AddListener(SkipFwd);
        ResetSpeedButton.onClick.AddListener(ResetMovieSpeed);
        StartCoroutine(IEUpdateClock());
        Duration = Movie.Movie.GetDuration();
        TimeSlider.maxValue = Duration;
    }

    void Update()
    {
        if (!Movie.Playing)
        {
            UpdateSliderValue = false;
        }
        if (UpdateSliderValue)
        { 
            CurrentTime = Movie.Movie.GetTime();
            UpdateTimeSlider();
        }        
    }

    IEnumerator IEUpdateClock()
    {
        while (true)
        { 
            if (Movie.Playing)
            {
                TimeSpan t = TimeSpan.FromSeconds(CurrentTime);
				CurrentTimeText.text = string.Format("{0:D2}:{1:D2}:{2:D3}",
                t.Minutes,
					t.Seconds,
					t.Milliseconds);
            }
            yield return new WaitForSeconds(1.0f/50.0f);
        }
    }

    public void OnBeginDrag(PointerEventData _EventData)
    {
        UpdateSliderValue = false;
    }

    public void OnDrag(PointerEventData _EventData)
    {
        Movie.MovieTime = TimeSlider.value;
    }

    public void OnEndDrag(PointerEventData _EventData)
    {
        Movie.MovieTime = TimeSlider.value;
        UpdateSliderValue = true;
    }

    public void OnPointerDown(PointerEventData _EventData)
    {
        UpdateSliderValue = false;
        Movie.MovieTime = TimeSlider.value;
        UpdateSliderValue = true;
    }

    void Play()
    {
        if (!Movie.Playing)
        {
            UpdateSliderValue = true;
            Movie.Play();
        }
    }

    void Pause()
    {
        if (Movie.Playing)
        {
            UpdateSliderValue = false;
            Movie.Pause();
        }
    }

    void Stop()
    {
        if (Movie.Playing)
        {
            UpdateSliderValue = false;
            Movie.Stop();
            Movie.MovieTime = 0;
            UpdateTimeSlider();
        }
    }

    public void SetSpeed(float SpeedScalar)
    { 
        if ( Movie != null )
		{
			Movie.MovieTimeScalar = SpeedScalar;
		}
    }

    void UpdateTimeSlider()
    {
        TimeSlider.value = Movie.MovieTime;
    }

    void ResetMovieSpeed()
    {
        Movie.MovieTimeScalar = 1;
        UpdateSpeedSlider();
    }

    void UpdateSpeedSlider()
    {
        SpeedSlider.value = Movie.MovieTimeScalar;
    }

    void SkipFwd()
    {
        float NewTime = Movie.MovieTime + SkipAmount;
        if (NewTime > Duration)
        {
            NewTime = Duration;
        }
        Movie.MovieTime = NewTime;
        UpdateTimeSlider();
    }

    void SkipBwd()
    {
        float NewTime = Movie.MovieTime - SkipAmount;
        if (NewTime < 0)
        {         
            NewTime = 0;
        }
        Movie.MovieTime = NewTime;
    }
}
