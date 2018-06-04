using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColliderExtensions
{
	/// <summary>
	/// Determines whether this instance is player.
	/// </summary>
	/// <param name="collider">The collider.</param>
	/// <returns>
	///   <c>true</c> if the specified collider is player; otherwise, <c>false</c>.
	/// </returns>
	public static bool IsPlayer(this Collider collider)
	{
		return collider.gameObject.CompareTag("Player");
	}
}
