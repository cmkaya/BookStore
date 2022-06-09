using System.Diagnostics;
using AppCore.Records;

namespace Entities;

public class BookGenre : BaseRecordCore
{
    public int BookId { get; set; }
    public int GenreId { get; set; }
    public Book Book { get; set; }
    public Genre Genre { get; set; }
}