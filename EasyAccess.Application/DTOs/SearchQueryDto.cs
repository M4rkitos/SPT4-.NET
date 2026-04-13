public class SearchQueryDto
{
    public int PageNumber { get; set; } = 1; 
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; } 
    public bool IsAscending { get; set; } = true;
    public string? Filter { get; set; }
}