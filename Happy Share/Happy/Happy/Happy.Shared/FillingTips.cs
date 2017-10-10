using System;
using System.Collections.Generic;
using System.Text;

namespace Happy
{
    class FillingTips
    {
        public static List<Tips> addItem()
        {
            List<Tips> tipsList=new List<Tips>();
            tipsList.Add(new Tips { tipText = "Do things for others", tipImage = "Assets/giving.png", tipTitle = "Giving" });
            tipsList.Add(new Tips { tipText = "Connect with people", tipImage = "Assets/relating.png", tipTitle = "Relating" });
            tipsList.Add(new Tips { tipText = "Take care of your body", tipImage = "Assets/exercising.png", tipTitle = "Exercising" });
            tipsList.Add(new Tips { tipText = "Notice the world around", tipImage = "Assets/appreciating.png", tipTitle = "Appreciating" });
            tipsList.Add(new Tips { tipText = "Keep learning new things", tipImage = "Assets/trying_out.png", tipTitle = "Trying Out" });
            tipsList.Add(new Tips { tipText = "Have goals to look forward to", tipImage = "Assets/direction.png", tipTitle = "Direction" });
            tipsList.Add(new Tips { tipText = "Find ways to bounce back", tipImage = "Assets/resilience.png", tipTitle = "Resilience" });
            tipsList.Add(new Tips { tipText = "Take a positive approach", tipImage = "Assets/emotion.png", tipTitle = "Emotion" });
            tipsList.Add(new Tips { tipText = "Be comfortable with who you are", tipImage = "Assets/acceptance.png", tipTitle = "Acceptance" });
            tipsList.Add(new Tips { tipText = "Be part of something bigger", tipImage = "Assets/meaning.png", tipTitle = "Meaning" });


            return tipsList;
        }
    }
}
