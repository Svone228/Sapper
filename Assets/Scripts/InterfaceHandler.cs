using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InterfaceHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Text minesText;
    int minesCount;
    public string defaultString;
    void Awake()
    {
        minesText = ObjectManager.GetMinesPanel().transform.GetChild(0).GetComponent<Text>();
        
    }
    public void SetMinesCount(int minesCount) 
    {
        this.minesCount = minesCount;
        minesText.text = defaultString + minesCount;
    }
    public void MinesCountIncrease() 
    {
        minesText.text = defaultString + ++minesCount;
    }
    public void MinesCountReduce()
    {
        minesText.text = defaultString + --minesCount;
    }
    //public void 
}
