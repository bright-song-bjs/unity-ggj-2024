using System.Collections.Generic;
using UnityEngine;

public class CityGrid: MonoBehaviour {
	public int columnCount;

	public int rowCount;

	public List<DraggableItem> GetDraggableItems(
		int centerIndex,
		Area area
	) {
		var indices = new List<int>();
		var i = centerIndex / rowCount;
		var j = centerIndex % columnCount;
		var maxRow = rowCount - 1;
		var maxColumn = columnCount - 1;
		if (0 < i && i < maxRow && 0 < j && j < maxColumn) {
			switch (area) {
			case Area.One:
				indices.Add(centerIndex - 1);
				indices.Add(centerIndex + 1);
				var up = centerIndex - columnCount;
				indices.Add(up - 1);
				indices.Add(up);
				indices.Add(up + 1);
				var down = centerIndex + columnCount;
				indices.Add(down - 1);
				indices.Add(down);
				indices.Add(down + 1);
				break;
			}
		} else if (i == 0 && j == 0) {
			switch (area) {
			case Area.One:
				indices.Add(centerIndex + 1);
				var down = centerIndex + columnCount;
				indices.Add(down);
				indices.Add(down + 1);
				break;
			}
		} else if (i == 0 && 0 < j && j < maxColumn) {
			switch (area) {
			case Area.One:
				indices.Add(centerIndex - 1);
				indices.Add(centerIndex + 1);
				var down = centerIndex + columnCount;
				indices.Add(down - 1);
				indices.Add(down);
				indices.Add(down + 1);
				break;
			}
		} else if (i == 0 && j == maxColumn) {
			switch (area) {
			case Area.One:
				indices.Add(centerIndex - 1);
				var down = centerIndex + columnCount;
				indices.Add(down - 1);
				indices.Add(down);
				break;
			}
		} else if (j == 0 && 0 < i && i < maxRow) {
			switch (area) {
			case Area.One:
				indices.Add(centerIndex + 1);
				var up = centerIndex - columnCount;
				indices.Add(up);
				indices.Add(up + 1);
				var down = centerIndex + columnCount;
				indices.Add(down);
				indices.Add(down + 1);
				break;
			}
		} else if (j == maxColumn && 0 < i && i < maxRow) {
			switch (area) {
			case Area.One:
				indices.Add(centerIndex - 1);
				var up = centerIndex - columnCount;
				indices.Add(up - 1);
				indices.Add(up);
				var down = centerIndex + columnCount;
				indices.Add(down - 1);
				indices.Add(down);
				break;
			}
		} else if (i == maxRow && j == 0) {
			switch (area) {
			case Area.One:
				indices.Add(centerIndex + 1);
				var up = centerIndex - columnCount;
				indices.Add(up);
				indices.Add(up + 1);
				break;
			}
		} else if (i == maxRow && 0 < j && j < maxColumn) {
			switch (area) {
			case Area.One:
				indices.Add(centerIndex - 1);
				indices.Add(centerIndex + 1);
				var up = centerIndex - columnCount;
				indices.Add(up - 1);
				indices.Add(up);
				indices.Add(up + 1);
				break;
			}
		} else if (i == maxRow && j == maxColumn) {
			switch (area) {
			case Area.One:
				indices.Add(centerIndex - 1);
				var up = centerIndex - columnCount;
				indices.Add(up - 1);
				indices.Add(up);
				break;
			}
		}

		var draggableItems = new List<DraggableItem>();
		foreach (var index in indices) {
			var slot = transform.GetChild(index);
			if (slot.childCount > 0) {
				var draggable = slot.GetChild(0).GetComponent<DraggableItem>();
				if (draggable != null) {
					draggableItems.Add(draggable);
				}
			}
		}
		return draggableItems;
	}
}
