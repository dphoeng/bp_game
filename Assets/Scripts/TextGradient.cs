using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextGradient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TMP_Text title = GetComponent<TMP_Text>();
        title.ForceMeshUpdate();
        TMP_TextInfo word = title.textInfo;
        int count = word.characterCount;
        for (int i = 0; i < word.characterCount; ++i)
        {
            int meshIndex = title.textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = title.textInfo.characterInfo[i].vertexIndex;

            Color32[] vertexColors = title.textInfo.meshInfo[meshIndex].colors32;
            vertexColors[vertexIndex + 0] = new Color32(255, (byte)(255 / (count - 1) * i), (byte)(255 / (count - 1) * i), 255);
            vertexColors[vertexIndex + 1] = new Color32(255, (byte)(255 / (count - 1) * i), (byte)(255 / (count - 1) * i), 255);
            vertexColors[vertexIndex + 2] = new Color32(255, (byte)(255 / (count - 1) * i), (byte)(255 / (count - 1) * i), 255);
            vertexColors[vertexIndex + 3] = new Color32(255, (byte)(255 / (count - 1) * i), (byte)(255 / (count - 1) * i), 255);
        }

        title.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
