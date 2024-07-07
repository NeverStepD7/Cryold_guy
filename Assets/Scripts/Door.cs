using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TrapActivator
{
    public GameObject[] doorParts;
    void SetChildActive(bool open)
	{
        isOpen = open;
        foreach (var item in doorParts)
            item.SetActive(open);
	}
    public bool isOpen;
	public override void SwitchPress()
	{
        isOpen = !isOpen;
        SetChildActive(isOpen);
    }
    public void Door_Open() => SetChildActive(false);
    public void Door_Close() => SetChildActive(true);
    void Start()
    {
        SetChildActive(!isOpen);
    }
}
