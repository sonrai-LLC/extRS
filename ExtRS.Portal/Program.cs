using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Threading.RateLimiting;
using ExtRS.Portal.Data;
using ExtRS.Portal.Models;
using Sonrai.ExtRS;
using Azure.Identity;

//DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true) 
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddNewtonsoftJson(); // Required in .NET Core 3 and later. Optional before
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
//builder.Services.AddRateLimiter(options =>
//{
//	options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//	{
//		return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
//			new FixedWindowRateLimiterOptions
//			{
//				PermitLimit = 20,
//				AutoReplenishment = true,
//				Window = TimeSpan.FromSeconds(10)
//			});
//	});
//});
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//	options.Cookie.Name = "_dltdgst";
//	options.IdleTimeout = TimeSpan.FromSeconds(1000);
//	options.Cookie.HttpOnly = true;
//	options.Cookie.IsEssential = true;
//});

builder.Services.AddScoped<EncryptionService>();
//builder.Services.AddScoped<UserModel>();
//builder.Services.AddScoped<SignInManager<UserModel>>();

//builder.Services
//.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
//options =>
//{
//    options.LoginPath = new PathString("/UserSettings/Login");
//    options.AccessDeniedPath = new PathString("/error");
//})
//.AddGoogle(googleOptions =>
//{
//    googleOptions.ClientId = "317716568671-ojlguitoep61213n4msmrm456nmh9vm8.apps.googleusercontent.com";
//    googleOptions.ClientSecret = "V6WUd5M7E3DP3qhcD9deR7Ma";
//})
//.AddLinkedIn(o =>
//{
//    o.ClientId = "78cirhgq4h5ntb";
//    o.ClientSecret = "vhMZf9vPEsNJeQFH";
//});

builder.Services.AddRazorPages();
//builder.Services.AddIdentityCore<UserModel>(options => options.SignIn.RequireConfirmedAccount = true)
//	.AddEntityFrameworkStores<ApplicationDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    var keyVaultEndpoint = new Uri("https://sonraivault.vault.azure.net/");
    builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseRateLimiter();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//app.UseSession();
//app.UseAuthentication();
//app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	name: "default",
	pattern: "{controller=Reports}/{action=Reports}");
	endpoints.MapRazorPages();
});

app.MapRazorPages();

app.Run();
