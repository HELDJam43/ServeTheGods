using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PostRandomizer : MonoBehaviour {

    public float lifeTime = 3;
    public float fastDistance = 0;

    public Gradient darkColors;
    public Gradient lightColors;
    public Sprite[] profilePics;

    public Image profileBG;
    public Image profilePic;

    public TextMeshProUGUI username;
    public TextMeshProUGUI bodyText;
    public Transform ratingStarParent;

    string food = "burger";

    public bool DEBUGRANDOMIZE = false;
    public RatingType DEBUGTYPE = RatingType.Fed;

    static string[] USERNAMES = {"chicken", "duck", "goose", "hen", "bird", "quack", "honk", "ghost", "butt", "evil", "devil", "demon",
    "fried", "fry", "cool", "chill", "angry", "nerd", "woke", "death", "doom", "fake", "post", "shit", "dead", "poster", "cow", "pig", "goat",
    "ram", "me", "you", "thing", "again", "twice", "double", "triple", "mix", "song", "music", "game", "dare", "ludum", "person", "thing", "egg",
    "burger", "foodie", "critic", "critical", "rater", "rates", "rating", "restaurant", "metal", "rage", "god", "priest", "cleric", "knight",
    "blue", "purple", "green", "recycled", "internet", "web", "spider", "bug", "milk", "sandwich", "cheese", "satan", "king", "queen", "prince",
    "princess", "royal", "royalty", "noble", "killer", "assasin", "laser", "space", "stuff", "chew", "chomp", "eat", "eater", "eating", "critique",
    "gamer", "games", "star", "one", "two", "three", "four", "five", "nope", "please", "space", "moon", "sun", "witch", "hunter", "wizard",
    "baker", "chef", "cook", "fries", "milkshake", "shake", "chocolate", "vanilla", "moo", "llama", "alpaca", "anime", "memes", "meme",
    "universe", "leek", "spin", "meat", "meaty", "beef", "beefy", "pork", "bun", "roll", "blaze", "leaf", "fox", "wolf", "frog", "toad"};

    static string[] FEDRATINGS = { "Just ate a $P $F", "Had a $P $F today", "$P $F here", "Try the $F", "The $F was $P", "$P", "$P $F",
    "Get the $F", "The $F is $P", "Best $F in town", "The $F is the best", "Love the $F", "Best $F I've ever had."};

    static string[] LIFTEDRATINGS = { "The $C attacked me", "$E $C", "$E $C", "$E $C", "$E $C", "The $C is $E" };

    static string[] COOKEDRATINGS = {"help", "run", "I'm beeing cooked", "they're going to eat me", "oh no", "help me", "I'm literally dying",
    "too hot", "they're eating people", "I died", "I'm dead", "I'm melting", "The $C is serving me", "I'm being sacrificed", "$E $C", "$E $C",
    "they eat people", "don't sacrifice me, bro", "I got roasted", "It's too hot in here", "RIP me", "this is fine"};

    static string[] NEGATIVECOMMENTS = { "go somewhere else", "$E restaurant", "not worth it", "$E", "too crowded",
    "bad location", "too hot", "too cold", "get out", "don't go here", "run while you still can", "$E place", "$E crowd", "$E atmosphere",
    "$E mood",  "$E people", "$E locals"};

    static string[] CHEFALIAS = { "chef", "cook", "waiter", "staff", "owner", "player", "main character", "server", "chef", "chef", "cook", "cook" };
    static string[] POSITIVECOMMENTS = { "$P stuff", "$P food", "always $P", "$P dishes", "cheap", "$G price", "$G service", "$G atmosphere",
    "$G $C", "$G $C", "$G $C", "$G location", "$G locals", "consistently $P", "perfectly $P", "$G value"};
    static string[] GOOD = {"great", "good", "awesome", "sweet", "rad", "perfect", "lovely", "wonderful", "nice", "mind-blowing",
    "best", "cool", "trendy", "impossible", "godly", "top-tier", "stellar", "excellent", "decent", "OK", "acceptable", "passable"};
    static string[] POSITIVE = { "delicious", "tasty", "savory", "sweet", "perfectly cooked", "hot", "edible", "mouth-watering"};
    static string[] EVIL = { "terrible", "bad", "shit", "worst", "evil", "rude", "garbage", "crap", "asshole", "worthless", "useless",
    "awful", "unacceptable", "lousy"};

    public enum RatingType
    {
        Fed,
        LiftedUp,
        ThrownOut,
        Cooked,
        WrongFood,
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if (DEBUGRANDOMIZE)
        {
            DEBUGRANDOMIZE = false;
            Randomize(DEBUGTYPE, "burger");
        }
	}

    private string StringReplacer(string input)
    {
        if(input.Contains("$P"))
        {
            int p = Random.Range(0, GOOD.Length + POSITIVE.Length);
            if(p < GOOD.Length)
            {
                input = input.Replace("$P", GOOD[p]);
            } else
            {
                input = input.Replace("$P", POSITIVE[p - GOOD.Length]);
            }
        }
        if(input.Contains("$G"))
        {
            input = input.Replace("$G", GOOD[Random.Range(0, GOOD.Length)]);
        }
        if (input.Contains("$C"))
        {
            input = input.Replace("$C", CHEFALIAS[Random.Range(0, CHEFALIAS.Length)]);
        }
        if (input.Contains("$E"))
        {
            input = input.Replace("$E", EVIL[Random.Range(0, EVIL.Length)]);
        }
        if (input.Contains("$F"))
        {
            input = input.Replace("$F", food);
        }


        input += RandomPunctuation();

        if (Random.value < .5f)
        {
            input = char.ToUpper(input[0]) + input.Substring(1); //capitalize
        }

        return input;
    }

    private void SetStars(int starRating)
    {
        Image[] stars = ratingStarParent.GetComponentsInChildren<Image>();

        for(int i = 0; i < 5; i++)
        {
            if(i + 1 > starRating)
            {
                stars[i].color = new Color(0, 0, 0, 0);
            }
        }
    }

    private string RandomPunctuation()
    {
        switch(Random.Range(0, 5))
        {
            case 0:
                return "";
            case 1:
                return ".";
            case 2:
                return "!";
            case 3:
                return ",";
            case 4:
                string exclamations = "!";
                int e = Random.Range(1, 5);
                for(int i = 0; i < e; i++)
                {
                    if(Random.value < 0.35f)
                    {
                        exclamations += "1";
                    } else
                    {
                        exclamations += "!";
                    }
                }
                return exclamations;
            default:
                return "";
        }
    }

    private string HashtagPositive()
    {
        string hashtag = "";
        float r = Random.value;
        if(r < .3f)
        {
            hashtag = GOOD[Random.Range(0, GOOD.Length)];
        } else if (r < .7f)
        {
            hashtag = POSITIVE[Random.Range(0, POSITIVE.Length)];
        }
        else if (r < .8f)
        {
            hashtag = POSITIVE[Random.Range(0, POSITIVE.Length)] + food;
        } else if (r < .9f)
        {
            hashtag = food;
        } else
        {
            hashtag = GOOD[Random.Range(0, GOOD.Length)] + CHEFALIAS[Random.Range(0, CHEFALIAS.Length)];
        }
        hashtag = hashtag.Replace(" ", "");
        return "#" + hashtag;
    }

    public void Randomize(RatingType ratingType, string ratedFood)
    {
        food = ratedFood;
        //randomize profile pic
        Color color1;
        Color color2;
        if (Random.value > .5f)
        {
            color1 = darkColors.Evaluate(Random.value);
            color2 = lightColors.Evaluate(Random.value);
        } 
        else
        {
            color2 = darkColors.Evaluate(Random.value);
            color1 = lightColors.Evaluate(Random.value);
        }
        profileBG.color = color1;
        profilePic.color = color2;
        profilePic.sprite = profilePics[Random.Range(0, profilePics.Length)];

        //randomize username
        string usernameString = "@";
        usernameString += USERNAMES[Random.Range(0, USERNAMES.Length)];
        string secondName = USERNAMES[Random.Range(0, USERNAMES.Length)];
        if(Random.value < .65f && (usernameString + secondName).Length <= 18)
        {
            usernameString = usernameString + secondName;
        }
        if (usernameString.Length <= 12) {
            int numbers = Random.Range(-10, 3);
            if (numbers > 0)
            {
                for (int i = 0; i < numbers; i++)
                {
                    usernameString += Random.Range(0, 9);
                }
            }
        }
        username.text = usernameString;

        //random rating
        string body = "";
        switch (ratingType)
        {
            case RatingType.Fed:
                SetStars(5);
                if (Random.value < .4f)
                {
                    body += StringReplacer(POSITIVECOMMENTS[Random.Range(0, FEDRATINGS.Length)]) + " ";
                }
                if (Random.value < .2f)
                {
                    body += StringReplacer(POSITIVECOMMENTS[Random.Range(0, FEDRATINGS.Length)]) + " ";
                }

                body += StringReplacer(FEDRATINGS[Random.Range(0, FEDRATINGS.Length)]);

                if (Random.value < .3f)
                {
                    body += " " + StringReplacer(POSITIVECOMMENTS[Random.Range(0, FEDRATINGS.Length)]);
                }
                if (Random.value < .2f)
                {
                    body += " " + StringReplacer(POSITIVECOMMENTS[Random.Range(0, FEDRATINGS.Length)]);
                }

                //hashtags
                if(Random.value < .4f)
                {
                    body += " " + HashtagPositive();
                }
                if (Random.value < .15f)
                {
                    body += " " + HashtagPositive();
                }
                break;
            case RatingType.Cooked:
                SetStars(1);
                if (Random.value < .2f)
                {
                    body += StringReplacer(NEGATIVECOMMENTS[Random.Range(0, NEGATIVECOMMENTS.Length)]) + " ";
                }
                body += StringReplacer(COOKEDRATINGS[Random.Range(0, COOKEDRATINGS.Length)]);
                if (Random.value < .2f)
                {
                    body += " " + StringReplacer(NEGATIVECOMMENTS[Random.Range(0, NEGATIVECOMMENTS.Length)]);
                }
                break;
            case RatingType.LiftedUp:
                SetStars(3);
                if (Random.value < .2f)
                {
                    body += StringReplacer(NEGATIVECOMMENTS[Random.Range(0, NEGATIVECOMMENTS.Length)]) + " ";
                }
                body += StringReplacer(LIFTEDRATINGS[Random.Range(0, LIFTEDRATINGS.Length)]);
                if (Random.value < .2f)
                {
                    body += " " + StringReplacer(NEGATIVECOMMENTS[Random.Range(0, NEGATIVECOMMENTS.Length)]);
                }
                break;
            case RatingType.ThrownOut:
                SetStars(3);
                if (Random.value < .2f)
                {
                    body += StringReplacer(NEGATIVECOMMENTS[Random.Range(0, NEGATIVECOMMENTS.Length)]) + " ";
                }
                body += StringReplacer(LIFTEDRATINGS[Random.Range(0, LIFTEDRATINGS.Length)]);
                if (Random.value < .2f)
                {
                    body += " " + StringReplacer(NEGATIVECOMMENTS[Random.Range(0, NEGATIVECOMMENTS.Length)]);
                }
                break;
            case RatingType.WrongFood:
                SetStars(3);
                if (Random.value < .2f)
                {
                    body += StringReplacer(NEGATIVECOMMENTS[Random.Range(0, NEGATIVECOMMENTS.Length)]) + " ";
                }
                body += StringReplacer(LIFTEDRATINGS[Random.Range(0, LIFTEDRATINGS.Length)]);
                if (Random.value < .2f)
                {
                    body += " " + StringReplacer(NEGATIVECOMMENTS[Random.Range(0, NEGATIVECOMMENTS.Length)]);
                }
                break;
            default:
                break;
        }
        bodyText.text = body;
    }
}
