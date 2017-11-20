using UnityEngine;
using System.Collections;

public class NetworkMenu : MonoBehaviour {

	// 접속 IP
	public string connectionIP = "127.0.0.1";
	// 접속 포트번호
	public int portNumber = 1000;
	// 접속 여부
	public static bool Connected { get; private set;}

	private void OnConnectedToServer()
	{
		// Client가 연결됨
		Connected = true;
	}

	private void OnServerInitialized()
	{
		// Server가 생성됨
		Connected = true;
	}

	private void OnDisconnectedFromServer()
	{
		// 연결이 끈어지거나, 누락
		Connected = false;
	}

	private void OnGUI()
	{ // 현재 사용자의 네트워크에 접속 여부 판단
		if (!Connected) {
			connectionIP = GUILayout.TextField (connectionIP);
			int.TryParse (GUILayout.TextField (portNumber.ToString ()), out portNumber);

			// 채팅에 접속하는 버튼
			if (GUILayout.Button ("Connect")){
				// 채팅 서버 접속 : Connect(접속IP, 접속포트번호)
				Network.Connect (connectionIP, portNumber);

			}
			// 채팅을 생성하는 버튼
			if (GUILayout.Button ("Server"))
				// 채팅 서버 생성 : InitializeServer(접속자수, 포트번호, NAT사용여부)
				Network.InitializeServer (10, portNumber, true);
		}
		else
			// 접속 상태일때 메시지 출력
			GUILayout.Label ("Client Count : " + Network.connections.Length.ToString ());

	}
}
