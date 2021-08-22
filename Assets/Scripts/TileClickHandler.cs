using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileClickHandler : MonoBehaviour, IPointerClickHandler
{
    public Tile tile;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            tile?.PressLeftClick();
        else if (eventData.button == PointerEventData.InputButton.Right && !tile.opened)
            tile?.PressRightClick();
    }
    public void SetTile(Tile tile) 
    {
        this.tile = tile;
    }
}
