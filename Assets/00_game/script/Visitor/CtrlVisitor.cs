using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CtrlVisitor : MonoBehaviourEx {

	public enum STEP
	{
		NONE		= 0,
		WAIT		,
		MOVE		,
		END			,
		MAX			,
	}
	public STEP m_eStep;
	private STEP m_eStepPre;

	public bool m_bUp;

	[SerializeField]
	private float m_fTimer;

	[SerializeField]
	private int m_iType;
	[SerializeField]
	private float m_fAnimationTimer;
	[SerializeField]
	private int m_iAnimationFrame;

	[SerializeField]
	private int m_iPosX;
	[SerializeField]
	private int m_iPosY;

	[SerializeField]
	private int m_iTargetX;
	[SerializeField]
	private int m_iTargetY;
	[SerializeField]
	private int m_iTargetItemSerial;

	[SerializeField]
	private Vector3 m_v3StartPosition;
	[SerializeField]
	private Vector3 m_v3TargetPosition;

	private float INTERVAL = 1.0f;
	private float INTERVAL_ANIMATION = 1.0f;

	[SerializeField]
	private UI2DSprite m_sprChara;

	private void setSprite (int _iType, int _iFrame){
		m_sprChara.sprite2D = SpriteManager.Instance.Load (string.Format ("texture/ui/people{0}_{1:D2}", _iType, _iFrame));
	}

	private Vector3 getPosition( int _iX , int _iY ){
		return (DefineOld.CELL_X_DIR.normalized * DefineOld.CELL_X_LENGTH * ((float)_iX+0.5f)) + (DefineOld.CELL_Y_DIR.normalized * DefineOld.CELL_Y_LENGTH * ((float)_iY+0.5f));
	}
	private void setDepth( int _iX , int _iY ){
		int iDepth = 100 - (_iX + _iY);// + (m_dataItemParam.height-1));
		m_sprChara.depth = iDepth;
	}

	public bool IsActive(){
		return m_eStep != STEP.END;
	}

	public void Initialize( int _iType , int _iItemSerial ){
		m_iType = _iType;
		DataItemParam item_param = DataManager.Instance.m_dataItem.Select (_iItemSerial);

		//Debug.LogError (string.Format ("x={0} y={1}", _iX, _iY));
		//myTransform.localPosition = (DefineOld.CELL_X_DIR.normalized * DefineOld.CELL_X_LENGTH * item_param.x) + (DefineOld.CELL_Y_DIR.normalized * DefineOld.CELL_Y_LENGTH * item_param.y);
		m_bUp = true;
		m_iPosX = item_param.x;
		m_iPosY = item_param.y;
		m_iTargetX = m_iPosX;
		m_iTargetY = m_iPosY;

		m_iAnimationFrame = 0;
		setSprite (m_iType, m_iAnimationFrame);

		myTransform.localPosition = getPosition (m_iPosX, m_iPosY);
		setDepth (m_iPosX, m_iPosY);
		m_sprChara.gameObject.SetActive (true);

		m_eStep = STEP.WAIT;
	}

	// Update is called once per frame
	void Update () {
		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			m_eStepPre  = m_eStep;
			bInit = true;
		}

		switch (m_eStep) {
		case STEP.WAIT:
			if (bInit) {
				m_iPosX = m_iTargetX;
				m_iPosY = m_iTargetY;
				if (m_bUp) {
					setDepth (m_iPosX, m_iPosY);
				}
			}
			if (m_bUp) {
				List<DataItemParam > check_list = new List<DataItemParam> ();
				int x = m_iPosX + 1;
				int y = m_iPosY;
				DataItemParam param = DataManager.Instance.m_dataItem.SelectOne (string.Format (" item_id = {0} and x = {1} and y = {2} ", DefineOld.ITEM_ID_ROAD, x, y));
				if (param.item_id == DefineOld.ITEM_ID_ROAD) {
					check_list.Add (param);
				}
				x = m_iPosX;
				y = m_iPosY + 1;
				param = DataManager.Instance.m_dataItem.SelectOne (string.Format (" item_id = {0} and x = {1} and y = {2} ", DefineOld.ITEM_ID_ROAD, x, y));
				if (param.item_id == DefineOld.ITEM_ID_ROAD) {
					check_list.Add (param);
				}
				if (0 < check_list.Count) {
					int iIndex = UtilRand.GetRand (check_list.Count);
					m_iTargetItemSerial = check_list [iIndex].item_serial;
					m_iTargetX = check_list [iIndex].x;
					m_iTargetY = check_list [iIndex].y;
					m_eStep = STEP.MOVE;
				} else {
					m_bUp = false;
				}
			} else {
				List<DataItemParam > check_list = new List<DataItemParam> ();
				int x = m_iPosX - 1;
				int y = m_iPosY;
				DataItemParam param = DataManager.Instance.m_dataItem.SelectOne (string.Format (" item_id = {0} and x = {1} and y = {2} ", DefineOld.ITEM_ID_ROAD, x, y));
				if (param.item_id == DefineOld.ITEM_ID_ROAD) {
					check_list.Add (param);
				}
				x = m_iPosX;
				y = m_iPosY - 1;
				param = DataManager.Instance.m_dataItem.SelectOne (string.Format (" item_id = {0} and x = {1} and y = {2} ", DefineOld.ITEM_ID_ROAD, x, y));
				if (param.item_id == DefineOld.ITEM_ID_ROAD) {
					check_list.Add (param);
				}
				if (0 < check_list.Count) {
					int iIndex = UtilRand.GetRand (check_list.Count);
					m_iTargetItemSerial = check_list [iIndex].item_serial;
					m_iTargetX = check_list [iIndex].x;
					m_iTargetY = check_list [iIndex].y;
					m_eStep = STEP.MOVE;
				} else {
					x = m_iPosX - 2;
					y = m_iPosY;
					param = DataManager.Instance.m_dataItem.SelectOne (string.Format (" item_id = {0} and x = {1} and y = {2} ", DefineOld.ITEM_ID_GATE, x, y));
					if (param.item_id == DefineOld.ITEM_ID_GATE) {
						check_list.Add (param);
					}
					x = m_iPosX - 2;
					y = m_iPosY - 1;
					param = DataManager.Instance.m_dataItem.SelectOne (string.Format (" item_id = {0} and x = {1} and y = {2} ", DefineOld.ITEM_ID_GATE, x, y));
					if (param.item_id == DefineOld.ITEM_ID_GATE) {
						check_list.Add (param);
					}
					x = m_iPosX;
					y = m_iPosY - 2;
					param = DataManager.Instance.m_dataItem.SelectOne (string.Format (" item_id = {0} and x = {1} and y = {2} ", DefineOld.ITEM_ID_GATE, x, y));
					if (param.item_id == DefineOld.ITEM_ID_GATE) {
						check_list.Add (param);
					}
					x = m_iPosX - 1;
					y = m_iPosY - 2;
					param = DataManager.Instance.m_dataItem.SelectOne (string.Format (" item_id = {0} and x = {1} and y = {2} ", DefineOld.ITEM_ID_GATE, x, y));
					if (param.item_id == DefineOld.ITEM_ID_GATE) {
						check_list.Add (param);
					}
					if (0 < check_list.Count) {
						m_eStep = STEP.END;
					} else {
						m_bUp = true;
					}
				}
			}
			break;

		case STEP.MOVE:
			if (bInit) {
				m_fTimer = 0.0f;

				m_v3StartPosition = myTransform.localPosition;
				DataItemParam param = DataManager.Instance.m_dataItem.Select (m_iTargetItemSerial);
				m_v3TargetPosition = getPosition (param.x, param.y);
				if (!m_bUp) {
					setDepth (m_iTargetX, m_iTargetY);
				}

				if (m_v3StartPosition.x < m_v3TargetPosition.x) {
					m_sprChara.flip = UIBasicSprite.Flip.Horizontally;
				} else {
					m_sprChara.flip = UIBasicSprite.Flip.Nothing;
				}
			}

			m_fAnimationTimer += Time.deltaTime;
			if (INTERVAL_ANIMATION < m_fAnimationTimer) {
				m_iAnimationFrame += 1;
				if (2 <= m_iAnimationFrame) {
					m_iAnimationFrame = 0;
				}
				setSprite (m_iType, m_iAnimationFrame);
			}

			m_fTimer += Time.deltaTime;
			Vector3 set_position;
			if (Linear (m_fTimer / INTERVAL, m_v3StartPosition, m_v3TargetPosition, out set_position)) {
				m_eStep = STEP.WAIT;
			}
			myTransform.localPosition = set_position;
			break;
		case STEP.END:
			if (bInit) {
				m_sprChara.gameObject.SetActive (false);
			}
			break;
		case STEP.MAX:
		default:
			break;
		}
	}
}









