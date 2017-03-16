using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TutorialScript_DestoryedallCrates : MonoBehaviour {
    public Flowchart flowchart;
    // Use this for initialization
    public void Triggered()
    {
        int x = flowchart.GetIntegerVariable("CratesLeft");
        flowchart.SetIntegerVariable("CratesLeft", x-1);
    }
}
