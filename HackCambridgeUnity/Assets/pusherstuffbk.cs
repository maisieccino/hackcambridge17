
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class pusherstuffbk : MonoBehaviour {

	string url;
	string lastId = "0";

	// Use this for initialization
	void Start () {

		url = "https://api.private-beta-1.pusherplatform.com:443/apps/9fa04888-85b6-459d-b581-53048c584fe8/feeds/vote";

		StartCoroutine(FeedLoop());
	}

	IEnumerator FeedLoop() {
		Debug.Log ("FeedLoop");
		for (;;) {
			StartCoroutine (GetFeed ());
			yield return new WaitForSeconds (.2f);
		}
	}

	IEnumerator GetFeed() {
		if (lastId != "0") {
			url = "https://api.private-beta-1.pusherplatform.com:443/apps/9fa04888-85b6-459d-b581-53048c584fe8/feeds/vote?from_id=" + lastId;
		}
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			// Show results as text
			// JSONObject res = new Sim(www.downloadHandler.text);
			JSONNode res = JSON.Parse(www.downloadHandler.text);
			JSONArray items = res["items"].AsArray;
			Debug.Log (res.ToString());
			if (items.Count > 0) {
				//				if (lastId != "0") {
				res ["items"].Remove (0);
				items = res ["items"].AsArray;
				//				}
				if (items.Count > 0) {
					lastId = items [items.Count - 1] ["id"];
				}
			}


			// Or retrieve results as binary data
			//			byte[] results = www.downloadHandler.data;
		}
	}



	// Update is called once per frame
	void Update () {

	}
}
