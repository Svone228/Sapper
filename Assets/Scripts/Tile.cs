using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public FieldScript mainScript;
    public Vector2Int position;
    public Sprite Mine;
    public Sprite Flag;
    Button button;
    Text text;
    Image image;
    Sprite defaultImage;
    bool flagged;
    bool bomb = false;
    public bool opened = false;
    private void Awake()
    {
        Initialization();
    }
    void Initialization()
    {
        GetButton();
        SetButtonParameters();
        GetText();
        GetImage();
    }
    void Start()
    {

    }


    void GetButton()
    {
        button = transform.GetChild(0).GetComponent<Button>();
    }
    void SetButtonParameters()
    {
        button.gameObject.GetComponent<TileClickHandler>().SetTile(this);
    }
    void GetText()
    {
        text = transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }
    void GetImage()
    {
        image = GetComponent<Image>();
        defaultImage = image.sprite;
    }

    public void PressRightClick()
    {
        SwitchFlag();
        mainScript.WinCheck();

    }
    public void PressLeftClick()
    {
        if (!opened) 
        {
            mainScript.OpenTile(position);
            mainScript.WinCheck();
        }
        else
        {
            mainScript.OpenAround(this);
        }
        

    }
    public bool IsFlagged()
    {
        return flagged;
    }
    bool SwitchFlag()
    {
        if (flagged)
        {
            flagged = false;
            image.sprite = defaultImage;
            ObjectManager.GetInterfaceHandler().MinesCountIncrease();
        }
        else
        {
            flagged = true;
            image.sprite = Flag;
            ObjectManager.GetInterfaceHandler().MinesCountReduce();
        }
        return flagged;
    }
    public void SetBomb() 
    {
        bomb = true;
    }
    public bool IsPlanted() 
    {
        return bomb;
    }
    public void SetText(string a) 
    {
        text.text = a;
    }
    public void ShowBomb() 
    {
        image.sprite = Mine;
    }
    public void OpenTile(int countMines = 0) 
    {
        opened = true;
        if(countMines == 0) 
        {
            
        }
        else 
        {
            text.text = countMines.ToString();
        }
        SetWhite();
    }
    void SetWhite() 
    {
        image.color = Color.white;
    }
}
