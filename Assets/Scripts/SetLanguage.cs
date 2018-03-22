using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLanguage : MonoBehaviour {

    


    public void SetEnglish()
    {
        Language.language = Language.Lang.enUS;
        Language.UpdateTextLagunage();    
    }

    public void SetSpanish()
    {

        Language.language = Language.Lang.esES;
        Language.UpdateTextLagunage();
    }

   
}

