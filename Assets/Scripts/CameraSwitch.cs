using UnityEngine;

public class CameraSwitch : MonoBehaviour {

	public GameObject[] cameras;

	public void ChangeCamera(int i) {
		if(i < cameras.Length) {
			for (int j = 0; j < cameras.Length; j++) {
				cameras[j].SetActive(false);
			}

			cameras[i].SetActive(true);
		}
	}
}
