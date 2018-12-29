using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CardGenerator : MonoBehaviour {

    public PostProcessingProfile profile;
    public List<CardSlot> slots = new List<CardSlot>();
    public List<CardSlot> behindSlots = new List<CardSlot>();
    public Cards cards;
    /// <summary>
    /// Called when the slots have changed and the animation has finished
    /// </summary>
    public System.Action CardsChanged;
    /// <summary>
    /// Time in seconds it takes to shuffle
    /// </summary>
    public float shuffleTime = 8;

    /// <summary>
    /// Time it takes to set the opacity of each card to 0 
    /// </summary>
    public float hideCardSpeed = .2f;

    bool isShuffling = false;
    List<Interpolation> shuffleEffectsInterpolation;
    Interpolation[] stopCardDelays;
    Interpolation[] slotsInterpolations;

    public void Start()
    {
    }
    public void InitializeSlotsInterpolations()
    {
        slotsInterpolations = new Interpolation[slots.Count];
        for (int i = 0; i < slots.Count; i++)
        {
            slotsInterpolations[i] = InitializeSlotInterpolation(i, hideCardSpeed);
        }
    }
    public Interpolation InitializeSlotInterpolation(int index, float length)
    {
        var slotsInterpolation = new Interpolation(length);
        slotsInterpolation.AddKeyFrame(new Interpolation.KeyFrame(0, 1)); // Opacity 1
        slotsInterpolation.AddKeyFrame(new Interpolation.KeyFrame(length, 0)); // Opacity 0
        var slot = slots[index];
        slotsInterpolation.FrameCallback += (kf) =>
        {
            //var c = slot.CardImage.color;
            //slot.CardImage.color = new Color(c.r, c.g, c.b, kf.value);
        };
        return slotsInterpolation;
    }

    public void InitializeStopCardDelays()
    {
        stopCardDelays = new Interpolation[slots.Count];
        var startDuration = shuffleTime * .9f  - (slots.Count - 1) * 3;
        for (int i = 0; i < slots.Count; i++)
        {
            var interpDuration = startDuration + i * 3f;
            var interp = new Interpolation(interpDuration);
            interp.AddKeyFrame(0, hideCardSpeed);
            interp.AddKeyFrame(interpDuration * .6f, hideCardSpeed);
            interp.AddKeyFrame(interpDuration, hideCardSpeed * 10);
            stopCardDelays[i] = interp;

        }
    }

    void Update()
    {
        if (isShuffling)
        {
            shuffleEffectsInterpolation.All((x) => { x.Next(); return true; }); // Total shuffle time
            
            List<int> randNoRepeat = new List<int>(Enumerable.Range(0, cards.cards.Count));
            bool allCardsEnded = true;
            // Switch behind cards (opacity 1) to fore cards (opacity 0)
            for (int i = 0; i < slots.Count; i++)
            {
                float newDelay = stopCardDelays[i].Next().value;
                bool slotFinished = stopCardDelays[i].HasEnded;
                if (slotsInterpolations[i].HasEnded)
                {
                    slots[i].CardImage.texture = behindSlots[i].CardImage.texture;
                    if (!slotFinished)
                    {
                        slotsInterpolations[i] = InitializeSlotInterpolation(i, newDelay);
                        int ridx = Random.Range(0, randNoRepeat.Count - 1);
                        if (behindSlots[i].CurrentCard == randNoRepeat[ridx])
                        {
                            ridx++;
                        }
                        behindSlots[i].CurrentCard = randNoRepeat[ridx];
                        allCardsEnded &= false;
                    }
                    else
                    {
                        allCardsEnded &= true;
                    }
                }
                else
                {
                    slotsInterpolations[i].Next();
                }
                randNoRepeat.Remove(behindSlots[i].CurrentCard);
            }
            if (allCardsEnded && shuffleEffectsInterpolation.All((x) => x.HasEnded))
            {
                isShuffling = false;
                if (CardsChanged != null) CardsChanged(); // Notify the cards have changed and the animation has finished
                return;
            }
            //if (slotsInterpolation.HasEnded)
            //{
            //    slotsInterpolation.Reset();
            //    // Switch behind cards (opacity 1) to fore cards (opacity 0)
            //    for (int i = 0; i < slots.Count; i++)
            //    {
            //        slots[i].CardImage.sprite = behindSlots[i].CardImage.sprite;
            //    }

            //    List<int> randNoRepeat = new  List<int>(Enumerable.Range(0, cards.cards.Count));
            //    // Randomly choose another behind card
            //    for(int i = 0; i < behindSlots.Count; i++)
            //    {
            //        var slot = behindSlots[i];
            //        if(2 > randNoRepeat.Count)
            //        {
            //            throw new System.Exception("You need more cards to be able of generating cards randomly");
            //        }
            //        int ridx = Random.Range(0, randNoRepeat.Count-1);
            //        if(slot.CurrentCard == randNoRepeat[ridx])
            //        {
            //            ridx++;
            //        }
            //        slot.CurrentCard = randNoRepeat[ridx];
            //        randNoRepeat.RemoveAt(ridx);
            //    }

            //    if (shuffleInterpolation.HasEnded)
            //    {
            //        isShuffling = false;
            //        if (CardsChanged != null) CardsChanged(); // Notify the cards have changed and the animation has finished
            //        return;
            //    }
            //}
            //slotsInterpolation.Next(); // Reduces this slot opacity
           
        }
    }

    public void InitializeShuffleEffectsInterpolation()
    {
        shuffleEffectsInterpolation = new List<Interpolation>();
        
        var saturationEffect = new Interpolation(shuffleTime);
        saturationEffect.AddKeyFrame(new Interpolation.KeyFrame(0, profile.colorGrading.settings.basic.saturation));
        saturationEffect.AddKeyFrame(new Interpolation.KeyFrame(.5f, .5f));
        saturationEffect.AddKeyFrame(new Interpolation.KeyFrame(shuffleTime * .96f, .3f));
        saturationEffect.AddKeyFrame(new Interpolation.KeyFrame(shuffleTime, profile.colorGrading.settings.basic.saturation));
        saturationEffect.FrameCallback += (kf) =>
        {
            if (profile != null)
            {
                var settings = profile.colorGrading.settings;
                settings.basic.saturation = kf.value;
                profile.colorGrading.settings = settings;
            }
        };
        shuffleEffectsInterpolation.Add(saturationEffect);


        var contrastEffect = new Interpolation(shuffleTime);
        contrastEffect.AddKeyFrame(new Interpolation.KeyFrame(0, profile.colorGrading.settings.basic.contrast));
        contrastEffect.AddKeyFrame(new Interpolation.KeyFrame(.5f, 1.8f));
        contrastEffect.AddKeyFrame(new Interpolation.KeyFrame(shuffleTime * .96f, 2.0f));
        contrastEffect.AddKeyFrame(new Interpolation.KeyFrame(shuffleTime, profile.colorGrading.settings.basic.contrast));
        contrastEffect.FrameCallback += (kf) =>
        {
            if (profile != null)
            {
                var settings = profile.colorGrading.settings;
                settings.basic.contrast = kf.value;
                profile.colorGrading.settings = settings;
            }
        };
        shuffleEffectsInterpolation.Add(contrastEffect);


        var vignetteEffect = new Interpolation(shuffleTime);
        vignetteEffect.AddKeyFrame(new Interpolation.KeyFrame(0, profile.vignette.settings.intensity));
        vignetteEffect.AddKeyFrame(new Interpolation.KeyFrame(.5f, .2f));
        vignetteEffect.AddKeyFrame(new Interpolation.KeyFrame(shuffleTime * .96f, .24f));
        vignetteEffect.AddKeyFrame(new Interpolation.KeyFrame(shuffleTime, profile.vignette.settings.intensity));
        vignetteEffect.FrameCallback += (kf) =>
        {
            if (profile != null)
            {
                var settings = profile.vignette.settings;
                settings.intensity= kf.value;
                profile.vignette.settings = settings;
            }
        };
        shuffleEffectsInterpolation.Add(vignetteEffect);


        var grainEffect = new Interpolation(shuffleTime);
        grainEffect.AddKeyFrame(new Interpolation.KeyFrame(0, profile.grain.settings.intensity));
        grainEffect.AddKeyFrame(new Interpolation.KeyFrame(.5f, .3f));
        grainEffect.AddKeyFrame(new Interpolation.KeyFrame(shuffleTime * .96f, 1f));
        grainEffect.AddKeyFrame(new Interpolation.KeyFrame(shuffleTime, profile.grain.settings.intensity));
        grainEffect.FrameCallback += (kf) =>
        {
            if (profile != null)
            {
                var settings = profile.grain.settings;
                settings.intensity = kf.value;
                profile.grain.settings = settings;
            }
        };
        shuffleEffectsInterpolation.Add(grainEffect);

    }

    public void Shuffle()
    {
        if (!isShuffling)
        {
            Debug.Log("Start shuffling");

            InitializeShuffleEffectsInterpolation();
            InitializeSlotsInterpolations();
            InitializeStopCardDelays();

            // Randomly choose another behind card
            if(cards.cards.Count == 0)
            {
                Debug.LogError("There are no cards to shuffle!");
                return;
            }

            foreach (var slot in behindSlots)
            {
                // This way you will never get the same card randomly
                slot.CurrentCard = (slot.CurrentCard + Random.Range(1, cards.cards.Count)) % cards.cards.Count;
            }
            isShuffling = true;
        }
    }

}
