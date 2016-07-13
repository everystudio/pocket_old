using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapRootBase<T,U> : MonoBehaviourEx where T : MapChipBase<U> where U : DataMapChipBaseParam , new(){

	private MapData m_mapData;
	public MapData map_data{
		get{
			if (m_mapData == null) {
				m_mapData = new MapData ();
				m_mapData.Load ("data/map_data");
			}
			return m_mapData;
		}
	}
	
	protected bool m_bInitialized;
	protected BoxCollider m_boxCollider;
	protected Camera m_camera;

	protected List<T> m_mapchipList = new List<T>();

	public void MoveAdd( float _fX , float _fY ){
		myTransform.localPosition += new Vector3 (_fX, _fY, 0.0f); 
	}

	public void Initialize( List<U> _paramList , string _strMapData , Camera _camera)  {
		if (m_bInitialized) {
			return;
		}
		// m_mapDataを直接利用するのは基本ここだけにする
		if (m_mapData == null) {
			m_mapData = new MapData ();
		}

		// カメラお設定
		m_camera = _camera;

		// 大きすぎるけどとりあえず困らないでしょ
		// 必要があれば外部化する
		m_boxCollider = gameObject.AddComponent<BoxCollider> ();
		m_boxCollider.size = new Vector3( 10000.0f , 10000.0f , 0.0f );
		m_boxCollider.center = new Vector3( 0.0f , 10000.0f*0.5f , 0.0f );

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
					script.gameObject.name = string.Format ("fielditem_{0:D2}_{1:D2}" ,x,y);

					//CtrlFieldItem script = obj.GetComponent<CtrlFieldItem> ();
					bool bHit = false;
					foreach (U param in _paramList) {
						if (param.x == x && param.y == y) {
							script.Initialize (map_data,param);
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
						if (x == map_data.GetWidth() || y == map_data.GetHeight()) {
							iDummyItemId = -1;
						}
						script.Initialize ( map_data, x, y, iDummyItemId);
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

	// 無理やり入力を解除したい場合はここを継承して利用
	public virtual bool ForceInterceptInput(){
		return false;
	}

	/**
	 * 当たり判定関連の処理
	 * 
	 * 
	 * */
	public bool GetGrid( GameObject _goRoot , Vector2 _inputPoint , out int _iX , out int _iY , Camera _camera ){
		_iX = 0;
		_iY = 0;

		if (ForceInterceptInput() == true ) {
			return false;
		}

		bool bRet = false;
		RaycastHit hit;

		//Debug.Log (_camera);
		Ray ray = _camera.ScreenPointToRay (new Vector3 (_inputPoint.x, _inputPoint.y, 0.0f));
		float fDistance = 100.0f;

		_iX = 0;
		_iY = 0;

		//レイを投射してオブジェクトを検出
		if (Physics.Raycast (ray, out hit, fDistance)) {
			//Debug.Log (hit.collider.gameObject.name);
			if (hit.collider.gameObject.name.Equals (map_data.TouchableObjectName)) {
				GameObject objPoint = new GameObject ();
				objPoint.transform.position = hit.point;
				objPoint.transform.parent = _goRoot.transform;

				// ここの計算式は後で見直します
				int calc_x = Mathf.FloorToInt ((objPoint.transform.localPosition.x + (objPoint.transform.localPosition.y * 2.0f)) / 160.0f);
				int calc_y = Mathf.FloorToInt (((objPoint.transform.localPosition.y * 2.0f) - objPoint.transform.localPosition.x) / 160.0f);
				//Debug.Log ("calc_x=" +  calc_x.ToString () + " calc_y=" +  calc_y.ToString ());
				bRet = true;
				_iX = calc_x;
				_iY = calc_y;

				//Debug.LogError (string.Format ("x:{0} y:{1} posx{2} posy{3}", calc_x, calc_y, objPoint.transform.localPosition.x, objPoint.transform.localPosition.y));
				Destroy (objPoint);
			}
		}
		return bRet;
	}

	public bool GetGrid( GameObject _goRoot , Vector2 _inputPoint , out int _iX , out int _iY ){
		return GetGrid (_goRoot, _inputPoint, out _iX, out _iY, m_camera );
	}
	public bool GetGrid( Vector2 _inputPoint , out int _iX , out int _iY ){
		return GetGrid (gameObject, _inputPoint, out _iX, out _iY );
	}
	public bool GridHit( int _iX , int _iY , int _iItemX , int _iItemY , int _iItemWidth , int _iItemHeight , out int _iOffsetX , out int _iOffsetY ){
		_iOffsetX = 0;
		_iOffsetY = 0;

		//Debug.Log ("x:" + _dataItem.x.ToString () + " y:" + _dataItem.y.ToString () + " w:" + _dataItem.width.ToString () + " h:" + _dataItem.height.ToString ());

		bool bHit = false;
		for (int x = _iItemX; x < _iItemX + _iItemWidth; x++) {
			for (int y = _iItemY; y < _iItemY + _iItemHeight; y++) {
				if (_iX == x && _iY == y) {

					_iOffsetX = x - _iItemX;
					_iOffsetY = y - _iItemY;
					bHit = true;
					break;
				}
			}
		}
		return bHit;
	}

	public bool GridHit( int _iX , int _iY , DataItemParam _dataItem , out int _iOffsetX , out int _iOffsetY ){

		return GridHit (_iX, _iY, _dataItem.x, _dataItem.y, _dataItem.width, _dataItem.height, out _iOffsetX, out _iOffsetY);
		/*
		_iOffsetX = 0;
		_iOffsetY = 0;

		//Debug.Log ("x:" + _dataItem.x.ToString () + " y:" + _dataItem.y.ToString () + " w:" + _dataItem.width.ToString () + " h:" + _dataItem.height.ToString ());

		bool bHit = false;
		for (int x = _dataItem.x; x < _dataItem.x + _dataItem.width; x++) {
			for (int y = _dataItem.y; y < _dataItem.y + _dataItem.height; y++) {
				if (_iX == x && _iY == y) {

					_iOffsetX = x - _dataItem.x;
					_iOffsetY = y - _dataItem.y;
					bHit = true;
					break;
				}
			}
		}
		return bHit;
		*/
	}

	public bool GridHit( int _iX , int _iY , DataItemParam _dataItem ){

		int iOffsetX = 0;
		int iOffsetY = 0;

		return GridHit (_iX, _iY, _dataItem, out iOffsetX, out iOffsetY);
	}





	// Update is called once per frame
	void Update () {
	
	}
}










