using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class TimeSliderControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
	public PlaybackControls	playbackControls;

    public Slider TimeSlider;
    public PopMovieSimple Movie;

	//	dont modify the slider whilst it's being dragged
    bool UpdateSliderValue = true;

    void Start()
    {
		if (TimeSlider == null)
			TimeSlider = GetComponent<Slider> ();
		TimeSlider.maxValue = Movie.Movie.GetDuration ();
    }


    void Update()
    {
        if (UpdateSliderValue)
        {
            UpdateTimeSlider();
        }
    }

		
	public void OnBeginDrag(PointerEventData _EventData)
	{
        UpdateSliderValue = false;
    }

    public void OnDrag(PointerEventData _EventData)
    {
		SetMovieTime ();
    }

    public void OnEndDrag(PointerEventData _EventData)
    {
		SetMovieTime ();
        UpdateSliderValue = true;
    }

    public void OnPointerDown(PointerEventData _EventData)
    {
		UpdateSliderValue = false;
		SetMovieTime ();
        UpdateSliderValue = true;
    }

  
    public void SetSpeed(float SpeedScalar)
    { 
        if ( Movie != null )
		{
			Movie.MovieTimeScalar = SpeedScalar;
		}
    }

	void SetMovieTime(float Time)
	{
		if (Movie) {
			Movie.MovieTime = Time;
		}
	}

	void SetMovieTime()
	{
		SetMovieTime (TimeSlider.value);
	}

    void UpdateTimeSlider()
    {
		var CurrentTime = Movie.MovieTime;
		TimeSlider.value = CurrentTime;
    }
 
}
