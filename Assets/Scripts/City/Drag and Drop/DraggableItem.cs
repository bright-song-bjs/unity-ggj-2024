using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(Image))]
public class DraggableItem: 
	MonoBehaviour, 
	IBeginDragHandler, 
	IDragHandler, 
	IEndDragHandler 
{
	[SerializeField]
	private List<LandType> landsRequirement = new List<LandType>();

	[HideInInspector]
	public HashSet<LandType> requiredLandTypes = new HashSet<LandType>();

	[HideInInspector]
	public Transform parentAfterDrag;

	[HideInInspector]
	public Image image;

	private RectTransform rectTransform;

	public bool moveable = true;

	protected virtual void Awake() {
		foreach (var landType in landsRequirement) {
			requiredLandTypes.Add(landType);
		}
		landsRequirement = null;
		image = GetComponent<Image>();
		rectTransform = GetComponent<RectTransform>();
	}

	private void Update() {
		var inNeedItem = this as InNeed;
		if (inNeedItem != null) {
			inNeedItem.RecieveHelps();
		}

		var helpfulItem = this as Helpful;
		if (helpfulItem != null) {
			var building = helpfulItem as Building;
			if (building != null) {
				if (building.isBuilt) {
					helpfulItem.ProvideHelps();
				}
			} else {
				helpfulItem.ProvideHelps();
			}
		}
	}
	
	public void OnBeginDrag(PointerEventData eventData) {
		if (!moveable) {
			return;
		}
		parentAfterDrag = transform.parent;
		image.raycastTarget = false;
		transform.SetParent(UIController.Instance.worldCanvas);
		var localPosition = rectTransform.localPosition;
		localPosition.z = 5f;
		rectTransform.localPosition = localPosition;
	}

	public void OnDrag(PointerEventData eventData) {
		if (!moveable) {
			return;
		}
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			UIController.Instance.worldCanvas,
			eventData.position,
			Camera.main,
			out var localPosition
		);
		rectTransform.localPosition = localPosition;
	}

	public void OnEndDrag(PointerEventData eventData) {
		if (!moveable) {
			return;
		}
		transform.SetParent(parentAfterDrag);
		image.raycastTarget = true;
		var localPosition = rectTransform.localPosition;
		localPosition.z = 0f;
		rectTransform.localPosition = localPosition;
	}

	public bool IsInSlot(
		out LandType landType,
		out int index
	) {
		var slot = transform.parent.GetComponent<Slot>();
		if (slot != null) {
			landType = slot.landType;
			index = slot.transform.GetSiblingIndex();
			return true;
		} else {
			landType = LandType.None;
			index = -1;
			return false;
		}
	}
}
