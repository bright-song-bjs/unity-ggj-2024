using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HelpsCombination {
	public List<Help> helps;

	public HashSet<Help> requiredHelps;

	public Need need;
}