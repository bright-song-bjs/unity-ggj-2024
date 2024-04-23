using UnityEngine;
using UnityEngine.UI;

public class HapinessBar: MonoBehaviour {
	[SerializeField]
	private Image currentHapinessLayer;

	[SerializeField]
	private Image transitionLayer;

	[SerializeField]
	private float transitionPerSecond;

	private float targetFill;

	private float currentFill = 1.0f;

	private void Update() {
		if (!Mathf.Approximately(currentFill, targetFill)) {
			currentFill = Mathf.MoveTowards(
				currentFill, 
				targetFill,
				transitionPerSecond * Time.deltaTime
			);
			transitionLayer.fillAmount = currentFill;
		}
	}

	public void SetHapinessPercentage(float percentage) {
		currentHapinessLayer.fillAmount = percentage;
		targetFill = percentage;
	}
}