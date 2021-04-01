using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelArrow : ButtonScript
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClick()
    {
        base.OnClick();
        Debug.Log("click");
    }

    //basis voor alle knoppen is klikken maar bij de travelarrow heb je ook nog pop up + de rest en omdat dit op alleen de arrow komt hoef je niet te checken op welke button je klikt
}
