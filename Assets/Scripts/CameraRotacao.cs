using UnityEngine;

public class CameraRotacao : MonoBehaviour {

    public float VelocidadeAngular = 180f;

    Vector3 posBranca, posPreta;
    Quaternion rotBranca, rotPreta;

    bool indoParaBranca, indoParaPreta;

	void Start () {
        indoParaBranca = false;
        indoParaPreta = false;

        posBranca = transform.position;
        posPreta = posBranca;
        posPreta.z = -posPreta.z;

        Vector3 angulos = transform.rotation.eulerAngles;
        rotBranca = Quaternion.Euler(angulos.x, angulos.y, angulos.z);
        rotPreta = Quaternion.Euler(angulos.x, angulos.y + 180f, angulos.z);

    }
	
	
	void Update () {
	    if (indoParaBranca)
        {
            if (Vector3.Distance(transform.position, posBranca) > 2f)
            {
                transform.RotateAround(Vector3.zero, Vector3.up, VelocidadeAngular * Time.deltaTime);
            }
            else
            {
                transform.position = posBranca;
                transform.rotation = rotBranca;
                indoParaBranca = false;
            }
        }
        if (indoParaPreta)
        {
            if (Vector3.Distance(transform.position, posPreta) > 2f)
            {
                transform.RotateAround(Vector3.zero, Vector3.up, VelocidadeAngular * Time.deltaTime);
            }
            else
            {
                transform.position = posPreta;
                transform.rotation = rotPreta;
                indoParaPreta = false;
            }
        }
    }

    public void irParaBranca()
    {
        indoParaBranca = true;
    }

    public void irParaPreta()
    {
        indoParaPreta = true;
    }
}
