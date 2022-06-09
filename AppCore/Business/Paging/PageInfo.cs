namespace AppCore.Business.Paging;

public class PageInfo
{
    public int CurrentPage { get; set; } = 1;
    public int RecordPerPage { get; set; } = 10;
    public int TotalRecord { get; set; }
    public int GetTotalPageNumber()
    {
        return (int)Math.Ceiling((decimal)TotalRecord / RecordPerPage);
    }
}