using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Citizen: DraggableItem, InNeed {
	private List<Need> _needs = new List<Need>();

	private HashSet<Help> _recievedHelps = new HashSet<Help>();

	[SerializeField]
	private int _hapinessLosingRateDelta;

	[SerializeField]
	private int _needFulfilledBonus;

	private float _timerDuration = 3f;

	private bool _isLifted = true;

	private Coroutine timer;

	public List<Need> needs {
		get => _needs;
	}

	public HashSet<Help> recievedHelps {
		get => _recievedHelps;
		set => _recievedHelps = value;
	}

	public int hapinessLosingRateDelta {
		get => _hapinessLosingRateDelta;
	}

	public int needFulfilledBonus {
		get => _needFulfilledBonus;
	}

	public float timerDuration {
		get => _timerDuration;
		set => _timerDuration = value;
	}

	public bool isLifted {
		get => _isLifted;
		set => _isLifted = value;
	}

	private void OnDestroy() {
		StopLosingHapiness();
	}

	public void StartLosingHapiness() {
		if (timer != null) {
			StopCoroutine(timer);
		}
		timer = StartCoroutine(Timer());
	}

	public void StopLosingHapiness() {
		if (timer != null) {
			StopCoroutine(timer);
			timer = null;
		}
	}

	public void OnNeedFulfilled(Need need) {
		Debug.Log("a");
		StopLosingHapiness();
		var ui = UIController.Instance.OpenMenu(UIType.NeedFulfilledMenu);
		var needFilfilledMenu = ui as NeedFulfilledMenu;
		if (needFilfilledMenu != null) {
			needFilfilledMenu.SetNeed(need);
		}
		GameController.Instance.UpdateHapiness(needFulfilledBonus);
		Destroy(gameObject);
	}

	private IEnumerator Timer() {
		while (true) {
			var controller = GameController.Instance;
			controller.UpdateHapiness(-hapinessLosingRateDelta);
			yield return new WaitForSeconds(timerDuration);
		}
	}
}