using System;
using System.Collections.Generic;
using System.Text;

namespace EinsteinRiddles
{
    class Input04
    {
        public Assertion[] Assertions;
        public List<Family> Families;
        public String Info;

        public Input04()
        {
            Info = "PROBLEME EXTREME #20 8x8";

            Families = new List<Family> {
                new Family("Sports", new List<String> { "Bowling", "Rugby", "Volley", "Ballet", "Basket", "Football", "BaseBall", "Golf" }),
                new Family("Nourriture", new List<String> { "Maïs", "Champignon", "Pâtes", "Ananas", "Steak", "Tomate", "Banane", "Fromage" }),
                new Family("Véhicules", new List<String> { "Train", "Zeppelin", "Hélicoptère", "Sous Marin", "Ambulance", "Voiture", "Camion", "Char" }),
                new Family("Metiers", new List<String> { "Policier", "Pilote", "Medecin", "Dentiste", "Ingenieur", "Photographe", "Scientifique", "Enseignant" }),
                new Family("Coiffes", new List<String> { "Chapeau de cowboy", "Casquette", "Couronne", "Bonnet de Noël", "Chapeau de sorcière", "Chapeau de diplomé", "Chapeau à plume", "Chapeau de Sherlock"}),
                new Family("Instruments", new List<String> { "Harpe", "Guitare", "Violon", "Trompette", "Tambourin", "Xylophone", "Flute", "Saxophone" }),
                new Family("Drapeaux", new List<String> { "Italie", "Suède", "Russie", "Angleterre", "Allemagne", "Etats Unis", "France", "Japon" }),
                new Family("Metiers", new List<String> { "Renard", "Grenouille", "Elephant", "Chaton", "Chien", "Lapin", "Rat", "Lion" })
             };

            Assertions = new Assertion[] {
                new Assertion(1, "Ingenieur", "Ballet", false, true),
                new Assertion(2, "Suède", "Ballet", false, true),
                new Assertion(3, "BaseBall", "Chapeau de diplomé", true, true),
                new Assertion(4, "Bonnet de Noël", "Golf", false, true),
                new Assertion(5, "Lion", "Basket", true, true),
                new Assertion(6, "Zeppelin", "Banane", true, true),
                new Assertion(7, "Suède", "Champignon", false, true),
                new Assertion(8, "Maïs", "Chapeau à plume", false, true),
                new Assertion(9, "Ananas", "Couronne", true, true),
                new Assertion(10, "Tomate", "Lapin", false, true),
                new Assertion(11, "Renard", "Fromage", false, true),
                new Assertion(12, "Rat", "Pâtes", true, true),
                new Assertion(13, "Dentiste", "Ambulance", false, true),
                new Assertion(14, "Photographe", "Voiture", true, true),
                new Assertion(15, "France", "Hélicoptère", false, true),
                new Assertion(16, "Train", "Chapeau de Sherlock", true, true),
                new Assertion(17, "Elephant", "Sous Marin", false, true),
                new Assertion(18, "Voiture", "Rat", true, true),
                new Assertion(19, "Scientifique", "Tambourin", true, true),
                new Assertion(20, "Policier", "Italie", false, true),
                new Assertion(21, "Policier", "Chapeau de sorcière", true, true),
                new Assertion(22, "Enseignant", "Couronne", false, true),
                new Assertion(23, "Ingenieur", "Chien", false, true),
                new Assertion(24, "Suède", "Violon", false, true),
                new Assertion(25, "Suède", "Guitare", false, true),
                new Assertion(26, "Xylophone", "Couronne", false, true),
                new Assertion(27, "Guitare", "Chapeau de sorcière", false, true),
                new Assertion(28, "Violon", "Elephant", false, true),
                new Assertion(29, "Renard", "Tambourin", false, true),

                new Assertion(30, "Grenouille", "Tambourin", false, true),
                new Assertion(31, "Russie", "Chapeau de diplomé", true, true),
                new Assertion(32, "Italie", "Casquette", false, true),
                new Assertion(33, "Suède", "Chapeau à plume", true, true),
                new Assertion(34, "France", "Chien", false, true),
                new Assertion(35, "Suède", "Lion", false, true),
                new Assertion(36, "Chapeau de sorcière", "Grenouille", false, true),
                new Assertion(37, "Casquette", "Elephant", false, true),
                new Assertion(38, "Japon", "Ballet", false, true),
                new Assertion(39, "Chapeau de sorcière", "Rugby", true, true),

                new Assertion(40, "Rat", "Football", true, true),
                new Assertion(41, "Tomate", "Chapeau de sorcière", false, true),
                new Assertion(42, "Tomate", "Grenouille", false, true),
                new Assertion(43, "Renard", "Champignon", false, true),
                new Assertion(44, "Scientifique", "Hélicoptère", true, true),
                new Assertion(45, "Ambulance", "Couronne", true, true),
                new Assertion(46, "Renard", "Sous Marin", false, true),
                new Assertion(47, "Enseignant", "Violon", false, true),
                new Assertion(48, "Scientifique", "Bonnet de Noël", false, true),
                new Assertion(49, "Enseignant", "Chien", false, true),

                new Assertion(50, "Chapeau de sorcière", "Trompette", false, true),
                new Assertion(51, "Flute", "Grenouille", false, true),
                new Assertion(52, "Lion", "Guitare", false, true),
                new Assertion(53, "Japon", "Chapeau de sorcière", false, true),
                new Assertion(54, "Angleterre", "Chapeau de cowboy", true, true),
                new Assertion(55, "Etats Unis", "Chien", false, true),
                new Assertion(56, "Lion", "Violon", false, true),
                new Assertion(57, "Renard", "Rugby", false, true),
                new Assertion(58, "Maïs", "Casquette", false, true),
                new Assertion(59, "Enseignant", "Char", false, true),

                new Assertion(60, "Char", "Chaton", true, true),
                new Assertion(61, "Pilote", "Suède", true, true),
                new Assertion(62, "Angleterre", "Harpe", true, true),
                new Assertion(63, "Xylophone", "Elephant", false, true),
                new Assertion(64, "Italie", "Chapeau de Sherlock", false, true),
                new Assertion(65, "Chapeau de diplomé", "Chien", false, true),
                new Assertion(66, "Etats Unis", "Tomate", false, true),
                new Assertion(67, "Japon", "Camion", true, true),
                new Assertion(68, "Dentiste", "Chien", false, true),
                new Assertion(69, "Grenouille", "Guitare", false, true),

                new Assertion(70, "Etats Unis", "Bowling", true, true),
                new Assertion(71, "Dentiste", "Flute", true, true),
                new Assertion(72, "Etats Unis", "Casquette", false, true),
                new Assertion(73, "Chapeau de diplomé", "Tambourin", false, true),
                new Assertion(74, "Chien", "Maïs", true, true)
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



