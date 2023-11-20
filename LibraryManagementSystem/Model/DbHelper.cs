using LibraryManagementSystem.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementSystem.Model
{
    public class CustomError : Exception
    {
        private String _msg;

        public int code;

        override
        public String Message
        { get => _msg; }




        public CustomError(String record, int code =  400)
        {
            this.code=code;
            this._msg = record;

        }
    }

    public class DbHelper
    {
        private LmsContext _context;

        public DbHelper(LmsContext context) { _context = context; }
        public List<Book> GetBooks()
        {
            var dataList = _context.Books.Take(10).ToList();
            return dataList;
        }
        public Book? GetBooksById(int Id)
        {
            var row = _context.Books.Where(s => s.Id.Equals(Id)).FirstOrDefault();
            return row;

        }
        public Member? GetMemberById(int Id)
        {
            var row = _context.Members.Where(s => s.Id.Equals(Id)).FirstOrDefault();
            return row;

        }
        /// <summary>
        /// POST/PUT
        /// </summary>
        public Member SaveMember(Member memberModel)
        {
            Member? mTable;

            //PUT
            mTable = _context.Members.Where(x => x.Email.Equals(memberModel.Email)).FirstOrDefault();
            if (mTable != null)

            {
                throw new CustomError("Member already found");
            }
            
                mTable = new Member();
                mTable.FirstName = memberModel.FirstName;
                mTable.LastName = memberModel.LastName;
                mTable.Email = memberModel.Email;
                mTable.DateOfBirth = memberModel.DateOfBirth;
                _context.Members.Add(mTable);
                _context.SaveChanges();
                return mTable;

        }
        /// <summary>
        /// POST/PUT
        /// </summary>
        public dynamic BooklendingRequest(BookLendingRequest model)
        {

            BorrowedBook bTable = new BorrowedBook();
            var book = GetBooksById(model.BookId);
            if (book == null)
            {
                throw new CustomError("No Book found");
            }
            CheckLendingActive(book, model.MemberId);

      
                if (book!.AvailableCopies > 0)
                {

                    bTable = new BorrowedBook();
                    bTable.MemberId = model.MemberId;
                    bTable.BookId = model.BookId;
                    bTable.BorrowDate = model.BorrowDate;
                    bTable.DueDate = model.BorrowDate.AddDays(14);
                    bTable.ReturnDate = null;
                    _context.BorrowedBooks.Add(bTable);
                    _context.SaveChanges();
                    book.AvailableCopies = book.AvailableCopies - 1;
                    _context.Books.Update(book);
                    _context.SaveChanges();
                    return bTable;
                }
                else
                {
                    Waitlist wTable = new Waitlist();
                    wTable = new Waitlist();
                    wTable.MemberId = model.MemberId;
                    wTable.BookId = model.BookId;
                    wTable.RequestedTime = DateTime.Now;
                    _context.Waitlists.Add(wTable);
                    _context.SaveChanges();
                    return wTable;
                }
            

        }
        /// <summary>
        /// POST/PUT
        /// </summary>
        public BorrowedBook BookReturn(BookReturn model)
        {
            BorrowedBook? bTable;
            var book = GetBooksById(model.BookId);
            if (book == null)
            {
                throw new CustomError("No book found");
            }
            bTable = _context.BorrowedBooks.Where(x => x.MemberId.Equals(model.MemberId) && x.BookId.Equals(model.BookId) && x.ReturnDate==null).FirstOrDefault();
            if (bTable == null)
            {
                throw new CustomError("No borrow book found");
            }
            else
            {
                if (bTable.BorrowDate <= model.ReturnDate)
                {
                    bTable.ReturnDate = model.ReturnDate;
                    _context.BorrowedBooks.Update(bTable);
                    _context.SaveChanges();
                }
                else
                {
                    throw new CustomError("Return date must be greater than borrow date");
                }
            }
            book.AvailableCopies = book.AvailableCopies + 1;
            _context.Books.Update(book);
            _context.SaveChanges();
            return bTable;

        }
        public void CheckLendingActive(Book book, int memberid)
        {
            List<Lendingactivesummary> Las = _context.Lendingactivesummarys.Where(s => s.MemberId == memberid).ToList();
            if ((Las.FirstOrDefault()?.Total ?? 0) >= 3)
                throw new CustomError("Maximum 3 book Limit reached");
            foreach (var item in Las)
            {
                if ((item.Genre == book.Genre) && item.GenreCount >= 2)
                {
                    throw new CustomError("For same genre Maximum 2 book Limit reached");
                }
            }
        }
    }
}
