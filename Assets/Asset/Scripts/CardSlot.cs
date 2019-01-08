using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour {

    private int currentCard = 0;

    public int CurrentCard {
        get
        {
            return currentCard;
        }
        set
        {
            CardImage.texture = cards.cards[value];
            currentCard = value;
        }
    }

    public Cards cards;
    public RawImage CardImage;

	// Use this for initialization
	void Start () {
        if (cards == null) Debug.LogWarning("Forgot to set cards reference on this object ("+name+")");
        CardImage = GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
