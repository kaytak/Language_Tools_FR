# Language_Tools_FR
Grammatical utility tools for French
Dictionary based French verb conjugation.

Example
            VerbConjugation vdata = new VerbConjugation();
            List<FrVerb> vlist = vdata.list.Where(v => v.verb == "aller").ToList();

Copyright disclamer of the original French verb data:
 Copyright 2006 • CC BY • First Year French • University of Texas at Austin
 https://www.laits.utexas.edu/fi/vcr/
 https://creativecommons.org/licenses/by/4.0/
