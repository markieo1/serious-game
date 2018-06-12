using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Achievement")]
public class Achievement : ScriptableObject
{
	/// <summary>
	/// Achievement Name
	/// </summary>
	public string Name;

	/// <summary>
	/// Achievement description
	/// </summary>
	public string Description;

	/// <summary>
	/// Count to unlock achievement
	/// </summary>
	public int CountToUnlock;

	/// <summary>
	/// Is unlocked
	/// </summary>
	public bool Unlocked;
}
