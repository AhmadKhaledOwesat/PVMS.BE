using PVMS.Application.Dto;
using PVMS.Application.Interfaces;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Enum;
using PVMS.Domain.Interfaces;
using PVMS.Infrastructure.EfContext;
using PVMS.Infrastructure.Extensions;
using System.Security.Cryptography;

namespace PVMS.Application.Bll
{
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
    public class UserBll(
        IBaseDal<Users, Guid, UserFilter> baseDal,
        ISmsService smsService,
        IAuthenticationManager authenticationManager,
        ISettingBll settingBll,
        IEmailSender emailSender,
        IInnovaMapper innovaMapper,
        IUserRoleBll userRoleBll,
        IUserWorkFlowDefinitionBll userWorkFlowDefinitionBll,
        StudioContext context) : BaseBll<Users, Guid, UserFilter>(baseDal), IUserBll
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
    {
        private static string GenerateOtp(int digits)
        {
            return "123456";
            if (digits != 4 && digits != 6)
                throw new ArgumentException("OTP length must be 4 or 6 digits.", nameof(digits));
            int min = (int)Math.Pow(10, digits - 1);   // 1000 or 100000
            int max = (int)Math.Pow(10, digits);       // 10000 or 1000000
            return RandomNumberGenerator.GetInt32(min, max).ToString();
        }
        public override async Task AddAsync(Users entity)
        {
            if (await GetCountByExpressionAsync(a => a.UserName == entity.UserName) > 0)
                throw new Exception("رمز المستخدم موجود مسبقاً");
            entity.Password = entity.Password.HashedPassword();
            await base.AddAsync(entity);
        }
        public async Task<InnovaResponse<UsersDto>> LoginAsync(string userName, string password)
        {
            //x.Password == password.HashedPassword() &&
            Users user = await baseDal.FindByExpressionAsync(x =>  x.UserName == userName && x.Active == 1);
            if (user == null) return new InnovaResponse<UsersDto>(null, "الرجاء التاكد من كلمة المرور ورمز المستخدم", false);
            if (user.UserTypeId == (int)UserType.Violators) return new InnovaResponse<UsersDto>(null, "لا تمتلك صلاحية استخدام لوحة التحكم", false);
            UsersDto usersDto = innovaMapper.Map<UsersDto>(user);
            usersDto.Permssions = user.UserRoles.Where(a => a.Role != null && a.Role.Active == 1).SelectMany(a => a.Role?.RolePrivileges).Select(x => x.PrivilegeId).ToArray() ?? [];
            usersDto.Token = authenticationManager.GenerateToken(usersDto.FullName, usersDto.Id).Token;
            usersDto.WorkFlowStatus = user.UserRoles.Where(a => a.Role != null && a.Role.Active == 1).SelectMany(a => a.Role?.RoleStatus).Select(x => x.StatusId).ToArray() ?? [];
            string otp = GenerateOtp(6);
           // await smsService.SendSmsAsync(user.MobileNo, otp,SmsType.Otp);
            usersDto.Otp = otp;
            return new InnovaResponse<UsersDto>(usersDto);
        }
        public async Task<InnovaResponse<string>> ResetPasswordAsync(string userName)
        {
            Users user = await baseDal.FindByExpressionAsync(x => x.UserName == userName && x.Active == 1);
            if (user == null) return new InnovaResponse<string>(null, "الرجاء التاكد من رمز المستخدم", false);
            string otp = GenerateOtp(4);
            await smsService.SendSmsAsync(user.MobileNo, otp, SmsType.Otp);
            return new InnovaResponse<string>(otp);
        }

        public async Task<InnovaResponse<string>> UpdatePasswordAsync(string userName, string newPassword)
        {
            if (await FindByExpressionAsync(a => a.UserName == userName) is Users user && !newPassword.IsNullOrEmpty())
            {
                user.Password = newPassword.HashedPassword();
                await base.UpdateAsync(user);
                return new InnovaResponse<string>(user.Password);
            }
            return new InnovaResponse<string>("User not found", "User not found", false);
        }
        public async Task<InnovaResponse<bool>> UpdateLocationAsync(Guid id, string currentLocation)
        {
            try
            {
                Users users = await FindByExpressionAsync(a => a.Id == id);
                if (users is not null)
                {
                    users.CurrentLocation = currentLocation;
                    await base.UpdateAsync(users);
                    return new(true);
                }
            }
            finally
            {
            }
            return new(false);
        }
        private async Task<InnovaResponse<string>> SendEamilAsync(Users user)
        {
            string link = "https://PVMS.com/update-password/" + user.Id;
            string emailBody = $@"<!doctype html>
<html>
  <head>
    <meta charset=""utf-8""/>
    <meta name=""viewport"" content=""width=device-width,initial-scale=1""/>
  </head>
  <body style=""font-family: Arial, Helvetica, sans-serif; color: #222; margin:0; padding:20px; background:#f6f8fb;"">
    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
      <tr>
        <td align=""center"">
          <table width=""600"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""background:#ffffff; border-radius:8px; overflow:hidden; box-shadow:0 2px 6px rgba(0,0,0,0.08);"">
            <tr>
              <td style=""padding:24px; text-align:left;"">
                <h2 style=""margin:0 0 12px 0; font-size:20px;"">Hello {user.FullName},</h2>
                <p style=""margin:0 0 16px 0; line-height:1.5;"">
                  We received a request to reset your password for your PVMS account. Click the button below to set a new password. This link will expire in 30 minutes.
                </p>

                <p style=""text-align:center; margin:20px 0;"">
                  <a href=""{link}"" style=""display:inline-block; padding:12px 20px; text-decoration:none; border-radius:6px; font-weight:600; border:1px solid #1a73e8;"">
                    Set your password
                  </a>
                </p>

                <p style=""margin:0 0 8px 0; font-size:13px; color:#555;"">
                  If the button doesn't work, copy and paste this URL into your browser:
                </p>
                <p style=""word-break:break-all; font-size:13px; color:#0066cc; margin:4px 0 16px 0;"">
                  {link}
                </p>

                <hr style=""border:none; border-top:1px solid #eee; margin:16px 0;""/>

                <p style=""font-size:12px; color:#777; margin:0 0 6px 0;"">
                  If you didn't request a password reset, you can ignore this email — no changes were made to your account.
                </p>
                <p style=""font-size:12px; color:#777; margin:0 0 12px 0;"">
                  Need help? Contact <a href=""mailto:info@PVMS.com"" style=""color:#0066cc;"">info@PVMS.com</a>
                </p>

                <p style=""font-size:13px; color:#999; margin:0;"">Thanks,<br/>PVMS – Centralizing Your Mobile World</p>
              </td>
            </tr>
            <tr>
              <td style=""background:#f2f4f7; padding:12px; font-size:12px; color:#999; text-align:center;"">
                © {DateTime.Now.Year}.  All rights reserved.
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </body>
</html>
";

            var toEmail = await settingBll.FindByExpressionAsync(a => a.SettingName == "DCP.Notification.Email");
            await emailSender.SendAsync("Password Reset", emailBody, toEmail.SettingValue);
            return new InnovaResponse<string>("");
        }

        public override Task<PageResult<Users>> GetAllAsync(UserFilter searchParameters)
        {
            if (searchParameters is not null)
            {
                searchParameters.Expression = new Func<Users, bool>(a =>
                (searchParameters.Keyword.IsNullOrEmpty() || a.FullName.Contains(searchParameters?.Keyword))
                && (searchParameters.UserTypeId == null || a.UserTypeId == searchParameters.UserTypeId)
                 && (searchParameters.Active == null || a.Active == searchParameters.Active)
                 && (searchParameters.RoleId == null || a.UserRoles.Any(x => x.RoleId == searchParameters.RoleId.Value))
                );
            }

            return base.GetAllAsync(searchParameters);
        }

        public override async Task UpdateAsync(Users entity)
        {
            if (await GetCountByExpressionAsync(a => a.UserName == entity.UserName && a.Id != entity.Id) > 0)
                throw new Exception("رمز المستخدم موجود مسبقاً");

            if (!string.IsNullOrEmpty(entity.NewPassword))
                entity.Password = entity.NewPassword.HashedPassword();
            await HandleUserRoles(entity);
            await HandleUserWorkFlowDefinitions(entity);
            await base.UpdateAsync(entity);
        }



        private async Task HandleUserRoles(Users entity)
        {
            if (entity.UserRoles == null)
                return;

            List<UserRole> userRoles = await userRoleBll.FindAllByExpressionAsync(x => x.UserId == entity.Id);
            if (userRoles.Count > 0)
                await userRoleBll.DeleteRangeAsync(userRoles);

            foreach (var item in entity.UserRoles)
            {
                item.Role = null;
                item.Id = default;
            }

            await userRoleBll.AddRangeAsync([.. entity.UserRoles]);
        }

        private async Task HandleUserWorkFlowDefinitions(Users entity)
        {
            if (entity.UserWorkFlowDefinitions == null)
                return;

            var userExists = await GetCountByExpressionAsync(x => x.Id == entity.Id) > 0;
            if (!userExists)
                throw new Exception("المستخدم غير موجود.");

            var incoming = entity.UserWorkFlowDefinitions.ToList();
            if (incoming.Any(x => x.WorkFlowDefinitionId == Guid.Empty))
                throw new Exception("معرف تعريف سير العمل غير صالح.");
            if (incoming.Count > 0 && entity.UserTypeId != (int)UserType.Violators)
                throw new Exception("ربط تعريفات سير العمل متاح فقط للمستخدمين من النوع 2.");

            var duplicateWorkflowIds = incoming
                .GroupBy(x => x.WorkFlowDefinitionId)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (duplicateWorkflowIds.Count > 0)
                throw new Exception("لا يمكن تكرار ربط نفس تعريف سير العمل للمستخدم.");

            var workflowIds = incoming.Select(x => x.WorkFlowDefinitionId).Distinct().ToList();
            if (workflowIds.Count > 0)
            {
                var existingWorkflowIds = context.WorkFlowDefinitions
                    .Where(x => workflowIds.Contains(x.Id))
                    .Select(x => x.Id)
                    .ToList();
                var missing = workflowIds.Except(existingWorkflowIds).ToList();
                if (missing.Count > 0)
                    throw new Exception($"تعريفات سير العمل التالية غير موجودة: {string.Join(", ", missing)}");
            }

            var existingMappings = await userWorkFlowDefinitionBll.FindAllByExpressionAsync(x => x.UserId == entity.Id);
            if (existingMappings.Count > 0)
                await userWorkFlowDefinitionBll.DeleteRangeAsync(existingMappings);

            foreach (var item in incoming)
            {
                item.User = null;
                item.WorkFlowDefinition = null;
                item.Id = default;
                item.UserId = entity.Id;
            }

            if (incoming.Count > 0)
                await userWorkFlowDefinitionBll.AddRangeAsync(incoming);
        }
    }
}
