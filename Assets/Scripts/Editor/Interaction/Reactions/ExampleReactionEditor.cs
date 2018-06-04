using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

[CustomEditor(typeof(ExampleReaction))]
public class ExampleReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Example Reaction";
	}
}
