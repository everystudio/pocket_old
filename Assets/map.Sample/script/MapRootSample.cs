using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapRootSample : MapRootBase<MapChipSample,DataMapChipSampleParam> {

	public Camera m_setCamera;

	void Start(){

		// 画像読み込み
		SpriteManager.Instance.LoadAtlas ("atlas/ad001");
		SpriteManager.Instance.LoadAtlas ("atlas/back001");
		SpriteManager.Instance.LoadAtlas ("atlas/back002");
		SpriteManager.Instance.LoadAtlas ("atlas/item001");
		SpriteManager.Instance.LoadAtlas ("atlas/item002");
		SpriteManager.Instance.LoadAtlas ("atlas/item003");
		SpriteManager.Instance.LoadAtlas ("atlas/item004");
		SpriteManager.Instance.LoadAtlas ("atlas/item005");
		SpriteManager.Instance.LoadAtlas ("atlas/monster001");
		SpriteManager.Instance.LoadAtlas ("atlas/monster002");
		SpriteManager.Instance.LoadAtlas ("atlas/staff001");
		SpriteManager.Instance.LoadAtlas ("atlas/tutorial001");
		SpriteManager.Instance.LoadAtlas ("atlas/tutorial002");
		SpriteManager.Instance.LoadAtlas ("atlas/tutorial003");
		SpriteManager.Instance.LoadAtlas ("atlas/ui001");
		SpriteManager.Instance.LoadAtlas ("atlas/ui002");
		SpriteManager.Instance.LoadAtlas ("atlas/ui003");


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
			



	}



}
