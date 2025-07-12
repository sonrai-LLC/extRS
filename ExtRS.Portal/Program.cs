using Azure.Identity;
using ExtRS.Portal.Data;
using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sonrai.ExtRS;
using System.Data.Common;
using System.Threading.RateLimiting;
using WebPWrecover.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Sonrai.ExtRS.Models;

DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationCookie(o =>
{
    o.ExpireTimeSpan = TimeSpan.FromMinutes(2);
    o.SlidingExpiration = true;
    o.Cookie.Name = "_dltdgst";
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().WithRazorPagesRoot("/Areas");
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
            new FixedWindowRateLimiterOptions
            {
                PermitLimit = 1000,
                AutoReplenishment = true,
                Window = TimeSpan.FromMinutes(15)
            });
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration["defaultConnection"]));
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

builder.Services.AddAuthentication()
.AddCookie()
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["googleClientId"]!;
    googleOptions.ClientSecret = builder.Configuration["googleClientSecret"]!;

})
.AddLinkedIn(o =>
{
    o.ClientId = builder.Configuration["linkedInClientId"]!;
    o.ClientSecret = builder.Configuration["linkedInClientSecret"]!;
})
.AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.ClientId = builder.Configuration["microsoftClientId"]!;
    microsoftOptions.ClientSecret = builder.Configuration["microsoftClientSecret"]!;
});

builder.Services.Configure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(1); // Set expiration time for the cookie
    options.SlidingExpiration = true;  // Enables sliding expiration
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddDistributedMemoryCache();
builder.Services.ConfigureApplicationCookie(o =>
{
    o.ExpireTimeSpan = TimeSpan.FromDays(5);
    o.SlidingExpiration = true;
});
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "_dltdgst";
    options.IdleTimeout = TimeSpan.FromSeconds(1000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddScoped<EncryptionService>();
builder.Services.AddScoped<ApplicationUser>();
builder.Services.AddScoped<IdentityUser>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}
else
{
    var keyVaultEndpoint = new Uri("https://sonraivault.vault.azure.net/");
    builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
}

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.UseCors(builder => builder
.WithOrigins("https://localhost", "https://ssrssrv.net", "https://portal.ssrssrv.net")
.AllowAnyMethod()
.AllowAnyHeader());

app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Dashboard}/{action=Dashboard}");
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Dashboard}");
});

app.MapControllers();
app.MapRazorPages();

app.Run();
