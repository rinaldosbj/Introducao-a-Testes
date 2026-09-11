using ScholarshipEligibility;
using Xunit;

namespace ScholarshipEligibility.Tests;

public class ScholarshipEvaluatorTests
{
    [Fact]
    public void ShouldApproveEligibleApplicant()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 8.5, 92.0, true, false);

        Assert.Equal(Status.APPROVED, result.Status);
        Assert.Equal(new[] { "Applicant meets all scholarship requirements." }, result.Reasons);
    }

    [Fact]
    public void ShouldRequireManualReviewForApplicantUnder18()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(17, 8.0, 90.0, true, false);

        Assert.Equal(Status.MANUAL_REVIEW, result.Status);
        Assert.Equal(new[] { "Applicant is under 18 and requires manual review." }, result.Reasons);
    }

    [Fact]
    public void ShouldRejectApplicantYoungerThanMinimumAge()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(15, 8.0, 90.0, true, false);

        Assert.Equal(Status.REJECTED, result.Status);
        Assert.Contains("Applicant is younger than the minimum age.", result.Reasons);
    }

    [Fact]
    public void ShouldRejectApplicantWithLowGpa()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 5.9, 90.0, true, false);

        Assert.Equal(Status.REJECTED, result.Status);
        Assert.Contains("GPA is below the minimum required.", result.Reasons);
    }

    [Fact]
    public void ShouldRejectApplicantWithLowAttendance()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 8.0, 74.9, true, false);

        Assert.Equal(Status.REJECTED, result.Status);
        Assert.Contains("Attendance rate is below the minimum required.", result.Reasons);
    }

    [Fact]
    public void ShouldRejectApplicantWithoutRequiredCourses()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 8.0, 90.0, false, false);

        Assert.Equal(Status.REJECTED, result.Status);
        Assert.Contains("Required courses have not been completed.", result.Reasons);
    }

    [Fact]
    public void ShouldRejectApplicantWithDisciplinaryRecord()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 8.0, 90.0, true, true);

        Assert.Equal(Status.REJECTED, result.Status);
        Assert.Contains("Applicant has a disciplinary record.", result.Reasons);
    }

    [Fact]
    public void ShouldRequireManualReviewAtMinimumReviewAge()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(16, 8.0, 90.0, true, false);

        Assert.Equal(Status.MANUAL_REVIEW, result.Status);
    }

    [Fact]
    public void ShouldNotAddAgeReasonAtAge18()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 8.0, 90.0, true, false);

        Assert.Equal(Status.APPROVED, result.Status);
    }

    [Fact]
    public void ShouldRequireManualReviewAtGpa6()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 6.0, 90.0, true, false);

        Assert.Equal(Status.MANUAL_REVIEW, result.Status);
        Assert.Contains("GPA is in the manual review range.", result.Reasons);
    }

    [Fact]
    public void ShouldNotRequireManualReviewAtGpa7()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 7.0, 90.0, true, false);

        Assert.Equal(Status.APPROVED, result.Status);
    }

    [Fact]
    public void ShouldRequireManualReviewAtAttendance75()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 8.0, 75.0, true, false);

        Assert.Equal(Status.MANUAL_REVIEW, result.Status);
        Assert.Contains("Attendance rate is in the manual review range.", result.Reasons);
    }

    [Fact]
    public void ShouldNotRequireManualReviewAtAttendance80()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 8.0, 80.0, true, false);

        Assert.Equal(Status.APPROVED, result.Status);
    }

    [Fact]
    public void ShouldRejectInvalidNegativeGpa()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => ScholarshipEvaluator.EvaluateScholarship(18, -0.1, 90.0, true, false)
        );

        Assert.Equal("GPA must be between 0 and 10.", exception.Message);
    }

    [Fact]
    public void ShouldRejectInvalidGpaAboveMaximum()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => ScholarshipEvaluator.EvaluateScholarship(18, 10.1, 90.0, true, false)
        );

        Assert.Equal("GPA must be between 0 and 10.", exception.Message);
    }

    [Fact]
    public void ShouldRejectInvalidNegativeAttendance()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => ScholarshipEvaluator.EvaluateScholarship(18, 8.0, -0.1, true, false)
        );

        Assert.Equal("Attendance rate must be between 0 and 100.", exception.Message);
    }

    [Fact]
    public void ShouldRejectInvalidAttendanceAboveMaximum()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => ScholarshipEvaluator.EvaluateScholarship(18, 8.0, 100.1, true, false)
        );

        Assert.Equal("Attendance rate must be between 0 and 100.", exception.Message);
    }

    [Fact]
    public void ShouldPrioritizeRejectionOverManualReview()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(17, 5.5, 90.0, true, false);

        Assert.Equal(Status.REJECTED, result.Status);

        Assert.Equal(new[] { "GPA is below the minimum required." }, result.Reasons);
    }

    [Fact]
    public void ShouldReturnAllApplicableRejectionReasons()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(15, 5.0, 70.0, false, true);

        Assert.Equal(Status.REJECTED, result.Status);
        Assert.Equal(5, result.Reasons.Count);

        Assert.Contains("Applicant is younger than the minimum age.", result.Reasons);

        Assert.Contains("GPA is below the minimum required.", result.Reasons);

        Assert.Contains("Attendance rate is below the minimum required.", result.Reasons);

        Assert.Contains("Required courses have not been completed.", result.Reasons);

        Assert.Contains("Applicant has a disciplinary record.", result.Reasons);
    }

    [Fact]
    public void ShouldAcceptGpaAtMinimumBoundary()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 0.0, 90.0, true, false);

        Assert.Equal(Status.REJECTED, result.Status);

        Assert.Contains("GPA is below the minimum required.", result.Reasons);
    }

    [Fact]
    public void ShouldAcceptGpaAtMaximumBoundary()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 10.0, 90.0, true, false);

        Assert.Equal(Status.APPROVED, result.Status);

        Assert.Equal(new[] { "Applicant meets all scholarship requirements." }, result.Reasons);
    }

    [Fact]
    public void ShouldAcceptAttendanceAtMinimumBoundary()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 8.0, 0.0, true, false);

        Assert.Equal(Status.REJECTED, result.Status);

        Assert.Contains("Attendance rate is below the minimum required.", result.Reasons);
    }

    [Fact]
    public void ShouldAcceptAttendanceAtMaximumBoundary()
    {
        var result = ScholarshipEvaluator.EvaluateScholarship(18, 8.0, 100.0, true, false);

        Assert.Equal(Status.APPROVED, result.Status);

        Assert.Equal(new[] { "Applicant meets all scholarship requirements." }, result.Reasons);
    }
}
