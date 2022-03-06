using Language_Tools_FR;
using Microsoft.JSInterop;
using System.Text.Json;
using static Language_Tools_FR.VerbConjugation;

namespace WasmInterface
{
    public class LangTools
    {

        [JSInvokable]
        public string GetHelloMessage(String Name) => $"Hello, {Name}!";

        [JSInvokable]
        public static Task GetVerbConjJson(string verb_word)
        {
            VerbConjugation vdata = new VerbConjugation();

            //var oo = verb.list.Select(v=> new String[2] { v.verb,v.translation } ).Distinct().ToList();
            
            List<FrVerb> vlist = vdata.list.Where(v => v.verb == verb_word).ToList();

            Console.WriteLine(vlist.Count());
            
            //return Task.CompletedTask;
            return Task.FromResult(JsonSerializer.Serialize(vlist));
        }

        [JSInvokable]
        public static Task GetVerbConj(string verb_word,VerbTense tense)
        {
            VerbConjugation vdata = new VerbConjugation();

            //var oo = verb.list.Select(v=> new String[2] { v.verb,v.translation } ).Distinct().ToList();

            List<FrVerb> vlist = vdata.list.Where(v => v.verb == verb_word && v.tense==tense).ToList();

            Console.WriteLine(vlist.Count());

            //return Task.CompletedTask;
            return Task.FromResult(vlist);
        }
    }
}
