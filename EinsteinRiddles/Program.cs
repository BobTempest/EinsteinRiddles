using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EinsteinRiddles
{
     /*
      *     WHAT to do Next :  input files 
      *                        push to higher limits 
      *                        Test with irregular inputs
      *                        
      */

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            World world = new World();
            int foundInteractions = world.WorldInMotion(0);

            if (foundInteractions != world.Table.Count() * (world.Families[0].Items.Count * world.Families[0].Items.Count))
            {
                world.doDream(foundInteractions);
            }

            world.PrintAReport();
            Console.WriteLine("END OF PGM");
            Console.ReadLine();
        }
    }

    public class World
    {
        public Assertion[] Assertions;
        public List<Family> Families;
        public List<Family> FamiliesOrphans;
        public string Name;
        public List<FamilyRelationship> Table;
        public List<List<string>> Chains;

        public World()
        {
            // FACILE 408
            Console.WriteLine("Importing Problem....");

            //Input01 input = new Input01();
            Input02 input = new Input02();

            Families = input.Families;
            Assertions = input.Assertions;
            Name = input.Info;

            string copy = Helper.Clone<List<Family>>(Families);
            FamiliesOrphans = Helper.ReVive<List<Family>>(copy);

            // A chain is a final statement like : One Name eats one Food in this vehicule with this nationality.
            // We litterally chain matches.
            Chains = new List<List<string>>();

            Table = new List<FamilyRelationship>();
            // creating tree
            for (int i = 0; i < Families.Count; i++)
            {
                // create a serie of family relationship
                for (int j = i + 1; j < Families.Count; j++)
                {
                    var fr = new FamilyRelationship(Families[i].Name, Families[j].Name);
                    foreach (var item01 in Families[i].Items)
                    {
                        foreach (var item02 in Families[j].Items)
                        {
                            var ir = new ItemRelationship(new List<String> { item01, item02 });
                            ir.families = new List<string> { GetFamilyNameForItem(item01), GetFamilyNameForItem(item02)};
                            fr.itemRelationships.Add(ir);
                        }
                    }
                    Table.Add(fr);
                }
            }
            /*
            FamilyRelationships = new 
                chapeau/Medic
                    chapeau1 / Medic 1 / Unknown-true-false /locked
                    chapeau1 / Medic 2
                    chapeau1 / Medic 3
                    chapeau1 / Medic 4
                    chapeau2 / Medic 1
                    chapeau2 / Medic 2
                    .....

                chapeau/Vehicule
            */

            Console.WriteLine("PROBLEM " + Name + " Created.");
        }

        public int WorldInMotion(int numberOfInteractionAlreadyFound)
        {
            int maxNbrOfItemInteractions = Table.Count() * (Families[0].Items.Count * Families[0].Items.Count);
            int changes = 0;
            int rounds = 0;
            while ((changes > 0 || rounds == 0) && rounds < 100 && numberOfInteractionAlreadyFound < maxNbrOfItemInteractions)
            {            
                rounds++;
                changes = 0;
                Console.WriteLine("[][][][][] STARTING ROUND " + rounds + ".");

                int modifications = GoThroughARoundOfAssertions();
                Console.WriteLine("======== Ran through goThroughARoundOfAssertions. " + modifications + " modifications");
                changes += modifications;

                // Search matching relationships Pilot likes billet de 10, Mais likes billet de 10 THEN Pilot likes Mais
                int modifications02 = SearchForNewChains();
                Console.WriteLine("======== Ran through SearchForNewChains. " + modifications02 + " modifications");

                changes += modifications02;

                // treating Unknown : search for a OneLeft Situation
                int modifications03 = SearchForLastOneStandingSituations();
                Console.WriteLine("======== Ran through SearchForLastOneStandingSituations. " + modifications03 + " modifications");
                changes += modifications03;

                // looking for orphans in almost complete chains
                int modifications04 = CanWeAddAnOrphanToAChain();
                Console.WriteLine("======== Ran through CanWeAddAnOrphanToAChain. " + modifications04 + " modifications");
                changes += modifications04;

                numberOfInteractionAlreadyFound += changes;
                Console.WriteLine("[][][][][] FINISHING ROUND " + rounds + " WITH " + changes + " CHANGES. And Total interactions are : " + numberOfInteractionAlreadyFound);
            }

            return numberOfInteractionAlreadyFound;
        }

        public void PrintAReport()
        {
            int totalNbrOfMatches = 0;
            int TotalExpectedMatches = 0;

            Console.WriteLine("**************** REPORT *******************************");

            Console.WriteLine("********   FAMILY RELATIONSHIPS ************************");
            foreach (var familyRelationship in Table)
            {
                int matches = 0;
                int expectedMatches = Families[0].Items.Count;
                Console.WriteLine("*******" + familyRelationship .families[0] + " & " + familyRelationship.families[1] + " *********************");
                foreach (var ItemRelationship in familyRelationship.itemRelationships)
                {
                    if (ItemRelationship.status == status.MATCH)
                    {
                        //Console.WriteLine(ItemRelationship.items[0] + " MATCHES " + ItemRelationship.items[1]);
                        matches++;
                    }
                    else if (ItemRelationship.status == status.NONE)
                    {
                        //Console.WriteLine(ItemRelationship.items[0] + " DO NOT MATCH " + ItemRelationship.items[1]);
                    }
                }

                totalNbrOfMatches += matches;
                TotalExpectedMatches += expectedMatches;
                Console.WriteLine("******* Found " + matches + " matches on " + expectedMatches + " expected *********************");
            }

            PrintChains();

            Console.WriteLine("**************** FINAL REPORT *******************************");
            Console.WriteLine("******* Found " + totalNbrOfMatches + " matches on " + TotalExpectedMatches + " expected *********************");

        }

        public void PrintChains()
        {
            Console.WriteLine("********************** CHAINS *******************************");
            foreach (var chain in Chains)
            {
                string outputLine = "CHAIN : ";
                outputLine += printAChain(chain);
                Console.WriteLine(outputLine);
            }
        }

        public void doDream(int foundSoFar)
        {
            Console.WriteLine("||||||||||||| Let's dream in experimental Land |||||||||||||||||");
            Console.WriteLine("************** CHAINS BEFORE THE DREAM  **************************");
            PrintChains();
            int maxElementInAChain = Families.Count;

            // find one big chain and a smaller one which is compatible.
            for (int i = 0; i < Chains.Count - 1; i++)
            {
                List<string> chain = Chains[i];
                int missingElements = maxElementInAChain - chain.Count;
                for (int j = i + 1; j < Chains.Count; j++)
                {
                    if (Chains[j].Count <= missingElements)
                    {
                        // let's find out if these two chains are compatible
                        if (areTheyCompatible(Chains[i], Chains[j]))
                        {
                            Console.WriteLine("||||| It could be cool to try to liase CHAIN :" + printAChain(Chains[i]) + " with CHAIN : " + printAChain(Chains[j]));

                            // BACK UPS
                            string ChainsBackUp = Helper.Clone(Chains);
                            string TableBackUp = Helper.Clone(Table);
                            string FamiliesOrphansBackUp = Helper.Clone(FamiliesOrphans);

                            // merge The Chosen Chains
                            int elementImpacted = 0;
                            bool HybridationIsBroken = false;
                            foreach (var addedItem in Chains[j])
                            {
                                //add it to the bigger chain
                                Chains[i].Add(addedItem);

                                // set the match with every other element of the parent chain
                                foreach (var item in Chains[i])
                                {
                                    if (item != addedItem)
                                    {
                                        int returnValue = FindIndexOfFamilyRelationShipAndSetAMatch(item, addedItem);
                                        if (returnValue < 0)
                                        {
                                            // Console.WriteLine("**** Error. this Hybridation does not work");
                                            HybridationIsBroken = true;
                                            break;
                                        }
                                        else
                                        {
                                            elementImpacted += returnValue;
                                        }
                                    }
                                }

                                if (HybridationIsBroken)
                                {
                                    Console.WriteLine("**** Error. this Hybridation does not work. Afterfirst bouturing attempt");
                                    //  Restore
                                    Chains = Helper.ReVive<List<List<string>>>(ChainsBackUp);
                                    Table = Helper.ReVive<List<FamilyRelationship>>(TableBackUp);
                                    FamiliesOrphans = Helper.ReVive<List<Family>>(FamiliesOrphansBackUp);
                                    Console.WriteLine("**** Chains after BAckup reconstruction");
                                    PrintChains();
                                    continue;
                                }
                            }

                            // running the world
                            Chains[j] = new List<string>();
                            Chains = DoCleanChains(Chains);

                            int returnValue02 = WorldInMotion(foundSoFar);

                            if (returnValue02 < 0 ) // erreur critique de conflit 
                            {
                                Console.WriteLine("**** Error. this Hybridation does not work. Critical error after running the world");
                                HybridationIsBroken = true;

                                //  Restore
                                Chains = Helper.ReVive<List<List<String>>>(ChainsBackUp);
                                Table = Helper.ReVive<List<FamilyRelationship>>(TableBackUp);
                                FamiliesOrphans = Helper.ReVive<List<Family>>(FamiliesOrphansBackUp);

                                Console.WriteLine("**** Chains after Backup reconstruction");
                                PrintChains();

                                continue;
                            }
                            else if (returnValue02 == foundSoFar) // plus rien ne bouge
                            {
                                Console.WriteLine("**** Error. this Hybridation does not work. Plus rien ne bouge");
                            }
                            else
                            {
                                Console.WriteLine("We ran the world and reach its end.");
                                PrintChains();
                                // TODO : SEARCH FOR ORPHANS
                                Console.WriteLine("||||| EndOf THe Dream");
                            }

                        }
                        else
                        {
                            Console.WriteLine("||||| Non Compatible Chains : CHAIN :" + printAChain(Chains[i]) + " do not match with CHAIN : " + printAChain(Chains[j]));
                        }
                    }
                }
            }
        }

        private string printAChain(List<string> chain)
        {
            string outputLine = "";
            if (chain.Count > 0)
            {
                foreach (var item in chain)
                {
                    outputLine += " " + item + " -";
                }
                outputLine = outputLine.Substring(0, outputLine.Length - 2);
            }
            else {
                outputLine += " e m p t y ";
            }

            outputLine += ".";
            return outputLine;
        }

        public int GoThroughARoundOfAssertions()
        {
            int elementImpacted = 0;
            foreach (var assertion in Assertions)
            {
                if (assertion.isFresh)
                {
                    // Get the Index of the FamilyRelationship
                    int indexOfFamilyRelationShip = -1;
                    for (int i = 0; i < Table.Count; i++)
                    {
                        if (Table[i].families.Contains(assertion.familyOfElementOne) && Table[i].families.Contains(assertion.familyOfElementTwo))
                        {
                            indexOfFamilyRelationShip = i;
                            break;
                        }
                    }

                    if(indexOfFamilyRelationShip == -1)
                    {
                        Console.WriteLine("ERROR : Couldn't find FamilyRelationship for families " + assertion.familyOfElementOne + " and " + assertion.familyOfElementTwo);
                    }
                    else
                    {
                        // declaration of relation
                        if (assertion.relation == true)
                        {
                            elementImpacted += SetAMatch(assertion.elementOne, assertion.elementTwo, indexOfFamilyRelationShip);
                            assertion.isFresh = false;
                        }
                        else
                        {
                            //declaration of non-relation
                            foreach (var relation in Table[indexOfFamilyRelationShip].itemRelationships)
                            {
                                if (relation.items.Contains(assertion.elementOne) && relation.items.Contains(assertion.elementTwo))
                                {
                                    int returnValue = relation.setRelationTo(status.NONE);
                                    if (returnValue > 0) elementImpacted++;
                                    break;
                                }
                            }

                             elementImpacted += SearchFor_JUST_ONE_LEFT_Situations(assertion.elementOne, indexOfFamilyRelationShip);
                             elementImpacted += SearchFor_JUST_ONE_LEFT_Situations(assertion.elementTwo, indexOfFamilyRelationShip);
                        }
                    }
                }
            }
            return elementImpacted;
        }

        private int SearchFor_JUST_ONE_LEFT_Situations(string item, int indexOfFamilyRelationShip)
        {
            int nbrOfNONEforItem = 0;
            List<String> itemsInNONEForItem = new List<string>();

            foreach (var relation in Table[indexOfFamilyRelationShip].itemRelationships)
            {
                if (relation.items.Contains(item) && relation.status == status.NONE)
                {
                    itemsInNONEForItem.Add(relation.items[0] == item ? relation.items[1] : relation.items[0]);
                    nbrOfNONEforItem++;
                }

                if (relation.items.Contains(item) && relation.status == status.MATCH)
                {
                    nbrOfNONEforItem = 999999;
                    break;
                }
            }

            int nbrOfItemsOfOtherFamily = 0;
            var ItemsOfOtherFamily = new List<string>();

            if (itemsInNONEForItem.Count > 0)
            {
                ItemsOfOtherFamily = Families.Where(f => f.Name == GetFamilyNameForItem(itemsInNONEForItem[0])).First().Items;
                nbrOfItemsOfOtherFamily = ItemsOfOtherFamily.Count;
            }

            if (nbrOfNONEforItem == nbrOfItemsOfOtherFamily - 1)
            {
                // FIND the missing element
                string matchingItem = ItemsOfOtherFamily.Where(f => !itemsInNONEForItem.Contains(f)).First();

                Console.WriteLine("**** We have a 'all NONE but One' situation. NbrOgfNoneFor item " + item + " is " + nbrOfNONEforItem +
                          " and nbrOfItemForHisFamily is : " + nbrOfItemsOfOtherFamily + " \n and NoneItemsCount is : " + itemsInNONEForItem.Count +
                          " and item is : " + matchingItem);

                return SetAMatch(item, matchingItem, indexOfFamilyRelationShip);
            }

            return 0;
        }

        public int SearchForLastOneStandingSituations()
        {
            int modifiedElements = 0;
            int i = 0;

            foreach (var familyRelationship in Table)
            {
                foreach (var relation in familyRelationship.itemRelationships)
                {
                    if (relation.status == status.UNKNOWN)
                    {
                        modifiedElements += SearchFor_JUST_ONE_LEFT_Situations(relation.items[0], i); 
                    }

                    if (relation.status == status.UNKNOWN)
                    {
                        modifiedElements += SearchFor_JUST_ONE_LEFT_Situations(relation.items[1], i);
                    }
                }
                i++;
            }
            return modifiedElements;
        }

        public int CanWeAddAnOrphanToAChain()
        {
            int elementImpacted = 0;
            foreach (var chain in Chains)
            {
                if (chain.Count == Families.Count -1)
                {
                    Console.WriteLine("**** This chain is almost complete !");
                    // find the missing family
                    List<string> foundFamiliesInThisChain = new List<string>();
                    foreach (var item in chain)
                    {
                        foundFamiliesInThisChain.Add(GetFamilyNameForItem(item));
                    }

                    foreach (var family in FamiliesOrphans)
                    {
                        if (!foundFamiliesInThisChain.Contains(family.Name))
                        {
                            if (family.Items.Count == 1)
                            {
                                Console.WriteLine("**** BOOYAH !!!!! We found the Orphan " + family.Items[0] + " for the Chain : " + printAChain(chain));
                                string newItem = family.Items[0];

                                // set the match with every element of the chain ! 
                                foreach (var item in chain)
                                {
                                    if (item != newItem)
                                    {
                                        elementImpacted += FindIndexOfFamilyRelationShipAndSetAMatch(item, newItem);
                                    }
                                }

                                // remove from orphans
                                family.Items.Remove(newItem);

                                // add to chain
                                chain.Add(newItem);
                            }

                            break;
                        }
                    }

                }
            }
            return elementImpacted;
        }

        public int SearchForNewChains()
        {
            int elementImpacted = 0;
            bool doCleanPlease = false;

            for (int i = 1; i < Chains.Count; i++)
            {
                // find an orphan Chain
                if (Chains[i].Count == 2)
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        string newItem = String.Empty;
                        string chainingItem = String.Empty;

                        if (Chains[i].Count > 0 && Chains[j].Contains(Chains[i][0]) && !Chains[j].Contains(Chains[i][1]))
                        {
                            newItem = Chains[i][1];
                            chainingItem = Chains[i][0];
                        }
                        else if (Chains[i].Count > 0 &&  Chains[j].Contains(Chains[i][1]) && !Chains[j].Contains(Chains[i][0]))
                        {
                            newItem = Chains[i][0];
                            chainingItem = Chains[i][1];
                        }

                        if (Chains[i].Count > 0 && Chains[j].Contains(Chains[i][1]) && Chains[j].Contains(Chains[i][0]))
                        {
                            // this orphan Chain has its two elements in a bigger chain already
                            Chains[i] = new List<string>();
                        }

                        if (newItem.Length > 0 )
                        {
                            Console.WriteLine("**** Chaining " + newItem + " To Chain index " + j + " containing " + printAChain(Chains[j]));
                            doCleanPlease = true;
                            //add it to the bigger chain
                            Chains[j].Add(newItem);
                            // remove this orphan chain
                            Chains[i] = new List<string>();
                            // set the match with every other element of the parent chain
                            foreach (var item in Chains[j])
                            {
                                if (item != newItem && item != chainingItem)
                                {
                                    elementImpacted += FindIndexOfFamilyRelationShipAndSetAMatch(item, newItem);
                                }
                            }
                        }
                    }
                }
            }

            // TODO if a chain is length - 1 , search for an orphan in the missing family
            // TODO Analyse long chains and if it's not compatible, place NONEs for its elements.

            var cleanChains = new List<List<string>>();
            if (doCleanPlease)
            {
                Chains = DoCleanChains(Chains);
            }

            Chains = OrderChainsByLength(Chains); // Biggest above

            return elementImpacted;
        }

        public int FindIndexOfFamilyRelationShipAndSetAMatch(string itemOne, string itemTwo)
        {
            string familyForItemOne = GetFamilyNameForItem(itemOne);
            string familyForItemTwo = GetFamilyNameForItem(itemTwo);
            int indexOfFamilyRelationShip = -1;

            for (int i = 0; i < Table.Count; i++)
            {
                if (Table[i].families.Contains(familyForItemOne) && Table[i].families.Contains(familyForItemTwo))
                {
                    indexOfFamilyRelationShip = i;
                    break;
                }
            }

            if (indexOfFamilyRelationShip == -1)
            {
                Console.WriteLine("ERROR : Couldn't find FamilyRelationship for families " + familyForItemOne + " and " + familyForItemTwo);
            }

            return SetAMatch(itemOne, itemTwo, indexOfFamilyRelationShip, false);
        }

        public int SetAMatch(string itemOne, string itemTwo, int indexOfFamilyRelationShip, bool AddAChain = true)
        {
            int elementImpacted = 0;
            foreach (var relation in Table[indexOfFamilyRelationShip].itemRelationships)
            {
                if (relation.items.Contains(itemOne) && relation.items.Contains(itemTwo))
                {
                    elementImpacted += relation.setRelationTo(status.MATCH);
                }
                else if ((relation.items.Contains(itemOne) && !relation.items.Contains(itemTwo)) ||
                            (relation.items.Contains(itemTwo) && !relation.items.Contains(itemOne)))
                {
                    elementImpacted += relation.setRelationTo(status.NONE);
                }
            }

            // Adding to chains as a new virginal chain
            if (AddAChain)
            {
                Chains.Add(new List<string> { itemOne, itemTwo });
                isNotAnOrphanAnymore(itemOne);
                isNotAnOrphanAnymore(itemTwo);
            }

            return elementImpacted;
        }

        public string GetFamilyNameForItem(string item)
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

        public List<string> GetFamilyItemsByName(string familyName)
        {
            foreach (var family in Families)
            {
                if (family.Name == familyName)
                {
                    return family.Items;
                }
            }

            return null;
        }

        public static List<List<string>> OrderChainsByLength(List<List<string>> chains)
        {
            int moves = 0;
            do
            {
                moves = 0;
                for (int i = 0; i < chains.Count - 1; i++)
                {
                    if (chains[i].Count < chains[i + 1].Count)
                    {
                        List<string> buffer = chains[i];
                        chains[i] = chains[i + 1];
                        chains[i + 1] = buffer;
                        moves++;
                    }
                }
            }
            while (moves > 0);

            return chains;
        }

        public void isNotAnOrphanAnymore(string item)
        {
            string familyName = GetFamilyNameForItem(item);
            foreach (var familyOrphans in FamiliesOrphans)
            {
                if (familyOrphans.Name == familyName)
                {

                    familyOrphans.Items.Remove(item);
                }
            }
        }

        public bool areTheyCompatible(List<String> chainOne, List<string> chainTwo)
        {
            List<String> FamiliesInChainOne = new List<string>();
            foreach (var item in chainOne)
            {
                FamiliesInChainOne.Add(GetFamilyNameForItem(item));
            }

            foreach (var item in chainTwo)
            {
                if (FamiliesInChainOne.Contains(GetFamilyNameForItem(item)))
                {
                    return false;
                }
            }

            return true;
        }

        public List<List<string>> DoCleanChains(List<List<String>> chains)
        {
            var cleanChains = new List<List<string>>();

            foreach (var chain in chains)
            {
                if (chain.Count > 0)
                {
                    cleanChains.Add(chain);
                }
            }

            return cleanChains;
        }
    }

    public class Family
    {
        public string Name { get; set; }
        public List<String> Items { get; set; }
        public Family(string name, List<String> items)
        {
            Name = name;
            Items = items;
        }
    }

    public class ItemRelationship
    {
        public List<String> items;
        public List<String> families;
        public status status;
        public Boolean locked; // is it usefull ?

        public ItemRelationship(List<String> items)
        {
            this.items = items;
            this.status = status.UNKNOWN;
            this.locked = false;
        }

        internal int setRelationTo(status incomingStatus)
        {
            if (locked == true && status != incomingStatus)
            {
                Console.WriteLine("FATAL ERROR : Trying to set a conflicting relationShip");
                return -99999999;
                /*
                throw new ArgumentException("FATAL ERROR : Trying to set a conflicting relationShip. " +
                "Trying to set relation " + items[0] + "/" + items[1] + " to " + incomingStatus + " But it is locked and set to " + status + " already." );
                */
            }
            else if (locked == false)
            {
                locked = true;
                status = incomingStatus;
                return 1;
            }

            return 0;
        }
    }

    public class FamilyRelationship
    {
        public List<String> families;
        public List<ItemRelationship> itemRelationships;
        //public ItemRelationship[] itemRelationships;
        public Boolean locked; // is it usefull ?

        public FamilyRelationship(string family1, string family2)
        {
            this.families = new List<String> { family1, family2 };
            this.itemRelationships = new List<ItemRelationship>();
            this.locked = false;
        }
    }

    public enum status {
        MATCH,
        NONE,
        UNKNOWN
    }

    public class Helper
    {
        public static string Clone<T>(T source)
        {
            return JsonConvert.SerializeObject(source);
        }

        public static T ReVive<T>(string serialized)
        {
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }

    public class Assertion
    {
        public int id;
        public string elementOne;
        public string elementTwo;
        public string familyOfElementOne;
        public string familyOfElementTwo;
        public Boolean relation;
        public Boolean isFresh;

        public Assertion(int id, string elementOne, string elementTwo, bool relation, bool isFresh)
        {
            this.id = id;
            this.elementOne = elementOne;
            this.elementTwo = elementTwo;
            this.relation = relation;
            this.isFresh = isFresh;
        }
    }
}
