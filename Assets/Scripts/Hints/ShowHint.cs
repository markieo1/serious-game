using UnityEngine;
using UnityEngine.UI;

public class ShowHint : MonoBehaviour {

    public Text question;
    public string questionText = "";

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            question.text = questionText;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            question.text = "";
        }
    }
}
