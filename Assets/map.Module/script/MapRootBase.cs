using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapRootBase<T,U> : MonoBehaviourEx where T : MapChipBase<U> where U : DataMapChipBaseParam {

	private MapData m_mapData;
	public MapData map_data{
		get{
			if (m_mapData == null) {
				m_mapData = new MapData ();
				m_mapData.Load ("csv/map_data");
			}
			return m_mapData;
		}
	}
	
	private bool m_bInitialized;

	public List<T> m_mapchipList = new List<T>();

	public void MoveAdd( float _fX , float _fY ){
		myTransform.localPosition += new Vector3 (_fX, _fY, 0.0f); 
	}

	public void Initialize( List<U> _paramList , string _strMapData )  {
		if (m_bInitialized) {
			return;
		}
		// m_mapDataを直接利用するのは基本ここだけにする
		m_mapData.Load (_strMapData);

		myTransform.localPosition = new Vector3 (0.0f, -300.0f, 0.0f);
		m_mapchipList.Clear ();

		List<MapGrid> ignoreGridList = new List<MapGrid> ();

		for (int x = 0; x < map_data.GetWidth() + 1; x++) {
			for (int y = 0; y < map_data.GetHeight() + 1; y++) {

				if (IsGridIgnore (ignoreGridList, x, y)) {
					//Debug.Log ("same" + x.ToString () + " " + y.ToString ());
				} else {
					//GameObject obj = PrefabManager.Instance.MakeScript<T> (prefab, gameObject);

					T script = PrefabManager.Instance.AddGameObject<T> (gameObject);

					script.gameObject.name = "fielditem_" + x.ToString () + "_" + y.ToString ();
					//CtrlFieldItem script = obj.GetComponent<CtrlFieldItem> ();
					bool bHit = false;
					foreach (U param in _paramList) {
						if (param.x == x && param.y == y) {
							script.Initialize (param);
							bHit = true;

							for (int dx = param.x; dx < param.x + param.width; dx++) {
								for (int dy = param.y; dy < param.y + param.height; dy++) {
									MapGrid ignoreGrid = new MapGrid (dx, dy);
									ignoreGridList.Add (ignoreGrid);
								}
							}
							break;
						}
					}
					if (bHit == false) {
						int iDummyItemId = 0;
						if (x == DataManager.user.m_iWidth || y == DataManager.user.m_iHeight) {
							iDummyItemId = -1;
						}
						script.Initialize (x, y, iDummyItemId);
					}
					m_mapchipList.Add (script);
				}
			}
		}
		m_bInitialized = true;
	}

	private bool IsGridIgnore( List<MapGrid> _ignoreGridList , int _x , int _y ){
		bool bRet = false;
		foreach (MapGrid checkGrid in _ignoreGridList) {
			if (checkGrid.Equals (_x, _y)) {
				bRet = true;
				break;
			}
		}
		return bRet;
	}


	public T GetMapChip( int _iMapChipSerial ){
		foreach (T item in m_mapchipList) {
			if (item.GetMapChipSerial() == _iMapChipSerial) {
				return item;
			}
		}
		return null;
	}

	public T GetMapChip( int _iX , int _iY ){
		foreach (T item in m_mapchipList) {
			if (item.GetX() == _iX && item.GetY() == _iY) {
				return item;
			}
		}
		return null;
	}


	protected void RemoveFieldItem( int _iX , int _iY ){
		int iIndex = 0;
		T removeScript = null;
		foreach (T script in m_mapchipList) {
			if (script.GetX() == _iX && script.GetY() == _iY) {
				removeScript = script;
				break;
			}
			iIndex += 1;
		}
		if (removeScript != null) {
			//Debug.Log ("removefielditem x:" + _iX.ToString () + " y:" + _iY.ToString ());
			removeScript.Remove ();
			m_mapchipList.RemoveAt (iIndex);
		} else {
			//Debug.Log ("removefielditem null x:" + _iX.ToString() + " y:" + _iY.ToString ());
		}
		return;
	}

	public void AddFieldItem( T _script ){
		for (int x = _script.GetX(); x < _script.GetX() + _script.param.width; x++) {
			for (int y = _script.param.y ; y < _script.param.y + _script.param.height; y++) {
				RemoveFieldItem (x, y);
				/*
				CtrlFieldItem script = GetFieldItem (x, y);
				if (script != null) {
					script.Remove ();
				}
				*/
			}
		}
		_script.gameObject.name = "fielditem_" + _script.param.x.ToString () + "_" + _script.param.y.ToString ();
		m_mapchipList.Add (_script);
		return;
	}


	protected bool checkRaod( int _iX , int _iY ){
		//Debug.Log( string.Format( "checkRoad x:{0} y{1}" , _iX , _iY ));
		bool bRet = false;
		T temp = GetMapChip (_iX , _iY);
		// コントローラーがとれて、まだチェックしてないやつはチェック
		if (temp != null && temp.m_eRoadConnection == MapChipBase<U>.ROAD_CONNECTION.NO_CHECK ){
			//Debug.Log (temp.gameObject.name);
			if (temp.IsRoad()) {
				bRet = true;
				temp.m_eRoadConnection = MapChipBase<U>.ROAD_CONNECTION.CONNECTION;
			} else {
				temp.m_eRoadConnection = MapChipBase<U>.ROAD_CONNECTION.DISCONNECT;
			}
		}
		return bRet;
	}

	protected void checkRaodSub( int _iX , int _iY ){

		// 自分のところは普通に調べる
		// というかこれやらなくてもよさそう
		// checkRaod (_iX, _iY);

		int iTempX = _iX;
		int iTempY = _iY;


		iTempX = _iX + 1;
		iTempY = _iY;
		if( checkRaod( iTempX , iTempY )){
			checkRaodSub (iTempX , iTempY);
		}
		iTempX = _iX;
		iTempY = _iY + 1;
		if( checkRaod( iTempX , iTempY )){
			checkRaodSub (iTempX , iTempY);
		}
		iTempX = _iX - 1;
		iTempY = _iY;
		if( checkRaod( iTempX , iTempY )){
			checkRaodSub (iTempX , iTempY);
		}
		iTempX = _iX;
		iTempY = _iY -1;
		if( checkRaod( iTempX , iTempY )){
			checkRaodSub (iTempX , iTempY);
		}
		return;
	}

	public void ConnectingRoadCheck(){

		// 状態を一度リセット
		foreach (T mapchip in m_mapchipList) {
			mapchip.m_eRoadConnection = MapChipBase<U>.ROAD_CONNECTION.NO_CHECK;
		}

		checkRaodSub (1, 1);

		// 状態を一度リセット
		foreach (T mapchip in m_mapchipList) {
			switch ((DefineOld.Item.Category)mapchip.param.category) {
			case DefineOld.Item.Category.CAGE:
			case DefineOld.Item.Category.SHOP:
				if (mapchip.param.item_id != DefineOld.ITEM_ID_ROAD) {
					mapchip.CheckAroundConnectRoad (this);
				}
				break;
			default:
				break;
			}
		}

		return;
	}

	// Update is called once per frame
	void Update () {
	
	}
}










