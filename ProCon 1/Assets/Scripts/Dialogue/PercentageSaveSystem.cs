using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using UnityEngine;

public class PercentageSaveSystem : MonoBehaviour {

    public string path;

    public void Initialize(string fileName) {
        path = Application.dataPath + fileName;
    }

}