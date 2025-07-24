namespace KFC.UseCases.Query;

public class QueryResult<T>
{
    public T Results { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages { get; private set; }
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    
    private QueryResult(T results, int totalCount, int totalPages, int pageNumber, int pageSize)
    {
        TotalCount = totalCount;
        TotalPages = totalPages;
        PageNumber = pageNumber;
        PageSize = pageSize;
        Results = results;
    }

    public static QueryResult<T> Success(T results, int totalCount, int totalPages, int pageNumber, int pageSize)
    {
        return new QueryResult<T>(results:results, totalCount: totalCount, totalPages: totalPages, pageNumber: pageNumber, pageSize: pageSize);
    }

}