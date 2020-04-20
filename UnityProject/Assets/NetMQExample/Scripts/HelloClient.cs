using UnityEngine;
using UnityEngine.UI;

public class HelloClient : MonoBehaviour
{
    private HelloRequester _helloRequester;

    [Tooltip("Text field to display the results of streaming.")]
    public Text ResultsField;

    private void Start()
    {
        _helloRequester = new HelloRequester();
        _helloRequester.ResultsField = ResultsField; 
        _helloRequester.Start();
    }

    private void OnDestroy()
    {
        _helloRequester.Stop();
    }
}