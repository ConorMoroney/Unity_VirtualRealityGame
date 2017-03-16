using UnityEngine;
using Fungus;

public class TutorialScript_Checkpoint : MonoBehaviour
{

    public Flowchart flowchart;
    public void Triggered()
    {

        flowchart.SetBooleanVariable("CheckpointUsed", true);
        flowchart.ExecuteBlock("checkpoint");
    }
}