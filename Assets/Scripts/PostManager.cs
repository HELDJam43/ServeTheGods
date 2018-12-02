using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostManager : MonoBehaviour {

    public static PostManager INSTANCE;

    public GameObject postPrefab;

    List<PostRandomizer> posts;
    float fallSpeed = 30f;
    float fastfallSpeed = 150f;

	// Use this for initialization
	void Start () {
        posts = new List<PostRandomizer>();
        INSTANCE = this;

    }
	
	// Update is called once per frame
	void Update () {
        /*if(MAKEPOST)
        {
            MakePost(PostRandomizer.RatingType.Fed, "food");
            MAKEPOST = false;
        }*/

        List<PostRandomizer> deletePosts = new List<PostRandomizer>();
		foreach(PostRandomizer p in posts)
        {
            p.transform.position = p.transform.position + new Vector3(0, -fallSpeed * Time.deltaTime, 0);
            if(p.fastDistance >= 0)
            {
                if(p.fastDistance <= Time.deltaTime * fastfallSpeed)
                {
                    p.GetComponent<RectTransform>().localPosition = p.GetComponent<RectTransform>().localPosition + new Vector3(0, -p.fastDistance, 0);
                    p.fastDistance = 0;
                } else
                {
                    p.GetComponent<RectTransform>().localPosition = p.GetComponent<RectTransform>().localPosition + new Vector3(0, -fastfallSpeed * Time.deltaTime, 0);
                    p.fastDistance -= fastfallSpeed * Time.deltaTime;
                }
            }
            p.lifeTime -= Time.deltaTime;
            if (p.lifeTime <= 0)
            {
                deletePosts.Add(p); 
            }
        }
        foreach(PostRandomizer p in deletePosts)
        {
            posts.Remove(p);
            Destroy(p.gameObject);
        }
	}

    public void MakePost(PostRandomizer.RatingType rating, string food)
    {
        PostRandomizer post = GameObject.Instantiate(postPrefab).GetComponent<PostRandomizer>();
        post.transform.SetParent(this.transform);
        post.GetComponent<RectTransform>().localPosition = Vector3.zero;
        post.Randomize(rating, food);
        float fastfallDistance = 0;
        foreach(PostRandomizer p in posts)
        {
            if(p.GetComponent<RectTransform>().localPosition.y > -200)
            {
                fastfallDistance = Mathf.Max(fastfallDistance, p.transform.localPosition.y + 115);
            }
        }
        foreach(PostRandomizer p in posts)
        {
            p.fastDistance += fastfallDistance;
        }
        posts.Add(post);
    }
}
