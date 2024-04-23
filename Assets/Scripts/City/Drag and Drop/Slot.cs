using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot: MonoBehaviour, IDropHandler {
	public LandType landType;

	public void OnDrop(PointerEventData eventData) {
		var draggable = eventData.pointerDrag.GetComponent<DraggableItem>();
		if (transform.childCount > 0) {
			if (landType == LandType.Water) {
				var citizen = draggable as Citizen;
				if (citizen != null) {
					Destroy(citizen);
				}
			} else {
				return;
			}
		}
		if (!draggable.requiredLandTypes.Contains(landType)) {
			return;
		}
		draggable.parentAfterDrag = transform;
		
		// buildings become not moveable
		var building = draggable as Building;
		if (building != null) {
			
				building.moveable = false;
				if (!building.isBuilt) {
					StartCoroutine(Build(building));
				}
		}
	}

	private IEnumerator Build(Building building) {
		building.isBuilt = false;
		building.image.sprite = building.buildingSprite;
		yield return new WaitForSeconds(building.buildingTime);
		building.isBuilt = true;
		building.image.sprite = building.normalSprite;
	}
}