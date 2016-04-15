﻿using UnityEngine;
using System.Collections;

public class MasterTableAudio : MasterTableBase<MasterAudioCSV> 
{
	private static readonly string FilePath = "CSV/master_load_audio";
	public void Load() { Load(FilePath); }
}

public class MasterAudioCSV : MasterBase
{
	public string filename { get; private set; }
	public int version { get; private set; }
	public string path { get; private set; }
	public int audio_type { get; private set; }
	public int del_flg { get; private set; }
}

