using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
	public bool isBeingTouched;

	public List<Collider2D> colisionActual;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		colisionActual.Add(collision);
		GameManager.instance.timerRunning = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		colisionActual.Remove(collision);
		if (colisionActual.Count == 0)
		{
			GameManager.instance.timerRunning = false;
			GameManager.instance.dangerText.SetActive(value: false);
			GameManager.instance.endTimer = 5f;
		}
	}
}

