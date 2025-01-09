using Scholarly.Models;

namespace Scholarly.Models
{
    public class Enrollments
    {
        //PK
        public required string enrollment_id { get; set; }
        public required string enrollment_date { get; set; }

        //fk
        public required Students Students_id { get; set; }
        public required Classes class_id { get; set; }

        // Constructor to initialize required properties
        public Enrollments(string enrollment_id, string enrollment_date, Students students_id, Classes class_id)
        {
            this.enrollment_id = enrollment_id;
            this.enrollment_date = enrollment_date;
            this.Students_id = students_id;
            this.class_id = class_id;
        }
    }
}

public class Grades
{
    public bool GradedOrNot { get; set; }
    public DateOnly grade_date { get; set; }
    public required Students Students_id { get; set; }
    public required Classes class_id { get; set; }

    // Constructor to initialize required properties
    public Grades(bool gradedOrNot, DateOnly grade_date, Students students_id, Classes class_id)
    {
        this.GradedOrNot = gradedOrNot;
        this.grade_date = grade_date;
        this.Students_id = students_id;
        this.class_id = class_id;
    }
}