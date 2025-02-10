using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Otanimes.Domain.Structs;

public struct Paginated<TObject>
{
    public int Page { get; set; }
    public int PagesCount { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItemsCount { get; set; }
    public IEnumerable<TObject> Items { get; set; }
    
    public Paginated(
        int page,
        int pagesCount,
        int itemsPerPage,
        int totalItemsCount,
        IEnumerable<TObject>? items)
    {
        Page = page;
        PagesCount = pagesCount;
        ItemsPerPage = itemsPerPage;
        TotalItemsCount = totalItemsCount;
        Items = items ?? new Collection<TObject>();
    }
}