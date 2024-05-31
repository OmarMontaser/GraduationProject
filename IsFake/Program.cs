using IsFakeRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IsFakeModels;
using IsFakeServices;
using IsFakeRepository.Interface;
using IsFakeRepository.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext> (options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//register usermanager , rolemanager --> userrole
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//        options=>options.Password.RequireDigit=true,    
    .AddEntityFrameworkStores<ApplicationDbContext>();
//    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IdentityRole>();
builder.Services.AddScoped<IStatements, StatementService>();
builder.Services.AddScoped<IManageAdmin, ManageAdminService>();
builder.Services.AddScoped<UserStatement>();
builder.Services.AddScoped<UserRecord>();

//builder.Services.AddScoped<>();


builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(); // Add this line



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
