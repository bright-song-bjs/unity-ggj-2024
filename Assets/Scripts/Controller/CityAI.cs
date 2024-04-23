using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityAI: MonoBehaviour {
	public static CityAI Instance { get; private set; }

	public List<Slot> slots = new List<Slot>();

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
	}

	private void Start() {
		//StartCoroutine(StartInstantiatingCitizens());
	}

	private IEnumerator StartInstantiatingCitizens() {
		var needs = new List<Need> {
			Need.Water, Need.Bread, Need.Library, Need.Necessity
		};

			var controller = GameController.Instance;
			var citizens = controller.citizens;
			var citizenPrefab = citizens[Random.Range(0, citizens.Count)];
			var cityGrid = controller.cityGrid.transform;
			Citizen citizen = null;
			// var obj = cityGrid.GetChild(Random.Range(0, cityGrid.childCount));
				// var slot = obj.GetComponent<Slot>();
				
				foreach (var i in slots) {
					Debug.Log(i);
					var duration = Random.Range(0f, 4f);
					yield return new WaitForSeconds(duration);
					//var obj = cityGrid.GetChild(i);
					//var slot = obj.GetComponent<Slot>();
					if (citizenPrefab.requiredLandTypes.Contains(i.landType)) {
						citizen = Instantiate(citizenPrefab, i.transform);
						var need = needs[Random.Range(0, needs.Count)];
						citizen.GetComponent<InNeed>().AddNeed(need);
					}
					yield return new WaitForSeconds(5f);
					Destroy(citizen);
				}
	}
}