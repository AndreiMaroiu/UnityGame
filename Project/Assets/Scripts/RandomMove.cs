using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour {

    public Vector2 dir;
    public float speed;

    private bool x;
    private bool y;

    void Start()
    {
        dir = Random.insideUnitCircle.normalized * (speed / 150);
    }

    void Update()
    {
        if ((Mathf.Abs(transform.position.x) > 5) || (Mathf.Abs(transform.position.y) > 7))
        {
            dir = -dir;
        }
        if(dir == new Vector2())
        {
            dir = Random.insideUnitCircle.normalized * (speed / 150);
        }

        transform.Translate(dir);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyPlanet")
        {
            dir = PlanetVector(dir).normalized * speed/150;
        }
        else if (other.tag == "Destroyer") 
        {
            x = other.GetComponent<BorderScript>().x;
            y = other.GetComponent<BorderScript>().y;
            dir = BorderVector();
        }
    }

    Vector2 PlanetVector(Vector2 dir, Vector2 vec = new Vector2())
    {
        vec = dir;
        dir = Random.insideUnitCircle;

        if (vec.x > 0)
        {
            while ( dir.x > 0 )
            {
                dir.x = Random.Range(-1, 0);
            }
        }
        else
        {
            while(dir.x < 0)
            {
                dir.x = Random.Range(0, 1);
            }
        }

        if(vec.y > 0)
        {
            while(dir.y > 0)
            {
                dir.y = Random.Range(-1, 0);
            }
        }
        else
        {
            while (dir.y < 0)
            {
                dir.y = Random.Range(0, 1);
            }
        }

        return dir;
    }

    Vector2 BorderVector()
    {
        dir = Random.insideUnitCircle.normalized * (speed / 150);
        if (x && y)
        {
            //aici x nu conteaza dar y trebuie sa fie pozitiv
            dir.y = Mathf.Abs(dir.y);
        }
        else if(x && !y)
        {
            //aici x nu conteaza dar y trebuie sa fie negativ
            dir.y = -Mathf.Abs(dir.y);
        }
        else if(!x && !y)
        {
            //aici y nu conteaza dar x trebuie sa fie negativ
            dir.x = -Mathf.Abs(dir.x);
        }
        else // !x && y
        {
            //aici y nu conteaza dar x trebuie sa fie pozitiv
            dir.x = Mathf.Abs(dir.x);
        }
        return dir;
    }
}
