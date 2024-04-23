using System.Collections.Generic;

class Stack<Element> where Element: class {
	private List<Element> elements;

	public Stack() {
		elements = new List<Element>();
	}

	public bool IsEmpty {
		get => elements.Count == 0;
	}
	
	public void Push(Element element) {
		elements.Add(element);
	}

	public Element Pop() {
		if (elements.Count == 0) {
			return null;
		} else {
			var lastIndex = elements.Count - 1;
			var lastElement = elements[lastIndex];
			elements.RemoveAt(lastIndex);
			return lastElement;
		}
	}

	public Element Peek() {
		if (elements.Count == 0) {
			return null;
		} else {
			return elements[elements.Count - 1];
		}
	}
}