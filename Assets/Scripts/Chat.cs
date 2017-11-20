using UnityEngine;
using System.Collections.Generic;

public class Chat : MonoBehaviour {

	// 보낸 채팅 결과
	public List<string> chatHistory = new List<string>();
	// 현재 메시지
	private string currentMessage = string.Empty;

	private void SendMassage()
	{
		if(!string.IsNullOrEmpty(currentMessage.Trim ()))
		{
			networkView.RPC ("ChatMessage", RPCMode.AllBuffered, new object[]{currentMessage});
			currentMessage = string.Empty;
		}
	}

	private void Bottom()
	{
		currentMessage = GUI.TextField (new Rect (0, Screen.height - 20, 500, 20), currentMessage);
		if (GUI.Button (new Rect (525, Screen.height - 20, 75, 20), "Send"))
			SendMassage();

		GUILayout.Space (15);
		for (int i=chatHistory.Count - 1; i>=0; i--)
			GUILayout.Label (chatHistory [i]);
	}

	private void Top()
	{
		GUILayout.Space (15);
		GUILayout.BeginHorizontal (GUILayout.Width (250));
		currentMessage = GUILayout.TextField (currentMessage);
		if (GUILayout.Button ("Send"))
			SendMassage();
		GUILayout.EndHorizontal ();
		
		foreach (string c in chatHistory)
			GUILayout.Label (c);
	}

	private void OnGUI()
	{
		if (!NetworkMenu.Connected)
						return;

		Bottom ();
		}

	[RPC]
	public void ChatMessage(string message)
	{
		chatHistory.Add (message);
	}
}
