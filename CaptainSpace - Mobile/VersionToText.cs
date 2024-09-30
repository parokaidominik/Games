using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionToText : MonoBehaviour
{
    private void Awake() 
    {
        GetComponent<TextMeshProUGUI>().text = "Version: "+ Application.version;  
    }
}
