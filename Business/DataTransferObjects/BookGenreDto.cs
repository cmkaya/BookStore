using AppCore.Records;

namespace Business.DataTransferObjects;

public class BookGenreDto : BaseRecordCore
{
    public int BookId { get; set; }
    public BookDto Book { get; set; }
    public int GenreId { get; set; }
    public GenreDto Genre { get; set; }
}