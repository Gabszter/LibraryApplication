namespace LibrarianApplication.Blazor.Model
{
    public class BorrowInfo
    {
        public string Title { get; set; }
        public int InventoryNumber { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
    };
}
