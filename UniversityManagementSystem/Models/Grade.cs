namespace UniversityManagementSystem.Models
{
    public partial class Grade
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public int Score { get; set; }

        public virtual Course Course { get; set; } = null!;

        public virtual Student Student { get; set; } = null!;


        //addon methods and new var

        // Method to convert the score to a letter grade
        public string GetLetterGrade()
        {
            if (Score >= 90)
                return "A";
            else if (Score >= 80)
                return "B";
            else if (Score >= 70)
                return "C";
            else if (Score >= 60)
                return "D";
            else
                return "F";
        }


        // Method to check if the grade is a passing grade
        public bool IsPassingGrade()
        {
            // Define logic to determine if the grade is a passing grade based on the score and grading criteria
            return Score >= 60;
        }
    }
}
