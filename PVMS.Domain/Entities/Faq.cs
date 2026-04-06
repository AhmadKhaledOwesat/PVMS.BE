namespace DcpTracker.Domain.Entities
{
    public class Faq : BaseEntity<Guid>
    {
        public string QuestionAr { get; set; }
        public string QuestionOt { get; set; }
        public string AnswerAr { get; set; }
        public string AnswerOt { get; set; }
        public int Active { get; set; }
    }
}
