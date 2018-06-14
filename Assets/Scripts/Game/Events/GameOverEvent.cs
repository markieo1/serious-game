using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEvent : EventBase
{
	public SugarLevelInstigator Instigator { get; private set; }

	public GameOverEvent() : this(SugarLevelInstigator.UNKNOWN)
	{

	}

	public GameOverEvent(SugarLevelInstigator instigator) : base()
	{
		this.Instigator = instigator;
	}
}
