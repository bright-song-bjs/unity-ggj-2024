using TMPro;
using UnityEngine;

public class NeedFulfilledMenu: UIBase {
	public TextMeshProUGUI needTMP;

	public void SetNeed(Need need) {
		switch (need) {
		case Need.Water:
			needTMP.text = "The citizen now get the water";
			break;
		case Need.Bread:
			needTMP.text = "The citizen now can eat the bread";
			break;
		case Need.Sugar:
			needTMP.text = "Sugar";
			break;
		case Need.Dissert:
			needTMP.text = "Dissert";
			break;
		case Need.Necessity:
			needTMP.text = "Necessity";
			break;
		case Need.Library:
			needTMP.text = "Book";
			break;
		default:
			needTMP.text = "Not implemented yet";
			break;
		}
	}

	public void OnButtonDown_Close() {
		UIController.Instance.CloseCurrentMenu();
	}
}