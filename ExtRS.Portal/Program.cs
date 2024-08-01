using Azure.Identity;
using ExtRS.Portal.Data;
using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sonrai.ExtRS;
using System.Configuration;
using System.Data.Common;
using System.Threading.RateLimiting;

DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services.AddRateLimiter(options =>
{
	options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
	{
		return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
			new FixedWindowRateLimiterOptions
			{
				PermitLimit = 20,
				AutoReplenishment = true,
				Window = TimeSpan.FromSeconds(10)
			});
	});
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.Cookie.Name = "_dltdgst";
	options.IdleTimeout = TimeSpan.FromSeconds(1000);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<EncryptionService>();
builder.Services.AddScoped<UserModel>();
builder.Services.AddScoped<IdentityUser>();
builder.Services.AddScoped<SignInManager<UserModel>>();

// SocialAuth .Use() here

builder.Services.AddRazorPages();
builder.Services.AddIdentityCore<UserModel>(options => options.SignIn.RequireConfirmedAccount = true)
 .AddEntityFrameworkStores<ApplicationDbContext>()
 .AddDefaultTokenProviders();

builder.Services.AddAuthentication();

// TODO: immplement!
builder.Services.Configure<IdentityOptions>(options =>
{
    // Configure Customize password requirements, lockout settings, etc.
});

builder.Services.AddAuthorization();


var app = builder.Build();

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Dashboard}");
    endpoints.MapRazorPages();
});
app.UseSession();
app.UseRateLimiter();
app.MapRazorPages();

app.UseCors(builder => builder
.WithOrigins("https://localhost", "https://ssrssrv.net")
.AllowAnyMethod()
.AllowAnyHeader());

app.Run();
