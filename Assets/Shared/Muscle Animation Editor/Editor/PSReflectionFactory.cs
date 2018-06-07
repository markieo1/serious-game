using UnityEngine;
using System.Collections;

public class PSReflectionFactory
{
	public static PSBaseReflection GetReflection ()
	{
		return new PSReflection ();
	}
}
