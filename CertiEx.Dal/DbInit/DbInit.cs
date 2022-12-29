using CertiEx.Domain.Exam;

namespace CertiEx.Dal.DbInit;

public class DbInit : IDbInit
{
    private readonly ApplicationDbContext _db;

    public DbInit(ApplicationDbContext db)
    {
        _db = db;
    }

    public void Initialize()
    {
        if (!_db.Exam.Any())
        {
            _db.Exam.Add(new Exam
            {
                Name = "Huawei Cloud Certified Associate",
                FullMarks = 10,
                Duration = 3
            });

            //_db.Exam.Add(new Exam
            //{
            //    Name = "(Coming Soon) AWS Cloud Practitioner",
            //    FullMarks = 0,
            //    Duration = 1
            //});

            //_db.Exam.Add(new Exam
            //{
            //    Name = "(Coming Soon) Azure Fundamentals",
            //    FullMarks = 0,
            //    Duration = 1
            //});

            _db.SaveChanges();
        }

        if (!_db.Question.Any())
        {
            _db.Question.Add(new Question
            {
                QuestionType = 1,
                DisplayText = "CDN stands for ______",
                ExamID = 1
            });
            _db.Question.Add(new Question
            {
                QuestionType = 1,
                DisplayText = "Which El service is used to implement batch data migration from below-cloud to over-the-cloud ?",
                ExamID = 1
            });
            _db.Question.Add(new Question
            {
                QuestionType = 1,
                DisplayText = "In which service can cloud service operation logs be queried ?",
                ExamID = 1
            });
            _db.Question.Add(new Question
            {
                QuestionType = 1,
                DisplayText = "Which of the following is not the version of enterprise host security ?",
                ExamID = 1
            });
            _db.Question.Add(new Question
            {
                QuestionType = 1,
                DisplayText = "Which of the Following description about the ELB configuration is incorrect?",
                ExamID = 1
            });

            _db.SaveChanges();

        }

        if (!_db.Choice.Any())
        {
            //Q1
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Cloud Distributed Network",
                QuestionID = 1
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Content Delivery Network",
                QuestionID = 1
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Content Distributed Network",
                QuestionID = 1
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Cloud Delivery Network",
                QuestionID = 1
            });
            //Q2
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Data Access Service DIS",
                QuestionID = 2
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Real-time stream computing service Cloud Stream",
                QuestionID = 2
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Cloud Data Migration Service CDM",
                QuestionID = 2
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Data Warehouse Service DWS",
                QuestionID = 2
            });
            //Q3
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Security expert services .",
                QuestionID = 3
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Cloud Audit Service",
                QuestionID = 3
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Cloud Monitoring Service",
                QuestionID = 3
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Cloud analysis service",
                QuestionID = 3
            });
            //Q4
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Enterprise Edition",
                QuestionID = 4
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Personal Edition",
                QuestionID = 4
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Basic Edition",
                QuestionID = 4
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Webpage tamper-proonersion",
                QuestionID = 4
            });
            //Q5
            _db.Choice.Add(new Choice()
            {
                DisplayText = "You can choose to keep the session when you configure the list enter",
                QuestionID = 5
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "You can configure the monitoring strategy when creating the listener",
                QuestionID = 5
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Select IP when adding elastic load balancer",
                QuestionID = 5
            });
            _db.Choice.Add(new Choice()
            {
                DisplayText = "Configurable health check timeout",
                QuestionID = 5
            });
            _db.SaveChanges();
        }

        if (!_db.Answer.Any())
        {
            _db.Answer.Add(new Answer()
            {
                QuestionID = 1,
                ChoiceID = 2,
                DisplayText = "Content Delivery Network"
            });

            _db.Answer.Add(new Answer()
            {
                QuestionID = 2,
                ChoiceID = 7,
                DisplayText = "Cloud Data Migration Service CDM"
            });

            _db.Answer.Add(new Answer()
            {
                QuestionID = 3,
                ChoiceID = 10,
                DisplayText = "Cloud Audit Service"
            });
            
            _db.Answer.Add(new Answer()
            {
                QuestionID = 4,
                ChoiceID = 16,
                DisplayText = "Webpage tamper-proonersion"
            });

            _db.Answer.Add(new Answer()
            {
                QuestionID = 5,
                ChoiceID = 19,
                DisplayText = "Select IP when adding elastic load balancer"
            });
            _db.SaveChanges();

        }
    }
}