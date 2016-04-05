using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MovieController))]
public class MovieControllerInspector : PopMovieInspector
{
	public override void OnInspectorGUI()
	{
		var Object = target as MovieController;
		ShowInspectorDebug( Object.mMovie, this );
		DrawDefaultInspector();
	}
}

