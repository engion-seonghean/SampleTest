using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmacIO
{
    class ClassCommon
    {
    }
    class ProgramTest
    {
        //데이터 비교 클래스
        public class DataCompare : IEqualityComparer<Student>
        {
            public bool Equals(Student x, Student y)
            {
                if (string.Equals(x.Age.ToString(), y.Age.ToString(), StringComparison.OrdinalIgnoreCase)
                    && string.Equals(x.name, y.name, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(x.score.ToString(), y.score.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                return false;
            }

            void  test()
            {
            }

            public int GetHashCode(Student obj)
            {
                return obj.name.GetHashCode();
            }
        }

        public class Student
        {
            public int Age { get; set; }
            public string name { get; set; }
            public int score { get; set; }
        }

        static  public void MainTest()
        {
            //기존 데이터
            List<Student> old_data = new List<Student>()
            {
                new Student { Age = 19, name = "Tom" , score = 30},
                new Student { Age = 22, name = "Zeri" , score = 30},
                new Student { Age = 18, name = "Bob" , score = 30},
                new Student { Age = 20, name = "Ami" , score = 50},
            };
            //신규 데이터
            List<Student> new_data = new List<Student>()
            {
                new Student { Age = 19, name = "Tom" , score = 30},         //기존데이터 동일
                new Student { Age = 18, name = "Zeri" , score = 30},        //Age 데이터 갱신
                new Student { Age = 18, name = "Rora" , score = 30},        //새로운 데이터 추가
                new Student { Age = 19, name = "Ami" , score = 30}         //Score, Age 데이터 갱신
                //new Student { Age = 18, name = "Bob" , score = 30},      //Bob 데이터 삭제
            };

            //변경된 신규데이터
            List<Student> newChange = new_data.Except(old_data, new DataCompare()).ToList();
            //기존데이터에서 변경된 항목
            List<Student> oldChange = old_data.Except(new_data, new DataCompare()).ToList();

            Console.WriteLine("< 새로 변경된 데이터 >");
            foreach (var item in newChange)
            {
                
                Console.WriteLine("Age : " + item.Age);
                Console.WriteLine("name : " + item.name);
                Console.WriteLine("score : " + item.score);
                Console.WriteLine("============================================");
            }
            Console.WriteLine();
            Console.WriteLine("< 기존 데이터 변경사항 >");
            foreach (var item in oldChange)
            {
                Console.WriteLine("Age : " + item.Age);
                Console.WriteLine("name : " + item.name);
                Console.WriteLine("score : " + item.score);
                Console.WriteLine("============================================");
            }
        }
        
    } 
}
