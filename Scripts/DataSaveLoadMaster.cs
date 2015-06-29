using UnityEngine;
using System.Collections;

namespace DataSaveLoad{
	public class DataSaveLoadMaster : MonoBehaviour {

		public string folder;

		public SaveDataUI saveDataUI;
		public LoadDataUI loadDataUI;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void ShowSaveDialog(object data){
			saveDataUI.ShowDialog (data);
		}

		public void ShowLoadDialog(System.Type t){
			loadDataUI.ShowDialog (t);
		}
	}
}
