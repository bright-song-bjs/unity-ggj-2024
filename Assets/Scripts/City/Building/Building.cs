using System.Collections.Generic;
using UnityEngine;

public class Building: DraggableItem, Helpful {
	[SerializeField]
	private List<Help> _helps = new List<Help>();

	[HideInInspector]
	private Area _area = Area.One;

	[HideInInspector]
	public Sprite normalSprite;
	
	public Sprite buildingSprite;

	public float buildingTime;
	
	private int _slotIndex;

	[HideInInspector]
	public bool isBuilt = false;

	protected override void Awake() {
		base.Awake();
		normalSprite = image.sprite;
	}

  public List<Help> helps {
		get => _helps;
		set => _helps = value;
	}

  public Area area {
		get => _area;
	}

	public int slotIndex {
		get => _slotIndex;
		set => _slotIndex = value;
	}
}