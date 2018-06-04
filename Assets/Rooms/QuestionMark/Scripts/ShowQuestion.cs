using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowQuestion : MonoBehaviour {

    public Text question;
    public string questionText = "";
    public float time = 5;

    void Start()
    {
        Text txt = question.GetComponent<Text>();
    }

    void Update()
    {
        if(Input.GetButton("Help"))
        {
            
            DisplayText();
        }
    }

    void DisplayText()
    {
        question.text = questionText;
        //yield return new WaitForSeconds(time);
    }
}
