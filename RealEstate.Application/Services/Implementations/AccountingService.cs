using RealEstate.Application.Dto.Accounting;
using RealEstate.Core.Entities;
using RealEstate.DataAccess.Dapper;
using System.Text.Json;

namespace RealEstate.Application.Services.Implementations
{
    public class AccountingService : IAccountingService
    {
        private readonly ISqlDataAccess _db;

        public AccountingService(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Branch>> GetBranchesAsync()
        {
            string sql = "SELECT * FROM Branch";
            return await _db.LoadDataWithQuery<Branch, dynamic>(sql, new { });
        }

        public async Task<IEnumerable<GLGroup>> GetAllGLGroup()
        {
            string sql = "SELECT * FROM GLGroup";
            return await _db.LoadDataWithQuery<GLGroup, dynamic>(sql, new { });
        }

        public async Task<IEnumerable<GLHead>> GetAllGLHead()
        {
            string sql = "SELECT * FROM GLHead";
            return await _db.LoadDataWithQuery<GLHead, dynamic>(sql, new { });
        }

        public async Task<IEnumerable<MasterGroup>> GetAllMasterGroup()
        {
            string sql = "SELECT * FROM MasterGroup";
            return await _db.LoadDataWithQuery<MasterGroup, dynamic>(sql, new { });
        }

        public async Task InsertBranch(CreateBranchDto branch)
        {
            await _db.SaveDataAsync("dbo.spBranch_Insert", new
            {
                branch.RegisterDate,
                branch.RegistrationNumber,
                branch.BranchCode,
                branch.BranchName,
                branch.BranchNameNep,
                branch.NickName,
                branch.Street,
                branch.District,
                branch.Zone,
                branch.Country,
                branch.Province,
                branch.PhoneNumber,
                branch.FaxNumber,
                branch.Email,
                branch.CellNumber,
                branch.PanNumber,
                branch.ZipCode,
                branch.Url
            });
        }

        public async Task InsertGLGroup(CreateGLGroupDto glGroup)
        {
            await _db.SaveDataAsync("dbo.spGLGroup_Insert", new
            {
                glGroup.MasterGroupID,
                glGroup.GroupCode,
                glGroup.GroupName,
                glGroup.GroupNepali,
                glGroup.Killed,
                glGroup.Hidden
            });
        }

        public async Task InsertGLHead(CreateGLHeadDto glHead)
        {
            await _db.SaveDataAsync("dbo.spGLHead_Insert", new
            {
                glHead.BranchID,
                glHead.GroupID,
                glHead.GLName,
                glHead.GLNameNepali,
                glHead.OpeningBal,
                glHead.Killed,
                glHead.Hidden,
                glHead.Locked,
                glHead.IsSys,
                glHead.IsShare,
                glHead.OpeningShareAmount,
                glHead.OpeningShareQty,
                glHead.Address,
                glHead.Contact,
                glHead.Sex,
                glHead.IsBank,
                glHead.ShareHolder,
                glHead.AuditUserID
            });
        }

        public async Task InsertMasterGroup(CreateMasterGroupDto masterGroup)
        {
            await _db.SaveDataAsync("dbo.spMasterGroup_Insert", new
            {
                masterGroup.MasterGroupName,
                masterGroup.MasterGroupNepali
            });
        }

        public async Task InsertJournal(CreateJournalDto journal)
        {
            await _db.SaveDataAsync("dbo.spJournal_Insert", new
            {
                journal.UserID,
                journal.EntryDate,
                journal.ValueDate,
                journal.BranchID,
                JournalDetailsJson = JsonSerializer.Serialize(journal.JournalDetails)
            });
        }

        public async Task<IEnumerable<VoucherResponseDto>> GetAllVouchers(VoucherQueryDto request)
        {
            var result = await _db.LoadDataAsync<VoucherResponseDto, dynamic>("dbo.spJournal_GetAll", new { request.JournalID, request.FromDate, request.ToDate });
            return result;
        }

        public async Task VerifyJournal(VerifyJournalDto journal)
        {
            await _db.SaveDataAsync("dbo.spJournal_Verify", new
            {
                journal.JournalID,
                journal.Verified,
                journal.VerifiedBy
            });
        }
    }
}
