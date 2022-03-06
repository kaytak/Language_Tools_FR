using System;
using System.Collections.Generic;
using System.Text;
using static Language_Tools_FR.FR.VerbConjugation;

namespace Language_Tools_FR.FR
{
    class LangSetFR
    {
        Random _random;
        public LangSetFR()
        {
            _random = new Random();
        }
        public string RandomElem(List<string> list)
        {
            //var list = new List<string> { "one", "two", "three", "four" };
            int index = _random.Next(list.Count);
            return(list[index]);
        }
        public string getTypicalSubject(VerbSubject subject, VerbNumber number,VerbGender gender)
        {

                switch (subject)
                {
                    case VerbSubject.firstPerson:
                        if (number == VerbNumber.singular) return "je";
                        if (number == VerbNumber.plural) return "nous";
                        break;
                    case VerbSubject.secondPerson:
                        if (number == VerbNumber.singular) return "tu";
                        if (number == VerbNumber.plural) return "vous";
                        break;
                    case VerbSubject.thirdPerson:
                        if (number == VerbNumber.singular && gender == VerbGender.masculin) return "il";
                        if (number == VerbNumber.singular && gender == VerbGender.féminin) return "elle";
                        if (number == VerbNumber.plural && gender == VerbGender.masculin) return "ils";
                        if (number == VerbNumber.plural && gender == VerbGender.féminin) return "elles";
                        break;
                }
                return "";
        }
        public string getCODotherThan(VerbSubject subject, VerbNumber number, VerbGender gender)
        {
            List<string> cod = new List<string> { "me","te","le", "la","nous","vous","les" };
            switch (subject)
            {
                case VerbSubject.firstPerson:
                    if (number == VerbNumber.singular)  cod.Remove("me");
                    if (number == VerbNumber.plural) cod.Remove( "nous");
                    break;
               /* case VerbSubject.secondPerson:
                    if (number == VerbNumber.singular) return "tu";
                    if (number == VerbNumber.plural) return "vous";
                    break;
                case VerbSubject.thirdPerson:
                    if (number == VerbNumber.singular && gender == VerbGender.masculin) return "il";
                    if (number == VerbNumber.singular && gender == VerbGender.féminin) return "elle";
                    if (number == VerbNumber.plural && gender == VerbGender.masculin) return "ils";
                    if (number == VerbNumber.plural && gender == VerbGender.féminin) return "elles";
                    break;*/
            }
            return RandomElem(cod);
        }
    }
    public enum WordGender
    {
        masculin, féminin
    }
    public enum WordNumber
    {
        singular, plural
    }
    public enum ListCOD
    {
        me,te,le, la,nous,vous,les
    }
    public enum ListCOI
    {
        me, te, lui, nous, vous, leur
    }
}
