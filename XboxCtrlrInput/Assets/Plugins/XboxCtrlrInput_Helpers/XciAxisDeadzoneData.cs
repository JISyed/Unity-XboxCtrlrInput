using UnityEngine;

namespace XboxCtrlrInput
{
	/// <summary>
	/// 	Contains deadzone data of every axis for every joystick number (see XboxController enum).
	/// 	Mostly for use with XInput (Windows API)
	/// </summary>
	public class XciAxisDeadzoneData
	{
		private float[] leftStickX = new float[5];
		private float[] leftStickY = new float[5];
		private float[] rightStickX = new float[5];
		private float[] rightStickY = new float[5];
		private float[] leftTrigger = new float[5];
		private float[] rightTrigger = new float[5];


		public void Init(XciInputManagerClone inputManager)
		{
			this.leftStickX[0] = inputManager.SearchInputByName("XboxAxisXJoy0").dead;
			this.leftStickX[1] = inputManager.SearchInputByName("XboxAxisXJoy1").dead;
			this.leftStickX[2] = inputManager.SearchInputByName("XboxAxisXJoy2").dead;
			this.leftStickX[3] = inputManager.SearchInputByName("XboxAxisXJoy3").dead;
			this.leftStickX[4] = inputManager.SearchInputByName("XboxAxisXJoy4").dead;

			this.leftStickY[0] = inputManager.SearchInputByName("XboxAxisYJoy0").dead;
			this.leftStickY[1] = inputManager.SearchInputByName("XboxAxisYJoy1").dead;
			this.leftStickY[2] = inputManager.SearchInputByName("XboxAxisYJoy2").dead;
			this.leftStickY[3] = inputManager.SearchInputByName("XboxAxisYJoy3").dead;
			this.leftStickY[4] = inputManager.SearchInputByName("XboxAxisYJoy4").dead;

			this.rightStickX[0] = inputManager.SearchInputByName("XboxAxis4Joy0").dead;
			this.rightStickX[1] = inputManager.SearchInputByName("XboxAxis4Joy1").dead;
			this.rightStickX[2] = inputManager.SearchInputByName("XboxAxis4Joy2").dead;
			this.rightStickX[3] = inputManager.SearchInputByName("XboxAxis4Joy3").dead;
			this.rightStickX[4] = inputManager.SearchInputByName("XboxAxis4Joy4").dead;

			this.rightStickY[0] = inputManager.SearchInputByName("XboxAxis5Joy0").dead;
			this.rightStickY[1] = inputManager.SearchInputByName("XboxAxis5Joy1").dead;
			this.rightStickY[2] = inputManager.SearchInputByName("XboxAxis5Joy2").dead;
			this.rightStickY[3] = inputManager.SearchInputByName("XboxAxis5Joy3").dead;
			this.rightStickY[4] = inputManager.SearchInputByName("XboxAxis5Joy4").dead;

			this.leftTrigger[0] = inputManager.SearchInputByName("XboxAxis3Joy0").dead;
			this.leftTrigger[1] = inputManager.SearchInputByName("XboxAxis3Joy1").dead;
			this.leftTrigger[2] = inputManager.SearchInputByName("XboxAxis3Joy2").dead;
			this.leftTrigger[3] = inputManager.SearchInputByName("XboxAxis3Joy3").dead;
			this.leftTrigger[4] = inputManager.SearchInputByName("XboxAxis3Joy4").dead;

			this.rightTrigger[0] = inputManager.SearchInputByName("XboxAxis3Joy0").dead;
			this.rightTrigger[1] = inputManager.SearchInputByName("XboxAxis3Joy1").dead;
			this.rightTrigger[2] = inputManager.SearchInputByName("XboxAxis3Joy2").dead;
			this.rightTrigger[3] = inputManager.SearchInputByName("XboxAxis3Joy3").dead;
			this.rightTrigger[4] = inputManager.SearchInputByName("XboxAxis3Joy4").dead;
		}


		public float[] LeftStickX
		{
			get
			{
				return this.leftStickX;
			}
		}

		public float[] LeftStickY
		{
			get
			{
				return this.leftStickY;
			}
		}

		public float[] RightStickX
		{
			get
			{
				return this.rightStickX;
			}
		}

		public float[] RightStickY
		{
			get
			{
				return this.rightStickY;
			}
		}

		public float[] LeftTrigger
		{
			get
			{
				return this.leftTrigger;
			}
		}

		public float[] RightTrigger
		{
			get
			{
				return this.rightTrigger;
			}
		}
	}
}
