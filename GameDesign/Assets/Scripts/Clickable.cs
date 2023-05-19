using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClickType
{
    LEFT_CLICK,
    RIGHT_CLICK
}

public interface Clickable
{
    public void Click(ClickType clickType);    
}
