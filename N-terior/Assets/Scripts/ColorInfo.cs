using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;

[System.Serializable]
public class ColorInfo
{
    public string name;
    public string hex;
    public float price;
}

[System.Serializable]
public class ColorList
{
    public List<ColorInfo> colors;
}
