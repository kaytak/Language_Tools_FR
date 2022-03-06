using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Language_Tools_FR.VerbConjugation;

namespace Language_Tools_FR
{
    public class SentenceHelper
    {
        enum MacroWord { Subject,Verb, COD_NonPronominaux, Null}
        class Segment
        {
            public List<Segment> subSeg { get; set; }
            public string word { get; set; }
            public WordGender gender { get; set; }
            public WordNumber number { get; set; }
            public MacroWord type { get; set; }
        }
        class Sentence
        {
            public List<Segment> segments;
            public string verb1 { get; set; }
            public string translationEN { get; set; }
            public Sentence()
            {
                segments = new List<Segment>();
            }
        }
        List<Sentence> _sentenceStore;
        VerbConjugation _verbData;
        LangSetFR _langSetFR;

        public SentenceHelper() {
            _sentenceStore = new List<Sentence>();
            _verbData = new VerbConjugation();
            _langSetFR = new LangSetFR();
        }
        public void addSentence(string sentence1,string verb1,string transEN)
        {
            Sentence sentence = new Sentence();
            sentence.verb1 = verb1;
            sentence.translationEN = transEN;
            string[] input = sentence1.Split();
            char[] TrimChar = { '.'};
            foreach (var seg in input)
            {
                Segment s = new Segment();
                s.word = seg.Trim().Trim(TrimChar);
                switch (s.word)
                {
                    case "##SUBJECT":
                        s.type = MacroWord.Subject;
                        break;
                    case "##COD_NONPRN":
                        s.type = MacroWord.COD_NonPronominaux;
                        break;
                    case "##VERB":
                        s.type = MacroWord.Verb;
                        break;
                    default:
                        s.type = MacroWord.Null;
                        break;
                }
                   
                sentence.segments.Add(s);
            }
            _sentenceStore.Add(sentence);
        }
        public string parse(int idx, VerbSubject vSubject,VerbGender vGender,VerbNumber vNumber,VerbTense vTense)
        {
            var sentence=_sentenceStore[idx];
            List<FrVerb> vlist = _verbData.list.Where(v => v.verb == sentence.verb1 && v.tense == vTense && v.subject==vSubject && v.number==vNumber).ToList();
            string output = "";
            foreach(var seg in sentence.segments)
            {
                switch (seg.word)
                {
                    case "##SUBJECT":
                        seg.word= _langSetFR.getTypicalSubject(vSubject, vNumber, vGender);
                        break;
                    case "##COD_NONPRN":
                        seg.word = _langSetFR.getCODotherThan(vSubject, vNumber, vGender);
                        break;
                    case "##VERB":
                        seg.word = vlist.FirstOrDefault().conjVerb;
                        var buf1 = seg.word.Split(" ");
                        if (buf1.Length > 1)
                        {
                            var seg2 = new Segment();
                            seg.subSeg = new List<Segment>();
                            seg.subSeg.Add(seg2);
                        }
                        break;
                    default:
                        
                        break;
                }
            }
            foreach (var seg in sentence.segments)
            {
                output = output + seg.word + " ";
            }
            return output.Trim();
        }

    }
}
