using UnityEngine;
using Fungus;

public class TutrialScript_BlockPunched : MonoBehaviour {
    public Flowchart flowchart;
    public void Triggered()
    {
        flowchart.SetBooleanVariable("CrateDestroyed",true);
    }
}
