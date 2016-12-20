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
		[SerializeField] public InputManagerEntry[] inputManagerEntries;
	}
}
