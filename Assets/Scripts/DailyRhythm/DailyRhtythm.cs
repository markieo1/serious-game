using System;
using System.Collections;
using UnityEngine;

public class DailyRhtythm : MonoBehaviour
{	   	
	public float sunRadius = 10.0f;
	public Vector3 sunPosition = new Vector3();
	private Vector3 sunOrigin = new Vector3();
	private int syncTick = 0;
	private float sunAngle = 0;
	

	void Start()
	{
		sunOrigin = transform.localPosition;	  

	}

	private void Update()
	{			 
		if (syncTick >= Time.timeScale)
		{
			if (sunAngle >= 360)
			{
				sunAngle = 0;
			}
			syncTick = 0;
			sunAngle += 1 * Time.timeScale * Time.deltaTime;
		}

		UpdateSunPosition();
		transform.LookAt(sunOrigin);
		syncTick++;
	}

	private void UpdateSunPosition()
	{
		sunPosition.x = sunOrigin.x + (sunRadius * (float)Math.Cos(Mathf.Deg2Rad * sunAngle));
		sunPosition.y = sunOrigin.y + (sunRadius * (float)Math.Sin(Mathf.Deg2Rad * sunAngle));
		transform.localPosition = sunPosition;
	}
}
