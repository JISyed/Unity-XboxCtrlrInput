using UnityEngine;
using UnityEditor;

using System;
using System.IO;
using System.Collections.Generic;

namespace XboxCtrlrInput.Editor {

	static class PackageExporter {

		static readonly string DefaultUnityPackageName = "XboxCtrlrInput"; // TODO: add auto incremental version

		static readonly string[] PackageIgnoredFiles = {
			"Assets/Editor/XboxCtrlrInput/PackageExporter.cs" // don't include package exporter script
		};

		[MenuItem("Window/XboxCtrlrInput/Export Unity Package...")]
		static void ExportUnityPackage() {

			string fileName = EditorUtility.SaveFilePanel("Save Unity Package", "Assets", DefaultUnityPackageName, "unitypackage");
			if (string.IsNullOrEmpty(fileName)) {
				return;
			}

			string[] assetPathNames = ListPackageFiles("Assets", PackageIgnoredFiles);
			Debug.Log("Total package assets: " + assetPathNames.Length);

			AssetDatabase.ExportPackage(assetPathNames, fileName);
			Debug.Log("Package written to '" + fileName + "'");
		}

		static string[] ListPackageFiles(string rootDir, params string[] ignoredFiles) {
			return ListFiles(rootDir, delegate(string path) {

				// should we ignore path?
				if (Array.IndexOf(ignoredFiles, path) != -1) {
					return false;
				}

				// skip meta files
				if (path.EndsWith(".meta")) {
					return false;
				}

				// the rest is OK
				return true;
			});
		}

		#region File Helpers

		private delegate bool FilenameFilter(string path);

		private static string[] ListFiles(string directory, FilenameFilter filter) {
			List<string> list = new List<string>();
			ListFiles(list, directory, filter);
			return list.ToArray();
		}
		
		private static void ListFiles(List<string> list, string directory, FilenameFilter filter) {
			if (filter(directory)) {
				list.Add(directory);
				
				string[] directories = Directory.GetDirectories(directory);
				foreach (string child in directories) {
					ListFiles(list, child, filter);
				}
				
				string[] files = Directory.GetFiles(directory);
				foreach (string file in files) {
					if (filter(file)) {
						list.Add(file);
					}
				}
			}
		}
		
		#endregion
	}
}
