using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ConfirmDialogUI : MonoBehaviour
{

	public Text title;
	public Text content;
	public Button approveButton;
	private Text approveButtonText;
	public Button cancelButton;
	private Text cancelButtonText;
	private Action<bool> action;

	public void Show (string title, string content, 
		                 string approveText, string cancelText, 
		                 Action<bool> action)
	{
		Setup ();

		this.title.text = title;
		this.content.text = content;
		approveButtonText.text = approveText;
		cancelButtonText.text = cancelText;

		this.action = action;

		gameObject.SetActive (true);

	}

	void Setup ()
	{
		if (approveButtonText != null)
			return;

		approveButtonText = approveButton.GetComponentsInChildren<Text> (true) [0];
		cancelButtonText = cancelButton.GetComponentsInChildren<Text> (true) [0];
			
		approveButton.onClick.AddListener (() => {
			action (true);
			gameObject.SetActive (false);
		});
		cancelButton.onClick.AddListener (() => {
			action (false);
			gameObject.SetActive (false);
		});
	}

	// Use this for initialization
	void Start ()
	{
		Setup ();
	}
		
	// Update is called once per frame
	void Update ()
	{
		
	}
}
