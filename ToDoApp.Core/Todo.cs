namespace ToDoApp.Core
{
    public class Todo
    {
        public Guid Id { get; }
        public string Title { get; set; }

        private Todo(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
        }

        public static Result<Todo> Create(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Result<Todo>.Failure("Title cannot be empty");
            
            return Result<Todo>.Success(new Todo(title));
        }
    }

}