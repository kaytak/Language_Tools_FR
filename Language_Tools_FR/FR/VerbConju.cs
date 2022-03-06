using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Language_Tools_FR
{
    public class VerbConjugation
    {
        public class FrVerb {
            public string verb { get; set; }
            public string conjVerb { get; set; }
            public string translation { get; set; }
            public string translationJ { get; set; }
            public VerbConjType conjType { get; set; }
            public VerbTense tense { get; set; }
            public VerbGender gender { get; set; }
            public VerbSubject subject { get; set; }
            public VerbNumber number { get; set; }
            public bool pronom { get; set; }
            public string expression // Not implimented
            {
                get {
                    if (subject == VerbSubject.firstPerson && number == VerbNumber.singular)
                    {
                        var firstl = conjVerb.Substring(0, 1);
                        if (firstl == "a"|| firstl == "i" || firstl == "u" || firstl == "e" || firstl == "o")
                        {
                            return "j'" + conjVerb;
                        }
                        else
                        {
                            return "je " + conjVerb;
                        }
                    }
                    else
                    {
                        return typicalSubject + " "+conjVerb;
                    }
                }
            }
            public string tenseName
            {
                get { return tense.ToString().Replace("_"," "); }
            }
            
            public string typicalSubject // Not implimented
            {
                get
                {
                    switch (subject)
                    {
                        case VerbSubject.firstPerson:
                            if (number == VerbNumber.singular) return "je, j'";
                            if (number == VerbNumber.plural) return "nous";
                                break;
                        case VerbSubject.secondPerson:
                            if (number == VerbNumber.singular) return "tu";
                            if (number == VerbNumber.plural) return "vous";
                            break;
                        case VerbSubject.thirdPerson:
                            if (number == VerbNumber.singular&&gender==VerbGender.masculin) return "il";
                            if (number == VerbNumber.singular && gender == VerbGender.féminin) return "elle";
                            if (number == VerbNumber.plural && gender == VerbGender.masculin) return "ils";
                            if (number == VerbNumber.plural && gender == VerbGender.féminin) return "elles";
                            break;
                    }
                    return "";
                }
            }
            public FrVerb clone()
            {
                FrVerb clone1 = new FrVerb();
                clone1.verb = this.verb;
                clone1.conjVerb = this.conjVerb;
                clone1.translation = this.translation;
                clone1.translationJ = this.translationJ;
                clone1.conjType = this.conjType;
                clone1.tense = this.tense;
                clone1.gender = this.gender;
                clone1.subject = this.subject;
                clone1.number = this.number;
                return clone1;
            }
        }
        public enum VerbSubject
        {
            undefined,firstPerson,secondPerson,thirdPerson
        }
        public enum VerbGender
        {
            undefined, masculin, féminin
        }
        public enum VerbNumber
        {
            undefined, singular, plural
        }
        public enum VerbConjType
        {
            undefined, other_irregulars, er_verbs, stem_changing_verbs
        }
        public enum VerbTense
        {
            undefined, Présent, Passé_composé, Impératif, Aller_infinitif, Imparfait, Futur, Conditionnel, Subjonctif, Plus_que_parfait, Conditionnel_passé, Subjonctif_passé
        }
        public VerbConjugation()
        {
            _verbs = new List<FrVerb>();
            initialize();
        }
        List<FrVerb> _verbs;
        public FrVerb Find(Predicate<FrVerb> f)
        {
            return _verbs.Find(f);
        }
        public List<FrVerb> list
        {
            get { return _verbs; }
        }
        void setConj(FrVerb verb, string[] fields)
        {
            FrVerb vr1 = verb.clone();
            /*if (verb.pronom)
            {
                for (int tenseNum = 0; tenseNum < 11; tenseNum++)
                {
                    vr1.tense = (VerbTense)tenseNum;
                    vr1.conjVerb = fields[2*tenseNum + 1]+" "+ fields[2 * tenseNum + 1]; _verbs.Add(vr1.clone());
                }
            }
            else
            {*/
            for (int tenseNum = 1; tenseNum < 12; tenseNum++)
            {
                vr1.tense = (VerbTense)tenseNum;
                vr1.conjVerb = fields[tenseNum ];
                string vrb = vr1.conjVerb;
                if (vrb.Length>3 &&vrb.Substring(vrb.Length - 3, 3) == "(e)")
                {
                    if (vr1.gender == VerbGender.masculin) vr1.conjVerb = vrb[0..^3];
                    if (vr1.gender == VerbGender.féminin) vr1.conjVerb = vrb[0..^3] + "e";
                }
                if (vrb.Length > 4 && vrb.Substring(vrb.Length - 4, 4) == "(e)s")
                {
                    if (vr1.gender == VerbGender.masculin) vr1.conjVerb = vrb[0..^4]+"s";
                    if (vr1.gender == VerbGender.féminin) vr1.conjVerb = vrb[0..^4] + "es";
                }
                if (vrb.Length > 6 && vrb.Substring(vrb.Length - 6, 6) == "(e)(s)")
                {
                    if (vr1.gender == VerbGender.masculin && vr1.number==VerbNumber.singular) vr1.conjVerb = vrb[0..^6] ;
                    if (vr1.gender == VerbGender.masculin && vr1.number == VerbNumber.plural) vr1.conjVerb = vrb[0..^6] + "s";
                    if (vr1.gender == VerbGender.féminin && vr1.number == VerbNumber.singular) vr1.conjVerb = vrb[0..^6] + "e";
                    if (vr1.gender == VerbGender.féminin && vr1.number == VerbNumber.plural) vr1.conjVerb = vrb[0..^6] + "es";
                }
                _verbs.Add(vr1.clone());
            }
            //}
            /*            vr1.tense = VerbTense.Présent;
                        vr1.conjVerb = fields[1];  _verbs.Add(vr1.clone());
                        vr1.tense = VerbTense.Passé_composé;
                        vr1.conjVerb = fields[2]; _verbs.Add(vr1.clone());
                        vr1.tense = VerbTense.Impératif;
                        vr1.conjVerb = fields[3]; _verbs.Add(vr1.clone());
                        vr1.tense = VerbTense.Aller_infinitif;
                        vr1.conjVerb = fields[4]; _verbs.Add(vr1.clone());
                        vr1.tense = VerbTense.Imparfait;
                        vr1.conjVerb = fields[5]; _verbs.Add(vr1.clone());
                        vr1.tense = VerbTense.Futur;
                        vr1.conjVerb = fields[6]; _verbs.Add(vr1.clone());
                        vr1.tense = VerbTense.Conditionnel;
                        vr1.conjVerb = fields[7]; _verbs.Add(vr1.clone());
                        vr1.tense = VerbTense.Subjonctif;
                        vr1.conjVerb = fields[8]; _verbs.Add(vr1.clone());
                        vr1.tense = VerbTense.Plus_que_parfait;
                        vr1.conjVerb = fields[9]; _verbs.Add(vr1.clone());
                        vr1.tense = VerbTense.Conditionnel_passé;
                        vr1.conjVerb = fields[10]; _verbs.Add(vr1.clone());
                        vr1.tense = VerbTense.Subjonctif_passé;
                        vr1.conjVerb = fields[11]; _verbs.Add(vr1.clone());*/
        }
        void setConjType(FrVerb verb, string conju)
        {
            switch (conju)
            {
                case "stem changing verbs":
                    verb.conjType = VerbConjType.stem_changing_verbs;
                    break;
            }
        }
        void checkPronom(FrVerb verb)
        {
            if(verb.verb.Substring(0,3)=="se " || verb.verb.Substring(0, 2)== "s'")
            {
                verb.pronom = true;
            }
            else
            {
                verb.pronom = false;
            }
        }
        void initialize()
        {
            var assembly = Assembly.GetExecutingAssembly();
            System.IO.Stream sr = assembly.GetManifestResourceStream("Language_Tools_FR.Resources.verbs.txt");

            using (TextFieldParser parser = new TextFieldParser(sr))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters("\t");

                FrVerb verb1 = new FrVerb();
                bool EndOfFile=false;
                bool NextVerb = false;

                //skip licence statement.
                parser.ReadFields(); parser.ReadFields(); parser.ReadFields();

                string[] buf1 = parser.ReadFields();
                verb1.verb = buf1[0];
                checkPronom(verb1);
                verb1.translation = parser.ReadFields()[0];
                setConjType(verb1, parser.ReadFields()[0]);
                while ((!parser.EndOfData)&&!EndOfFile)
                {
                    //Processing row
                    FrVerb v1 = verb1.clone();
                    string[] fields = parser.ReadFields();
                    switch (fields[0])
                    {
                        case "1":
                            v1.gender = VerbGender.masculin;
                            v1.subject = VerbSubject.firstPerson;
                            v1.number = VerbNumber.singular;
                            break;
                        case "2":
                            v1.gender = VerbGender.masculin;
                            v1.subject = VerbSubject.secondPerson;
                            v1.number = VerbNumber.singular;
                            break;
                        case "3":
                            v1.gender = VerbGender.masculin;
                            v1.subject = VerbSubject.thirdPerson;
                            v1.number = VerbNumber.singular;
                            break;
                        case "4":
                            v1.gender = VerbGender.masculin;
                            v1.subject = VerbSubject.firstPerson;
                            v1.number = VerbNumber.plural;
                            break;
                        case "5":
                            v1.gender = VerbGender.masculin;
                            v1.subject = VerbSubject.secondPerson;
                            v1.number = VerbNumber.plural;
                            break;
                        case "6":
                            v1.gender = VerbGender.masculin;
                            v1.subject = VerbSubject.thirdPerson;
                            v1.number = VerbNumber.plural;
                            break;
                        case "#EOF":
                            EndOfFile = true;
                            break;
                        default:
                            //buf1 = parser.ReadFields();
                            verb1.verb = fields[0];//buf1[0];
                            checkPronom(verb1);
                            verb1.translation = parser.ReadFields()[0];
                            setConjType(verb1, parser.ReadFields()[0]);
                            NextVerb = true;
                            break;
                    }
                    if ((!EndOfFile) && (!NextVerb)) 
                    { setConj(v1, fields); } 
                    else
                    {
                        NextVerb = false;
                    }

                 //   foreach (string field in fields)
                 //   {
                        //TODO: Process field
                 //   }
                }
            }
        }
    }
}
