using TMPro;
using UnityEngine;

public class NeedMenu: UIBase {
    public TextMeshProUGUI needTMP;

    public void SetNeed(Need need) {
        if (need == Need.Water) {
            needTMP.text = "Water";
        } else if (need == Need.Bread) {
            needTMP.text = "Bread";
        } else if (need == Need.Necessity) {
            needTMP.text = "Necessity";
        } else {
            needTMP.text = "???";
        }
    }

    public void OnButtonDown_Close() {
        UIController.Instance.CloseCurrentMenu();
    }
}
