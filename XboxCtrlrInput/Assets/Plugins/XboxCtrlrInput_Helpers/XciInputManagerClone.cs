using UnityEngine;

namespace XboxCtrlrInput
{
	/// <summary>
	/// 	An entry of XciInputManagerClone
	/// </summary>
	/// <remarks>
	/// 	Credit to Leslie Young (http://plyoung.appspot.com/blog/manipulating-input-manager-in-script.html)
	/// </remarks>
	[System.Serializable]
	public class InputManagerEntry
	{
		public enum Type
		{
			KeyOrMouseButton = 0,
			MouseMovement = 1,
			JoystickAxis = 2
		}

		[SerializeField] public string name;
		[SerializeField] public string descriptiveName;
		[SerializeField] public string descriptiveNegativeName;
		[SerializeField] public string negativeButton;
		[SerializeField] public string positiveButton;
		[SerializeField] public string altNegativeButton;
		[SerializeField] public string altPositiveButton;
		
		[SerializeField] public float gravity;
		[SerializeField] public float dead;
		[SerializeField] public float sensitivity;
		
		[SerializeField] public bool snap = false;
		[SerializeField] public bool invert = false;
		
		[SerializeField] public InputManagerEntry.Type type;
		
		[SerializeField] public int axis;
		[SerializeField] public int joyNum;
	}


	/// <summary>
	/// 	A clone of the Unity Input Manager for reading axis data
	/// </summary>
	[System.Serializable]
	public class XciInputManagerClone : ScriptableObject 
	{
		[SerializeField] private InputManagerEntry[] inputManagerEntries;

		public int NumberOfEntries
		{
			get
			{
				if(this.inputManagerEntries == null)
				{
					return -1;
				}

				return this.inputManagerEntries.Length;
			}
		}

		public InputManagerEntry this[int index]
		{
			get
			{
				return this.inputManagerEntries[index];
			}
		}

		/// <summary>
		/// 	Searchs by the name of the input.
		/// </summary>
		public InputManagerEntry SearchInputByName(string entryName)
		{
			InputManagerEntry foundEntry = null;

			InputManagerEntry currentEntry = null;
			int numEntries = this.NumberOfEntries;
			for(int i = 0; i < numEntries; i++)
			{
				currentEntry = this.inputManagerEntries[i];

				if(currentEntry.name.Equals(entryName))
				{
					foundEntry = currentEntry;
					break;
				}
			}

			return foundEntry;
		}

		/// <summary>
		/// 	WARNING: Clears entire Input Manager Clone
		/// </summary>
		public void Alloc(int numberOfEntries)
		{
			this.inputManagerEntries = new InputManagerEntry[numberOfEntries];

			for(int i = 0; i < numberOfEntries; i++)
			{
				this.inputManagerEntries[i] = new InputManagerEntry();
			}
		}
	}
}
