
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class pusherstuff : MonoBehaviour {

	string url;
	int lastId = 0;

	// Use this for initialization
	void Start () {

		url = "https://api.private-beta-1.pusherplatform.com:443/apps/9fa04888-85b6-459d-b581-53048c584fe8/feeds/vote";
		UnityWebRequest uwr = UnityWebRequest.Delete (url);
		uwr.Send ();

		StartCoroutine(FeedLoop());

	}

	IEnumerator FeedLoop() {
		Debug.Log ("FeedLoop");
		for (;;) {
			StartCoroutine (GetFeed ());
			yield return new WaitForSeconds (.5f);
		}
	}

	IEnumerator GetFeed() {
		if (lastId != 0) {
//			url = "https://api.private-beta-1.pusherplatform.com:443/apps/9fa04888-85b6-459d-b581-53048c584fe8/feeds/vote?from_id=" + lastId;
			url = "https://api.private-beta-1.pusherplatform.com:443/apps/9fa04888-85b6-459d-b581-53048c584fe8/feeds/vote?limit=5";
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

			// while if items[items.Count - 1] > lastID {
			//   url = "https://api.private-beta-1.pusherplatform.com:443/apps/9fa04888-85b6-459d-b581-53048c584fe8/feeds/vote?limit=5from_id=items[items.Count - 1]";
			//   UnityWebRequest www = UnityWebRequest.Get(url);
			//   yield return www.Send();
			//   JSONNode res = JSON.Parse(www.downloadHandler.text);
			//   items + res["items"].AsArray;
			// }

			// remove items <= lastID

			// lastID = items[0]
			if (items.Count > 0) {
				while (items [items.Count - 1]["id"].AsInt > lastId) {
					url = "https://api.private-beta-1.pusherplatform.com:443/apps/9fa04888-85b6-459d-b581-53048c584fe8/feeds/vote?limit=5from_id="+items[items.Count - 1].ToString();
					www = UnityWebRequest.Get (url);
					yield return www.Send ();
					items.Add (JSON.Parse (www.downloadHandler.text));
				}

				for (int i = items.Count; i >= 0; i--) {
					JSONNode node = items [i];
					if (node ["id"].AsInt <= lastId) {
						items.Remove(node);
					}
				}

				if (items [0] ["id"].AsInt != 0) {
					lastId = items [0] ["id"].AsInt;
				}
				Debug.Log (items);

				int[] scores = new int[4];
				foreach (JSONNode node in items) {
					int id = node ["data"].AsInt;
					scores [id]++;
				}

				for (int i = 0; i < 4; i++) {
					GameObject.Find ("player (" + i + ")").GetComponent<move> ().speed = scores [i];
					Debug.Log (scores[i]);
				}

			}

//			Debug.Log (res.ToString());
//			if (items.Count > 0) {
//				//				if (lastId != "0") {
//				res ["items"].Remove (0);
//				items = res ["items"].AsArray;
//				//				}
//				if (items.Count > 0) {
//					lastId = items [items.Count - 1] ["id"];
//				}
//			}


			// Or retrieve results as binary data
			//			byte[] results = www.downloadHandler.data;
		}
	}



	// Update is called once per frame
	void Update () {

	}
}
