﻿using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class DailyRhtythm : MonoBehaviour
{
	[SerializeField]
	[Range(0, 24)]
	int hour;

	[SerializeField]
	[Range(0, 60)]
	int minutes;

	DateTime time;
	private new Light light;

	private void Start()
	{
		light = GetComponent<Light>();
		TimeSpan currentTime = GameManager.Instance.GetTime();
		time = DateTime.Today + currentTime;
		hour = currentTime.Hours;
		minutes = currentTime.Minutes;
	}

	private void OnValidate()
	{
		time = time.Date + new TimeSpan(hour, minutes, 0);
	}

	private void Update()
	{
		time = time.Date + GameManager.Instance.GetTime();
		SetPosition();
	}

	void SetPosition()
	{
		Vector3 angles = new Vector3();
		double alt;
		double azi;
		SunPosition.CalculateSunPosition(time, out azi, out alt);
		angles.x = (float)alt * Mathf.Rad2Deg;
		angles.y = (float)azi * Mathf.Rad2Deg;
		transform.localRotation = Quaternion.Euler(angles);
		light.intensity = Mathf.InverseLerp(-12, 0, angles.x);
	}
}

public static class SunPosition
{
	private const double Deg2Rad = Math.PI / 180.0;
	private const double Rad2Deg = 180.0 / Math.PI;

	public static void CalculateSunPosition(
		DateTime dateTime, out double outAzimuth, out double outAltitude)
	{
		// Convert to UTC  
		dateTime = dateTime.ToUniversalTime();

		// Number of days from J2000.0.  
		double julianDate = 367 * dateTime.Year -
			(int)((7.0 / 4.0) * (dateTime.Year +
			(int)((dateTime.Month + 9.0) / 12.0))) +
			(int)((275.0 * dateTime.Month) / 9.0) +
			dateTime.Day - 730531.5;

		double julianCenturies = julianDate / 36525.0;

		// Sidereal Time  
		double siderealTimeHours = 6.6974 + 2400.0513 * julianCenturies;

		double siderealTimeUT = siderealTimeHours +
			(366.2422 / 365.2422) * (double)dateTime.TimeOfDay.TotalHours;

		double siderealTime = siderealTimeUT * 15;

		// Refine to number of days (fractional) to specific time.  
		julianDate += (double)dateTime.TimeOfDay.TotalHours / 24.0;
		julianCenturies = julianDate / 36525.0;

		// Solar Coordinates  
		double meanLongitude = CorrectAngle(Deg2Rad *
			(280.466 + 36000.77 * julianCenturies));

		double meanAnomaly = CorrectAngle(Deg2Rad *
			(357.529 + 35999.05 * julianCenturies));

		double equationOfCenter = Deg2Rad * ((1.915 - 0.005 * julianCenturies) *
			Math.Sin(meanAnomaly) + 0.02 * Math.Sin(2 * meanAnomaly));

		double elipticalLongitude =
			CorrectAngle(meanLongitude + equationOfCenter);

		double obliquity = (23.439 - 0.013 * julianCenturies) * Deg2Rad;

		// Right Ascension  
		double rightAscension = Math.Atan2(
			Math.Cos(obliquity) * Math.Sin(elipticalLongitude),
			Math.Cos(elipticalLongitude));

		double declination = Math.Asin(
			Math.Sin(rightAscension) * Math.Sin(obliquity));

		// Horizontal Coordinates  
		double hourAngle = CorrectAngle(siderealTime * Deg2Rad) - rightAscension;

		if (hourAngle > Math.PI)
		{
			hourAngle -= 2 * Math.PI;
		}

		double altitude = Math.Asin(Math.Sin(0 * Deg2Rad) *
			Math.Sin(declination) + Math.Cos(0 * Deg2Rad) *
			Math.Cos(declination) * Math.Cos(hourAngle));

		// Nominator and denominator for calculating Azimuth  
		// angle. Needed to test which quadrant the angle is in.  
		double aziNom = -Math.Sin(hourAngle);
		double aziDenom =
			Math.Tan(declination) * Math.Cos(0 * Deg2Rad) -
			Math.Sin(0 * Deg2Rad) * Math.Cos(hourAngle);

		double azimuth = Math.Atan(aziNom / aziDenom);

		if (aziDenom < 0) // In 2nd or 3rd quadrant  
		{
			azimuth += Math.PI;
		}
		else if (aziNom < 0) // In 4th quadrant  
		{
			azimuth += 2 * Math.PI;
		}

		outAltitude = altitude;
		outAzimuth = azimuth;
	}

	private static double CorrectAngle(double angleInRadians)
	{
		if (angleInRadians < 0)
		{
			return 2 * Math.PI - (Math.Abs(angleInRadians) % (2 * Math.PI));
		}
		else if (angleInRadians > 2 * Math.PI)
		{
			return angleInRadians % (2 * Math.PI);
		}
		else
		{
			return angleInRadians;
		}
	}
}
