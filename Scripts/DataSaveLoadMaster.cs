﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace DataSaveLoad{
	public class DataSaveLoadMaster : MonoBehaviour {

		public string folder;

		public SaveDataUI saveDataUI;
		public LoadDataUI loadDataUI;

		
		public delegate void DataLoadHandler(object data);
		public event DataLoadHandler dataLoadHandler;

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

		public string GetFolderPath(){
			return  string.Format("{0}/{1}", Application.persistentDataPath , folder);
		}
		
		public string GetFilePath(string fname){
			return  string.Format("{0}/{1}", GetFolderPath() , fname+".txt");
		}

		public void WriteFile(string path, object obj, System.Type t){
			
			print (t);
			XmlSerializer ser = new XmlSerializer (t);
			
			//書き込むファイルを開く（UTF-8 BOM無し）
			StringBuilder sb = new StringBuilder ();
			StringWriter sw = new StringWriter(sb);
			ser.Serialize(sw, obj);
			
			print (path);
			File.WriteAllBytes(path, Encoding.UTF8.GetBytes(sw.ToString()));
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

	}
}
