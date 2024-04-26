using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Windows;
using ThreadAsyncProjectUI.Models;

namespace ThreadAsyncProjectUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly DataContext _dataContext;

    public MainWindow()
    {
        InitializeComponent();

        var config = new ConfigurationBuilder()
            .AddJsonFile("jsconfig1.json")
            .Build();

        DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
            .UseSqlServer(config.GetConnectionString("Default"))
            .Options;
        _dataContext = new DataContext(options);

        //ThreadAsyncProjectUI.Models.ProductInfo productInfo1 = new ThreadAsyncProjectUI.Models.ProductInfo { Brand = "Brand A", Category = "Electronics", Description = "Description1" };
        //ThreadAsyncProjectUI.Models.ProductInfo productInfo2 = new ThreadAsyncProjectUI.Models.ProductInfo { Brand = "Brand B", Category = "Clothing", Description = "Description2" };
        //ThreadAsyncProjectUI.Models.ProductInfo productInfo3 = new ThreadAsyncProjectUI.Models.ProductInfo { Brand = "Brand C", Category = "Food & Beverage", Description = "Description3" };

        //_dataContext.ProductInfo.AddAsync(productInfo1);
        //_dataContext.ProductInfo.AddAsync(productInfo2);
        //_dataContext.ProductInfo.AddAsync(productInfo3);

        //Product product1 = new Product { Name = "Product 1", Price = 100, Info = productInfo1 };
        //Product product2 = new Product { Name = "Product 2", Price = 50, Info = productInfo2 };
        //Product product3 = new Product { Name = "Product 3", Price = 200, Info = productInfo3 };

        //_dataContext.Product.AddAsync(product1);
        //_dataContext.Product.AddAsync(product2);
        //_dataContext.Product.AddAsync(product3);

        //_dataContext.SaveChanges();

        dataGrid.ItemsSource = _dataContext.Product.ToList();
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        ThreadAsyncProjectUI.Models.ProductInfo productInfo = new ThreadAsyncProjectUI.Models.ProductInfo { Brand = tb_brand.Text, Category = tb_category.Text, Description = tb_desc.Text };
        _dataContext.ProductInfo.Add(productInfo);
        Product product = new Product { Name = tb_name.Text, Price = Convert.ToInt32(tb_price.Text), Info = productInfo };
        _dataContext.Product.AddAsync(product);
        _dataContext.SaveChanges();
        dataGrid.ItemsSource = _dataContext.Product.ToList();
    }
}
