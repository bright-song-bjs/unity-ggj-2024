using System.Collections.Generic;
using UnityEngine;

public interface Helpful {
  public List<Help> helps { get; set; }

  public Area area { get; }

  public int slotIndex { get; set; }

  public void ProvideHelps() {
    if (helps.Count == 0) {
      return;
    }
    var draggable = this as DraggableItem;
    if (draggable == null) {
      return;
    }
    if (draggable.IsInSlot(out var landType, out var index)) {
      slotIndex = index;
      AddOrRemoveHelpFromNeeds(true, index);
    } else {
      if (slotIndex != -1) {
        AddOrRemoveHelpFromNeeds(false, slotIndex);
        slotIndex = -1;
      }
    }
  }

  private void AddOrRemoveHelpFromNeeds(bool shouldAdd, int index) {
    var controller = GameController.Instance;
    if (controller == null) {
      return;
    }
    var items = controller.cityGrid.GetDraggableItems(index, area);
    foreach (var item in items) {
      var inNeedItem = item as InNeed;
      if (inNeedItem != null) {
        if (item.IsInSlot(out var landType, out index)) {
          if (shouldAdd) {
            foreach (var help in helps) {
              inNeedItem.recievedHelps.Add(help);
            }
          } else {
            foreach (var help in helps) {
              inNeedItem.recievedHelps.Remove(help);
            }
          }
        }
      }
    }
  }
}