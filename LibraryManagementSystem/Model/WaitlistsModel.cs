namespace LibraryManagementSystem.Model
{
    public class WaitlistsModel
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int BookId { get; set; }

        public DateTime RequestedTime { get; set; }
    }
}
