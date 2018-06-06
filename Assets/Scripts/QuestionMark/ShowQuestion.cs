using UnityEngine;
using UnityEngine.UI;

public class ShowQuestion : MonoBehaviour
{
    public Text question;
    public string questionText = "";
    public Transform target;
    public LayerMask layermask;

    void Start()
    {
        question = GetComponent<Text>();
    }

    void Update()
    {
        //Debug.Log(Physics.Linecast(transform.position, target.position, layermask.value));

        var MirrorObj = Physics.Linecast(transform.position, target.position, layermask.value);
        if(MirrorObj)
        {
            question.text = questionText;
        }
    }
}
