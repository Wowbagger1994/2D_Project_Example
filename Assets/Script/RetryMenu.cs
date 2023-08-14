using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = $"Score: {StaticProperty.score}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
