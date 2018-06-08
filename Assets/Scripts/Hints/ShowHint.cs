using UnityEngine;
using UnityEngine.UI;

public class ShowHint : MonoBehaviour {

    public Text question;
    public string questionText = "";
    private bool showtext;

	void Start () {
        question.GetComponent<Text>();
	}
	
	void Update () {
        if (showtext)
        {
            question.text = questionText;
        }
        else
        {
            question.text = "";
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            showtext = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            showtext = false;
        }
    }
}
