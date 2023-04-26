namespace DataLayer.Entities;

public class Grade : BaseEntity
{
    #region Properties and Indexers
    public Class Class { get; set; }
    public int ClassId { get; set; }
    public User Student { get; set; }
    public int StudentId { get; set; }
    public double Value { get; set; }
    #endregion
}
