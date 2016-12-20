using UnityEngine;
using System.Collections;

namespace XboxCtrlrInput
{
	/// <summary>
	///     Script used by XCI to access a clone of the InputManager at run-time
	/// </summary>
	public class XciInputManagerReader : MonoBehaviour 
	{
		[SerializeField] private XciInputManagerClone inputManager;

		// Use this for initialization
		void Start () 
		{

		}

		public XciInputManagerClone InputManager
		{
			get
			{
				return this.inputManager;
			}
		}
	}
}
