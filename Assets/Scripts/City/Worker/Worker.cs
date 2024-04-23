using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Worker: DraggableItem, InNeed, Helpful {
	public List<Need> _needs = new List<Need>();

	private HashSet<Help> _recievedHelps = new HashSet<Help>();

	[SerializeField]
	private int _hapinessLosingRateDelta;

	[SerializeField]
	private int _needFulfilledBonus;

	private float _timerDuration = 3f;

	private List<Help> _helps = new List<Help>();

	[SerializeField]
	private Area _area = Area.One;

	private int _slotIndex;

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

  public List<Help> helps {
		get => _helps;
		set => _helps = value;
	}

  public Area area {
		get => _area;
	}

	public float timerDuration {
		get => _timerDuration;
		set => _timerDuration = value;
	}

	public int slotIndex {
		get => _slotIndex;
		set => _slotIndex = value;
	}

	public bool isLifted {
		get => _isLifted;
		set => _isLifted = value;
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
		StopLosingHapiness();
		GameController.Instance.UpdateHapiness(needFulfilledBonus);
		UIController.Instance.OpenMenu(UIType.NeedFulfilledMenu);
		switch (need) {
		case Need.Water:
			helps.Add(Help.Water);
			break;
		case Need.Bread:
			helps.Add(Help.Bread);
			break;
		case Need.Sugar:
			helps.Add(Help.Sugar);
			break;
		case Need.Dissert:
			helps.Add(Help.Dissert);
			break;
		case Need.Necessity:
			helps.Add(Help.Necessity);
			break;
		case Need.Luxury:
			helps.Add(Help.Luxury);
			break;
		case Need.Library:
			helps.Add(Help.Library);
			break;
		case Need.Laboratory:
			helps.Add(Help.Laboratory);
			break;
		}
		

	}

	private IEnumerator Timer() {
		while (true) {
			var controller = GameController.Instance;
			controller.UpdateHapiness(-hapinessLosingRateDelta);
			yield return new WaitForSeconds(timerDuration);
		}
	}
}