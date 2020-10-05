using System;
using System.Collections.Generic;
using System.Text;

namespace EinsteinRiddles
{
    public class Input01 : IInputData
    {
        private Assertion[] m_assertions;
        public Assertion[] Assertions { get { return m_assertions; } }

        private List<Family> m_families;
        public List<Family> Families { get { return m_families; } }
        
        private String m_Info;
        public String Info { get { return m_Info; } }

        public Input01()
        {
            // FACILE 408
            m_Info = "PROBLEME FACILE #408 4x4";

            m_families = new List<Family> {
                new Family("Véhicules", new List<String> { "Ambulance", "Voiture", "Camion", "Char" }),
                new Family("Monnaies", new List<String> { "Billet de 5", "Billet de 10", "Billet de 20", "Billet de 50" }),
                new Family("Metiers", new List<String> { "Enseignant", "Policier", "Peintre", "Pilote" }),
                new Family("Nourriture", new List<String> { "Cerises", "Maïs", "Champignon", "Pâtes" })
                };

            m_assertions = new Assertion[] {
                new Assertion(1, "Camion", "Billet de 10", false, true),
                new Assertion(2, "Camion", "Billet de 20", true, true),
                new Assertion(3, "Billet de 10", "Peintre", false, true),
                new Assertion(4, "Billet de 5", "Maïs", false, true),
                new Assertion(5, "Billet de 20", "Policier", true, true),
                new Assertion(6, "Camion", "Cerises", true, true),
                new Assertion(7, "Billet de 10", "Enseignant", false, true),
                new Assertion(8, "Voiture", "Pâtes", false, true),
                new Assertion(9, "Billet de 5", "Enseignant", false, true),
                new Assertion(10, "Billet de 5", "Cerises", false, true),
                new Assertion(11, "Cerises", "Enseignant", false, true),
                new Assertion(12, "Ambulance", "Peintre", false, true),
                new Assertion(13, "Billet de 50", "Voiture", true, true),
                new Assertion(14, "Camion", "Policier", true, true),
                new Assertion(15, "Peintre", "Pâtes", true, true),
                new Assertion(16, "Billet de 20", "Pâtes", false, true),
                new Assertion(17, "Peintre", "Champignon", false, true),
                new Assertion(18, "Billet de 10", "Maïs", true, true),
            };

            // Gather families for items in the assertions
            for (int i = 0; i < Assertions.Length; i++)
            {
                Assertions[i].familyOfElementOne = getFamilyNameForItem(Assertions[i].elementOne);
                Assertions[i].familyOfElementTwo = this.getFamilyNameForItem(Assertions[i].elementTwo);
            }
        }

        public string getFamilyNameForItem(string item)
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
