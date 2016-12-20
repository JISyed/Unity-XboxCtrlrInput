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
		private static XciInputManagerReader instance = null;

		void Awake()
		{
			if(XciInputManagerReader.instance != null)
			{
				GameObject.Destroy(this.gameObject);
			}
			
			XciInputManagerReader.instance = this;

			// Load the InputManagerClone
			this.inputManager = Resources.Load<XciInputManagerClone>("XboxCtrlrInput/InputManagerClone");

			// Lives for the life of the game
			DontDestroyOnLoad(this.gameObject);
		}

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

		public static XciInputManagerReader Instance
		{
			get
			{
				if(XciInputManagerReader.instance == null)
				{
					GameObject xciInputManReaderObj = new GameObject("XboxCtrlrInput Input Manager Reader");
					xciInputManReaderObj.AddComponent<XciInputManagerReader>();
				}
				
				return XciInputManagerReader.instance;
			}
		}
	}
}
