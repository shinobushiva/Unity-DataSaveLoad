using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;


namespace DataSaveLoad {
	public class SaveDataUI : MonoBehaviour {

		public InputField fileName;

		public DataSaveLoadMaster manager;
		private ConfirmDialogUI confirmDialogUI;

		public object data;

		// Use this for initialization
		void Awake () {

			if (!manager) {
				print ("CameraSaveLoadManager is missing!");
				Destroy(this);
			}
			confirmDialogUI = GameObject.FindObjectOfType<ConfirmDialogUI> ();
		}

		// Update is called once per frame
		void Update () {
		
		}

		public void ShowDialog(object o){
			data = o;
			gameObject.SetActive (true);
		}

		public void Approved(){
			Approved (false);
		}

		public void Approved(bool forseOverride){

			//Application.persistentDataPath
			string folderPath = manager.GetFolderPath ();
			if (!Directory.Exists (folderPath)) {
				Directory.CreateDirectory(folderPath);
			}

			string filePath = manager.GetFilePath(fileName.text+".txt");
			if (!forseOverride && File.Exists (filePath)) {

				confirmDialogUI.Show ("The record already exists",
			                      "The record will be overridden. Do you really want to do it?",
			                      "Yes, I do", "No, I don't", 
			                      (b) => {
					if (b) {
						print ("overridden");
						manager.WriteFile(filePath, data, data.GetType());
						gameObject.SetActive(false);
					} else {
						print ("wasn't saved"); 
					}
				});
			} else {
				manager.WriteFile(filePath, data, data.GetType());
				gameObject.SetActive(false);
			}
		}


		public void Canceled(){
			data = null;
			gameObject.SetActive (false);
		}
	}
}
