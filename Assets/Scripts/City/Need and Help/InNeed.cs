using System.Collections.Generic;
using UnityEngine;

public interface InNeed {
	public List<Need> needs { get; }

	public HashSet<Help> recievedHelps { get; set; }

	public int hapinessLosingRateDelta { get; }

	public int needFulfilledBonus { get; }

	public float timerDuration { get; set; }

	public bool isLifted { get; set; }

	public void RecieveHelps() {
		if (needs.Count == 0) {
			return;
		}
		var controller = GameController.Instance;
		if (controller == null) {
			return;
		}
		var draggable = this as DraggableItem;
    if (draggable == null) {
      return;
    }
		if (draggable.IsInSlot(out LandType landType, out int index)) {
			isLifted = false;
			foreach (var need in needs) {
				foreach (var combination in controller.helpsCombinations) {
					if (combination.need == need) {
						if (combination.requiredHelps.IsSubsetOf(recievedHelps)) {
							needs.Remove(need);
							OnNeedFulfilled(need);
						} 
					}
				}
			}
    } else {
			if (!isLifted) {
				recievedHelps.Clear();
				isLifted = true;
			}
		}
	}

	public void StartLosingHapiness();

	public void StopLosingHapiness();

	public void OnNeedFulfilled(Need need);

	public void AddNeed(Need need) {
		UIController.Instance.OpenMenu(UIType.NeedMenu);
		StartLosingHapiness();
		needs.Add(need);
	}
}