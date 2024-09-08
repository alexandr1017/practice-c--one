using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class Person
    {
        String name;
        int age;

        public Person(int age, String name) {
            this.age = age;
            this.name = name;
        }

       public String printInfo() {
           return name + " " + age;
        }

    }
}
