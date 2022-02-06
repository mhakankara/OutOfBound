namespace OutOfBound.Models
{
    public class QuestionModel
    {
        public QuestionModel()
        {
            AnswerModels = new List<AnswerModel>();
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<AnswerModel> AnswerModels { get; set; }
        
    }
}
