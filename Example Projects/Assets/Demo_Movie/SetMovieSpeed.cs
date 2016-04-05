using UnityEngine;
using System.Collections;

public class SetMovieSpeed : MonoBehaviour {

	public PopMovieSimple		Movie;


	public void SetSpeed(float SpeedScalar)
	{
		if ( Movie != null )
		{
			Movie.MovieTimeScalar = SpeedScalar;
		}
	}

}
