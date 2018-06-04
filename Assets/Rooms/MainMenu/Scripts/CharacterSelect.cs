using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
	public void SelectBoy()
	{
		CharacterData.CharacterSelection = CharacterSelection.MALE;
	}

	public void SelectGirl()
	{
		CharacterData.CharacterSelection = CharacterSelection.FEMALE;
	}
}
