using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController: MonoBehaviour {
	public static GameController Instance { get; private set; }

	public CityGrid cityGrid;

	public List<HelpsCombination> helpsCombinations;

	public List<Citizen> citizens;

	public int initialHapiness;

	public int fatalHapiness;

	public int victoryHapiness;

	public int maxHapiness;

	[HideInInspector]
	public int currentHapiness;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}

		foreach (var combination in helpsCombinations) {
			var list = combination.helps;
			var set = new HashSet<Help>();
			foreach (var help in list) {
				set.Add(help);
			}
			combination.requiredHelps = set;
			combination.helps = null;
		}
	}

	private void Start() {
		SetHapiness(initialHapiness);
	}

	public void SetHapiness(int hapiness) {
		if (hapiness < fatalHapiness) {
			PlayerDead();
		} else if (hapiness > victoryHapiness) {
			PlayerVictory();
		} else {
			currentHapiness = hapiness;
			var percentage = (float)currentHapiness / (float)maxHapiness;
			UIController.Instance.hapinessBar.SetHapinessPercentage(percentage);
		}
	}

	public void UpdateHapiness(int delta) {
		Debug.Log(delta);
		Debug.Log(currentHapiness);
		var hapiness = currentHapiness + delta;
		SetHapiness(hapiness);
	}

	public void PlayerDead() {
		StopAllCoroutines();
		UIController.Instance.OpenMenu(UIType.DeathMenu);
	}

	public void PlayerVictory() {
		StopAllCoroutines();
		UIController.Instance.OpenMenu(UIType.VictoryMenu);
	}
}