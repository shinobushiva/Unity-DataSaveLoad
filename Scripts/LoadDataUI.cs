using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Text;
using System.Xml.Serialization;


namespace DataSaveLoad{
	public class LoadDataUI : MonoBehaviour {

		public RectTransform scrollContent;
		public DataEntry prefab;

		private DataSaveLoadMaster manager;
		public DataSaveLoadMaster Manager{
			get{
				if(!manager){
					manager = GameObject.FindObjectOfType<DataSaveLoadMaster> ();
					if (!manager) {
						print ("CameraSaveLoadManager is missing!");
						Destroy(this);
					}
				}
				return manager;
			}
		}
		private ConfirmDialogUI confirmDialogUI;

		public delegate void DataLoadHandler(object data);
		public event DataLoadHandler dataLoadHandler;

		private System.Type type;



		// Use this for initialization
		void Awake () {
			
			confirmDialogUI = GameObject.FindObjectOfType<ConfirmDialogUI> ();
		}

		public void UpdateView(){
			DataEntry[] cdes = scrollContent.GetComponentsInChildren<DataEntry> (true);
			foreach (DataEntry cde in cdes) {
				DestroyImmediate(cde.gameObject);
			}
			
			string folderPath = GetFolderPath();
			if (!Directory.Exists (folderPath)) {
				
			}
			string[] files = Directory.GetFiles (folderPath);
			
			foreach (string f in files) {
				print (f);
				DataEntry cde = GameObject.Instantiate(prefab) as DataEntry;
				cde.transform.SetParent(scrollContent.transform, false);
				cde.Set(new FileInfo(f));
				cde.loadDataUI = this;
			}
		}

		public string GetFolderPath(){
			return  string.Format("{0}/{1}", Application.persistentDataPath , Manager.folder);
		}

		public string GetFilePath(string fname){

			return  string.Format("{0}/{1}", GetFolderPath() , fname+".txt");

		}

		public void Load(FileInfo fi, System.Type t){
			string fn = fi.FullName;
			print (fn);
//			FileStream fs = new FileStream (fn, FileMode.Open, FileAccess.Read, FileShare.Read);
			StreamReader sr = new StreamReader(fn, new System.Text.UTF8Encoding(false));
			XmlSerializer ser = new XmlSerializer (t);
			object obj = ser.Deserialize (sr);
			sr.Close ();
			
			dataLoadHandler (obj);
		}

		public void Load(FileInfo fi){
			Load (fi, type);
		}


		public void ShowDialog(System.Type t){
			type = t;

			gameObject.SetActive (true);
			UpdateView ();
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}