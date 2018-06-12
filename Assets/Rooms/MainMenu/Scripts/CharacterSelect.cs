using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject PlayerToPlay;
    public GameObject PlayerNoPlay;

	public void SelectBoy()
	{
		CharacterData.CharacterSelection = CharacterSelection.MALE;
        if (PlayerToPlay.gameObject.name == "BoyPlayer")
        {
            PlayerToPlay.gameObject.SetActive(true);
        }
        PlayerNoPlay.gameObject.SetActive(false);
    }

	public void SelectGirl()
	{
		CharacterData.CharacterSelection = CharacterSelection.FEMALE;
        if (PlayerToPlay.gameObject.name == "GirlPlayer")
        {
            PlayerToPlay.gameObject.SetActive(true);
        }
        PlayerNoPlay.gameObject.SetActive(false);
    }
}
