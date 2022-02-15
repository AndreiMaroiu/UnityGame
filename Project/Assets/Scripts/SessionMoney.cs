using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionMoney : MonoBehaviour {

    public Text text;
    
	void Start ()
    {
        text.text = "+"  + PlayerPrefs.GetInt("SessionMoney"); 	
	}

}
