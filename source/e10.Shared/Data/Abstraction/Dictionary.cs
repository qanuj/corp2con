using System.Collections.Generic;

namespace e10.Shared.Data.Abstraction
{
    public abstract class Dictionary : Entity, IDictionary
    {
        public string Title { get; set; }
        public string Code { get; set; }
    }

    public abstract class Post : Entity
    {
        public string Title { get; set; }
        public string Body { get; set; }

        public Topic Topic { get; set; }
        public int TopicId { get; set; }
    }

    public class Faq : Post { }

    public class Topic : Entity
    {
        public string Name { get; set; }
        public IList<Post> Posts { get; set; }
    }
}