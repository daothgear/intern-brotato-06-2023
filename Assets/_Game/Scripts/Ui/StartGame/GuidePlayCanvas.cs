using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidePlayCanvas : MonoBehaviour
{
	[SerializeField] private List<GameObject> guidePlayCanvases;
	[SerializeField] private GameObject buttonNext;
	[SerializeField] private GameObject buttonPrev;

	private int currentCanvasIndex = 0;

	private void Start()
	{
		ShowCanvas(currentCanvasIndex);
		UpdateButtonVisibility();
	}

	public void ClickButtonNext()
	{
		currentCanvasIndex++;
		ShowCanvas(currentCanvasIndex);
		UpdateButtonVisibility();
	}

	public void ClickButtonPrev()
	{
		currentCanvasIndex--;
		ShowCanvas(currentCanvasIndex);
		UpdateButtonVisibility();
	}

	private void ShowCanvas(int index)
	{
		for (int i = 0; i < guidePlayCanvases.Count; i++)
		{
			guidePlayCanvases[i].SetActive(i == index);
		}
	}

	private void UpdateButtonVisibility()
	{
		buttonNext.SetActive(currentCanvasIndex < guidePlayCanvases.Count - 1);
		buttonPrev.SetActive(currentCanvasIndex > 0);
	}
}
