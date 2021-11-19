using System;
using System.Collections.Generic;
using System.Linq;
using geektrust.Enums;

namespace geektrust
{
    public class Person
    {
        #region Public Fields
        private string name;

        private Person father;

        private Person mother;

        private Gender gender;

        private List<Person> children;

        private Person spouse;

        private static List<Person> family = new List<Person>();
        #endregion

        #region Constructor
        public Person(string name, Person father, Person mother, Gender gender)
        {
            this.name = name;
            this.father = father;
            this.mother = mother;
            this.gender = gender;
            children = new List<Person>();
            family.Add(this);
        }
        #endregion

        #region Get, Set

        public string Name { get => name; set => name = value; }

        public List<Person> Children { get => children; set => children = value; }

        public Gender Gender { get => gender; set => gender = value; }

        public static List<Person> Family { get => family; set => family = value; }

        #endregion

        #region Public Methods

        public void AddChildren(List<Person> children)
        {
            foreach (Person person in children)
            {
                Children.Add(person);
                this.spouse.children.Add(person);
            }
        }

        public List<Person> GetPeopleBasedOnRelationship(string relation)
        {
            switch (relation)
            {
                case "Paternal-Uncle":
                    return GetPaternalMembers(Gender.Male);

                case "Paternal-Aunt":
                    return GetPaternalMembers(Gender.Female);

                case "Maternal-Uncle":
                    return GetMaternalMembers(Gender.Male);

                case "Maternal-Aunt":
                    return GetMaternalMembers(Gender.Female);

                case "Sister-In-Law":
                    return GetSisterOrBrotherInLaws(Gender.Female);

                case "Brother-In-Law":
                    return GetSisterOrBrotherInLaws(Gender.Male);

                case "Son":
                    return GetChildren(Gender.Male);

                case "Daughter":
                    return GetChildren(Gender.Female);

                case "Siblings":
                    return GetSiblings();

                case "Grandmother":
                    return GetGrandParents(Gender.Female);

                case "Grandfather":
                    return GetGrandParents(Gender.Male);

                default:
                    return null;
            }
        }

        private List<Person> GetGrandParents(Gender gender)
        {
            List<Person> result = new List<Person>();
            if(this.father.mother != null)
            {
                result.Add(gender == Gender.Female ? this.father.mother : this.father.father);
            }
            else
            {
                result.Add(gender == Gender.Female ? this.mother.mother : this.mother.father);
            }
            return result;
        }

        public void AddNewChild(string childName, Gender gender)
        {
            Person newChild = new Person(childName, this.GetHusband(), this, gender);
            this.AddChildren(new List<Person> { newChild });
        }

        //This method will add spouse to person
        public void AddSpouse(Person person)
        {
            this.spouse = person;
            person.spouse = this;
        }

        #endregion

        #region Private Methods

        //Returns Paternal-Aunt if gender is Female otherwise Paternal-Uncle
        private List<Person> GetPaternalMembers(Gender gender)
        {
            List<Person> result = new List<Person>();
            if (father == null)
            {
                return result;
            }
            if (father.father == null)
            {
                return result;
            }

            result.AddRange(father.father.children.Where(x => x.gender == gender).Where(x => x.name != this.father.name));
            return result;
        }

        //Returns Maternal-Aunt if gender is Female otherwise Maternal-Uncle
        private List<Person> GetMaternalMembers(Gender gender)
        {
            List<Person> result = new List<Person>();
            if (mother == null)
            {
                return result;
            }
            if (mother.mother == null)
            {
                return result;
            }

            result.AddRange(mother.mother.children.Where(x => x.gender == gender).Where(x => x.name != this.mother.name));
            return result;
        }

        private List<Person> GetSisterOrBrotherInLaws(Gender gender)
        {
            List<Person> result = new List<Person>();
            if (this.spouse != null && this.spouse.father != null)
            {
                result.AddRange(this.spouse.father.children.Where(x => x.gender == gender).Where(x => x.name != this.spouse.name));
            }
            if (this.father != null)
            {
                result.AddRange(this.father.children.Select(x => x.spouse).Where(x => x != null && x.gender == gender).Where(x => x.name != this.spouse?.name));
            }

            return result;
        }

        private List<Person> GetChildren(Gender gender)
        {
            return this.children.Where(x => x.gender == gender).ToList();
        }

        private List<Person> GetSiblings()
        {
            List<Person> result = new List<Person>();
            if (this.father != null)
            {
                result.AddRange(this.father.children.Where(x => x.name != this.name).ToList());
            }

            return result;
        }

        private Person GetHusband()
        {
            return this.spouse;
        } 

        #endregion
    }
}
