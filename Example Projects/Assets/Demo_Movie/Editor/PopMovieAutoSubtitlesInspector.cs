using UnityEngine;
using System.Collections;
using UnityEditor;


/*
	Example of how to use the inspector on a custom type
*/
[CustomEditor(typeof(PopMovieAutoSubtitles))]
public class PopMovieAutoSubtitlesInspector : PopMovieInspector
{
	public override void OnInspectorGUI()
	{
		var Object = target as PopMovieAutoSubtitles;
		ShowInspectorDebug( Object.Instance, this );
		DrawDefaultInspector();
	}
}

