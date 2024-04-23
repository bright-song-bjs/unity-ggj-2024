using UnityEngine;

public class UIBase: MonoBehaviour {
	public UIType type;

	public void Show() {
		gameObject.SetActive(true);
	}

	public void Hide() {
		gameObject.SetActive(false);
	}

	public void Destroy() {
		Destroy(gameObject);
	}
}