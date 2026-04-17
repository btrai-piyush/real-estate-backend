namespace RealEstate.Application.Dto.Accounting
{
    public class VoucherQueryDto
    {
        public int? JournalID { get; set; } = null;
        public DateTime? FromDate { get; set; } = null;
        public DateTime? ToDate { get; set; } = null;
    }
}
