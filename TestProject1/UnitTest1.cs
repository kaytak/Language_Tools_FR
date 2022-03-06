using NUnit.Framework;
using Language_Tools_FR;
using System;
using System.Linq;
using static Language_Tools_FR.VerbConjugation;
using System.Text;
using System.Collections.Generic;
using System.Text.Json;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Console.WriteLine("Hello World!");
            VerbConjugation verb = new VerbConjugation();

            //var oo = verb.list.Select(v=> new String[2] { v.verb,v.translation } ).Distinct().ToList();
            var oo = verb.list.Where(v => v.tense == VerbTense.Présent && v.number == VerbNumber.singular && v.subject == VerbSubject.firstPerson).ToList();
            //var oo = verb.list.Where(v => v.verb == "plaire").ToList();

            Console.OutputEncoding = Encoding.UTF8;
            foreach (var v1 in oo)
            {
                Console.WriteLine("\"" + v1.verb + " : " + v1.translation + "\",");
                //  Console.WriteLine(v1.typicalSubject +" "+ v1.conjVerb);
            }

            List<SingleVerb> verb2 = new List<SingleVerb>();
            verb.list.ForEach(v => verb2.Add(new SingleVerb { verb = v.verb, conjTp = v.conjType, conjV = v.conjVerb, tense = v.tense, gender = v.gender, subj = v.subject, num = v.number, pronom = v.pronom }));
            string jsonString = JsonSerializer.Serialize(verb2);
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            VerbConjugation vdata = new VerbConjugation();

            //var oo = verb.list.Select(v=> new String[2] { v.verb,v.translation } ).Distinct().ToList();

            List<FrVerb> vlist = vdata.list.Where(v => v.verb == "aller").ToList();
            Assert.Pass();

        }
    }
}