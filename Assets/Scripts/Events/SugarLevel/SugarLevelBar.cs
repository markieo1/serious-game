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

	private Image _targetBar;

	//private Action<EventParam> sugarLevelListener;

	void Awake()
	{
		//sugarLevelListener = new UnityAction<object>(ChangeSugarLevel);
	}

	void OnEnable()
	{
		//EventManager.StartListening("SugarLevelChanged", ChangeSugarLevel);
		EventManager.StartListening("SugarLevelChanged", ChangeSugarLevel);
	}

	void OnDisable()
	{
		//EventManager.StopListening("SugarLevelChanged", ChangeSugarLevel);
		EventManager.StopListening("SugarLevelChanged", ChangeSugarLevel);
	}

	// Use this for initialization
	void Start()
	{
		SugarBar.maxValue = MaxSugarLevel;
		SugarBar.value = SugarLevel;
		if (SugarBar.fillRect != null) _targetBar =
			SugarBar.fillRect.GetComponent<Image>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ChangeSugarLevel(Hashtable eventParams)
	{
		if (eventParams.ContainsKey("SugarLevel"))
		{
			SugarLevel += float.Parse(eventParams["SugarLevel"].ToString());
			SugarBar.value = SugarLevel;
			if (SugarLevel <= 0)
			{
				SugarLevel = 0;
				SugarBar.value = SugarLevel;
			}

			MatchHPbarColor();
		}
	}

	void MatchHPbarColor()
	{
		var currentHealthPercentage = (SugarLevel * 100) / SugarBar.maxValue;
		if (currentHealthPercentage >= 75)
		{
			_targetBar.color = Color.red;
		}
		else if (currentHealthPercentage < 75 && currentHealthPercentage >= 25)
		{
			_targetBar.color = Color.green;
		}
		else if (currentHealthPercentage < 25)
		{
			_targetBar.color = Color.blue;
		}
	}
}
