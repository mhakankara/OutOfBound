namespace OutOfBound.Models
{
    public class AnswerModel
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; }
        public int QuestionModelID { get; set; }
    }
}