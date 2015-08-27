using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

using System;
using System.IO;
using System.Collections;

namespace XboxCtrlrInput.Editor {
	static class InputManagerReplacer {
		[MenuItem("Window/XboxCtrlrInput/Replace InputManager.asset...")]
		static void ReplaceInputManagerAsset() {

			if (!EditorUtility.DisplayDialog("XboxCtrlrInput",
					"This will replace ProjectSettings/InputManager.asset (a backup file will be created)", "Continue", "Cancel")) {
				return;
			}

			DirectoryInfo assetsDirectory = new DirectoryInfo("Assets");
			if (!assetsDirectory.Exists) {
				Debug.LogError("Can't resolve 'Assets' directory");
				return;
			}

			string projectSettingsPath = Path.Combine(assetsDirectory.Parent.FullName, "ProjectSettings");
			if (!Directory.Exists(projectSettingsPath)) {
				Debug.LogError("Can't resolve 'ProjectSettings' directory");
				return;
			}

			string settingsFilename = EditorSettings.serializationMode == SerializationMode.ForceText ? 
				"InputManagerText": "InputManagerBinary";
			string settingsFile = Path.Combine("Assets/Editor/XboxCtrlrInput/InputManager Copies", settingsFilename);

			if (!File.Exists(settingsFile)) {
				Debug.LogError("Can't resolve '" + settingsFile + "' file");
				return;
			}

			string originalSettingsFile = Path.Combine(projectSettingsPath, "InputManager.asset");
			string backupSettingsFile = originalSettingsFile + ".bak";
			File.Copy(originalSettingsFile, backupSettingsFile);

			File.Copy(settingsFile, originalSettingsFile, true);
			Debug.Log("Backup file: " + backupSettingsFile);
		}
	}
}
