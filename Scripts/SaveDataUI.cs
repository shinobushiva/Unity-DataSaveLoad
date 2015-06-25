using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Xml.Serialization;

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
			string folderPath = string.Format("{0}/{1}", Application.persistentDataPath , manager.folder);
			if (!Directory.Exists (folderPath)) {
				Directory.CreateDirectory(folderPath);
			}

			string filePath = string.Format("{0}/{1}", folderPath , fileName.text+".txt");
			if (!forseOverride && File.Exists (filePath)) {

				confirmDialogUI.Show ("The record already exists",
			                      "The record will be overridden. Do you really want to do it?",
			                      "Yes, I do", "No, I don't", 
			                      (b) => {
					if (b) {
						print ("overridden");
						WriteFile(filePath, data, data.GetType());
						gameObject.SetActive(false);
					} else {
						print ("wasn't saved"); 
					}
				});
			} else {
				WriteFile(filePath, data, data.GetType());
				gameObject.SetActive(false);
			}
		}

		private void WriteFile(string path, object obj, System.Type t){

			print (t);
			XmlSerializer ser = new XmlSerializer (t);

			//書き込むファイルを開く（UTF-8 BOM無し）
			StringBuilder sb = new StringBuilder ();
			StringWriter sw = new StringWriter(sb);
			ser.Serialize(sw, obj);

			print (path);
			File.WriteAllBytes(path, Encoding.UTF8.GetBytes(sw.ToString()));
		}


		public void Canceled(){
			data = null;
			gameObject.SetActive (false);
		}
	}
}
