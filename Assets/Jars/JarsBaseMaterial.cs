using UnityEngine;


public class JarsBaseMaterial : MonoBehaviour
{
    public Texture newTexture; // ����� �������� � ����������
    private Renderer rend;
    private MaterialPropertyBlock mpb;

    void Start()
    {
        rend = GetComponent<Renderer>(); // �������� �������� �������
        mpb = new MaterialPropertyBlock(); 

        // �������� ������� �������� ���������
        rend.GetPropertyBlock(mpb);

        // ������������� ����� �������� (Base Map � Shader Graph / ����������� ������� URP)
        mpb.SetTexture("_BaseMap", newTexture);  

        // ��������� ���������
        rend.SetPropertyBlock(mpb);
    }
}

