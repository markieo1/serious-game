using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInput : MonoBehaviour
{
	public InputField Input;
	public void ConfirmInput()
	{
		CharacterData.CharacterAge = int.Parse(Input.text);
	}
}
