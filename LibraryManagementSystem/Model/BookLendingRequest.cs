using LibraryManagementSystem.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryManagementSystem.Model
{
    public class BookLendingRequest
    {
        public int MemberId { get; set; }

        public int BookId { get; set; }

        public DateOnly BorrowDate { get; set; }


    }
    public class BookLendingResponse : BookLendingRequest
    {
    
        public string  Message { get; set; }
        public DateOnly DueDate { get; set; }

        public static BookLendingResponse fromBorrowBook(BorrowedBook model)
        {

            return new BookLendingResponse() {  BookId=model.BookId, MemberId=model.MemberId, BorrowDate = model.BorrowDate, Message ="Book request completed..", DueDate=model.DueDate };
        }

        public static BookLendingResponse fromWaitList(Waitlist model)
        {

            return new BookLendingResponse() { BookId = model.BookId, MemberId = model.MemberId, Message = "Book request added in Waitlist Queue" };
        }


    }
}
