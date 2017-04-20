using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapRootSample : MapRootBase<MapChipSample,DataMapChipSampleParam> {

	public Camera m_setCamera;

	void Start(){

		// 画像読み込み
		/*
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/ad001");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/back001");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/back002");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/item001");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/item002");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/item003");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/item004");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/item005");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/monster001");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/monster002");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/staff001");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/tutorial001");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/tutorial002");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/tutorial003");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/ui001");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/ui002");
		SpriteManager.Instance.LoadSpriteAtlas ("atlas/ui003");
		*/


		DataMapChipSample mapchip_sample = new DataMapChipSample ();
		mapchip_sample.Load ("data/mapchip_sample");

		List<DataMapChipSampleParam> param_list = mapchip_sample.list;

		Initialize (param_list,"data/map_data" , m_setCamera );

	}

	void Update(){

		if (InputManager.Instance.Info.TouchUp) {
			InputManager.Instance.Info.TouchUp = false;
			int iGridX = 0;
			int iGridY = 0;

			if (GetGrid (InputManager.Instance.Info.TouchPoint, out iGridX, out iGridY)) {
				Debug.Log (string.Format ("grid({0},{1})", iGridX, iGridY));

			} else {
				Debug.Log ("no hit");
			}
		}


		if (InputManager.Instance.Info.Swipe) {

			myTransform.localPosition += new Vector3 (InputManager.Instance.Info.SwipeAdd.x, InputManager.Instance.Info.SwipeAdd.y, 0.0f);

			float fMaxX = map_data.GetWidth() * map_data.CELL_X_DIR.x;
			float fMinX = fMaxX * -1.0f;

			if (myTransform.localPosition.x < fMinX) {
				myTransform.localPosition = new Vector3 (fMinX, myTransform.localPosition.y, myTransform.localPosition.z);
			} else if (fMaxX < myTransform.localPosition.x) {
				myTransform.localPosition = new Vector3 (fMaxX, myTransform.localPosition.y, myTransform.localPosition.z);
			} else {
			}
			float fMaxY = 0.0f;
			float fMinY = map_data.GetHeight()*2 * map_data.CELL_X_DIR.y * -1.0f;
			if (myTransform.localPosition.y < fMinY) {
				myTransform.localPosition = new Vector3 (myTransform.localPosition.x, fMinY , myTransform.localPosition.z);
			} else if (fMaxY < myTransform.localPosition.y) {
				myTransform.localPosition = new Vector3 (myTransform.localPosition.x, fMaxY , myTransform.localPosition.z);
			} else {
			}

			//m_eStep = STEP.SWIPE;
		}



	}



}
