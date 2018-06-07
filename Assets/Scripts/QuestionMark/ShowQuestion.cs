using UnityEngine;
using UnityEngine.UI;

public class ShowQuestion : MonoBehaviour
{
    public Text question;
    public string questionText = "";
    private bool showtext = false;

    void Start()
    {
        question.GetComponent<Text>();
    }

    void OnTriggerStay(Collider collider)
    {
        if(collider.tag == "Player")
        {
            showtext = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player")
        {
            showtext = false;
        }
    }

    void Update()
    {
        if(showtext)
        {
            question.text = questionText;
        }
        else
        {
            question.text = "";
        }
    }
}
