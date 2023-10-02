using System.Collections.Generic;
using UnityEngine;

public class SceneGUI : MonoBehaviour
{
	private int toolbarSelection = 0;
	private System.String[] toolbarLabels = { "Parent objects", "UvMapping", "UV-Mapping-02", "House disaster", "Cut Test and debug" };

    [System.Obsolete]
    public void Awake()
	{
		//toolbarSelection = 0;
		toolbarSelection = Application.loadedLevel;
		//Application.LoadLevel(toolbarSelection);
	}

    [System.Obsolete]
    public void OnGUI()
	{
		toolbarSelection = GUI.Toolbar(new Rect(10, Screen.height - 30, Screen.width - 20, 20), toolbarSelection, toolbarLabels);
		
		if (GUI.changed)
		{
			Application.LoadLevel(toolbarSelection);
		}
	}
}