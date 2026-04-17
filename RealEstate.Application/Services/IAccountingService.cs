using RealEstate.Application.Dto.Accounting;
using RealEstate.Core.Entities;

namespace RealEstate.Application.Services
{
    public interface IAccountingService
    {
        Task<IEnumerable<Branch>> GetBranchesAsync();
        Task InsertBranch(CreateBranchDto branch);
        Task InsertMasterGroup(CreateMasterGroupDto masterGroup);
        Task<IEnumerable<MasterGroup>> GetAllMasterGroup();
        Task InsertGLGroup(CreateGLGroupDto glGroup);
        Task<IEnumerable<GLGroup>> GetAllGLGroup();
        Task InsertGLHead(CreateGLHeadDto glHead);
        Task<IEnumerable<GLHead>> GetAllGLHead();
        Task InsertJournal(CreateJournalDto journal);
        Task<IEnumerable<VoucherResponseDto>> GetAllVouchers(VoucherQueryDto request);
        Task VerifyJournal(VerifyJournalDto journal);
    }
}
