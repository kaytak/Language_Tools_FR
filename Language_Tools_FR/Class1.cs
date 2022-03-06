namespace Language_Tools_FR
{
    public class Class1
    {

    }
        public class SingleVerb
    {
        public string verb { get; set; }
        public string conjV { get; set; }
        //public string translation { get; set; }
        //public string translationJ { get; set; }
        public VerbConjType conjTp { get; set; }
        public VerbTense tense { get; set; }
        public VerbGender gender { get; set; }
        public VerbSubject subj { get; set; }
        public VerbNumber num { get; set; }
        public bool pronom { get; set; }
    }
}