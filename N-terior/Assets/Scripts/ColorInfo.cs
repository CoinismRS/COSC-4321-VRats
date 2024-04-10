using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorStruct : MonoBehaviour
{
    // Start is called before the first frame update
    public class ColorInfo
    {
        public string name;
        public string hex;
    }

    [System.Serializable]
    public class ColorList
    {
        public List<ColorInfo> colors;
    }
}
