using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SugarLevelBar : MonoBehaviour
{

	public Slider SugarBar;
	public Color HighColor;
	public Color GoodColor;
	public Color LowColor;
	private Image _targetBar;
	private float SugarLevel;
	private float HighSugarLevel;
	private float LowSugarLevel;

	void OnDisable()
	{
		EventManager.StopListening<SugarChangedEvent>(ChangeSugarLevel);
	}

	// Use this for initialization
	void Start()
	{
		EventManager.StartListening<SugarChangedEvent>(ChangeSugarLevel);

		SugarBar.maxValue = GameManager.Instance.MaximumBloodSugarLevel;
		SugarBar.value = SugarLevel;
		if (SugarBar.fillRect != null) _targetBar =
			SugarBar.fillRect.GetComponent<Image>();

		HighSugarLevel = GameManager.Instance.MaximumBloodSugarLevel / 100 * 75;
		LowSugarLevel = GameManager.Instance.MaximumBloodSugarLevel / 100 * 25;

		MatchHPbarColor();
	}

	public void ChangeSugarLevel(SugarChangedEvent @event)
	{
		SugarLevel = @event.Value;
		SugarBar.value = SugarLevel;
		if (SugarLevel <= GameManager.Instance.MinimumBloodSugarLevel)
		{
			SugarLevel = GameManager.Instance.MinimumBloodSugarLevel;
			SugarBar.value = SugarLevel;
		}

		MatchHPbarColor();
	}

	void MatchHPbarColor()
	{
		var currentHealthPercentage = (SugarLevel * 100) / SugarBar.maxValue;
		if (currentHealthPercentage >= HighSugarLevel)
		{
			_targetBar.color = HighColor;
		}
		else if (currentHealthPercentage < HighSugarLevel && currentHealthPercentage >= LowSugarLevel)
		{
			_targetBar.color = GoodColor;
		}
		else if (currentHealthPercentage < LowSugarLevel)
		{
			_targetBar.color = LowColor;
		}
	}
}
