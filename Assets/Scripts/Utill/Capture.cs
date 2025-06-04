using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Capture : MonoBehaviour
{
    public Camera cam;
    public RenderTexture rt;
    public Image bg;
    [SerializeField] private string _filename;

    public GameObject[] obj;
    int nowCnt = 0;

    private void Start()
    {
        cam = Camera.main;
    }

    public void Create()
    {
        StartCoroutine(CaptureImage());
    }

    public void CreateAll()
    {
        StartCoroutine(AllCaptureImage());
    }

    IEnumerator CaptureImage()
    {
        yield return null;

        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false, true);
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);

        yield return null;

        var data = tex.EncodeToPNG();
        string name = _filename;
        string extention = ".png";
        string path = Application.persistentDataPath + "/ItemImage/";

        Debug.Log(path);

        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        File.WriteAllBytes(path + name + extention, data);

        yield return null;
    }

    IEnumerator AllCaptureImage()
    {
        while (nowCnt < obj.Length)
        {
            var nowObj = Instantiate(obj[nowCnt].gameObject);
            yield return null;

            Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false, true);
            RenderTexture.active = rt;
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);

            yield return null;

            var data = tex.EncodeToPNG();
            string name = $"{obj[nowCnt].gameObject.name}";
            string extention = ".png";
            string path = Application.persistentDataPath + "/ItemImage/";

            Debug.Log(path);

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            File.WriteAllBytes(path + name + extention, data);

            yield return null;

            DestroyImmediate(nowObj);
            nowCnt++;

            yield return null;
        }
    }
}
