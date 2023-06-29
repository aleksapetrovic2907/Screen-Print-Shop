using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Aezakmi.PrintSystem
{
    public class StepsCountManager : GloballyAccessibleBase<StepsCountManager>
    {
        public int currentStep;
        public int totalSteps;

        [SerializeField] private List<Image> stepsImages;
        [SerializeField] private Sprite currentStepSprite;
        [SerializeField] private Sprite completedStepSprite;
        [SerializeField] private Sprite upcomingStepSprite;

        public void MoveToNextStep()
        {
            stepsImages[currentStep].sprite = completedStepSprite;

            if(++currentStep == stepsImages.Count) return;

            stepsImages[currentStep].sprite = currentStepSprite;
        }

        public void ResetSteps()
        {
            foreach (var img in stepsImages)
            {
                img.sprite = upcomingStepSprite;
            }

            stepsImages[0].sprite = currentStepSprite;
        }
    
        public void ResetToSecondStep()
        {
            stepsImages[0].sprite = completedStepSprite;

            currentStep = 1;

            stepsImages[currentStep].sprite = currentStepSprite;

            for(int i = 2; i < stepsImages.Count; i++)
                stepsImages[i].sprite = upcomingStepSprite;
        }
    }
}