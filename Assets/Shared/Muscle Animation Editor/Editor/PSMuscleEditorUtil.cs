using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PSMuscleEditorUtil
{
	public static string PathTruncate (string value)
	{
		if (string.IsNullOrEmpty (value))
			return value;

		string[] strs = value.Split ('/');
		if (strs.Length > 2) {
			value = string.Format ("{0}/.../{1}", strs [0], strs [strs.Length - 1]);
		}

		return value; 
	}
}
