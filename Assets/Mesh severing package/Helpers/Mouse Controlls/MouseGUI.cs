using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseForce), typeof(MouseSever), typeof(MouseShatter))]
public class MouseGUI : MonoBehaviour
{
	public int defaultSelection = 0;
	private int toolbarSelection = 0;
	
	
	private System.String[] toolbarLabels = { "Drag (Click and drag)", "Sever (Click and drag, release)", "Break (Click)" };
	
	private MouseShatter mouseShatter;
	private MouseForce mouseForce;
	private MouseSever mouseSplit;
	
	
	public void Awake()
	{
		toolbarSelection = defaultSelection;
		
		mouseSplit = GetComponent<MouseSever>();
		mouseForce = GetComponent<MouseForce>();
		mouseShatter = GetComponent<MouseShatter>();
		
		
		SelectTool();
	}
	
	public void OnGUI()
	{
		toolbarSelection = GUI.Toolbar(new Rect(10, 10, Screen.width - 20, 20), toolbarSelection, toolbarLabels);
		
		if (GUI.changed)
		{
			SelectTool();
		}
	}
	
	private void SelectTool()
	{
		mouseShatter.enabled = false;
		mouseSplit.enabled = false;
		mouseForce.enabled = false;
		
		if (toolbarSelection == 0)
		{
			mouseForce.enabled = true;
		}
		else if (toolbarSelection == 1)
		{
			mouseSplit.enabled = true;
		}
		else if (toolbarSelection == 2)
		{
			mouseShatter.enabled = true;
		}
	}
}