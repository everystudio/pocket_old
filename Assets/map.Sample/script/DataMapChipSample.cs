using UnityEngine;
using System.Collections;


[System.Serializable]
public class DataMapChipSampleParam : DataMapChipBaseParam{

	public int m_sample_int;
	public float m_sample_float;
	public string m_sample_string;

	public int sample_int { get{ return m_sample_int;} set{m_sample_int = value; } }
	public float sample_float { get{ return m_sample_float;} set{m_sample_float = value; } }
	public string sample_string { get{ return m_sample_string;} set{m_sample_string = value; } }


}


public class DataMapChipSample : DataMapChipBase<DataMapChipSampleParam> {
	
}
