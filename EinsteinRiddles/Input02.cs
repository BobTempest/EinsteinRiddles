using System;
using System.Collections.Generic;
using System.Text;

namespace EinsteinRiddles
{
    public class Input02 : IInputData
    {
        private Assertion[] m_assertions;
        public Assertion[] Assertions { get { return m_assertions; } }

        private List<Family> m_families;
        public List<Family> Families { get { return m_families; } }

        private String m_Info;
        public String Info { get { return m_Info; } }

        public Input02()
        {
            // MOYEN 1211 6x6
            m_Info = "PROBLEME Moyen #1211 6x6";

            m_families = new List<Family> {
                new Family("Nourriture", new List<String> { "Fromage", "Ananas", "Cerises", "Maïs", "Champignon", "Pâtes" }),
                new Family("Medecine", new List<String> { "Seringue", "Scalpel", "Microscope", "Pilule", "Attelle", "Transfusion" }),
                new Family("Animaux", new List<String> { "Grenouille", "Elephant", "Chaton", "Chien", "Lapin", "Rat" }),
                new Family("Boissons", new List<String> { "Bière", "Eau", "Lait", "Coca", "Energizer", "Pasteque" }),
                new Family("Météo", new List<String> { "Neige", "Nuit", "Lever de Soleil", "Tornade", "Soleil", "Pluie" }),
                new Family("Gaming", new List<String> { "Playstation", "PC", "Casque", "Portable", "Souris", "Joystick" })
             };

            m_assertions = new Assertion[] {
                new Assertion(1, "Attelle", "Lever de Soleil", true, true),
                new Assertion(2, "Elephant", "Lait", false, true),
                new Assertion(3, "Tornade", "Joystick", false, true),
                new Assertion(4, "Chien", "Pasteque", false, true),
                new Assertion(5, "Tornade", "Souris", false, true),
                new Assertion(6, "Lapin", "Neige", true, true),
                new Assertion(7, "Pâtes", "Casque", false, true),
                new Assertion(8, "Cerises", "Souris", true, true),
                new Assertion(9, "Pâtes", "Soleil", false, true),
                new Assertion(10, "Champignon", "Pluie", false, true),
                new Assertion(11, "Seringue", "Pasteque", true, true),
                new Assertion(12, "Seringue", "Rat", false, true),
                new Assertion(13, "Neige", "PC", false, true),
                new Assertion(14, "Scalpel", "Joystick", false, true),
                new Assertion(15, "Rat", "Souris", false, true),
                new Assertion(16, "Coca", "Casque", false, true),
                new Assertion(17, "Energizer", "Soleil", true, true),
                new Assertion(18, "Soleil", "PC", false, true),
                new Assertion(19, "Rat", "Casque", false, true),
                new Assertion(20, "Chaton", "Joystick", true, true),
                new Assertion(21, "Pilule", "Neige", false, true),
                new Assertion(22, "Pluie", "PC", false, true),
                new Assertion(23, "Nuit", "PC", false, true),
                new Assertion(24, "Lait", "Joystick", false, true),
                new Assertion(25, "Pasteque", "Neige", false, true),
                new Assertion(26, "Chaton", "Pasteque", false, true),
                new Assertion(27, "Microscope", "Neige", false, true),
                new Assertion(28, "Pilule", "Souris", false, true),
                new Assertion(29, "Maïs", "Portable", false, true),
                new Assertion(30, "Eau", "Lever de Soleil", true, true),
                new Assertion(31, "Neige", "Casque", false, true),
                new Assertion(32, "Lait", "Neige", false, true),
                new Assertion(33, "Neige", "Souris", false, true),
                new Assertion(34, "Transfusion", "Tornade", true, true),
                new Assertion(35, "Grenouille", "Portable", true, true),
                new Assertion(36, "Ananas", "Casque", false, true),
                new Assertion(37, "Pâtes", "Portable", false, true),
                new Assertion(38, "Fromage", "Playstation", true, true),
                new Assertion(39, "Energizer", "Joystick", true, true),
                new Assertion(40, "Transfusion", "PC", false, true),
                new Assertion(41, "Chien", "Nuit", false, true),
                new Assertion(42, "Lait", "Casque", false, true),
                new Assertion(43, "Ananas", "Portable", false, true),
                new Assertion(44, "Attelle", "Rat", true, true),
                new Assertion(45, "Seringue", "Elephant", false, true),
                
            };

            // Gather families for items in the assertions
            for (int i = 0; i < Assertions.Length; i++)
            {
                Assertions[i].familyOfElementOne = getFamilyNameForItem(Assertions[i].elementOne);
                Assertions[i].familyOfElementTwo = getFamilyNameForItem(Assertions[i].elementTwo);
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

