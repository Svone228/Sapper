using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectManager : MonoBehaviour
{
    public GameObject MinesPanel;
    public FieldScript MainField;
    public GameObject Difficulty;
    public InterfaceHandler InterfaceHandler;
    public Text Smile;
    static GameObject minesPanel;
    static FieldScript mainField;
    static GameObject difficulty;
    static InterfaceHandler interfaceHandler;
    static Text smile;
    void Awake()
    {
        minesPanel = MinesPanel;
        mainField = MainField;
        difficulty = Difficulty;
        interfaceHandler = InterfaceHandler;
        smile = Smile;
    }
    public static GameObject GetMinesPanel() 
    {
        return minesPanel;
    }
    public static FieldScript GetMainField()
    {
        return mainField;
    }
    public static GameObject GetDifficulty()
    {
        return difficulty;
    }
    public static InterfaceHandler GetInterfaceHandler()
    {
        return interfaceHandler;
    }
    public static Text GetSmile() 
    {
        return smile;
    }
    // Update is called once per frame
    
}
