using UnityEngine;
using UnityEditor;

namespace XboxCtrlrInput.Editor
{
	/// <summary>
	/// 	Clones Unity's Input Manager into a new file at /Assets/Plugins/InputManagerClone.asset
	/// </summary>
	/// <remarks>
	/// 	Do NOT modify!!!
	/// 	Credit to Leslie Young (http://plyoung.appspot.com/blog/manipulating-input-manager-in-script.html)
	/// </remarks>
	public static class InputManagerCloner 
	{
		/// <summary>
		/// 	Runs through all the child objects and return the one that matches the given name
		/// </summary>
		private static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
		{
			SerializedProperty child = parent.Copy();
			child.Next(true);

			do
			{
				if (child.name == name) return child;
			}
			while (child.Next(false));

			return null;
		}


		[MenuItem("Window/XboxCtrlrInput/Clone Input Manager")]
		public static void CloneInputManager()
		{
			// Make the new blank instance for the InputManager Clone
			XciInputManagerClone inputManagerClone = ScriptableObject.CreateInstance<XciInputManagerClone>();

			// Retrieve the data of the original InputManager
			SerializedObject originalInputManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
			SerializedProperty originalInputEntriesCollection = originalInputManager.FindProperty("m_Axes");

			// Find the number of entries in the InputManager and allocate space in the clone
			int NumOfEntries = originalInputEntriesCollection.arraySize;
			inputManagerClone.Alloc(NumOfEntries);

			// Loop through every input entry and copy them into the clone
			InputManagerEntry currentEntry = null;
			SerializedProperty originalEntry = null;
			for(int i = 0; i < NumOfEntries; ++i)
			{
				originalEntry = originalInputEntriesCollection.GetArrayElementAtIndex(i);
				currentEntry = inputManagerClone[i];


				currentEntry.name = InputManagerCloner.GetChildProperty(originalEntry, "m_Name").stringValue;
				currentEntry.descriptiveName = InputManagerCloner.GetChildProperty(originalEntry, "descriptiveName").stringValue;
				currentEntry.descriptiveNegativeName = InputManagerCloner.GetChildProperty(originalEntry, "descriptiveNegativeName").stringValue;
				currentEntry.negativeButton = InputManagerCloner.GetChildProperty(originalEntry, "negativeButton").stringValue;
				currentEntry.positiveButton = InputManagerCloner.GetChildProperty(originalEntry, "positiveButton").stringValue;
				currentEntry.altNegativeButton = InputManagerCloner.GetChildProperty(originalEntry, "altNegativeButton").stringValue;
				currentEntry.altPositiveButton = InputManagerCloner.GetChildProperty(originalEntry, "altPositiveButton").stringValue;
				currentEntry.gravity = InputManagerCloner.GetChildProperty(originalEntry, "gravity").floatValue;
				currentEntry.dead = InputManagerCloner.GetChildProperty(originalEntry, "dead").floatValue;
				currentEntry.sensitivity = InputManagerCloner.GetChildProperty(originalEntry, "sensitivity").floatValue;
				currentEntry.snap = InputManagerCloner.GetChildProperty(originalEntry, "snap").boolValue;
				currentEntry.invert = InputManagerCloner.GetChildProperty(originalEntry, "invert").boolValue;
				currentEntry.type = (InputManagerEntry.Type) InputManagerCloner.GetChildProperty(originalEntry, "type").intValue;
				currentEntry.axis = InputManagerCloner.GetChildProperty(originalEntry, "axis").intValue;
				currentEntry.joyNum = InputManagerCloner.GetChildProperty(originalEntry, "joyNum").intValue;
			}


			// Now save the Input Manager clone to file

			// Hard-coded path (always replaces what was originally there) (Do NOT change!)
			string finalAssetPath = "Assets/Resources/XboxCtrlrInput/InputManagerClone.asset";

			// Create a new data asset into a file on the chosen path
			AssetDatabase.CreateAsset(inputManagerClone, finalAssetPath);
			
			// Save and Focus
			AssetDatabase.SaveAssets();
		}
	}
}
