namespace LibraryManagementSystem.Model
{
    public class BorrowedBookModel
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int BookId { get; set; }

        public DateOnly BorrowDate { get; set; }

        public DateOnly DueDate { get; set; }

        public DateOnly? ReturnDate { get; set; }
    }
}
