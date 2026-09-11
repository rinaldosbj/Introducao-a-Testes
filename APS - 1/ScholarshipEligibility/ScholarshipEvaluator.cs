namespace ScholarshipEligibility;

public static class ScholarshipEvaluator
{
    public static EvaluationResult EvaluateScholarship(
        int age,
        double gpa,
        double attendanceRate,
        bool hasRequiredCourses,
        bool disciplinaryRecord
    )
    {
        ValidateInputs(gpa, attendanceRate);

        var rejectionReasons = new List<string>();
        var reviewReasons = new List<string>();

        // Rule 1 - Age
        if (age < 16)
        {
            rejectionReasons.Add("Applicant is younger than the minimum age.");
        }
        else if (age <= 17)
        {
            reviewReasons.Add("Applicant is under 18 and requires manual review.");
        }

        // Rule 2 - GPA
        if (gpa < 6.0)
        {
            rejectionReasons.Add("GPA is below the minimum required.");
        }
        else if (gpa < 7.0)
        {
            reviewReasons.Add("GPA is in the manual review range.");
        }

        // Rule 3 - Attendance
        if (attendanceRate < 75.0)
        {
            rejectionReasons.Add("Attendance rate is below the minimum required.");
        }
        else if (attendanceRate < 80.0)
        {
            reviewReasons.Add("Attendance rate is in the manual review range.");
        }

        // Rule 4 - Required courses
        if (!hasRequiredCourses)
        {
            rejectionReasons.Add("Required courses have not been completed.");
        }

        // Rule 5 - Disciplinary record
        if (disciplinaryRecord)
        {
            rejectionReasons.Add("Applicant has a disciplinary record.");
        }

        // Final decision
        if (rejectionReasons.Count > 0)
        {
            return new EvaluationResult(Status.REJECTED, rejectionReasons);
        }

        if (reviewReasons.Count > 0)
        {
            return new EvaluationResult(Status.MANUAL_REVIEW, reviewReasons);
        }

        return new EvaluationResult(
            Status.APPROVED,
            new List<string> { "Applicant meets all scholarship requirements." }
        );
    }

    private static void ValidateInputs(double gpa, double attendanceRate)
    {
        if (gpa < 0.0 || gpa > 10.0)
        {
            throw new ArgumentException("GPA must be between 0 and 10.");
        }

        if (attendanceRate < 0.0 || attendanceRate > 100.0)
        {
            throw new ArgumentException("Attendance rate must be between 0 and 100.");
        }
    }
}
