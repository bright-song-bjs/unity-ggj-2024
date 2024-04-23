using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController: MonoBehaviour {
	public static UIController Instance { get; private set; }

	public RectTransform worldCanvas;

	public RectTransform screenCanvas;

	public HapinessBar hapinessBar;

	[SerializeField]
	private List<UIBase> uiPrefabs;

	private Dictionary<UIType, UIBase> uiPrefabByType = 
		new Dictionary<UIType, UIBase>();

	private Stack<UIBase> uiStack = new Stack<UIBase>();

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}

		foreach (var uiPrefab in uiPrefabs) {
			uiPrefabByType.Add(uiPrefab.type, uiPrefab);
		}
		uiPrefabs = null;
	}

	public UIBase OpenMenu(UIType uiType, bool hideCurrent = true) {
		var uiPrefab = uiPrefabByType[uiType];
		var uiBase = Instantiate(uiPrefab, screenCanvas, false);
		if (!uiStack.IsEmpty && hideCurrent) {
			uiStack.Peek().Hide();
		}
		uiStack.Push(uiBase);
		uiBase.Show();
		return uiBase;
	}

	public void CloseCurrentMenu() {
		if (uiStack.IsEmpty) {
			return;
		}
		var uiBase = uiStack.Pop();
		uiBase.Destroy();
		if (!uiStack.IsEmpty) {
			uiStack.Peek().Show();
		}
	}
}