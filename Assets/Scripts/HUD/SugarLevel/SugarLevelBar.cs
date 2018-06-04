using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SugarLevelBar : MonoBehaviour
{

	public Slider SugarBar;
	public float MaxSugarLevel;
	public float SugarLevel;
	public Color HighColor;
	public Color GoodColor;
	public Color LowColor;
	private Image _targetBar;

	void OnDisable()
	{
		EventManager.StopListening<SugarChangedEvent>(ChangeSugarLevel);
	}

	// Use this for initialization
	void Start()
	{
		EventManager.StartListening<SugarChangedEvent>(ChangeSugarLevel);

		SugarBar.maxValue = MaxSugarLevel;
		SugarBar.value = SugarLevel;
		if (SugarBar.fillRect != null) _targetBar =
			SugarBar.fillRect.GetComponent<Image>();

		MatchHPbarColor();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ChangeSugarLevel(SugarChangedEvent @event)
	{
		SugarLevel = @event.Value;
		SugarBar.value = SugarLevel;
		if (SugarLevel <= 0)
		{
			SugarLevel = 0;
			SugarBar.value = SugarLevel;
		}

		MatchHPbarColor();
	}

	void MatchHPbarColor()
	{
		var currentHealthPercentage = (SugarLevel * 100) / SugarBar.maxValue;
		if (currentHealthPercentage >= 75)
		{
			_targetBar.color = HighColor;
		}
		else if (currentHealthPercentage < 75 && currentHealthPercentage >= 25)
		{
			_targetBar.color = GoodColor;
		}
		else if (currentHealthPercentage < 25)
		{
			_targetBar.color = LowColor;
		}
	}
}
