using UnityEngine;
using UnityEngine.UI;

public class ShowQuestion : MonoBehaviour
{
    public Text question;
    public string questionText = "";
    //public float time;    

    void Start()
    {
        question = GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetButton("Help"))
        {
            question.text = questionText;
        }
        if (Input.GetButton("Cancel"))
        {
            question.text = "";
        }
    }
}
