using System.Collections.Generic;

namespace Tema2
{
    public static class StudentRepo {
        
        public static List<Student> Students = new List<Student>() {
            new Student() {Id =1 , Nume = "Popescu", Prenume = "Alina", Facultate= "IS", An_studiu =3 },
            new Student() {Id =2 , Nume = "Chetreanu", Prenume = "Raluca", Facultate= "IS", An_studiu =4 },
            new Student() {Id =3 , Nume = "Hagi", Prenume = "Marco", Facultate= "AC", An_studiu =2 }

        };
    }
}