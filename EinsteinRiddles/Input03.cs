using System;
using System.Collections.Generic;
using System.Text;

namespace EinsteinRiddles
{
    class Input03
    {
        public Assertion[] Assertions;
        public List<Family> Families;
        public String Info;

        public Input03()
        {
            // MOYEN 122 5x5
            Info = "PROBLEME Moyen #122 5x5";

            Families = new List<Family> {
                new Family("Prénoms", new List<String> { "Sarah", "Camille", "Paul", "Ryan", "Florian" }),
                new Family("Coiffes", new List<String> { "Chapeau", "Casquette", "Casque", "Bonnet", "Chapeau de Sorcière"}),
                new Family("Météo", new List<String> { "Orage", "Nuage", "Eclipse", "Neige", "Nuit" }),
                new Family("Metiers", new List<String> { "Policier", "Pilote", "Peintre", "Dentiste", "Ingenieur" }),
                new Family("Medecine", new List<String> { "Scalpel", "Microscope", "Pilule", "Attelle", "Poche de sang" })
             };

            Assertions = new Assertion[] {
                new Assertion(1, "Scalpel", "Peintre", false, true),
                new Assertion(2, "Policier", "Microscope", false, true),
                new Assertion(3, "Paul", "Neige", true, true),
                new Assertion(4, "Sarah", "Eclipse", false, true),
                new Assertion(5, "Camille", "Peintre", false, true),
                new Assertion(6, "Casque", "Poche de sang", true, true),
                new Assertion(7, "Pilote", "Nuage", true, true),
                new Assertion(8, "Ingenieur", "Neige", false, true),
                new Assertion(9, "Poche de sang", "Nuage", true, true),
                new Assertion(10, "Ingenieur", "Eclipse", false, true),
                new Assertion(11, "Attelle", "Nuit", false, true),
                new Assertion(12, "Ingenieur", "Pilule", false, true),
                new Assertion(13, "Ryan", "Pilote", true, true),
                new Assertion(14, "Neige", "Chapeau de Sorcière", true, true),
                new Assertion(15, "Microscope", "Chapeau", false, true),
                new Assertion(16, "Orage", "Attelle", false, true),
                new Assertion(17, "Nuit", "Pilule", false, true),
                new Assertion(18, "Florian", "Ingenieur", false, true),
                new Assertion(19, "Dentiste", "Eclipse", false, true),
                new Assertion(20, "Florian", "Eclipse", false, true),
                new Assertion(21, "Scalpel", "Neige", true, true),
                new Assertion(22, "Pilule", "Casquette", true, true),
                new Assertion(23, "Florian", "Casquette", true, true),
                new Assertion(24, "Camille", "Chapeau", true, true),
                new Assertion(25, "Ryan", "Peintre", false, true),
                new Assertion(26, "Dentiste", "Chapeau", false, true),
                new Assertion(27, "Nuage", "Microscope", false, true)
            };

            // Gather families for items in the assertions
            for (int i = 0; i < Assertions.Length; i++)
            {
                Assertions[i].familyOfElementOne = getFamilyNameForItem(Assertions[i].elementOne);
                Assertions[i].familyOfElementTwo = getFamilyNameForItem(Assertions[i].elementTwo);
            }
        }

        private string getFamilyNameForItem(string item)
        {
            foreach (var family in Families)
            {
                if (family.Items.Contains(item))
                {
                    return family.Name;
                }
            }

            throw new ArgumentException("FATAL ERROR : COULDN'T FIND A FAMILY FOR THIS ITEM : " + item);
        }
    }
}


