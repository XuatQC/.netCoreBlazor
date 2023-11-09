using System.Runtime.CompilerServices;
using CandidateSystem.DataLayer;
using CandidateSystem.BussinessLogic.CandidateService;
using CandidateSystem.DataLayer.EmailRepository;
using CandidateSystem.BussinessLogic;
using CandidateSystem.Shared;
using CandidateSystem.DataLayer.InterviewMeetingRepository;
using CandidateSystem.BussinessLogic.AssessService;
using CandidateSystem.DataLayer.JwtRepository;
using CandidateSystem.DataLayer.UserRefreshTokenRes;
using CandidateSystem.DataLayer.UserServiceRepository;

namespace CandidateSystem.Server
{
    public static class ServiceRegistration
    {
        public static void RegistrationDependency(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<ICandidateRes, CandidateRes>();
            services.AddScoped<IDatabaseConnector, DatabaseConnector>();
            services.AddScoped<IEmailRes, EmailRes>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMailSender, MailSender>();
            services.AddScoped<IInterviewMeetingDetailRes, InterviewMeetingDetailRes>();
            services.AddScoped<IInterviewMeetingDetailSer, InterviewMeetingDetailSer>();
            services.AddScoped<IInterviewMeetingService, InterviewMeetingService>();
            services.AddScoped<IInterviewMeetingRes, InterviewMeetingRes>();
            services.AddScoped<IAssessRes, AssessRes>();
            services.AddScoped<IAssessSer, AssessSer>();
            services.AddScoped<IInterviewerRes, InterviewerRes>();
            services.AddScoped<IInterviewerSer, InterviewerSer>();
            services.AddScoped<IJwtRes, JwtRes>();
            services.AddScoped<IUserRefreshToken, UserRefreshToken>();
            services.AddScoped<IUserAuthenticateRes, UserAuthenticateRes>();

        }
    }
}
