using System;
using System.Collections.Generic;
using geektrust.Enums;

namespace geektrust
{
    public class Family
    {
        public void Run(string filePath)
        {
            InitialiseFamilyTree();
            ProcessInputFile(filePath);
        }

        #region Private Methods
        private static void InitialiseFamilyTree()
        {
            //Shan and Anga
            Person Shan = new Person("Shan", null, null, Gender.Male);
            Person Anga = new Person("Anga", null, null, Gender.Female);
            Shan.AddSpouse(Anga);

            //Shan and Anga's children
            Person Chit = new Person("Chit", Shan, Anga, Gender.Male);
            Person Ish = new Person("Ish", Shan, Anga, Gender.Male);
            Person Vich = new Person("Vich", Shan, Anga, Gender.Male);
            Person Aras = new Person("Aras", Shan, Anga, Gender.Male);
            Person Satya = new Person("Satya", Shan, Anga, Gender.Female);
            Shan.AddChildren(new List<Person> { Chit, Ish, Vich, Aras, Satya });

            //Chit's spouse
            Person Amba = new Person("Amba", null, null, Gender.Female);
            Chit.AddSpouse(Amba);

            //Vich's spouse
            Person Lika = new Person("Lika", null, null, Gender.Female);
            Vich.AddSpouse(Lika);

            //Aras' spouse
            Person Chitra = new Person("Chitra", null, null, Gender.Female);
            Aras.AddSpouse(Chitra);

            //Satya's spouse
            Person Vyan = new Person("Vyan", null, null, Gender.Male);
            Satya.AddSpouse(Vyan);

            //Chit and Amba's Children
            Person Dritha = new Person("Dritha", Chit, Amba, Gender.Female);
            Person Tritha = new Person("Tritha", Chit, Amba, Gender.Female);
            Person Vritha = new Person("Vritha", Chit, Amba, Gender.Male);

            Chit.AddChildren(new List<Person> { Dritha, Tritha, Vritha });

            //Dhrita's spouse
            Person Jaya = new Person("Jaya", null, null, Gender.Male);
            Dritha.AddSpouse(Jaya);

            //Dhrita and Jaya's son
            Person Yodhan = new Person("Yodhan", Jaya, Dritha, Gender.Male);
            Dritha.AddChildren(new List<Person> { Yodhan });

            //Vich and Lika's children
            Person Vila = new Person("Vila", Vich, Lika, Gender.Female);
            Person Chika = new Person("Chika", Vich, Lika, Gender.Female);
            Vich.AddChildren(new List<Person> { Vila, Chika });

            //Aras and Chitra's children
            Person Jnki = new Person("Jnki", Aras, Chitra, Gender.Female);
            Person Ahit = new Person("Ahit", Aras, Chitra, Gender.Male);
            Aras.AddChildren(new List<Person> { Jnki, Ahit });

            //Jnki's spouse
            Person Arit = new Person("Arit", null, null, Gender.Male);
            Jnki.AddSpouse(Arit);

            //Arit and Jnki's children
            Person Laki = new Person("Laki", Arit, Jnki, Gender.Male);
            Person Lavnya = new Person("Lavnya", Arit, Jnki, Gender.Female);
            Arit.AddChildren(new List<Person> { Laki, Lavnya });

            //Satya's children
            Person Asva = new Person("Asva", Vyan, Satya, Gender.Male);
            Person Vyas = new Person("Vyas", Vyan, Satya, Gender.Male);
            Person Atya = new Person("Atya", Vyan, Satya, Gender.Female);
            Satya.AddChildren(new List<Person> { Asva, Vyas, Atya });

            //Asva's spouse
            Person Satvy = new Person("Satvy", null, null, Gender.Female);
            Asva.AddSpouse(Satvy);

            //Asva and Satvy's child
            Person Vasa = new Person("Vasa", Asva, Satvy, Gender.Male);
            Asva.AddChildren(new List<Person> { Vasa });

            //Vyas' spouse
            Person Krpi = new Person("Krpi", null, null, Gender.Female);
            Vyas.AddSpouse(Krpi);

            //Vyas and krpi's children
            Person Kriya = new Person("Kriya", Vyas, Krpi, Gender.Male);
            Person Krithi = new Person("Krithi", Vyas, Krpi, Gender.Female);
            Vyas.AddChildren(new List<Person> { Kriya, Krithi });
        }

        private static void ProcessInputFile(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                Data data = ProcessInputData(line);

                Person person = GetPersonObject(data.PersonName);
                if (IsPersonNotFound(person))
                {
                    continue;
                }

                if (data.Action == "GET_RELATIONSHIP")
                {
                    List<Person> result = person.GetPeopleBasedOnRelationship(data.Relation);
                    PrintResult(result);
                }
                else if (data.Action == "ADD_CHILD")
                {
                    if (IsPersonMale(person))
                    {
                        continue;
                    }
                    person.AddNewChild(data.ChildName, data.Gender);
                    Console.WriteLine("CHILD_ADDITION_SUCCEEDED");
                }
            }
        }

        private static Data ProcessInputData(string line)
        {
            string[] input = line.Split(" ");
            Data data = new Data();
            if (input.Length == 3)
            {
                data.Action = input[0];
                data.PersonName = input[1];
                data.Relation = input[2];
            }
            else
            {
                data.Action = input[0];
                data.PersonName = input[1];
                data.ChildName = input[2];
                data.Gender = input[3] == "Female" ? Gender.Female : Gender.Male;
            }

            return data;
        }

        private static Person GetPersonObject(string personName)
        {
            return Person.Family.Find(x => x.Name == personName);
        }

        private static bool IsPersonNotFound(Person person)
        {
            if (person == null)
            {
                Console.WriteLine("PERSON_NOT_FOUND");
                return true;
            }

            return false;
        }

        private static bool IsPersonMale(Person familyMember)
        {
            if (familyMember.Gender == Gender.Male)
            {
                Console.WriteLine("CHILD_ADDITION_FAILED");
                return true;
            }

            return false;
        }

        private static void PrintResult(List<Person> result)
        {
            if (result.Count == 0)
            {
                Console.Write("NONE");
            }

            foreach (Person person in result)
            {
                Console.Write($"{person.Name} ");
            }

            Console.WriteLine();
        } 
        #endregion
    }
}
