namespace LibraryManagementSystem.Model
{
    public class BookReturn
    {
        public int MemberId { get; set; }

        public int BookId { get; set; }

        public DateOnly ReturnDate { get; set; }
    }
}
