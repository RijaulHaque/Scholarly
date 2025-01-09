namespace Scholarly.Models
{
    public class Grades
    {
        //PK
        public bool GradedOrNot { get; set; }
        public DateOnly grade_date { get; set; }


        //fk
        public required Students Students_id { get; set; }

        public required Classes class_id { get; set; }

       
    }
}
