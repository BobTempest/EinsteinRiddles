using EinsteinRiddles;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestsForEinsteinsRiddles
{
    public class Tests
    {
        private World _world;

        [SetUp]
        public void Setup()
        {
            _world = new World();
        }

        [Test]
        public void WorldIsPopulated()
        {
            Assert.IsTrue(_world.Families != null, "World Families is null");
            Assert.IsTrue(_world.Families.Count > 0, "World Families does not exist");
            Assert.IsTrue(_world.Assertions != null, "World Assertions is null");
            Assert.IsTrue(_world.Assertions.Length > 0, "World Assertions does not exist");
        }

        [Test]
        public void AllTheFamiliesHaveTheSameNumberOfItems()
        {
            int NumberOfItemsPerFamily = _world.Families[0].Items.Count;
            for (int i = 1; i < _world.Families.Count; i++)
            {
                Assert.IsTrue(NumberOfItemsPerFamily == _world.Families[i].Items.Count, "Family " + _world.Families[i].Name + " has noot a regular number of items");
            }
        }

        [Test]
        public void DoItemsInFamiliesMatchItemsInAssertions()
        {
            string Error = "";
            List<String> items = new List<string>();
            for (int i = 0; i < _world.Families.Count; i++)
            {
                Family f = _world.Families[i];
                for (int j = 0; j < f.Items.Count; j++)
                {
                    items.Add(f.Items[j]);
                }
            }

            foreach (var assertion in _world.Assertions)
            {
                if (!items.Contains(assertion.elementOne))
                {
                    Error = "Can't find " + assertion.elementOne + " in families in assertion number " + assertion.id;
                    break;
                }

                if (!items.Contains(assertion.elementTwo))
                {
                    Error = "Can't find " + assertion.elementTwo + " in families in assertion number " + assertion.id;
                    break;
                }
            }
            Assert.IsTrue(Error == "", Error);
        }

        [Test]
        public void TableHasTheRightNumberOfFamilyRelationship()
        {
            int numberOfFamilies = _world.Families.Count;
            int numberOfFamilyRelationships = _world.Table.Count;
            int expected = 0;
            for (int i = 1; i < numberOfFamilies; i++)
            {
                expected += i;
            }
            Assert.IsTrue(numberOfFamilyRelationships == expected, "Number of Families is : " + numberOfFamilies + " expected was : " + expected + " but found : " + numberOfFamilyRelationships + " FamilyRemationships");
        }

        [Test]
        public void TableHasTheRightNumberOfItemRelationship()
        {
            int numberOfFamilies = _world.Families.Count;
            int numberOfItemsPerFamily = _world.Families[0].Items.Count;
            int ExpectedNumberOfItemRelationshipPerFamily = numberOfItemsPerFamily * numberOfItemsPerFamily;
            for (int i = 0; i < _world.Table.Count; i++)
            {
                Assert.IsTrue(_world.Table[i].itemRelationships.Count == ExpectedNumberOfItemRelationshipPerFamily, "Wrong number of itemRelationship in FamilyRelationship number " + (i + 1));
            }
        }

        [Test]
        public void OrderChainsIsRight()
        {
            List<List<string>> input = new List<List<string>>();
            input.Add(new List<string> { "", "" });
            input.Add(new List<string> { "", "", "" });
            input.Add(new List<string> { "", "", "", "" });
            input.Add(new List<string> { "", "" });
            input.Add(new List<string> { "", "", "", "", "" });

            var output = World.OrderChainsByLength(input);

            Assert.IsTrue(output[0].Count == 5, "WRONG SORTING: 5 should be First");
            Assert.IsTrue(output[1].Count == 4, "WRONG SORTING: 4 should be Second");
            Assert.IsTrue(output[2].Count == 3, "WRONG SORTING: 3 should be Third");
            Assert.IsTrue(output[3].Count == 2, "WRONG SORTING: 2 should be Fourth");
            Assert.IsTrue(output[4].Count == 2, "WRONG SORTING: 2 should be Last");
        }

        [Test]
        public void WorldIsBackUpped()
        {
            this._world.Table[0].families[0] = "Gentil";
            this._world.Table[0].itemRelationships[3].status = status.MATCH;
            this._world.Chains.Add(new List<string> { "canard", "pipin", "Floti", "Ramon" , "Chazou", "Floti" });

            string ChainsBackUp = Helper.Clone(this._world.Chains);
            string TableBackUp = Helper.Clone(this._world.Table);

            this._world.Table[0].itemRelationships[3].status = status.NONE;
            this._world.Table[0].families[0] = "Pinocul";
            this._world.Chains[0].Add("Japon");
            this._world.Chains[0].Add("Turquie");

            Assert.IsTrue(this._world.Table[0].families[0] == "Pinocul");
            Assert.IsTrue(this._world.Chains[0][0] == "canard");
            Assert.IsTrue(this._world.Chains[0][7] == "Turquie");
            Assert.IsTrue(this._world.Chains[0].Count == 8);
            Assert.IsTrue(this._world.Table[0].itemRelationships[3].status == status.NONE);

            this._world.Chains = Helper.ReVive<List<List<string>>>((string)ChainsBackUp);
            this._world.Table = Helper.ReVive<List<FamilyRelationship>>(TableBackUp);

            Assert.IsTrue(this._world.Table[0].families[0] == "Gentil");
            Assert.IsTrue(this._world.Chains[0][0] == "canard");
            Assert.IsTrue(this._world.Chains[0][5] == "Floti");
            Assert.IsTrue(this._world.Chains[0].Count == 6);
            Assert.IsTrue(this._world.Table[0].itemRelationships[3].status == status.MATCH);
        }

    }
}